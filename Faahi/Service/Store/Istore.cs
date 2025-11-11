using Faahi.Dto;
using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.View_Model.store;

namespace Faahi.Service.Store
{
    public interface Istore
    {
        Task<ServiceResult<Store_users>> Create_sellers(Store_users st_sellers);

        Task<ServiceResult<st_stores>> Create_stores(st_stores store_Add);

        Task<ServiceResult<List<st_store_view>>> Get_store(Guid company_id);

        Task<ServiceResult<List<st_Users>>> Get_seller(Guid company_id);

        Task<ServiceResult<st_Users>> Seller_update_password(string token, string email, string password);

        Task<ServiceResult<st_UserRoles>> Create_roles(st_UserRoles st_UserRoles);

        Task<ServiceResult<List<st_UserRoles>>> Get_roles_by_company_id();

        Task<ServiceResult<st_UserStoreAccess>> Create_store_access(st_UserStoreAccess st_UserStoreAccess);

        Task<ServiceResult<List<st_stores>>> Get_store_by_email(string email);

        Task<ServiceResult<st_UserRoles>> Get_userrole(Guid user_id,Guid store_id);

        Task<ServiceResult<List<st_StoreCategories>>> Create_StoreCategories(List<st_StoreCategories> st_StoreCategories);

        Task<ServiceResult<st_stores>> Update_store(Guid store_id, st_stores st_Stores);

        Task<ServiceResult<st_store_view>> Get_store_by_storeid(Guid store_id);

        Task<ServiceResult<List<st_StoreCategories>>> update_category(List<st_StoreCategories> st_StoreCategories);

        Task<ServiceResult<st_StoresAddres>> add_sub_address(Guid store_id,st_StoresAddres st_StoresAddres);

        Task<ServiceResult<st_stores>> Delete_store(Guid store_id);



    }
}
