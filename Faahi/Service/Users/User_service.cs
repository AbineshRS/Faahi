using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.am_vcos;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.Users
{
    public class User_service:IUser
    {
        private readonly ApplicationDbContext _context;

        public User_service(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult<ap_Vendors>> Create_vendors(ap_Vendors vendors)
        {
            if (vendors == null)
            {
                return new ServiceResult<ap_Vendors>
                {
                    Success = false,
                    Message = "NO Data found",
                    Status = -1
                };
            }
           
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
            vendors.vendor_code="V-"+ Convert.ToString( vendorCode);
            vendors.payment_term_id = vendors.payment_term_id ;
            vendors.preferred_payment_method=vendors.preferred_payment_method;
            vendors.withholding_tax_rate=vendors.withholding_tax_rate;
            vendors.ap_control_account=vendors.ap_control_account;
            vendors.note=vendors.note;
            vendors.created_at=DateTime.Now;
            vendors.updated_at=DateTime.Now;
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
        public async Task<ServiceResult<ar_Customers>> Create_customer(ar_Customers ar_Customers)
        {
            if(ar_Customers == null)
            {
                return new ServiceResult<ar_Customers>
                {
                    Success = false,
                    Message = "NO Data found",
                    Status=-1
                };
            }
           
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

            ar_Customers.customer_id=Guid.CreateVersion7();
            ar_Customers.customer_code="C-"+customerCode;
            ar_Customers.price_tier_id=ar_Customers.price_tier_id;
            ar_Customers.payment_term_id = ar_Customers.payment_term_id;
            ar_Customers.credit_limit=ar_Customers.credit_limit;
            ar_Customers.default_shipping_address_id = ar_Customers.default_shipping_address_id;
            ar_Customers.default_billing_address_id=ar_Customers.default_billing_address_id ;
            ar_Customers.loyalty_level=ar_Customers.loyalty_level;
            ar_Customers.loyalty_points=ar_Customers.loyalty_points;
            ar_Customers.note=ar_Customers.note;
            ar_Customers.created_at = DateTime.Now;
            ar_Customers.updated_at = DateTime.Now;
            ar_Customers.credit_hold=ar_Customers.credit_hold;
            ar_Customers.tax_exempt=ar_Customers.tax_exempt;
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
    }
}
