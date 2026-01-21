using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Microsoft.EntityFrameworkCore;

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
                var existing_im_purchase = await _context.im_purchase_listing.Include(a=>a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == im_Purchase_Listing.listing_id);
                if (existing_im_purchase == null)
                {
                    im_Purchase_Listing.listing_id = Guid.CreateVersion7();
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
                        item.detail_id = Guid.CreateVersion7();
                        item.listing_id = im_Purchase_Listing.listing_id;
                        item.product_id = item.product_id;
                        item.sub_variant_id = item.sub_variant_id;
                        item.uom_name = item.uom_name;
                        item.unit_price = item.unit_price;
                        item.product_description = item.product_description;
                        item.discount_amount = item.discount_amount;
                        item.tax_amount = item.tax_amount;
                        item.freight_amount = item.freight_amount;
                        item.other_expenses = item.other_expenses;
                        item.line_total = item.quantity*item.unit_price;
                        //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                        item.notes = item.notes;
                        item.expiry_date = item.expiry_date;
                        item.quantity = item.quantity;
                    }
                    _context.im_purchase_listing.Add(im_Purchase_Listing);

                }
                else
                {
                    existing_im_purchase.site_id = im_Purchase_Listing.site_id;
                    existing_im_purchase.vendor_id = im_Purchase_Listing.vendor_id;
                    existing_im_purchase.created_at = DateTime.Now;
                    existing_im_purchase.edit_user_id = im_Purchase_Listing.edit_user_id;
                    existing_im_purchase.payment_mode = im_Purchase_Listing.payment_mode;
                    existing_im_purchase.purchase_type = im_Purchase_Listing.purchase_type;
                    existing_im_purchase.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                    existing_im_purchase.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                    existing_im_purchase.currency_code = im_Purchase_Listing.currency_code;
                    existing_im_purchase.exchange_rate = im_Purchase_Listing.exchange_rate;
                    existing_im_purchase.sub_total = im_Purchase_Listing.sub_total;
                    existing_im_purchase.discount_amount = im_Purchase_Listing.discount_amount;
                    existing_im_purchase.freight_amount = im_Purchase_Listing.freight_amount;
                    existing_im_purchase.tax_amount = im_Purchase_Listing.tax_amount;
                    existing_im_purchase.other_expenses = im_Purchase_Listing.other_expenses;
                    existing_im_purchase.received_at = im_Purchase_Listing.received_at;
                    existing_im_purchase.notes = im_Purchase_Listing.notes;
                    existing_im_purchase.status = im_Purchase_Listing.status;
                    existing_im_purchase.doc_total = (im_Purchase_Listing.sub_total - im_Purchase_Listing.discount_amount + im_Purchase_Listing.tax_amount + im_Purchase_Listing.freight_amount + im_Purchase_Listing.other_expenses);
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        var existing_purchase = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                        if (existing_purchase == null)
                        {
                            item.detail_id = Guid.CreateVersion7();
                            item.listing_id = im_Purchase_Listing.listing_id;
                            item.product_id = item.product_id;
                            item.sub_variant_id = item.sub_variant_id;
                            item.uom_name = item.uom_name;
                            item.unit_price = item.unit_price;
                            item.discount_amount = item.discount_amount;
                            item.tax_amount = item.tax_amount;
                            item.freight_amount = item.freight_amount;
                            item.other_expenses = item.other_expenses;
                            item.product_description = item.product_description;
                            item.line_total = item.quantity * item.unit_price;

                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            item.notes = item.notes;
                            item.expiry_date = item.expiry_date;
                            item.quantity = item.quantity;
                            _context.im_purchase_listing_details.Add(item);
                            existing_im_purchase.im_purchase_listing_details.Add(item);

                        }
                        else
                        {

                            existing_purchase.uom_name = item.uom_name;
                            existing_purchase.unit_price = item.unit_price;
                            existing_purchase.discount_amount = item.discount_amount;
                            existing_purchase.tax_amount = item.tax_amount;
                            existing_purchase.freight_amount = item.freight_amount;
                            existing_purchase.product_description = item.product_description;
                            existing_purchase.other_expenses = item.other_expenses;
                            existing_purchase.quantity = item.quantity;
                            existing_purchase.line_total = item.quantity * item.unit_price;

                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            existing_purchase.notes = item.notes;
                            existing_purchase.expiry_date = item.expiry_date;
                            _context.im_purchase_listing_details.Update(existing_purchase);
                        }
                            
                        //_context.im_purchase_listing.Add(item);
                    }
                    _context.im_purchase_listing.Update(existing_im_purchase);
                }


                await _context.SaveChangesAsync();
                return new ServiceResult<im_purchase_listing>
                {
                    Success = true,
                    Status = 1,
                    Message = "Data saved successfully",
                    Data = im_Purchase_Listing
                };

            }
            catch (Exception ex)
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

        public async Task<ServiceResult<List<im_purchase_listing>>> Purchase_list(Guid site_id)
        {
            try
            {
                if (site_id == null)
                {
                    _logger.LogInformation("No id found site_id");
                    return new ServiceResult<List<im_purchase_listing>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Not found ID"
                    };
                }
                var purchase_list = await _context.im_purchase_listing.Where(a => a.site_id == site_id).ToListAsync();
                if (purchase_list.Count == 0)
                {
                    _logger.LogInformation("NO data found");
                    return new ServiceResult<List<im_purchase_listing>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Not found"
                    };
                }
                return new ServiceResult<List<im_purchase_listing>>
                {
                    Status = 200,
                    Success = true,
                    Data = purchase_list,
                    Message = ""

                };
            }
            catch(Exception ex)
            {
                return new ServiceResult<List<im_purchase_listing>>
                {
                    Status = 500,
                    Success = false,

                };
            }
            
        }

        public async Task<ServiceResult<im_purchase_listing>> im_purchase_details(Guid listing_id)
        {
            try
            {
                if (listing_id == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                    };
                }
                var listing_data = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
                if (listing_data == null)
                {
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                    };
                }
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 200,
                    Success = true,
                    Data = listing_data,

                };
            }
            catch(Exception ex)
            {
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 500,
                    Success = false,

                };
            }
            

        }

        public async Task<ServiceResult<im_purchase_listing>> Update_purchase(Guid listing_id, im_purchase_listing im_Purchase_Listing)
        {
            try
            {
                if (listing_id == null)
                {
                    _logger.LogInformation("Update_purchase Not found listing_id");
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Update_purchase Not found listing_id"
                    };
                }
                var existing_purchase = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);

                //Delete
                var exist_deatils = await _context.im_purchase_listing_details.Where(a=>a.listing_id==existing_purchase.listing_id).ToListAsync();
                var new_deatil_id = im_Purchase_Listing.im_purchase_listing_details.Select(id => id.detail_id).ToList();
                var delete_deatils = exist_deatils.Where(a => !new_deatil_id.Contains(a.detail_id)).ToList();
                if (delete_deatils.Any())
                {
                    _context.im_purchase_listing_details.RemoveRange(delete_deatils);

                    var deletedProductIds = delete_deatils.Select(d => d.product_id).Distinct().ToList();
                    var temp_variants = await _context.temp_im_variants.Where(tv => deletedProductIds.Contains(tv.product_id)).ToListAsync();

                    if (temp_variants.Any())
                    {
                        _context.temp_im_variants.RemoveRange(temp_variants);
                    }
                    await _context.SaveChangesAsync();

                }

                if (existing_purchase != null)
                {

                    existing_purchase.edit_user_id = im_Purchase_Listing.edit_user_id;
                    existing_purchase.payment_mode = im_Purchase_Listing.payment_mode;
                    existing_purchase.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                    existing_purchase.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                    existing_purchase.currency_code = im_Purchase_Listing.currency_code;
                    existing_purchase.exchange_rate = im_Purchase_Listing.exchange_rate;
                    existing_purchase.sub_total = im_Purchase_Listing.sub_total;
                    existing_purchase.discount_amount = im_Purchase_Listing.discount_amount;
                    existing_purchase.freight_amount = im_Purchase_Listing.freight_amount;
                    existing_purchase.tax_amount = im_Purchase_Listing.tax_amount;
                    existing_purchase.other_expenses = im_Purchase_Listing.other_expenses;
                    existing_purchase.received_at = im_Purchase_Listing.received_at;
                    existing_purchase.notes = im_Purchase_Listing.notes;
                    existing_purchase.status = im_Purchase_Listing.status;
                    existing_purchase.doc_total = (im_Purchase_Listing.sub_total - im_Purchase_Listing.discount_amount + im_Purchase_Listing.tax_amount + im_Purchase_Listing.freight_amount + im_Purchase_Listing.other_expenses);
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        var existing_im_purchase_listing_details = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                        if (existing_im_purchase_listing_details != null)
                        {
                            existing_im_purchase_listing_details.product_id = item.product_id;
                            existing_im_purchase_listing_details.sub_variant_id = item.sub_variant_id;
                            existing_im_purchase_listing_details.uom_name = item.uom_name;
                            existing_im_purchase_listing_details.product_description = item.product_description;
                            existing_im_purchase_listing_details.unit_price = item.unit_price;
                            existing_im_purchase_listing_details.discount_amount = item.discount_amount;
                            existing_im_purchase_listing_details.tax_amount = item.tax_amount;
                            existing_im_purchase_listing_details.freight_amount = item.freight_amount;
                            existing_im_purchase_listing_details.other_expenses = item.other_expenses;
                            existing_im_purchase_listing_details.line_total  = item.quantity * item.unit_price;
                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            existing_im_purchase_listing_details.notes = item.notes;
                            existing_im_purchase_listing_details.expiry_date = item.expiry_date;
                            existing_im_purchase_listing_details.quantity = item.quantity;
                            _context.im_purchase_listing_details.Update(existing_im_purchase_listing_details);
                        }
                        else
                        {
                            item.detail_id = Guid.CreateVersion7();
                            item.listing_id = im_Purchase_Listing.listing_id;
                            item.product_id = item.product_id;
                            item.sub_variant_id = item.sub_variant_id;
                            item.uom_name = item.uom_name;
                            item.product_description = item.product_description;
                            item.unit_price = item.unit_price;
                            item.discount_amount = item.discount_amount;
                            item.tax_amount = item.tax_amount;
                            item.freight_amount = item.freight_amount;
                            item.other_expenses = item.other_expenses;
                            item.line_total = item.quantity * item.unit_price;

                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            item.notes = item.notes;
                            item.expiry_date = item.expiry_date;
                            item.quantity = item.quantity;
                            _context.im_purchase_listing_details.Add(item);
                            existing_purchase.im_purchase_listing_details.Add(item);
                        }
                    }
                }
                _context.im_purchase_listing.Update(existing_purchase);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 201,
                    Success = true,
                    Message = "Success",
                    Data = existing_purchase
                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error While Update_purchase");
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }
    }

}
