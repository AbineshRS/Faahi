using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.mk_blacklisted;
using Faahi.Migrations;
using Faahi.Model.site_settings;
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
            var transaction = await _context.Database.BeginTransactionAsync();
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
                await transaction.CommitAsync();
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
                    await transaction.RollbackAsync();
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
            var transaction = await _context.Database.BeginTransactionAsync();  
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
                await transaction.CommitAsync();
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
                    await transaction.RollbackAsync();
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
        public async Task<ServiceResult<mk_blacklisted_numbers>> Add_blacklist(mk_blacklisted_numbers mk_Blacklisted_)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (mk_Blacklisted_ == null)
                {
                    return new ServiceResult<mk_blacklisted_numbers>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var exising = await _context.mk_blacklisted_numbers.FirstOrDefaultAsync(a => a.phone_number == mk_Blacklisted_.phone_number);
                if (exising != null)
                {
                    return new ServiceResult<mk_blacklisted_numbers>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Already exists"
                    };
                }
                mk_Blacklisted_.blacklist_id = Guid.CreateVersion7();
                mk_Blacklisted_.business_id = mk_Blacklisted_.business_id;
                mk_Blacklisted_.phone_number = mk_Blacklisted_.phone_number;
                mk_Blacklisted_.reason= mk_Blacklisted_.reason;
                mk_Blacklisted_.created_at = DateTime.UtcNow;
                mk_Blacklisted_.updated_at=DateTime.UtcNow;
                mk_Blacklisted_.is_active = "T";
                _context.mk_blacklisted_numbers.Add(mk_Blacklisted_);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<mk_blacklisted_numbers>
                {
                    Status = 200,
                    Success = true,
                    Message = "success",
                    Data = mk_Blacklisted_
                };
            }
            catch(Exception ex)
            {
                
                await transaction.RollbackAsync();
                return new ServiceResult<mk_blacklisted_numbers>
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message
                };
            }
            
        }

        public async Task<ServiceResult<List<mk_blacklisted_numbers>>> blacklist(Guid business_id)
        {
            try
            {
                if (business_id == null)
                {
                    return new ServiceResult<List<mk_blacklisted_numbers>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var list = await _context.mk_blacklisted_numbers.Where(a=>a.business_id==business_id).ToListAsync();
                if(list.Count == 0)
                {
                    return new ServiceResult<List<mk_blacklisted_numbers>>
                    {
                        Status = 250,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<mk_blacklisted_numbers>>
                {
                    Success = true,
                    Status = 200,
                    Data = list
                };

            }catch(Exception ex)
            {
                return new ServiceResult<List<mk_blacklisted_numbers>>
                {
                    Status = 500,
                    Message = ex.Message

                };
            }
        }

        public async Task<ServiceResult<mk_blacklisted_numbers_dto>> Change_status(Guid blacklist_id, mk_blacklisted_numbers_dto mk_Blacklisted_)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (blacklist_id == null)
                {
                    return new ServiceResult<mk_blacklisted_numbers_dto>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data dound"
                    };
                }
                var existing = await _context.mk_blacklisted_numbers.FirstOrDefaultAsync(a => a.blacklist_id == blacklist_id);
                if (existing != null)
                {
                    existing.is_active= mk_Blacklisted_.is_active;
                    _context.mk_blacklisted_numbers.Update(existing);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                return new ServiceResult<mk_blacklisted_numbers_dto>
                {
                    Status = 200,
                    Success = true,
                    Message = "Updated",
                    //Data = existing
                };
                
            }catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return new ServiceResult<mk_blacklisted_numbers_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
