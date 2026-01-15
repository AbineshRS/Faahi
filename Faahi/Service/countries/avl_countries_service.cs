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
                int opr = 2; 
                 countries = await _context.avl_countries
                    .FromSqlRaw("EXEC dbo.GetAllProductCategories @opr={0}", opr)
                    .AsNoTracking()
                    .ToListAsync();
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
        public async Task<ServiceResult<string>> ImportAllCountriesAsync()
        {
            var result = new ServiceResult<string>();

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all?fields=name,cca2,currencies,timezones");

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Failed to fetch countries from API.";
                    return result;
                }

                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<CountryApiModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                foreach (var country in countries)
                {
                    if (country.currencies == null || country.currencies.Count == 0)
                        continue;

                    // Usually first currency is primary
                    var firstCurrency = country.currencies.First().Value;

                    // Check if country already exists (avoid duplicates)
                    var existing = await _context.fx_Currencies
                        .FirstOrDefaultAsync(x => x.country_code == country.cca2);

                    if (existing != null)
                        continue;
                    fx_Currencies fx_Currencies = new fx_Currencies();

                    fx_Currencies.currency_id = Guid.CreateVersion7();
                    fx_Currencies.country_name = country.name.common;
                    fx_Currencies.country_code = country.cca2;
                    fx_Currencies.currency_name = firstCurrency.name;
                    fx_Currencies.currency_symbol = firstCurrency.symbol;

                    // ✅ Initialize the timezones list before looping
                    fx_Currencies.fx_timezones = new List<fx_timezones>();

                    // ✅ Loop through each timezone from API response
                    if (country.timezones != null)
                    {
                        foreach (var tz in country.timezones)
                        {
                            fx_timezones fx_Timezone = new fx_timezones();
                            fx_Timezone.timezone_id = Guid.CreateVersion7();
                            fx_Timezone.currency_id = fx_Currencies.currency_id;
                            fx_Timezone.timezone = tz;
                            fx_Timezone.timezone_name = ExtractTimezoneName(tz); // optional, for readable name

                            fx_Currencies.fx_timezones.Add(fx_Timezone); // add to list
                        }
                    }

                    // ✅ Add the parent (and EF will insert child automatically)
                    _context.fx_Currencies.Add(fx_Currencies);
                    await _context.SaveChangesAsync();


                    //var fxCurrency = new fx_Currencies
                    //{
                    //    currency_id = Guid.NewGuid(),
                    //    country_name = country.name.common,
                    //    country_code = country.cca2,
                    //    currency_name = firstCurrency.name,
                    //    currency_symbol = firstCurrency.symbol,
                    //    fx_timezones = country.timezones.Select(tz => new fx_timezones
                    //    {
                    //        timezone_id = Guid.NewGuid(),
                    //        timezone = tz,
                    //        timezone_name = ExtractTimezoneName(tz)
                    //    }).ToList()
                    //};

                }


                result.Success = true;
                result.Message = "All countries, currencies, and timezones saved successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }
        private string ExtractTimezoneName(string tz)
        {
            if (string.IsNullOrWhiteSpace(tz))
                return "Unknown";

            if (tz.Contains('/'))
                return tz.Split('/').Last().Replace("_", " ");

            return tz;
        }
        public async Task<ServiceResult<List<fx_Currencies>>> get_all_Countries()
        {
            var countrys = await _context.fx_Currencies.Include(a=>a.fx_timezones).ToListAsync();
            if (countrys == null)
            {
                return new ServiceResult<List<fx_Currencies>> { Success = false };
            }
            return new ServiceResult<List<fx_Currencies>>
            {
                Status = 1,
                Success = true,
                Data = countrys
            };
        }
    }
}
