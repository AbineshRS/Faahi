using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using MailKit.Net.Imap;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Xml;

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
                im_ItemBatches im_ItemBatches = new im_ItemBatches();

                var existing_im_purchase = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == im_Purchase_Listing.listing_id);
                if (existing_im_purchase == null)
                {

                    //im_ItemBatches
                    var table = "im_ItemBatches";
                    var table_key = await _context.super_abi.FirstOrDefaultAsync(a => a.description == table);
                    var key = Convert.ToInt16(table_key.next_key);

                    var table_2 = "im_purchase_listing";
                    var table_key_2 = await _context.am_table_next_key.FindAsync(table_2);
                    var key_2 = Convert.ToInt16(table_key_2.next_key);

                    var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);

                    Decimal sub_total = 0;
                    Decimal other_expense = 0;
                    var year = DateTime.Now.Year;


                    im_Purchase_Listing.listing_id = Guid.CreateVersion7();
                    im_Purchase_Listing.site_id = im_Purchase_Listing.site_id;
                    im_Purchase_Listing.listing_code = im_Purchase_Listing.listing_code + year + "-" + Convert.ToString(key_2 + 1);
                    im_Purchase_Listing.vendor_id = im_Purchase_Listing.vendor_id;
                    im_Purchase_Listing.created_at = DateOnly.FromDateTime(DateTime.Now);
                    im_Purchase_Listing.edit_user_id = im_Purchase_Listing.edit_user_id;
                    im_Purchase_Listing.payment_mode = im_Purchase_Listing.payment_mode;
                    im_Purchase_Listing.purchase_type = im_Purchase_Listing.purchase_type;
                    im_Purchase_Listing.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                    im_Purchase_Listing.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                    im_Purchase_Listing.currency_code = im_Purchase_Listing.currency_code;
                    im_Purchase_Listing.exchange_rate = im_Purchase_Listing.exchange_rate;
                    im_Purchase_Listing.plastic_bag = im_Purchase_Listing.plastic_bag;
                    im_Purchase_Listing.sub_total = im_Purchase_Listing.sub_total;
                    im_Purchase_Listing.discount_amount = im_Purchase_Listing.discount_amount;
                    im_Purchase_Listing.freight_amount = im_Purchase_Listing.freight_amount;
                    im_Purchase_Listing.tax_amount = im_Purchase_Listing.tax_amount;
                    im_Purchase_Listing.other_expenses = im_Purchase_Listing.other_expenses;
                    im_Purchase_Listing.received_at = im_Purchase_Listing.received_at;
                    im_Purchase_Listing.local_referance = im_Purchase_Listing.local_referance;
                    im_Purchase_Listing.notes = im_Purchase_Listing.notes;
                    im_Purchase_Listing.status = im_Purchase_Listing.status;
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        item.detail_id = Guid.CreateVersion7();
                        item.listing_id = im_Purchase_Listing.listing_id;
                        item.product_id = item.product_id;
                        item.sub_variant_id = item.sub_variant_id;
                        item.store_variant_inventory_id = item.store_variant_inventory_id;
                        item.uom_name = item.uom_name;
                        item.unit_price = item.unit_price;
                        item.product_description = item.product_description;
                        item.discount_amount = item.discount_amount;
                        item.tax_amount = item.tax_amount;
                        item.barcode = item.barcode;
                        item.freight_amount = item.freight_amount;
                        item.other_expenses = item.other_expenses;
                        other_expense += Convert.ToDecimal(item.other_expenses);
                        item.line_total = item.quantity * item.unit_price;
                        sub_total += Convert.ToDecimal(item.line_total);
                        //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                        item.notes = item.notes;
                        item.expiry_date = item.expiry_date;
                        item.quantity = item.quantity;

                        if (item.expiry_date != null)
                        {
                            im_ItemBatches.item_batch_id = Guid.CreateVersion7();
                            im_ItemBatches.batch_id = key;
                            im_ItemBatches.company_id = im_site.company_id;
                            im_ItemBatches.detail_id = item.detail_id;
                            im_ItemBatches.store_id = im_Purchase_Listing.site_id;
                            im_ItemBatches.variant_id = item.sub_variant_id;
                            im_ItemBatches.store_variant_inventory_id = item.store_variant_inventory_id;
                            im_ItemBatches.expiry_date = item.expiry_date;
                            im_ItemBatches.batch_number = item.batch_no;
                            im_ItemBatches.received_quantity = item.quantity;
                            im_ItemBatches.on_hand_quantity = item.quantity;
                            im_ItemBatches.unit_cost = item.unit_price;
                            im_ItemBatches.total_cost = item.quantity * item.unit_price;
                            im_ItemBatches.is_active = "T";
                            im_ItemBatches.batch_on_hold = "F";
                            im_ItemBatches.received_date = DateTime.Now;
                            im_ItemBatches.reference_doc = im_Purchase_Listing.supplier_invoice_no;
                            im_ItemBatches.notes = im_ItemBatches.notes;
                            im_ItemBatches.barcode = item.barcode;
                            im_ItemBatches.sku = item.sku;
                            im_ItemBatches.product_description = item.product_description;
                            im_ItemBatches.created_at = DateTime.Now;
                            if (table_key != null)
                            {
                                table_key.next_key = key + 1;
                                _context.super_abi.Update(table_key);
                                await _context.SaveChangesAsync();
                            }
                            _context.im_itemBatches.Add(im_ItemBatches);
                        }
                    }
                    im_Purchase_Listing.sub_total = Convert.ToDecimal(sub_total);
                    im_Purchase_Listing.doc_total = (Convert.ToDecimal(sub_total) - im_Purchase_Listing.discount_amount + im_Purchase_Listing.tax_amount + im_Purchase_Listing.freight_amount + other_expense + im_Purchase_Listing.plastic_bag * im_Purchase_Listing.exchange_rate);
                    _context.im_purchase_listing.Add(im_Purchase_Listing);


                    table_key_2.next_key = key_2 + 1;
                    _context.am_table_next_key.Update(table_key_2);
                    await _context.SaveChangesAsync();
                    return new ServiceResult<im_purchase_listing>
                    {
                        Success = true,
                        Status = 1,
                        Message = "Data saved successfully",
                        Data = im_Purchase_Listing
                    };

                }
                else
                {
                    Decimal sub_total = 0;
                    Decimal other_expense = 0;

                    var im_purchase_sub_total = await _context.im_purchase_listing.FirstOrDefaultAsync(a => a.listing_id == existing_im_purchase.listing_id);
                    if (im_purchase_sub_total != null)
                    {
                        im_purchase_sub_total.sub_total = 0;
                        im_purchase_sub_total.doc_total = 0;
                        _context.im_purchase_listing.Update(im_purchase_sub_total);
                        await _context.SaveChangesAsync();
                    }

                    existing_im_purchase.site_id = im_Purchase_Listing.site_id;
                    existing_im_purchase.vendor_id = im_Purchase_Listing.vendor_id;
                    existing_im_purchase.created_at = DateOnly.FromDateTime(DateTime.Now);
                    existing_im_purchase.edit_user_id = im_Purchase_Listing.edit_user_id;
                    existing_im_purchase.payment_mode = im_Purchase_Listing.payment_mode;
                    existing_im_purchase.purchase_type = im_Purchase_Listing.purchase_type;
                    existing_im_purchase.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                    existing_im_purchase.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                    existing_im_purchase.currency_code = im_Purchase_Listing.currency_code;
                    existing_im_purchase.plastic_bag = im_Purchase_Listing.plastic_bag;
                    existing_im_purchase.exchange_rate = im_Purchase_Listing.exchange_rate;
                    existing_im_purchase.discount_amount = im_Purchase_Listing.discount_amount;
                    existing_im_purchase.freight_amount = im_Purchase_Listing.freight_amount;
                    existing_im_purchase.tax_amount = im_Purchase_Listing.tax_amount;
                    existing_im_purchase.other_expenses = im_Purchase_Listing.other_expenses;
                    existing_im_purchase.received_at = im_Purchase_Listing.received_at;
                    existing_im_purchase.local_referance = im_Purchase_Listing.local_referance;
                    existing_im_purchase.notes = im_Purchase_Listing.notes;
                    existing_im_purchase.status = im_Purchase_Listing.status;
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        var existing_purchase = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                        if (existing_purchase == null)
                        {
                            item.detail_id = Guid.CreateVersion7();
                            item.listing_id = im_Purchase_Listing.listing_id;
                            item.product_id = item.product_id;
                            item.sub_variant_id = item.sub_variant_id;
                            item.store_variant_inventory_id = item.store_variant_inventory_id;
                            item.uom_name = item.uom_name;
                            item.barcode = item.barcode;
                            item.unit_price = item.unit_price;
                            item.discount_amount = item.discount_amount;
                            item.tax_amount = item.tax_amount;
                            item.freight_amount = item.freight_amount;
                            item.other_expenses = item.other_expenses;
                            item.expiry_date = item.expiry_date;
                            item.product_description = item.product_description;
                            item.line_total = item.quantity * item.unit_price;

                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            item.notes = item.notes;
                            item.expiry_date = item.expiry_date;
                            item.quantity = item.quantity;
                            _context.im_purchase_listing_details.Add(item);
                            existing_im_purchase.im_purchase_listing_details.Add(item);

                            if (item.expiry_date != null)
                            {
                                var table = "im_ItemBatches";
                                var table_key = await _context.super_abi.FirstOrDefaultAsync(a => a.description == table);
                                var key = Convert.ToInt16(table_key.next_key);

                                var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);
                                im_ItemBatches.item_batch_id = Guid.CreateVersion7();
                                im_ItemBatches.batch_id = key;
                                im_ItemBatches.company_id = im_site.company_id;
                                im_ItemBatches.detail_id = item.detail_id;
                                im_ItemBatches.store_id = im_Purchase_Listing.site_id;
                                im_ItemBatches.store_variant_inventory_id = item.store_variant_inventory_id;
                                im_ItemBatches.batch_number = item.batch_no;
                                im_ItemBatches.variant_id = item.sub_variant_id;
                                im_ItemBatches.expiry_date = item.expiry_date;
                                im_ItemBatches.received_quantity = item.quantity;
                                im_ItemBatches.on_hand_quantity = item.quantity;
                                im_ItemBatches.unit_cost = item.unit_price;
                                im_ItemBatches.total_cost = item.quantity * item.unit_price;
                                im_ItemBatches.is_active = "T";
                                im_ItemBatches.received_date = DateTime.Now;
                                im_ItemBatches.reference_doc = im_Purchase_Listing.supplier_invoice_no;
                                im_ItemBatches.notes = im_ItemBatches.notes;
                                im_ItemBatches.notes = im_ItemBatches.notes;
                                im_ItemBatches.barcode = item.barcode;
                                im_ItemBatches.sku = item.sku;
                                im_ItemBatches.created_at = DateTime.Now;
                                if (table_key != null)
                                {
                                    table_key.next_key = key + 1;
                                    _context.super_abi.Update(table_key);
                                    await _context.SaveChangesAsync();
                                }
                                _context.im_itemBatches.Add(im_ItemBatches);
                            }

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
                            other_expense += Convert.ToDecimal(existing_purchase.other_expenses);

                            existing_purchase.quantity = item.quantity;
                            existing_purchase.line_total = item.quantity * item.unit_price;
                            //sub_total += Convert.ToDecimal( existing_purchase.line_total);

                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            existing_purchase.notes = item.notes;
                            existing_purchase.expiry_date = item.expiry_date;
                            _context.im_purchase_listing_details.Update(existing_purchase);

                            if (existing_purchase.expiry_date != null)
                            {
                                var existing_batches = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.variant_id == existing_purchase.sub_variant_id && a.store_id == existing_im_purchase.site_id);
                                if (existing_batches != null)
                                {
                                    existing_batches.store_id = existing_im_purchase.site_id;
                                    existing_batches.expiry_date = item.expiry_date;
                                    existing_batches.batch_number = item.batch_no;
                                    existing_batches.received_quantity = item.quantity;
                                    existing_batches.on_hand_quantity = item.quantity;
                                    existing_batches.unit_cost = item.unit_price;
                                    existing_batches.total_cost = item.quantity * item.unit_price;
                                    existing_batches.is_active = "T";
                                    existing_batches.reference_doc = im_Purchase_Listing.supplier_invoice_no;
                                    existing_batches.notes = im_ItemBatches.notes;
                                    existing_batches.product_description = existing_batches.product_description;
                                    existing_batches.barcode = existing_purchase.barcode;
                                    existing_batches.sku = existing_purchase.sku;
                                    _context.im_itemBatches.Update(existing_batches);
                                    await _context.SaveChangesAsync();
                                }

                            }
                        }

                        //_context.im_purchase_listing.Add(item);
                    }
                    _context.im_purchase_listing.Update(existing_im_purchase);
                    await _context.SaveChangesAsync();
                    var im_purchase = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_im_purchase.listing_id).ToListAsync();
                    if (im_purchase.Any())
                    {
                        foreach (var purchase in im_purchase)
                        {
                            other_expense += Convert.ToDecimal(purchase.other_expenses);
                            sub_total += Convert.ToDecimal(purchase.line_total);


                        }
                    }
                    existing_im_purchase.sub_total = Convert.ToDecimal(sub_total);
                    existing_im_purchase.doc_total = Convert.ToDecimal(sub_total) - im_Purchase_Listing.discount_amount + im_Purchase_Listing.tax_amount + im_Purchase_Listing.freight_amount + other_expense + im_Purchase_Listing.plastic_bag * im_Purchase_Listing.exchange_rate;
                    _context.im_purchase_listing.Update(existing_im_purchase);
                    await _context.SaveChangesAsync();

                    return new ServiceResult<im_purchase_listing>
                    {
                        Success = true,
                        Status = 1,
                        Message = "Data saved successfully",
                        Data = existing_im_purchase
                    };
                }

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
            catch (Exception ex)
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
            catch (Exception ex)
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
                Decimal sub_total = 0;
                Decimal other_expense = 0;
                var existing_purchase = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);

                //Delete
                var exist_deatils = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_purchase.listing_id).ToListAsync();
                var new_deatil_id = im_Purchase_Listing.im_purchase_listing_details.Select(id => id.detail_id).ToList();
                var delete_deatils = exist_deatils.Where(a => !new_deatil_id.Contains(a.detail_id)).ToList();
                if (delete_deatils.Any())
                {
                    _context.im_purchase_listing_details.RemoveRange(delete_deatils);

                    var deletedProductIds = delete_deatils.Select(d => d.detail_id).Distinct().ToList();
                    var temp_variants = await _context.temp_im_variants.Where(tv => deletedProductIds.Contains(tv.detail_id)).ToListAsync();

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
                    existing_purchase.edited_date_time = DateTime.Now;
                    existing_purchase.notes = im_Purchase_Listing.notes;
                    existing_purchase.local_referance = im_Purchase_Listing.local_referance;
                    existing_purchase.status = im_Purchase_Listing.status;
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
                            existing_im_purchase_listing_details.line_total = item.quantity * item.unit_price;
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
                    _context.im_purchase_listing.Update(existing_purchase);
                    await _context.SaveChangesAsync();

                    var im_purchase = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_purchase.listing_id).ToListAsync();
                    if (im_purchase.Any())
                    {
                        foreach (var purchase in im_purchase)
                        {
                            other_expense += Convert.ToDecimal(purchase.other_expenses);

                            sub_total += Convert.ToDecimal(purchase.line_total);

                        }
                    }

                    existing_purchase.sub_total = Convert.ToDecimal(sub_total);
                    existing_purchase.doc_total = (Convert.ToDecimal(sub_total) - existing_purchase.discount_amount + existing_purchase.tax_amount + existing_purchase.freight_amount + other_expense + existing_purchase.plastic_bag * existing_purchase.exchange_rate);
                    _context.im_purchase_listing.Update(existing_purchase);

                    if(existing_purchase.status== "Success")
                    {
                        var spResults = _context.Database.SqlQueryRaw<string>(
                            "EXEC dbo.sp_UpdateVariantCosts @listing_id=@listing_id ,@site_id=@site_id",
                            new SqlParameter("@listing_id", listing_id),
                            new SqlParameter("@site_id", existing_purchase.site_id)).AsEnumerable().ToList();
                        var spResponse = spResults.FirstOrDefault();


                        if(spResponse== "Success")
                        {
                            var temp_variant = exist_deatils.Where(d => d.is_varient == "T").Select(d => d.detail_id).ToList();

                            var temp_variant_results = _context.temp_im_variants
                                .AsEnumerable()
                                .Where(tv => temp_variant.Contains(tv.detail_id))
                                .ToList();

                            if (temp_variant_results.Any())
                            {
                                _context.temp_im_variants.RemoveRange(temp_variant_results);
                            }
                        }


                        
                    }
                    await _context.SaveChangesAsync();
                }

                return new ServiceResult<im_purchase_listing>
                {
                    Status = 201,
                    Success = true,
                    Message = "Success",
                    Data = existing_purchase
                };
            }
            catch (Exception ex)
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

        public async Task<ServiceResult<im_purchase_listing>> Update_purchase_calculation(Guid listing_id, im_purchase_listing im_Purchase_)
        {
            try
            {
                if (listing_id == null)
                {
                    _logger.LogInformation("Update_purchase_calculation Not found listing_id");
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Update_purchase_calculation Not found listing_id"
                    };
                }

                Decimal sub_total = 0;
                Decimal other_expense = 0;

                if (listing_id == null)
                {
                    _logger.LogInformation("Update_purchase_calculation Not found listing_id");
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Update_purchase_calculation Not found listing_id"
                    };
                }
                var existing_purchase = await _context.im_purchase_listing.FirstOrDefaultAsync(a => a.listing_id == listing_id);
                if (existing_purchase != null)
                {
                    existing_purchase.tax_amount = im_Purchase_.tax_amount;
                    existing_purchase.discount_amount = im_Purchase_.discount_amount;
                    existing_purchase.plastic_bag = im_Purchase_.plastic_bag;
                    existing_purchase.freight_amount = im_Purchase_.freight_amount;
                    _context.im_purchase_listing.Update(existing_purchase);
                    await _context.SaveChangesAsync();
                }
                var im_purchase = await _context.im_purchase_listing_details.Where(a => a.listing_id == listing_id).ToListAsync();
                if (im_purchase.Any())
                {
                    foreach (var purchase in im_purchase)
                    {
                        other_expense += Convert.ToDecimal(purchase.other_expenses);
                        sub_total += Convert.ToDecimal(purchase.line_total);
                      

                    }
                }
                existing_purchase.sub_total = Convert.ToDecimal(sub_total);
                existing_purchase.doc_total = Convert.ToDecimal(sub_total) - existing_purchase.discount_amount + existing_purchase.tax_amount + existing_purchase.freight_amount + other_expense * existing_purchase.exchange_rate + existing_purchase.plastic_bag;
                _context.im_purchase_listing.Update(existing_purchase);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 200,
                    Success = true,
                    Data = existing_purchase
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error While Update_purchase_calculation");
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<im_bin_location>> Add_bin_No(im_bin_location im_Bin_Location)
        {
            try
            {
                if (im_Bin_Location == null)
                {
                    _logger.LogInformation("Add_bin_No No data found");
                    return new ServiceResult<im_bin_location>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Add_bin_No No data found"
                    };
                }
                var existing_bin_location = await _context.im_bin_locations.Where(a => a.bin_code == im_Bin_Location.bin_code).ToListAsync();
                if (existing_bin_location.Any())
                {
                    return new ServiceResult<im_bin_location>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Bin Code already exists"
                    };
                }

                im_Bin_Location.bin_location_id = Guid.CreateVersion7();
                im_Bin_Location.store_id = im_Bin_Location.store_id;
                im_Bin_Location.company_id = im_Bin_Location.company_id;
                im_Bin_Location.bin_code = im_Bin_Location.bin_code;
                _context.im_bin_locations.Add(im_Bin_Location);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_bin_location>
                {
                    Status = 201,
                    Success = true,
                    Message = "Success",
                    Data = im_Bin_Location
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error While Add_bin_No");
                return new ServiceResult<im_bin_location>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<List<im_bin_location>>> Get_bin_Locations(Guid store_id)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No id found store_id");
                    return new ServiceResult<List<im_bin_location>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Not found ID"
                    };
                }
                var bin_locations = await _context.im_bin_locations.Where(a => a.store_id == store_id).ToListAsync();
                if (bin_locations.Count == 0)
                {
                    _logger.LogInformation("NO data found");
                    return new ServiceResult<List<im_bin_location>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Not found"
                    };
                }
                return new ServiceResult<List<im_bin_location>>
                {
                    Status = 200,
                    Success = true,
                    Data = bin_locations,
                    Message = ""
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<im_bin_location>>
                {
                    Status = 500,
                    Success = false,
                };
            }
        }

        public async Task<ServiceResult<im_purchase_listing_details>> Delete_im_purchase_listing(Guid detail_id)
        {
            try
            {
                if (detail_id == null)
                {
                    _logger.LogInformation("Delete_im_purchase_listing Not found detail_id");
                    return new ServiceResult<im_purchase_listing_details>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Delete_im_purchase_listing Not found detail_id"
                    };
                }
                var existing_purchase_listing = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == detail_id);
                if (existing_purchase_listing != null)
                {
                    _context.im_purchase_listing_details.Remove(existing_purchase_listing);
                    await _context.SaveChangesAsync();
                }
                var existing_batch = await _context.im_itemBatches.Where(a => a.detail_id == detail_id).ToListAsync();
                if(existing_batch.Any())
                {
                    _context.im_itemBatches.RemoveRange(existing_batch);
                    await _context.SaveChangesAsync();
                }
                var existing_temp_variant = await _context.temp_im_variants.Where(a => a.detail_id == detail_id).ToListAsync();
                if(existing_temp_variant.Any())
                {
                    _context.temp_im_variants.RemoveRange(existing_temp_variant);
                    await _context.SaveChangesAsync();
                }
                return new ServiceResult<im_purchase_listing_details>
                {
                    Status = 200,
                    Success = true,
                    Message = "Deleted successfully",
                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error While Delete_im_purchase_listing");
                return new ServiceResult<im_purchase_listing_details>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }

        public async Task<ServiceResult<im_purchase_listing>> Delete_purchase(Guid listing_id)
        {
            try
            {
                if (listing_id == null)
                {
                    _logger.LogInformation("Delete_purchase Not found listing_id");
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Delete_purchase Not found listing_id"
                    };
                }
                var existing_purchase = await _context.im_purchase_listing.FirstOrDefaultAsync(a => a.listing_id == listing_id);
                if (existing_purchase != null)
                {

                    var purchase_details = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_purchase.listing_id).ToListAsync();
                    var detailIds = purchase_details.Select(d => d.detail_id).ToList();
                    if (purchase_details.Any())
                    {
                        _context.im_purchase_listing_details.RemoveRange(purchase_details);
                    }
                    var item_batches = await _context.im_itemBatches.Where(a => detailIds.Contains(a.detail_id)).ToListAsync();
                    if (item_batches.Any())
                    {
                        _context.im_itemBatches.RemoveRange(item_batches);
                    }
                    var temp_variants = await _context.temp_im_variants.Where(a => detailIds.Contains(a.detail_id)).ToListAsync();
                    if (temp_variants.Any())
                    {
                        _context.temp_im_variants.RemoveRange(temp_variants);
                    }
                    _context.im_purchase_listing.Remove(existing_purchase);
                    await _context.SaveChangesAsync();
                }
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 200,
                    Success = true,
                    Message = "Deleted successfully",
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error While Delete_purchase");
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<im_ItemBatches>>> get_batches(Guid store_id)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<List<im_ItemBatches>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                var items = await _context.Set<im_ItemBatches>()
            .FromSqlRaw(
                "EXEC dbo.im_batches @store_id=@store_id, @opr=@opr",
                new SqlParameter("@store_id", store_id),
                new SqlParameter("@opr", 1)
            )
            .AsNoTracking()
            .ToListAsync();
                if (items == null)
                {
                    _logger.LogInformation("No data found im_batches");
                    return new ServiceResult<List<im_ItemBatches>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found im_batches"
                    };
                }
                return new ServiceResult<List<im_ItemBatches>>
                {
                    Success = true,
                    Status = 200,
                    Data = items
                };

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while get_batches ");
                return new ServiceResult<List<im_ItemBatches>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }
        public async Task<ServiceResult<List<im_ItemBatches>>> get_batches_search(Guid store_id, string searchText)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<List<im_ItemBatches>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                var items = await _context.Set<im_ItemBatches>()
            .FromSqlRaw(
                "EXEC dbo.im_batches @store_id=@store_id, @opr=@opr,@searchText=@searchText ",
                new SqlParameter("@store_id", store_id),
                new SqlParameter("@searchText", searchText),
                new SqlParameter("@opr", 2)
            )
            .AsNoTracking()
            .ToListAsync();
                if (items == null)
                {
                    _logger.LogInformation("No data found im_batches");
                    return new ServiceResult<List<im_ItemBatches>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found im_batches"
                    };
                }
                return new ServiceResult<List<im_ItemBatches>>
                {
                    Success = true,
                    Status = 200,
                    Data = items
                };

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while get_batches ");
                return new ServiceResult<List<im_ItemBatches>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }
        public async Task<ServiceResult<im_ItemBatches>> Get_item_batch(Guid item_batch_id)
        {
            try
            {
                if (item_batch_id == null)
                {
                    _logger.LogInformation("No item_batch_id found");
                    return new ServiceResult<im_ItemBatches>
                    {
                        Status = 400,
                        Success = false,
                        Message = "NO item_batch_id found"
                    };
                }

                var item = (await _context.Set<im_ItemBatches>()
                        .FromSqlRaw(
                                    "EXEC dbo.im_batches @item_batch_id=@item_batch_id, @opr=@opr",
                        new SqlParameter("@item_batch_id", item_batch_id),
                         new SqlParameter("@opr", 3))
                        .AsNoTracking()
                        .ToListAsync())
                        .FirstOrDefault();

                if (item == null)
                {
                    return new ServiceResult<im_ItemBatches>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<im_ItemBatches>
                {
                    Success = true,
                    Status = 200,
                    Data = item
                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while Get_item_batch");
                return new ServiceResult<im_ItemBatches>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
            
        }

        public async Task<ServiceResult<im_ItemBatches>> update_item_batch(Guid item_batch_id,im_ItemBatches im_ItemBatches)
        {
            try
            {
                if (item_batch_id == null)
                {
                    _logger.LogInformation("item_batch_id was not found");
                    return new ServiceResult<im_ItemBatches>
                    {
                        Status = 400,
                        Success = false,
                        Message = "item_batch_id was not found"
                    };
                }
                var existing_item = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.item_batch_id == item_batch_id);
                if (existing_item != null)
                {
                    existing_item.expiry_date = im_ItemBatches.expiry_date;
                    existing_item.batch_number = im_ItemBatches.batch_number;
                    existing_item.batch_promo_price = im_ItemBatches.batch_promo_price;
                    existing_item.promo_from_date = im_ItemBatches.promo_from_date;
                    existing_item.promo_to_date = im_ItemBatches.promo_to_date;
                    existing_item.batch_on_hold = im_ItemBatches.batch_on_hold;
                    _context.im_itemBatches.Update(existing_item);
                    await _context.SaveChangesAsync();
                }
                return new ServiceResult<im_ItemBatches>
                {
                    Status = 200,
                    Success = true,
                    Message = "Updated",
                    Data = existing_item
                };

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error While update_item_batch");
                return new ServiceResult<im_ItemBatches>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
            
        }
        public async Task<ServiceResult<im_purchase_listing>> Add_purchase_listing_excel(im_purchase_listing im_Purchase_Listing)
        {

            try
            {
                if (im_Purchase_Listing == null)
                {
                    _logger.LogInformation("NO data found to insert");
                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found to insert"
                    };
                }
                Decimal sub_total = 0;
                Decimal other_expense = 0;

                var table_2 = "im_purchase_listing";
                var table_key_2 = await _context.am_table_next_key.FindAsync(table_2);
                var key_2 = Convert.ToInt16(table_key_2.next_key);
                var year = DateTime.Now.Year;

                im_Purchase_Listing.listing_id = Guid.CreateVersion7();
                im_Purchase_Listing.listing_code = im_Purchase_Listing.listing_code + year + "-" + Convert.ToString(key_2 + 1);
                im_Purchase_Listing.vendor_id = im_Purchase_Listing.vendor_id;
                im_Purchase_Listing.site_id = im_Purchase_Listing.site_id;
                im_Purchase_Listing.payment_mode = im_Purchase_Listing.payment_mode;
                im_Purchase_Listing.purchase_type = im_Purchase_Listing.purchase_type;
                im_Purchase_Listing.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                im_Purchase_Listing.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                im_Purchase_Listing.currency_code = im_Purchase_Listing.currency_code;
                im_Purchase_Listing.exchange_rate = im_Purchase_Listing.exchange_rate;
                im_Purchase_Listing.discount_amount = im_Purchase_Listing.discount_amount;
                im_Purchase_Listing.freight_amount = im_Purchase_Listing.freight_amount;
                im_Purchase_Listing.tax_amount = im_Purchase_Listing.tax_amount;
                im_Purchase_Listing.other_expenses = im_Purchase_Listing.other_expenses;
                im_Purchase_Listing.local_referance = im_Purchase_Listing.local_referance;
                im_Purchase_Listing.plastic_bag = im_Purchase_Listing.plastic_bag;
                im_Purchase_Listing.status = im_Purchase_Listing.status;
                im_Purchase_Listing.notes = im_Purchase_Listing.notes;
                im_Purchase_Listing.tax_amount = im_Purchase_Listing.tax_amount;
                foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                {
                    var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.sku == item.sku);
                    var im_store_varient = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.variant_id == im_varient.variant_id && a.store_id == im_Purchase_Listing.site_id);

                    item.detail_id = Guid.CreateVersion7();
                    item.listing_id = im_Purchase_Listing.listing_id;
                    item.product_id = im_varient.product_id;
                    item.sub_variant_id = im_varient.variant_id;
                    item.quantity = item.quantity;
                    item.unit_price = item.unit_price;
                    item.discount_amount = item.discount_amount;
                    item.tax_amount = item.tax_amount;
                    item.freight_amount = item.freight_amount;
                    item.other_expenses = item.other_expenses;
                    item.line_total = item.quantity * item.unit_price;

                    item.notes = item.notes;
                    item.varient_quantity = item.varient_quantity;
                    item.batch_no = item.batch_no;
                    item.bin_no = item.bin_no;
                    item.uom_name = item.uom_name;
                    item.barcode = item.barcode;
                    item.sku = item.sku;
                    item.product_description = item.product_description;
                    item.store_variant_inventory_id = im_store_varient.store_variant_inventory_id;

                    other_expense += Convert.ToDecimal(item.other_expenses);
                    sub_total += Convert.ToDecimal(item.line_total);
                    _context.im_purchase_listing_details.Add(item);

                }
                im_Purchase_Listing.sub_total = Convert.ToDecimal(sub_total);
                im_Purchase_Listing.doc_total = Convert.ToDecimal(sub_total) - im_Purchase_Listing.discount_amount + im_Purchase_Listing.tax_amount + im_Purchase_Listing.freight_amount + other_expense + im_Purchase_Listing.plastic_bag * im_Purchase_Listing.exchange_rate;

                table_key_2.next_key = key_2 + 1;
                _context.am_table_next_key.Update(table_key_2);
                 _context.im_purchase_listing.Add(im_Purchase_Listing);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 201,
                    Success = true,
                    Message = "Inserted",
                    Data = im_Purchase_Listing

                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro while Add_purchase_listing_excel");
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
