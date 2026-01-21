using Faahi.Model.temp_tables;
using Faahi.Service.temp_serv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.temp_ct
{
    [Route("api/[controller]")]
    [ApiController]
    public class tepmController : ControllerBase
    {
        private readonly Itemp_service _temp_service;
        public tepmController(Itemp_service itemp_Service)
        {
            _temp_service = itemp_Service;
        }

        [Authorize]
        [HttpPost]
        [Route("add_tempvarient")]
        public async Task<ActionResult<List<temp_im_variant>>> Add_temp_varient(List<temp_im_variant> varient)
        {
            if(varient == null)
            {
                return Ok("NO data found");
            }
            var result = await _temp_service.Add_temp_varient(varient);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_tempvariant/{store_id}")]
        public async Task<IActionResult> get_tempvariant(Guid store_id)
        {
            if (store_id == null)
            {
                return Ok("No Id found");
            }
            var result = await _temp_service.get_tempvariant(store_id);
            return Ok(result);
        }
    }
}
