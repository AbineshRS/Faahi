using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.temp_tables;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.temp_serv
{
    public class temp_service : Itemp_service
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
                Decimal quantity = 0;
                Decimal lineTotal = 0;
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

                    var existing_temp = await _context.temp_im_variants.FirstOrDefaultAsync(a => a.company_id == item.company_id && a.store_id == item.store_id &&
                    a.variant_id == item.variant_id && a.detail_id == item.detail_id);
                    if (existing_temp != null)
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
                        item.detail_id = item.detail_id;
                        item.variant_id = item.variant_id;
                        item.cost_price = item.cost_price;
                        item.quantity = item.quantity;
                        _context.temp_im_variants.Add(item);
                        await _context.SaveChangesAsync();


                    }
                }
                var existing_imPurchase_details = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == varient[0].detail_id);
                if (existing_imPurchase_details != null)
                {
                    //existing_imPurchase_details.quantity = 0;
                    existing_imPurchase_details.unit_price = 0;
                    existing_imPurchase_details.line_total = 0;
                    _context.im_purchase_listing_details.Update(existing_imPurchase_details);
                    await _context.SaveChangesAsync();
                }
                var temp_varient = await _context.temp_im_variants.Where(a => a.detail_id == varient[0].detail_id).ToListAsync();
                foreach (var items in temp_varient)
                {
                    lineTotal += Convert.ToDecimal(items.quantity) * Convert.ToDecimal(items.cost_price);
                    quantity += Convert.ToDecimal(items.quantity);
                }
                existing_imPurchase_details.line_total = lineTotal  ;
                _context.im_purchase_listing_details.Update(existing_imPurchase_details);


                await _context.SaveChangesAsync();
                return new ServiceResult<List<temp_im_variant>>
                {
                    Status = 201,
                    Success = true,
                    Message = "Success",
                    Data = varient.ToList()
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
