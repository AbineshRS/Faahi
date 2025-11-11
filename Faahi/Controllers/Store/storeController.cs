using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Store;
using Faahi.View_Model.store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class storeController : ControllerBase
    {
        private readonly Istore _istore;

        public storeController(Istore store)
        {
            _istore = store;
        }
        [Authorize]
        [HttpPost]
        [Route("add_store_users")]
        public async Task<ActionResult<Store_users>> Create_sellers(Store_users st_Sellers)
        {
            if (st_Sellers == null)
            {
                return Ok("No data found");
            }
            var result = await _istore.Create_sellers(st_Sellers);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("add_stores")]
        public async Task<ActionResult<st_stores>> Create_stores(StoreUserRequest storeUserRequest)
        {
            if (storeUserRequest == null)
            {
                return Ok("No data found");
            }
            var result = await _istore.Create_stores(storeUserRequest.st_stores);
            if (result.Status != 1)
            {
                return Ok(result);
            }
            if (result.Status ==1)
            {
                var storeId = result.Data.store_id;
                foreach (var category in storeUserRequest.StoreCategories)
                {
                    category.store_id = storeId;
                }
            }
           
            var storeCategoriesResult = await _istore.Create_StoreCategories(storeUserRequest.StoreCategories);
            return Ok(new
            {
                SellerResult = result,
                StoreCategoriesResult = storeCategoriesResult
            });
        }
        [Authorize]
        [HttpGet]
        [Route("get_store/{company_id}")]
        public async Task<ActionResult> Get_store(Guid company_id)
        {
            if (company_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _istore.Get_store(company_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_seller/{company_id}")]
        public async Task<ActionResult> Get_seller(Guid company_id)
        {
            if (company_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _istore.Get_seller(company_id);
            return Ok(result);
        }
        [HttpPost]
        [Route("seller_update_password/{token}/{email}/{password}")]
        public async Task<ActionResult<st_Users>> seller_update_password(string token, string email, string password)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var result = await _istore.Seller_update_password(token, email, password);
            return Ok(result);
        }
        [HttpPost]
        [Route("add_roles")]
        public async Task<ActionResult<st_UserRoles>> Create_roles(st_UserRoles st_UserRoles)
        {
            if (st_UserRoles == null)
            {
                return Ok("No data found");
            }
            var result = await _istore.Create_roles(st_UserRoles);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_roles_by_company_id")]
        public async Task<ActionResult> Get_roles_by_company_id()
        {
            
            var result = await _istore.Get_roles_by_company_id();
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("add_st_UserStoreAccess")]
        public async Task<ActionResult<st_UserStoreAccess>> Create_st_UserStoreAccess(st_UserStoreAccess st_UserStoreAccess)
        {
            if (st_UserStoreAccess == null)
            {
                return Ok("No data found");
            }
            // Assuming you have a method in Istore to handle this
            var result = await _istore.Create_store_access(st_UserStoreAccess);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_store_by_email/{email}")]
        public async Task<ActionResult> Get_store_by_email(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Ok("No email found");
            }
            var result = await _istore.Get_store_by_email(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_userrole/{user_id}/{store_id}")]
        public async Task<ActionResult> Get_userrole(Guid user_id, Guid store_id)
        {
            if (user_id == Guid.Empty || store_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _istore.Get_userrole(user_id, store_id);
            return Ok(result);
        }
        //[Authorize]
        [HttpGet]
        [Route("get_store_by_storeid/{store_id}")]
        public async Task<ActionResult> Get_store_by_storeid(Guid store_id)
        {
            if (store_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _istore.Get_store_by_storeid(store_id);
            return Ok(result);
        }


        [Authorize]
        [HttpPost]
        [Route("update_store/{store_id}")]
        public async Task<ActionResult> Update_store(Guid store_id, StoreUserRequest  storeUserRequest)
        {
            if (store_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _istore.Update_store(store_id, storeUserRequest.st_stores);
            if (result.Status != 1)
            {
                return Ok(result);
            }
            if (result.Status == 1)
            {
                var storeId = result.Data.store_id;
                foreach (var category in storeUserRequest.StoreCategories)
                {
                    category.store_id = store_id;
                }
            }
            var storeCategoriesResult = await _istore.update_category(storeUserRequest.StoreCategories);
            return Ok(new
            {
                SellerResult = result,
                StoreCategoriesResult = storeCategoriesResult
            });
        }
        //[Authorize]
        [HttpPost]
        [Route("add_sub_address/{store_id}")]
        public async Task<ActionResult> add_sub_address(Guid store_id,st_StoresAddres st_StoresAddres)
        {
            if(store_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var resut = await _istore.add_sub_address(store_id, st_StoresAddres);
            return Ok(resut);
        }
        [Authorize]
        [HttpPost]
        [Route("delete_store/{store_id}")]
        public async Task<IActionResult> Delete_store(Guid store_id)
        {
            if(store_id == Guid.Empty)
            {
                return Ok("not found");
            }
            var resut = await _istore.Delete_store(store_id);

            return Ok(resut);
        }

    }

}
