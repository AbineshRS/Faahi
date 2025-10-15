using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;

namespace Faahi.Service.im_products.im_purchase
{
    public class im_purchase_service : Iim_purchase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<im_purchase_service> _logger;
        public im_purchase_service(ApplicationDbContext context, ILogger<im_purchase_service> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<im_purchase_listing>> Create_im_purchase(im_purchase_listing im_Purchase_Listing)
        {
            if (im_Purchase_Listing == null)
            {
                _logger.LogError("Create_im_purchase: No data found");
                return new ServiceResult<im_purchase_listing>
                {
                    Success = false,
                    Status = -1,
                    Message = "No data found",
                    Data = null
                };
            }
            try
            {
                im_Purchase_Listing.listing_id = Guid.NewGuid();
                im_Purchase_Listing.site_id = im_Purchase_Listing.site_id;
                im_Purchase_Listing.vendor_id = im_Purchase_Listing.vendor_id;
                im_Purchase_Listing.created_at = DateTime.Now;
                im_Purchase_Listing.edit_user_id = im_Purchase_Listing.edit_user_id;
                im_Purchase_Listing.payment_mode = im_Purchase_Listing.payment_mode;
                im_Purchase_Listing.purchase_type = im_Purchase_Listing.purchase_type;
                im_Purchase_Listing.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                im_Purchase_Listing.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                im_Purchase_Listing.currency_code = im_Purchase_Listing.currency_code;
                im_Purchase_Listing.exchange_rate = im_Purchase_Listing.exchange_rate;
                im_Purchase_Listing.sub_total = im_Purchase_Listing.sub_total;
                im_Purchase_Listing.discount_amount = im_Purchase_Listing.discount_amount;
                im_Purchase_Listing.freight_amount = im_Purchase_Listing.freight_amount;
                im_Purchase_Listing.tax_amount = im_Purchase_Listing.tax_amount;
                im_Purchase_Listing.other_expenses = im_Purchase_Listing.other_expenses;
                im_Purchase_Listing.received_at = im_Purchase_Listing.received_at;
                im_Purchase_Listing.notes = im_Purchase_Listing.notes;
                im_Purchase_Listing.status = im_Purchase_Listing.status;
                im_Purchase_Listing.doc_total = (im_Purchase_Listing.sub_total - im_Purchase_Listing.discount_amount + im_Purchase_Listing.tax_amount + im_Purchase_Listing.freight_amount + im_Purchase_Listing.other_expenses);
                foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                {
                    item.detail_id = Guid.NewGuid();
                    item.listing_id = im_Purchase_Listing.listing_id;
                    item.product_id = item.product_id;
                    item.sub_variant_id = item.sub_variant_id;
                    item.uom_id = item.uom_id;
                    item.unit_price = item.unit_price;
                    item.discount_amount = item.discount_amount;
                    item.tax_amount = item.tax_amount;
                    item.freight_amount = item.freight_amount;
                    item.other_expenses = item.other_expenses;
                    item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                    item.notes = item.notes;
                    item.expiry_date = item.expiry_date;
                }
                _context.im_purchase_listing.Add(im_Purchase_Listing);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_purchase_listing>
                {
                    Success = true,
                    Status = 1,
                    Message = "Data saved successfully",
                    Data = im_Purchase_Listing
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Create_im_purchase: An error occurred while creating purchase listing");
                return new ServiceResult<im_purchase_listing>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while processing your request.",
                    Data = null
                };
            }
            
        }
    }

}
