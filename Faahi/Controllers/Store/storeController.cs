using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
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
        [Route("add_sellers")]
        public async Task<ActionResult<st_sellers>> Create_sellers(st_sellers st_Sellers)
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
        public async Task<ActionResult<st_sellers>> seller_update_password(string token,string email,string password)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var result = await _istore.Seller_update_password(token,email,password);
            return Ok(result);
        }
    }

}
