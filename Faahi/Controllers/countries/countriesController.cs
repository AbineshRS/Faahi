using Faahi.Migrations;
using Faahi.Model.countries;
using Faahi.Service.countries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.countries
{
    [Route("api/[controller]")]
    [ApiController]
    public class countriesController : ControllerBase
    {
        private readonly Iavl_countries _iavl_Countries;
        public countriesController(Iavl_countries avl_Countries)
        {
            _iavl_Countries = avl_Countries;
        }

        [HttpPost]
        [Route("add_countries")]
        public async Task<ActionResult<avl_countries>> CreateAvailableCountry(avl_countries co_Avl_Countries)
        {
            if (co_Avl_Countries == null)
            {
                return Ok("no data found");
            }
            var currency = await _iavl_Countries.CreateAvailableCountry(co_Avl_Countries);
            return Ok(currency);
        }
        [HttpGet]
        [Route("countries_list")]
        public async Task<IActionResult> countries_list()
        {
            var result = await _iavl_Countries.GetAllCountries();
            return Ok(result);
        }
    }
}
