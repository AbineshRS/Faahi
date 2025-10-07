using Faahi.Model.Shared_tables;
using Faahi.Service.PartyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.SharedParties
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedPartiesController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public SharedPartiesController(IPartyService partyService)
        {
            _partyService = partyService;
        }
        [Authorize]
        [HttpPost]
        [Route("create_Partys")]
        public async Task<ActionResult<st_Parties>> Create_partys(st_Parties party)
        {
            if (party == null)
            {
                return Ok("No data found");
            }
            var created= await _partyService.Create_partys(party);
            return Ok(created);
        }
    }
}
