using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.am_vcos;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.Users
{
    public class User_service : IUser
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<User_service> _logger;
        public User_service(ApplicationDbContext context, ILogger<User_service> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ServiceResult<ap_Vendors>> Create_vendors(ap_Vendors vendors)
        {
            if (vendors == null)
            {
                _logger.LogWarning("Create_vendors: No data found");
                return new ServiceResult<ap_Vendors>
                {
                    Success = false,
                    Message = "NO Data found",
                    Status = -1
                };
            }
            try
            {
                var random = new Random();
                string vendorCode;
                bool exists;

                do
                {
                    vendorCode = random.Next(100000, 999999).ToString();
                    exists = await _context.ap_Vendors.AnyAsync(v => v.vendor_code == vendorCode);
                }
                while (exists);



                vendors.vendor_id = Guid.CreateVersion7();
                vendors.vendor_code = "V-" + Convert.ToString(vendorCode);
                vendors.payment_term_id = vendors.payment_term_id;
                vendors.preferred_payment_method = vendors.preferred_payment_method;
                vendors.withholding_tax_rate = vendors.withholding_tax_rate;
                vendors.ap_control_account = vendors.ap_control_account;
                vendors.note = vendors.note;
                vendors.created_at = DateTime.Now;
                vendors.updated_at = DateTime.Now;
                vendors.company_id = vendors.company_id;
                vendors.status = vendors.status;
                _context.ap_Vendors.Add(vendors);


                await _context.SaveChangesAsync();

                return new ServiceResult<ap_Vendors>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = vendors
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a vendor.");
                return new ServiceResult<ap_Vendors>
                {
                    Success = false,
                    Message = "An error occurred while creating the vendor.",
                    Status = -1
                };
            }

            

        }
        public async Task<ServiceResult<ar_Customers>> Create_customer(ar_Customers ar_Customers)
        {
            if (ar_Customers == null)
            {
                _logger.LogWarning("Create_customer: No data found");
                return new ServiceResult<ar_Customers>
                {
                    Success = false,
                    Message = "NO Data found",
                    Status = -1
                };
            }
            try
            {
                string customerCode;
                bool exists;
                var random = new Random();
                do
                {
                    customerCode = random.Next(100000, 999999).ToString();
                    exists = await _context.ar_Customers
                                           .AnyAsync(c => c.customer_code == customerCode);
                }
                while (exists);

                ar_Customers.customer_id = Guid.CreateVersion7();
                ar_Customers.customer_code = "C-" + customerCode;
                ar_Customers.price_tier_id = ar_Customers.price_tier_id;
                ar_Customers.payment_term_id = ar_Customers.payment_term_id;
                ar_Customers.credit_limit = ar_Customers.credit_limit;
                ar_Customers.default_shipping_address_id = ar_Customers.default_shipping_address_id;
                ar_Customers.default_billing_address_id = ar_Customers.default_billing_address_id;
                ar_Customers.loyalty_level = ar_Customers.loyalty_level;
                ar_Customers.loyalty_points = ar_Customers.loyalty_points;
                ar_Customers.note = ar_Customers.note;
                ar_Customers.created_at = DateTime.Now;
                ar_Customers.updated_at = DateTime.Now;
                ar_Customers.credit_hold = ar_Customers.credit_hold;
                ar_Customers.tax_exempt = ar_Customers.tax_exempt;
                ar_Customers.company_id = ar_Customers.company_id;
                ar_Customers.status = ar_Customers.status;
                _context.ar_Customers.Add(ar_Customers);



                await _context.SaveChangesAsync();

                return new ServiceResult<ar_Customers>
                {
                    Success = true,
                    Status = 1,
                    Message = "Success",
                    Data = ar_Customers
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a customer.");
                return new ServiceResult<ar_Customers>
                {
                    Success = false,
                    Message = "An error occurred while creating the customer.",
                    Status = -1
                };
            }
            
        }
        public async Task<ServiceResult<List<ar_Customers>>> Get_all_customer(Guid company_id)
        {
            if (company_id == null)
            {
                return new ServiceResult<List<ar_Customers>>
                {
                    Success = false,
                    Message = "NO Data found",
                    Status = -1
                };
            }
            var im_site = await _context.im_site.FirstOrDefaultAsync(s => s.site_id == company_id || s.company_id == company_id);
            var customers = await _context.ar_Customers.Where(c => c.company_id == im_site.company_id).OrderByDescending(d => d.created_at ?? DateTime.MinValue).ToListAsync();
            if (customers == null || customers.Count == 0)
            {
                return new ServiceResult<List<ar_Customers>>
                {
                    Success = false,
                    Message = "No customers found for the given site_id",
                    Status = 0,
                    Data = new List<ar_Customers>()
                };
            }
            return new ServiceResult<List<ar_Customers>>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = customers
            };
        }
        public async Task<ServiceResult<List<ap_Vendors>>> Get_all_vendors(Guid company_id)
        {
            if (company_id == null)
            {
                return new ServiceResult<List<ap_Vendors>>
                {
                    Success = false,
                    Message = "NO Data found",
                    Status = -1
                };
            }
            var im_site = await _context.im_site.FirstOrDefaultAsync(s => s.site_id == company_id || s.company_id == company_id);
            var vendors = await _context.ap_Vendors.Where(v => v.company_id == im_site.company_id).OrderByDescending(d=>d.created_at ?? DateTime.MinValue).ToListAsync();
            if (vendors == null || vendors.Count == 0)
            {
                return new ServiceResult<List<ap_Vendors>>
                {
                    Success = false,
                    Message = "No vendors found for the given site_id",
                    Status = 0,
                    Data = new List<ap_Vendors>()
                };
            }
            return new ServiceResult<List<ap_Vendors>>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = vendors
            };
        }
    }
}
