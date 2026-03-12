using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Migrations;
using Faahi.Model.table_key;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Faahi.Service.table_key
{
    public class table_key : Itable_key
    {
        private readonly ApplicationDbContext _context;
        public table_key(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<am_table_next_key>> Add_Key(am_table_next_key _Next_Key)
        {
            if (_Next_Key is null)
            {
                return new BadRequestObjectResult("No data found");
            }
            am_table_next_key key = new am_table_next_key();
            key.name = _Next_Key.name;
            key.business_id = _Next_Key.business_id;
            key.next_key = _Next_Key.next_key;
            key.site_code = _Next_Key.site_code;
            _context.am_table_next_key.Add(key);
            await _context.SaveChangesAsync();
            return key;
        }
        public async Task<List<am_table_next_key>> Get_all()
        {
            if (_context.am_table_next_key is null)
            {
                return null;
            }
            var table_key = await _context.am_table_next_key.ToListAsync();
            return table_key;
        }
        public async Task<ActionResult<am_table_next_key>> Update_key(am_table_next_key table_Next_Key)
        {
            if (table_Next_Key is null)
            {
                return null;
            }
            var existing = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table_Next_Key.name);
            existing.name = table_Next_Key.name;
            existing.next_key = table_Next_Key.next_key;
            existing.business_id = table_Next_Key.business_id;
            existing.site_code = table_Next_Key.site_code;
            await _context.SaveChangesAsync();

            return existing;
        }
        public async Task<am_table_next_key> GetByName(string name)
        {
            if (name is null)
            {
                return null;
            }
            var existing = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == name);

            return existing;
        }
        public async Task<am_table_next_key> GetNextKey_company(Guid business_id, string name)
        {
            if (business_id == null)
            {
                return null;
            }
            var existing = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.business_id == business_id && a.name == name);
            return existing;
        }
        public async Task<am_table_next_key> delete_key(string name)
        {
            if (name is null)
            {
                return null;
            }
            var existing = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == name);
            _context.am_table_next_key.Remove(existing);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<ServiceResult<super_abi>> Add_Super_Table_Key(super_abi super_Abi)
        {
            try
            {
                if (super_Abi == null)
                {
                    return new ServiceResult<super_abi>
                    {
                        Success = false,
                        Message = "No data found"
                    };
                }
                super_Abi.description = super_Abi.description;
                super_Abi.next_key = super_Abi.next_key;
                _context.super_abi.Add(super_Abi);
                await _context.SaveChangesAsync();
                return new ServiceResult<super_abi>
                {
                    Success = true,
                    Data = super_Abi,
                    Message = "Super table key added successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<super_abi>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResult<super_admin_keys>> Add_Super_Table_Key(super_admin_keys super_admin_keys)
        {
            try
            {
                if (super_admin_keys == null)
                {
                    return new ServiceResult<super_admin_keys>
                    {
                        Success = false,
                        Message = "No data found"
                    };
                }
                super_admin_keys.name = super_admin_keys.name;
                super_admin_keys.next_key = super_admin_keys.next_key;
                super_admin_keys.site_code = super_admin_keys.site_code;
                _context.super_admin_keys.Add(super_admin_keys);
                await _context.SaveChangesAsync();
                return new ServiceResult<super_admin_keys>
                {
                    Success = true,
                    Data = super_admin_keys,
                    Message = "Super admin key added successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<super_admin_keys>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}

