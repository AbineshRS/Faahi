using Faahi.Dto;
using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;

namespace Faahi.Service.Store
{
    public interface Istore
    {
        Task<ServiceResult<st_Users>> Create_sellers(st_Users st_sellers);

        Task<ServiceResult<st_stores>> Create_stores(st_stores st_Stores);

        Task<ServiceResult<List<st_stores>>> Get_store(Guid company_id);

        Task<ServiceResult<List<st_Users>>> Get_seller(Guid company_id);

        Task<ServiceResult<st_Users>> Seller_update_password(string token, string email, string password);

        Task<ServiceResult<st_UserRoles>> Create_roles(st_UserRoles st_UserRoles);

        Task<ServiceResult<List<st_UserRoles>>> Get_roles_by_company_id(Guid company_id);

        Task<ServiceResult<st_UserStoreAccess>> Create_store_access(st_UserStoreAccess st_UserStoreAccess);

    }
}
