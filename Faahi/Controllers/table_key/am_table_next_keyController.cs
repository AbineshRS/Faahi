using Faahi.Controllers.Application;
using Faahi.Model.table_key;
using Faahi.Service.table_key;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.table_key
{
    [Route("api/[controller]")]
    [ApiController]
    public class am_table_next_keyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Itable_key _table_key;
        public am_table_next_keyController(ApplicationDbContext context,Itable_key tale_key)
        {
            _context = context;
            _table_key = tale_key;
        }
        [HttpPost]
        [Route("add_key")]
        public async Task<ActionResult<am_table_next_key>> Add_Key(am_table_next_key next_Key)
        {
            if (next_Key == null)
            {
                return Ok("no data found");
            }
           var key =await _table_key.Add_Key(next_Key);
            if(key == null)
            {
                return Ok("not inserted");
            }
            
            return Ok(key);
        }
        [HttpGet]
        [Route("get_all_key")]
        public async Task<ActionResult<IEnumerable<am_table_next_key>>> Get_all()
        {
            if(_context.am_table_next_key is null)
            {
                return Ok("no data found");
            }
            var table_key= await _table_key.Get_all();

            return Ok(table_key);
        }
        [HttpPost]
        [Route("update_key")]
        public async Task<ActionResult<am_table_next_key>> Update_key(am_table_next_key table_Next_Key)
        {
            if(table_Next_Key == null)
            {
                return Ok("no data found");
            }
            var updated_data = await _table_key.Update_key(table_Next_Key);

            return Ok(updated_data);
        }
        [HttpGet]
        [Route("get/{name}")]
        public async Task<ActionResult<am_table_next_key>> GetByName(string name)
        {
            if(name == null)
            {
                return Ok("not found");
            }
            var data = await _table_key.GetByName(name);
            return Ok(data);
        }
        [HttpDelete]
        [Route("delete/{name}")]
        public async Task<ActionResult<am_table_next_key>> delete_key(string name)
        {
            if(name == null)
            {
                return Ok("not found");
            }
            var data = await _table_key.delete_key(name);
            return Ok(data);
        }

        [HttpPost]
        [Route("add_super_table_key")]
        public async Task<ActionResult<super_abi>> Add_Super_Table_Key(super_abi super_Abi)
        {
            var result = await _table_key.Add_Super_Table_Key(super_Abi);
            return Ok("Super table keys added successfully.");
        }
    }
}
