using Faahi.Dto;
using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;

namespace Faahi.Service.Store
{
    public interface Istore
    {
        Task<ServiceResult<st_sellers>> Create_sellers(st_sellers st_sellers);

        Task<ServiceResult<st_stores>> Create_stores(st_stores st_Stores);

        Task<ServiceResult<List<st_stores>>> Get_store(Guid company_id);

        Task<ServiceResult<List<st_sellers>>> Get_seller(Guid company_id);

        Task<ServiceResult<st_sellers>> Seller_update_password(string token, string email, string password);

    }
}
