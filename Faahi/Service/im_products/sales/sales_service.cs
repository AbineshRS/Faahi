using AutoMapper.Configuration.Annotations;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Faahi.Model.sales;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.im_products.sales
{
    public class sales_service : Isales
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<sales_service> _logger;
        public sales_service(ApplicationDbContext applicationDb, ILogger<sales_service> sales_log)
        {
            _context = applicationDb;
            _logger = sales_log;
        }

        public async Task<ServiceResult<so_payment_type>> Create_payment(so_payment_type so_payment_type)
        {
            var transactio = await _context.Database.BeginTransactionAsync();
            try
            {
                if (so_payment_type == null)
                {
                    _logger.LogInformation("no data found");
                    return new ServiceResult<so_payment_type>
                    {
                        Status = 400,
                        Success = false,
                        Message = "NO data found"
                    };
                }
                var exisiting = await _context.so_Payment_Types.AnyAsync(a => a.PayTypeCode == so_payment_type.PayTypeCode);
                if (exisiting)
                {
                    return new ServiceResult<so_payment_type>
                    {
                        Status = 300,
                        Message = "Already Exist",
                        Success = false,

                    };
                }
                so_payment_type.PayTypeCode = so_payment_type.PayTypeCode;
                so_payment_type.company_id = so_payment_type.company_id;
                so_payment_type.Bank_pcnt = so_payment_type.Bank_pcnt;
                so_payment_type.is_avilable = so_payment_type.is_avilable;
                so_payment_type.req_det = so_payment_type.req_det;
                so_payment_type.Description = so_payment_type.Description;
                so_payment_type.card_type = so_payment_type.card_type;
                so_payment_type.cash_types = so_payment_type.cash_types;
                so_payment_type.Order = so_payment_type.Order;
                so_payment_type.co_business = null;

                await _context.so_Payment_Types.AddAsync(so_payment_type);
                await _context.SaveChangesAsync();
                await transactio.CommitAsync();
                return new ServiceResult<so_payment_type>
                {
                    Status = 201,
                    Success = true,
                    Message = "Success",
                    Data = so_payment_type
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro while Create_payment ");
                await transactio.RollbackAsync();
                return new ServiceResult<so_payment_type>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                };
            }


        }
        public async Task<ServiceResult<List<so_payment_type>>> get_payment(Guid company_id)
        {
            try
            {
                if (company_id == null)
                {
                    _logger.LogInformation($"company_id={company_id}");
                    return new ServiceResult<List<so_payment_type>>
                    {
                        Success = false,
                        Status = 400,
                        Message = "NO company_id found"
                    };
                }
                var date = await _context.so_Payment_Types.Where(a => a.company_id == company_id).OrderByDescending(a => a.Order).ToListAsync();
                if (date == null || date.Count == 0)
                {
                    return new ServiceResult<List<so_payment_type>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "NO data found"
                    };

                }
                return new ServiceResult<List<so_payment_type>>
                {
                    Status = 200,
                    Message = "Success",
                    Success = true,
                    Data = date
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return new ServiceResult<List<so_payment_type>>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                };
            }

        }
        public async Task<ServiceResult<so_payment_type>> Get_payment(string payTypeCode)
        {
            try
            {
                if (payTypeCode == null)
                {
                    _logger.LogInformation("NO data payTypeCode found");
                    return new ServiceResult<so_payment_type>
                    {
                        Success = false,
                        Status = 400,
                        Message = "No Data foun payTypeCode"
                    };
                }
                var date = await _context.so_Payment_Types.FirstOrDefaultAsync(a => a.PayTypeCode == payTypeCode);
                return new ServiceResult<so_payment_type>
                {
                    Status = 200,
                    Message = "Success",
                    Success = true,
                    Data = date
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<so_payment_type>
                {
                    Success = false,
                    Message = ex.Message,
                    Status = 500,
                };
            }

        }
        public async Task<ServiceResult<so_payment_type>> Update_payment(string payTypeCode, so_payment_type so_payment)
        {
            var transation = await _context.Database.BeginTransactionAsync();
            try
            {
                if (payTypeCode == null)
                {
                    _logger.LogInformation($"payTypeCode {payTypeCode}");
                    return new ServiceResult<so_payment_type>
                    {
                        Success = false,
                        Status = 400,
                        Message = "NO data found"
                    };
                }
                var existing = await _context.so_Payment_Types.FirstOrDefaultAsync(a => a.PayTypeCode == payTypeCode);
                existing.company_id = so_payment.company_id;
                existing.Bank_pcnt = so_payment.Bank_pcnt;
                existing.is_avilable = so_payment.is_avilable;
                existing.req_det = so_payment.req_det;
                existing.Description = so_payment.Description;
                existing.card_type = so_payment.card_type;
                existing.cash_types = so_payment.cash_types;
                existing.Order = so_payment.Order;
                existing.co_business = null;

                _context.so_Payment_Types.Update(existing);
                await _context.SaveChangesAsync();
                await transation.CommitAsync();
                return new ServiceResult<so_payment_type>
                {
                    Status = 200,
                    Message = "Updated",
                    Success = true,
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                await transation.RollbackAsync();
                _logger.LogInformation($"{ex.Message}", ex);
                return new ServiceResult<so_payment_type>
                {
                    Success = false,
                    Message = ex.Message,
                    Status = 500,

                };
            }

        }
        public async Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches(Guid variant_id)
        {
            if (variant_id == null)
            {
                _logger.LogInformation("NO data found Get_item_batches");
                return new ServiceResult<List<im_ItemBatches>>
                {
                    Success = false,
                    Status = 400,
                    Message = "NO data found"
                };
            }
            var today = DateOnly.FromDateTime(DateTime.Today);



            var expery_count = await _context.im_itemBatches.Where(a => a.variant_id == variant_id).ToListAsync();


            return new ServiceResult<List<im_ItemBatches>>
            {
                Status = 200,
                Message = "Success",
                Success = true,
                Data = expery_count

            };

        }
        public async Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches_list(Guid variant_id, decimal requiredQuantity)
        {
            if (variant_id == null)
            {
                _logger.LogInformation("NO data found Get_item_batches");
                return new ServiceResult<List<im_ItemBatches>>
                {
                    Success = false,
                    Status = 400,
                    Message = "NO data found"
                };
            }
            var today = DateOnly.FromDateTime(DateTime.Today);



            var expery_count = await _context.im_itemBatches.Where(a => a.variant_id == variant_id && a.expiry_date > today && a.on_hand_quantity >= 0).OrderBy(a => a.expiry_date).ToListAsync();
            Decimal? remainingQty = requiredQuantity;

            List<im_ItemBatches> itemBatches = new List<im_ItemBatches>();
            foreach (var item in expery_count)
            {
                if (remainingQty <= 0)
                    break;

                decimal usedQty = Math.Min(item.on_hand_quantity ?? 0, remainingQty ?? 0);
                item.on_hand_quantity = usedQty;
                itemBatches.Add(item);

                remainingQty -= usedQty;
            }



            return new ServiceResult<List<im_ItemBatches>>
            {
                Status = 200,
                Message = "Success",
                Success = true,
                Data = itemBatches

            };

        }
    }
}
