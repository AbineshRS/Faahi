using Faahi.Model.tax_class_table;
using Faahi.Service.site_settings_service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.site_settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class Site_settingsController : ControllerBase
    {
        private readonly Isite_settings _site_settings;
        public Site_settingsController(Isite_settings isite_Settings)
        {
            _site_settings = isite_Settings;
        }
        [Authorize]
        [HttpPost]
        [Route("Add_tax_class")]
        public async Task<ActionResult<tx_TaxClasses>> Add_tax_class(tx_TaxClasses tx_TaxClasses)
        {
            if(tx_TaxClasses == null)
            {
                return Ok("No data found");
            }
            var result =await _site_settings.Add_tax_class(tx_TaxClasses);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Update_tax/{tax_class_id}")]
        public async Task<ActionResult>  Update_tax(Guid tax_class_id,tx_TaxClasses tx_TaxClasses)
        {
            if (tax_class_id == null)
            {
                return Ok("No tax_class_id found");
            }
            var result = await _site_settings.Update_tax(tax_class_id,tx_TaxClasses);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_tax/{company_id}")]
        public async Task<IActionResult> Get_tax(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("NO company_id found");
            }
            var result = await _site_settings.Get_tax(company_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_tax_class/{tax_class_id}")]
        public async Task<IActionResult> get_tax_class(Guid tax_class_id)
        {
            if (tax_class_id == null)
            {
                return Ok("NO company_id found");
            }
            var result = await _site_settings.get_tax_class(tax_class_id);
            return Ok(result);
        }
    }
}
