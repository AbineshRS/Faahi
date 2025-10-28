using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Store;
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
        public async Task<ActionResult<st_Users>> Create_sellers(st_Users st_Sellers)
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
        public async Task<ActionResult<st_stores>> Create_stores(st_stores st_Stores)
        {
            if (st_Stores == null)
            {
                return Ok("No data found");
            }
            var result = await _istore.Create_stores(st_Stores);
            return Ok(result);
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
        [Route("get_roles_by_company_id/{company_id}")]
        public async Task<ActionResult> Get_roles_by_company_id(Guid company_id)
        {
            if (company_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _istore.Get_roles_by_company_id(company_id);
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
    }

}
