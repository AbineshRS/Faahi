using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.tax_class_table;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.site_settings_service
{
    public class site_settings_service:Isite_settings
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<site_settings_service> _logger;
        public site_settings_service(ApplicationDbContext applicationDb)
        {
            _context = applicationDb;
        }

        public async Task<ServiceResult<tx_TaxClasses>> Add_tax_class(tx_TaxClasses tx_TaxClasses)
        {
            try
            {
                if (tx_TaxClasses == null)
                {
                    _logger.LogInformation("NO data found to inseert");
                    return new ServiceResult<tx_TaxClasses>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found to insert"
                    };
                }
                tx_TaxClasses.tax_class_id = Guid.CreateVersion7();
                tx_TaxClasses.company_id = tx_TaxClasses.company_id;
                tx_TaxClasses.tax_class_name = tx_TaxClasses.tax_class_name;
                tx_TaxClasses.rate_percent = tx_TaxClasses.rate_percent;
                _context.tx_TaxClasses.Add(tx_TaxClasses);
                await _context.SaveChangesAsync();
                return new ServiceResult<tx_TaxClasses>
                {
                    Status = 201,
                    Success = true,
                    Message = "Inserted",
                    Data = tx_TaxClasses
                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error While Add_tax_class");
                return new ServiceResult<tx_TaxClasses>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }

        public async Task<ServiceResult<tx_TaxClasses>> Update_tax(Guid tax_class_id,tx_TaxClasses tx_TaxClasses)
        {
            try
            {
                if (tax_class_id == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<tx_TaxClasses>
                    {
                        Status = 400,
                        Success = false,
                        Message = "NO data found"
                    };
                }
                var existing_tax = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == tax_class_id);
                existing_tax.rate_percent = tx_TaxClasses.rate_percent;
                existing_tax.created_at = DateTime.Now;
                _context.tx_TaxClasses.Update(existing_tax);
                await _context.SaveChangesAsync();
                return new ServiceResult<tx_TaxClasses>
                {
                    Status = 201,
                    Success = true,
                    Message = "updated",
                    Data = existing_tax
                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while Update_tax");
                return new ServiceResult<tx_TaxClasses>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }
        
        public async Task<ServiceResult<List<tx_TaxClasses>>> Get_tax(Guid company_id)
        {
            try
            {
                if (company_id == null)
                {
                    _logger.LogInformation("NO company_id found");
                    return new ServiceResult<List<tx_TaxClasses>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No company_id found"
                    };
                }
                var tax_list = await _context.tx_TaxClasses.Where(a => a.company_id == company_id).ToListAsync();
                return new ServiceResult<List<tx_TaxClasses>>
                {
                    Status = 200,
                    Success = true,
                    Data = tax_list
                };

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while Get_tax");
                return new ServiceResult<List<tx_TaxClasses>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
           
        }
        public async Task<ServiceResult<tx_TaxClasses>> get_tax_class(Guid tax_class_id)
        {
            try
            {
                if (tax_class_id == null)
                {
                    _logger.LogInformation("NO company_id found");
                    return new ServiceResult<tx_TaxClasses>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No company_id found"
                    };
                }
                var tax_list = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == tax_class_id);
                return new ServiceResult<tx_TaxClasses>
                {
                    Status = 200,
                    Success = true,
                    Data = tax_list
                };

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while Get_tax");
                return new ServiceResult<tx_TaxClasses>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
           
        }
    }
}
