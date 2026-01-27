using Faahi.Dto;
using Faahi.Model.table_key;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.table_key
{
    public interface Itable_key
    {
        Task<ActionResult<am_table_next_key>> Add_Key(am_table_next_key _Next_Key);

        Task<List<am_table_next_key>> Get_all();

        Task<ActionResult<am_table_next_key>> Update_key(am_table_next_key table_Next_Key);

        Task<am_table_next_key> GetByName(string name);  
        
        Task<am_table_next_key> delete_key(string name);

        Task<ServiceResult<super_abi>> Add_Super_Table_Key(super_abi super_Abi);
    }
}
