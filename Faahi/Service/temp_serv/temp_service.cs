using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.temp_tables;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.temp_serv
{
    public class temp_service:Itemp_service
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<temp_service> _logger;
        public temp_service(ApplicationDbContext context, ILogger<temp_service> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<List<temp_im_variant>>> Add_temp_varient(List<temp_im_variant> varient)
        {
            try
            {
                if (varient == null)
                {
                    _logger.LogInformation("NO data found to insert in Add_temp_varient ");
                    return new ServiceResult<List<temp_im_variant>>
                    {
                        Status = 400,
                        Success = false,
                        Data = null

                    };
                }
                foreach (var item in varient)
                {
                    
                    var existing_temp = await _context.temp_im_variants.FirstOrDefaultAsync(a=>a.company_id == item.company_id && a.store_id==item.store_id &&
                    a.variant_id==item.variant_id);
                    if(existing_temp != null)
                    {
                        existing_temp.cost_price = item.cost_price;
                        existing_temp.quantity = item.quantity;
                        //_context.temp_im_variants.Update(item);

                    }
                    else
                    {
                        item.temp_variant_id = Guid.CreateVersion7();
                        item.company_id = item.company_id;
                        item.store_id = item.store_id;
                        item.variant_id = item.variant_id;
                        item.cost_price = item.cost_price;
                        item.quantity = item.quantity;
                        _context.temp_im_variants.Add(item);

                    }

                       
                }
                
                await _context.SaveChangesAsync();
                return new ServiceResult<List<temp_im_variant>>
                {
                    Status = 201,
                    Success = true,
                    Message = "Success",
                    Data =  varient.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while inserting Temp data");
                return new ServiceResult<List<temp_im_variant>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }

        public async Task<ServiceResult<List<temp_im_variant>>> get_tempvariant(Guid store_id)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("NO store_id found");
                    return new ServiceResult<List<temp_im_variant>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "store_id Not found",

                    };
                }
                var temp_varient = await _context.temp_im_variants.Where(a => a.store_id == store_id).ToListAsync();
                if (temp_varient.Count == 0)
                {
                    _logger.LogInformation("no data found in temp_im_variants store_id");
                    return new ServiceResult<List<temp_im_variant>>
                    {
                        Status = 400,
                        Success = false,
                    };
                }
                return new ServiceResult<List<temp_im_variant>>
                {
                    Status = 200,
                    Success = true,
                    Data = temp_varient
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro  temp_im_variants ");
                return new ServiceResult<List<temp_im_variant>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
            

        }
    }
}
