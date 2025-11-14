using Faahi.Dto;
using Faahi.Model.countries;

namespace Faahi.Service.countries
{
    public interface Iavl_countries
    {
        Task<ServiceResult<avl_countries>> CreateAvailableCountry(avl_countries co_Avl_Countries);

        Task<ServiceResult<List<avl_countries>>> GetAllCountries();

        Task<ServiceResult<string>> ImportAllCountriesAsync();

        Task<ServiceResult<List<fx_Currencies>>> get_all_Countries();
    }
}
