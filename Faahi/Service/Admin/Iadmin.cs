using Faahi.Dto;
using Faahi.Model.Admin;

namespace Faahi.Service.Admin
{
    public interface Iadmin
    {

        Task<ServiceResult<super_admin>> AddAdminAsync(super_admin admin);

        Task<AuthResponse> LoginAsyn(string username, string password);

        Task<ServiceResult<sa_country_regions>> Addcountry(sa_country_regions regions);

        Task<ServiceResult<sa_regions>> Addregion(sa_regions countries);

        Task<ServiceResult<List<sa_country_regions>>> GetRegionsList();
    }
}
