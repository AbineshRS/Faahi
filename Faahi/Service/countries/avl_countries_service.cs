using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Faahi.Service.countries
{
    public class avl_countries_service : Iavl_countries
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<avl_countries_service> _logger;

        public avl_countries_service(ApplicationDbContext context, ILogger<avl_countries_service> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<avl_countries>> CreateAvailableCountry(avl_countries Avl_Countries)
        {
            if (Avl_Countries == null || Avl_Countries == null || string.IsNullOrWhiteSpace(Avl_Countries.name))
            {
                return new ServiceResult<avl_countries>
                {
                    Success = false,
                    Message = "Invalid or missing country data",
                    Status = -1
                };
            }


            // Check if country already exists
            var existingData = await _context.avl_countries
                .FirstOrDefaultAsync(a => a.name.ToLower() == Avl_Countries.name.ToLower());

            if (existingData != null)
            {
                return new ServiceResult<avl_countries>
                {
                    Success = false,
                    Message = "already Exisit",
                    Status = -2
                };
            }

            // Fetch country info
            var countryDetails = await GetCountryInfoByNameAsync(Avl_Countries.name);
            if (countryDetails == null)
            {
                return new ServiceResult<avl_countries>
                {
                    Success = false,
                    Message = "Country information not found",
                    Status = -3
                };
            }




            // Assign values
            Avl_Countries.avl_countries_id = Guid.CreateVersion7();
            Avl_Countries.country_code = countryDetails.Code;
            Avl_Countries.flag = countryDetails.FlagUrl;
            Avl_Countries.dialling_code = countryDetails.DialCode;
            Avl_Countries.currency_code = countryDetails.CurrencyCode;
            Avl_Countries.currency_name = countryDetails.CurrencyName;
            Avl_Countries.serv_available = "T";
            Avl_Countries.status = "T";


            try
            {
                await _context.avl_countries.AddAsync(Avl_Countries);


                await _context.SaveChangesAsync();


                _logger.LogInformation("Adding countrys", Avl_Countries);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Avl_Countries.avl_countries_id.ToString());
                return new ServiceResult<avl_countries>
                {
                    Success = false,
                    Status = -500,
                    Message = "Internal server error"
                };
            }
            return new ServiceResult<avl_countries>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = Avl_Countries
            };
        }
        public async Task<CountryInfo_Dto?> GetCountryInfoByNameAsync(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            using var httpClient = new HttpClient();

            // Try both endpoints: first /capital/, then /name/
            string[] endpoints =
            {
        $"https://restcountries.com/v3.1/capital/{Uri.EscapeDataString(countryName)}?fields=name,cca2,flags,idd,currencies",
        $"https://restcountries.com/v3.1/name/{Uri.EscapeDataString(countryName)}?fields=name,cca2,flags,idd,currencies"
    };

            foreach (var url in endpoints)
            {
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    continue;

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonDocument.Parse(json).RootElement;

                if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
                    continue;

                var country = root.EnumerateArray()
                    .FirstOrDefault(c =>
                        string.Equals(
                            c.GetProperty("name").GetProperty("common").GetString(),
                            countryName,
                            StringComparison.OrdinalIgnoreCase));

                // If exact match not found, just use first result
                if (country.ValueKind != JsonValueKind.Object)
                    country = root[0];

                string name = country.GetProperty("name").GetProperty("common").GetString() ?? "";
                string code = country.GetProperty("cca2").GetString() ?? "";
                string flagUrl = country.GetProperty("flags").GetProperty("png").GetString() ?? "";

                string dialCode = country.TryGetProperty("idd", out var idd) &&
                                  idd.TryGetProperty("root", out var rootCode) &&
                                  idd.TryGetProperty("suffixes", out var suffixes) &&
                                  suffixes.GetArrayLength() > 0
                                  ? rootCode.GetString() + suffixes[0].GetString()
                                  : "";

                string currencyCode = "", currencyName = "";
                if (country.TryGetProperty("currencies", out var currencies))
                {
                    var currency = currencies.EnumerateObject().FirstOrDefault();
                    currencyCode = currency.Name;
                    currencyName = currency.Value.GetProperty("name").GetString() ?? "";
                }

                return new CountryInfo_Dto
                {
                    Name = name,
                    Code = code,
                    FlagUrl = flagUrl,
                    DialCode = dialCode,
                    CurrencyCode = currencyCode,
                    CurrencyName = currencyName
                };
            }

            return null;
        }
        public async Task<ServiceResult<List<avl_countries>>> GetAllCountries()
        {
            List<avl_countries> countries;
            try
            {
                countries = await _context.avl_countries.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving countries");
                return new ServiceResult<List<avl_countries>>
                {
                    Success = false,
                    Status = -500,
                    Message = "Internal server error"
                };
            }
            return new ServiceResult<List<avl_countries>>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = countries
            };
        }
    }
}
