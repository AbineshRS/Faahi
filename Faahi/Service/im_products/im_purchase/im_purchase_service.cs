using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.Purchase_dto;
using Faahi.Dto.sales_dto;
using Faahi.Model.im_products;
using Faahi.Model.temp_tables;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
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
            var transaction = await _context.Database.BeginTransactionAsync();

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



                    var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);

                    var table_2 = "im_purchase_listing";
                    var table_key_2 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table_2 && a.business_id == im_site.company_id);
                    var key_2 = Convert.ToInt16(table_key_2.next_key);

                    var table_3 = "im_purchase_listing_details";
                    var table_key_3 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table_3 && a.business_id == im_site.company_id);
                    var key_3 = Convert.ToInt16(table_key_3.next_key);
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
                    im_Purchase_Listing.gst = im_Purchase_Listing.gst;
                    im_Purchase_Listing.notes = im_Purchase_Listing.notes;
                    im_Purchase_Listing.status = im_Purchase_Listing.status;
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        item.detail_id = Guid.CreateVersion7();
                        item.listing_id = im_Purchase_Listing.listing_id;
                        item.detail_code = "DT" + year + "-" + Convert.ToString(key_3 + 1);
                        item.listing_code = im_Purchase_Listing.listing_code;
                        item.product_id = item.product_id;
                        item.sub_variant_id = item.sub_variant_id;
                        item.store_variant_inventory_id = item.store_variant_inventory_id;
                        item.uom_name = item.uom_name;
                        item.unit_price = item.unit_price;
                        item.Product_title = item.Product_title;
                        item.Product_Brand = item.Product_Brand;
                        item.discount_amount = item.discount_amount ?? 0;
                        item.tax_amount = item.tax_amount;
                        item.barcode = item.barcode;
                        item.freight_amount = item.freight_amount;
                        item.other_expenses = item.other_expenses;
                        item.selling_price = item.selling_price;
                        other_expense += Convert.ToDecimal(item.other_expenses);
                        item.line_total = item.quantity * item.unit_price;
                        sub_total += Convert.ToDecimal(item.line_total);
                        //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                        item.notes = item.notes;
                        item.expiry_date = item.expiry_date;
                        item.quantity = item.quantity;
                        item.variant_qty = item.variant_qty;

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
                            im_ItemBatches.product_description = item.Product_title;

                            im_ItemBatches.created_at = DateTime.Now;
                            if (table_key != null)
                            {
                                table_key.next_key = key + 1;
                                _context.super_abi.Update(table_key);
                                await _context.SaveChangesAsync();
                            }
                            _context.im_itemBatches.Add(im_ItemBatches);
                        }
                        if (table_3 != null)
                        {
                            table_key_3.next_key = key_3 + 1;
                            _context.am_table_next_key.Update(table_key_3);
                            await _context.SaveChangesAsync();
                        }
                    }
                    decimal taxableAmount = sub_total - (im_Purchase_Listing.discount_amount ?? 0) + (im_Purchase_Listing.freight_amount ?? 0) + other_expense + ((im_Purchase_Listing.plastic_bag ?? 0) * (im_Purchase_Listing.exchange_rate ?? 1));
                    decimal gstPercent = Convert.ToDecimal(im_Purchase_Listing.tax_amount ?? 0);
                    decimal gstAmount = taxableAmount * gstPercent / 100;
                    im_Purchase_Listing.tax_amount = gstAmount;
                    im_Purchase_Listing.sub_total = Convert.ToDecimal(sub_total);
                    im_Purchase_Listing.doc_total = taxableAmount + gstAmount;
                    _context.im_purchase_listing.Add(im_Purchase_Listing);


                    table_key_2.next_key = key_2 + 1;
                    _context.am_table_next_key.Update(table_key_2);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

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
                    var year = DateTime.Now.Year;

                    var im_purchase_sub_total = await _context.im_purchase_listing.FirstOrDefaultAsync(a => a.listing_id == existing_im_purchase.listing_id);
                    if (im_purchase_sub_total != null)
                    {
                        im_purchase_sub_total.sub_total = 0;
                        im_purchase_sub_total.doc_total = 0;
                        _context.im_purchase_listing.Update(im_purchase_sub_total);
                        await _context.SaveChangesAsync();
                    }
                    var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);

                    var table_3 = "im_purchase_listing_details";
                    var table_key_3 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table_3 && a.business_id == im_site.company_id);
                    var key_3 = Convert.ToInt16(table_key_3.next_key);

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
                    existing_im_purchase.gst = im_Purchase_Listing.gst;
                    existing_im_purchase.notes = im_Purchase_Listing.notes;
                    existing_im_purchase.status = im_Purchase_Listing.status;
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        var existing_purchase = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                        if (existing_purchase == null)
                        {
                            item.detail_id = Guid.CreateVersion7();
                            item.detail_code= "DT" + year + "-" + Convert.ToString(key_3 + 1);
                            item.listing_code = existing_im_purchase.listing_code;
                            item.listing_id = im_Purchase_Listing.listing_id;
                            item.product_id = item.product_id;
                            item.sub_variant_id = item.sub_variant_id;
                            item.store_variant_inventory_id = item.store_variant_inventory_id;
                            item.uom_name = item.uom_name;
                            item.barcode = item.barcode;
                            item.unit_price = item.unit_price;
                            item.discount_amount = item.discount_amount ?? 0;
                            item.tax_amount = item.tax_amount;
                            item.freight_amount = item.freight_amount;
                            item.other_expenses = item.other_expenses;
                            item.expiry_date = item.expiry_date;
                            item.Product_title = item.Product_title;
                            item.Product_Brand = item.Product_Brand;
                            item.selling_price = item.selling_price;
                            item.variant_qty = item.variant_qty;
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
                                im_ItemBatches.product_description = item.Product_title;
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
                            if (table_3 != null)
                            {
                                table_key_3.next_key = key_3 + 1;
                                _context.am_table_next_key.Update(table_key_3);
                                await _context.SaveChangesAsync();
                            }

                        }
                        else
                        {

                            existing_purchase.uom_name = item.uom_name;
                            existing_purchase.unit_price = item.unit_price;
                            existing_purchase.discount_amount = item.discount_amount;
                            existing_purchase.tax_amount = item.tax_amount;
                            existing_purchase.freight_amount = item.freight_amount;
                            existing_purchase.batch_no = item.batch_no;

                            existing_purchase.Product_title = item.Product_title;
                            existing_purchase.Product_Brand = item.Product_Brand;
                            existing_purchase.other_expenses = item.other_expenses;
                            other_expense += Convert.ToDecimal(existing_purchase.other_expenses);

                            existing_purchase.variant_qty = item.variant_qty;
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
                                    //existing_batches.sku = existing_purchase.sku;
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
                    decimal taxableAmount = sub_total - (im_Purchase_Listing.discount_amount ?? 0) + (im_Purchase_Listing.freight_amount ?? 0) + other_expense + ((im_Purchase_Listing.plastic_bag ?? 0) * (im_Purchase_Listing.exchange_rate ?? 1));
                    decimal gstPercent = Convert.ToDecimal(im_Purchase_Listing.tax_amount ?? 0);
                    decimal gstAmount = taxableAmount * gstPercent / 100;
                    existing_im_purchase.tax_amount = gstAmount;
                    existing_im_purchase.sub_total = Convert.ToDecimal(sub_total);
                    existing_im_purchase.doc_total = taxableAmount + gstAmount;
                    _context.im_purchase_listing.Update(existing_im_purchase);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
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
                await transaction.RollbackAsync();

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

        public async Task<ServiceResult<temp_im_purchase_listing_details>> Update_temp_discount(Guid listing_id)
        {
            try
            {
                if (listing_id == null)
                {
                    return new ServiceResult<temp_im_purchase_listing_details>
                    {
                        Success = false,
                        Status = 300
                    };
                }
                var existing_list = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);

                foreach (var item in existing_list.im_purchase_listing_details)
                {
                    Decimal? line_discount_percent = 0;
                    Decimal? line_discount_split = 0;
                    Decimal? line_netamount = 0;

                    var existing_detailse = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                    var temp_deatilse = await _context.temp_Im_Purchase_Listing_Details.Where(a => a.detail_id == item.detail_id).ToListAsync();
                    foreach (var temp_item in temp_deatilse)
                    {

                        var temp_deatil = await _context.temp_Im_Purchase_Listing_Details.FirstOrDefaultAsync(a => a.temp_detail_id == temp_item.temp_detail_id);
                        temp_deatil.discount_amount = existing_detailse.discount_amount;
                        temp_deatil.listing_id = existing_detailse.listing_id;

                        line_discount_percent = temp_deatil.line_total / existing_list.sub_total * existing_detailse.discount_amount ?? 0;
                        line_discount_split = line_discount_percent / temp_deatil.quantity;
                        line_netamount = temp_deatil.unit_price - line_discount_split;
                        temp_deatil.line_net_cost = line_netamount;
                        temp_deatil.line_unit_total = temp_deatil.line_net_cost * temp_deatil.quantity;
                        _context.temp_Im_Purchase_Listing_Details.Update(temp_deatil);
                        await _context.SaveChangesAsync();

                        line_discount_percent = 0;
                        line_discount_split = 0;
                        line_netamount = 0;
                    }
                }
                return new ServiceResult<temp_im_purchase_listing_details>
                {
                    Status = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<temp_im_purchase_listing_details>
                {
                    Success = false,
                    Status = 500
                };
            }
        }

        public async Task<ServiceResult<im_purchase_listing>> Calculate_discount_avg(Guid listing_id)
        {
            try
            {
                if (listing_id == null)
                {
                    return new ServiceResult<im_purchase_listing>
                    {
                        Success = false,
                        Status = 300
                    };
                }
                var existing_list = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
                Decimal? line_discount_percent = 0;
                Decimal? line_discount_split = 0;
                Decimal? line_netamount = 0;
                foreach (var item in existing_list.im_purchase_listing_details)
                {

                    var existing_detailse = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                    line_discount_percent = existing_detailse.line_total / existing_list.sub_total * existing_detailse.discount_amount ?? 0;
                    line_discount_split = line_discount_percent / existing_detailse.quantity;
                    line_netamount = existing_detailse.unit_price - line_discount_split;
                    existing_detailse.line_net_cost = line_netamount;
                    existing_detailse.line_unit_total = existing_detailse.line_net_cost * existing_detailse.quantity;
                    _context.im_purchase_listing_details.Update(existing_detailse);
                    await _context.SaveChangesAsync();
                    line_discount_percent = 0;
                    line_discount_split = 0;
                    line_netamount = 0;


                }
                return new ServiceResult<im_purchase_listing>
                {
                    Success = true,
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<im_purchase_listing>
                {
                    Success = false,
                    Message = ex.Message,
                    Status = 500
                };
            }

        }

        public async Task<ServiceResult<List<PurchaseListDto>>> Purchase_list(Guid site_id)
        {
            try
            {
                if (site_id == null)
                {
                    _logger.LogInformation("No id found site_id");
                    return new ServiceResult<List<PurchaseListDto>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Not found ID"
                    };
                }
                var purchase_list = await _context.Database.SqlQueryRaw<PurchaseListDto>(
                    "EXEC dbo.sp_Purchase @opr=@opr,@store_id=@store_id",
                    new SqlParameter("@opr", 1),
                    new SqlParameter("@store_id", site_id)
                    ).ToListAsync();
                //var purchase_list = await _context.im_purchase_listing.Where(a => a.site_id == site_id).ToListAsync();
                if (purchase_list.Count == 0)
                {
                    _logger.LogInformation("NO data found");
                    return new ServiceResult<List<PurchaseListDto>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Not found"
                    };
                }
                return new ServiceResult<List<PurchaseListDto>>
                {
                    Status = 200,
                    Success = true,
                    Data = purchase_list,
                    Message = ""

                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<PurchaseListDto>>
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
                var json_list = await _context.Database.SqlQueryRaw<string>(
                                "EXEC dbo.sp_Purchase @opr=@opr,@list_id=@list_id",
                                 new SqlParameter("@opr", 2),
                                 new SqlParameter("@list_id", listing_id)
                             )
                             .ToListAsync();

                var json_result = string.Join("", json_list);

                var listing_data = JsonConvert.DeserializeObject<im_purchase_listing>(json_result);
                //var listing_data = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
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

        public async Task<ServiceResult<List<temp_im_purchase_listing_details>>> temp_im_purchase_details(Guid listing_id)
        {
            try
            {
                if (listing_id == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<List<temp_im_purchase_listing_details>>
                    {
                        Status = 400,
                        Success = false,
                    };
                }
                var json_list = await _context.Database.SqlQueryRaw<temp_im_purchase_listing_details>(
                        "EXEC dbo.sp_Purchase @opr=@opr,@list_id=@list_id",
                         new SqlParameter("@opr", 3),
                         new SqlParameter("@list_id", listing_id)
                     )
                     .ToListAsync();

                //var json_result = string.Join("", json_list);

                //var listing_data = JsonConvert.DeserializeObject<List<temp_im_purchase_listing_details>>(json_result);
                //var listing_data = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
                if (json_list == null)
                {
                    return new ServiceResult<List<temp_im_purchase_listing_details>>
                    {
                        Status = 400,
                        Success = false,
                    };
                }
                return new ServiceResult<List<temp_im_purchase_listing_details>>
                {
                    Status = 200,
                    Success = true,
                    Data = json_list,

                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<temp_im_purchase_listing_details>>
                {
                    Status = 500,
                    Success = false,

                };
            }


        }

        public async Task<ServiceResult<im_purchase_listing>> Update_purchase(Guid listing_id, im_purchase_listing im_Purchase_Listing)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
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
                var year = DateTime.Now.Year;

                var existing_purchase = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);

                //Delete
                var exist_deatils = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_purchase.listing_id).ToListAsync();
                var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);

                var table_3 = "im_purchase_listing_details";
                var table_key_3 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table_3 && a.business_id == im_site.company_id);
                var key_3 = Convert.ToInt16(table_key_3.next_key);

                if (existing_purchase != null)
                {

                    existing_purchase.edit_user_id = im_Purchase_Listing.edit_user_id;
                    existing_purchase.vendor_id = im_Purchase_Listing.vendor_id;
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
                    existing_purchase.created_at = existing_purchase.created_at;
                    existing_purchase.notes = im_Purchase_Listing.notes;
                    existing_purchase.local_referance = im_Purchase_Listing.local_referance;
                    existing_purchase.status = im_Purchase_Listing.status;
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        var existing_im_purchase_listing_details = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                        if (existing_im_purchase_listing_details != null)
                        {
                            var im_ProductVariants = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.product_id == item.product_id);
                            existing_im_purchase_listing_details.product_id = item.product_id;
                            //existing_im_purchase_listing_details.sub_variant_id = item.sub_variant_id;
                            existing_im_purchase_listing_details.uom_name = item.uom_name;
                            //existing_im_purchase_listing_details.Product_title = item.Product_title;
                            //existing_im_purchase_listing_details.Product_Brand = item.Product_Brand;
                            existing_im_purchase_listing_details.unit_price = item.unit_price;
                            existing_im_purchase_listing_details.discount_amount = item.discount_amount ?? 0;
                            existing_im_purchase_listing_details.tax_amount = item.tax_amount;
                            if (existing_im_purchase_listing_details.sku == null || existing_im_purchase_listing_details.sku == "")
                            {
                                existing_im_purchase_listing_details.sku = im_ProductVariants?.sku;
                            }
                            existing_im_purchase_listing_details.freight_amount = item.freight_amount;
                            existing_im_purchase_listing_details.other_expenses = item.other_expenses;
                            existing_im_purchase_listing_details.line_total = item.quantity * item.unit_price;
                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            existing_im_purchase_listing_details.notes = item.notes;
                            existing_im_purchase_listing_details.expiry_date = item.expiry_date;
                            existing_im_purchase_listing_details.quantity = item.quantity;
                            existing_im_purchase_listing_details.variant_qty = item.variant_qty;
                            existing_im_purchase_listing_details.selling_price = item.selling_price;
                            _context.im_purchase_listing_details.Update(existing_im_purchase_listing_details);
                        }
                        else
                        {
                            var im_ProductVariants = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.product_id == existing_im_purchase_listing_details.product_id);

                            item.detail_id = Guid.CreateVersion7();
                            item.listing_id = im_Purchase_Listing.listing_id;
                            item.listing_code = existing_purchase.listing_code;
                            item.detail_code= "DT" + year + "-" + Convert.ToString(key_3 + 1);
                            item.product_id = item.product_id;
                            item.sub_variant_id = item.sub_variant_id;
                            item.uom_name = item.uom_name;
                            item.Product_title = item.Product_title;
                            item.Product_Brand = item.Product_Brand;
                            item.unit_price = item.unit_price;
                            item.discount_amount = item.discount_amount;
                            item.tax_amount = item.tax_amount;
                            item.freight_amount = item.freight_amount;
                            item.other_expenses = item.other_expenses;
                            item.line_total = item.quantity * item.unit_price;
                            if (existing_im_purchase_listing_details.sku == null || existing_im_purchase_listing_details.sku == "")
                            {
                                existing_im_purchase_listing_details.sku = im_ProductVariants?.sku;
                            }
                            //item.line_total = (item.quantity - item.unit_price) - item.discount_amount + item.tax_amount + item.freight_amount + item.other_expenses;
                            item.notes = item.notes;
                            item.expiry_date = item.expiry_date;
                            item.quantity = item.quantity;
                            _context.im_purchase_listing_details.Add(item);
                            existing_purchase.im_purchase_listing_details.Add(item);

                            if (table_3 != null)
                            {
                                table_key_3.next_key = key_3 + 1;
                                _context.am_table_next_key.Update(table_key_3);
                                await _context.SaveChangesAsync();
                            }
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

                    decimal taxableAmount = sub_total - (im_Purchase_Listing.discount_amount ?? 0) + (im_Purchase_Listing.freight_amount ?? 0) + other_expense + ((im_Purchase_Listing.plastic_bag ?? 0) * (im_Purchase_Listing.exchange_rate ?? 1));
                    decimal gstPercent = Convert.ToDecimal(im_Purchase_Listing.tax_amount ?? 0);
                    decimal gstAmount = taxableAmount * gstPercent / 100;
                    existing_purchase.tax_amount = gstAmount;
                    existing_purchase.sub_total = Convert.ToDecimal(sub_total);
                    existing_purchase.doc_total = taxableAmount + gstAmount;
                    _context.im_purchase_listing.Update(existing_purchase);

                    if (existing_purchase.status == "Success")
                    {
                        var im_purchase_list = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_purchase.listing_id).ToListAsync();
                        List<im_InventoryTransactions> im_InventoryTransactions1 = new List<im_InventoryTransactions>();
                        foreach (var item in im_purchase_list)
                        {
                            var im_InventoryTransactions = new im_InventoryTransactions()
                            {
                                listing_id = item.listing_id,
                                store_id = existing_purchase.site_id,
                                variant_id = item.sub_variant_id,
                                trans_type = "PURCHASE",
                                quantity_change = item.quantity,
                                unit_cost = item.unit_price,
                                total_cost = item.line_total,
                                created_date_time = DateTime.Now
                            };

                            im_InventoryTransactions1.Add(im_InventoryTransactions);

                        }
                        _context.im_InventoryTransactions.AddRange(im_InventoryTransactions1);
                        await _context.SaveChangesAsync();

                        var temp_calult = await Update_temp_discount(listing_id);
                        if (!temp_calult.Success)
                        {
                            await transaction.RollbackAsync();

                            return new ServiceResult<im_purchase_listing>
                            {
                                Status = 500,
                                Success = false,
                                Message = temp_calult.Message
                            };
                        }
                        var calculation_discount = await Calculate_discount_avg(listing_id);
                        if (!calculation_discount.Success)
                        {
                            await transaction.RollbackAsync();

                            return new ServiceResult<im_purchase_listing>
                            {
                                Status = 500,
                                Success = false,
                                Message = calculation_discount.Message
                            };
                        }
                        //
                        var goodsHeaderResult = await Add_GoodsHeader(listing_id);
                        if (!goodsHeaderResult.Success)
                        {
                            await transaction.RollbackAsync();

                            return new ServiceResult<im_purchase_listing>
                            {
                                Status = 500,
                                Success = false,
                                Message = goodsHeaderResult.Message
                            };
                        }


                        var spResults = _context.Database.SqlQueryRaw<string>(
                            "EXEC dbo.sp_UpdateVariantCosts @listing_id=@listing_id ,@site_id=@site_id,@ope=@opr",
                            new SqlParameter("@listing_id", listing_id),
                            new SqlParameter("@site_id", existing_purchase.site_id),
                            new SqlParameter("@opr", 1)).AsEnumerable().ToList();
                        var spResponse = spResults.FirstOrDefault();


                        if (spResponse == "Success")
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
                    await transaction.CommitAsync();
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
                await transaction.RollbackAsync();

                _logger.LogInformation("Error While Update_purchase");
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<im_purchase_listing>> Calculate_subtotal(Guid listing_id)
        {
            if (listing_id == null)
            {
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 300,
                    Success = false,
                    Message = ""
                };
            }
            Decimal? sub_total = 0;

            var exising_list = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
            foreach (var item in exising_list.im_purchase_listing_details)
            {
                var im_purchase_deatils = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                sub_total += im_purchase_deatils.line_total;
            }
            exising_list.sub_total = sub_total;
            _context.im_purchase_listing.Update(exising_list);
            return new ServiceResult<im_purchase_listing>
            {
                Status = 200,
                Success = true
            };

        }

        public async Task<ServiceResult<im_purchase_listing_dto>> Update_purchase_return(Guid listing_id, im_purchase_listing_dto im_Purchase_Listing)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing_list = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
                foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                {
                    var existing_line = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                    var im_store = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == existing_line.store_variant_inventory_id);
                    if (existing_line != null)
                    {
                        existing_line.quantity = existing_line.quantity - item.return_quantity;
                        existing_line.line_total = existing_line.quantity * item.unit_price;

                        _context.im_purchase_listing_details.Update(existing_line);
                    }
                    if (im_store != null)
                    {
                        im_store.on_hand_quantity = im_store.on_hand_quantity - item.return_quantity;
                        _context.im_StoreVariantInventory.Update(im_store);
                    }


                    var existing_batches = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.detail_id == item.detail_id && a.variant_id == item.sub_variant_id && a.batch_number == item.batch_no);
                    if (existing_batches != null)
                    {
                        existing_batches.received_quantity = existing_batches.received_quantity - item.return_quantity;
                        _context.im_itemBatches.Update(existing_batches);
                    }
                }
                foreach (var temp_varient in im_Purchase_Listing.temp_Im_Purchase_Listing_Details)
                {
                    var existing_temp_varient = await _context.temp_Im_Purchase_Listing_Details.FirstOrDefaultAsync(a => a.temp_detail_id == temp_varient.temp_detail_id);
                    var purchase_details = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == existing_temp_varient.detail_id);
                    var im_store = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == existing_temp_varient.store_variant_inventory_id);
                    if (existing_temp_varient != null)
                    {
                        existing_temp_varient.quantity = existing_temp_varient.quantity - temp_varient.return_quantity;
                        _context.temp_Im_Purchase_Listing_Details.Update(existing_temp_varient);
                    }
                    if (purchase_details != null)
                    {
                        purchase_details.quantity = purchase_details.quantity - temp_varient.return_quantity;
                        _context.im_purchase_listing_details.Update(purchase_details);

                    }
                    if (im_store != null)
                    {
                        im_store.on_hand_quantity = im_store.on_hand_quantity - temp_varient.return_quantity;
                        _context.im_StoreVariantInventory.Update(im_store);

                    }
                }

                var sub_totoal = await Calculate_subtotal(listing_id);
                if (!sub_totoal.Success)
                {
                    await transaction.RollbackAsync();

                    return new ServiceResult<im_purchase_listing_dto>
                    {
                        Status = 500,
                        Success = false,
                        Message = sub_totoal.Message
                    };
                }
                var temp_calult = await Update_temp_discount(listing_id);
                if (!temp_calult.Success)
                {
                    await transaction.RollbackAsync();

                    return new ServiceResult<im_purchase_listing_dto>
                    {
                        Status = 500,
                        Success = false,
                        Message = temp_calult.Message
                    };
                }
                var calculation_discount = await Calculate_discount_avg(listing_id);
                if (!calculation_discount.Success)
                {
                    await transaction.RollbackAsync();

                    return new ServiceResult<im_purchase_listing_dto>
                    {
                        Status = 500,
                        Success = false,
                        Message = calculation_discount.Message
                    };
                }
                var spResults = _context.Database.SqlQueryRaw<string>(
                    "EXEC dbo.sp_UpdateVariantCosts @listing_id=@listing_id ,@site_id=@site_id,@opr=@opr",
                    new SqlParameter("@listing_id", listing_id),
                    new SqlParameter("@site_id", existing_list.site_id),
                    new SqlParameter("@opr", 2)).AsEnumerable().ToList();
                var spResponse = spResults.FirstOrDefault();
                if (spResponse != "Success")
                {
                    await transaction.RollbackAsync();

                    return new ServiceResult<im_purchase_listing_dto>
                    {
                        Status = 500,
                        Success = false,
                        Message = "Error"
                    };
                }
                var retuen_purchase = await Add_retuen_purchase(listing_id, im_Purchase_Listing);
                if (!retuen_purchase.Success)
                {
                    await transaction.RollbackAsync();

                    return new ServiceResult<im_purchase_listing_dto>
                    {
                        Status = 500,
                        Success = false,
                        Message = retuen_purchase.Message
                    };
                }
                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();
                return new ServiceResult<im_purchase_listing_dto>
                {
                    Status = 200,
                    Success = true,
                    Message = "Updated"
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Update_purchase_return");
                await transaction.RollbackAsync();

                return new ServiceResult<im_purchase_listing_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<im_GoodsReceiptHeaders>> Add_GoodsHeader(Guid listing_id)
        {
            //var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                if (listing_id == null)
                {
                    return new ServiceResult<im_GoodsReceiptHeaders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No Add_GoodsHeader listing_id found"
                    };
                }
                var year = DateTime.Now.Year;

                List<im_GoodsReceiptLines> im_GoodsReceiptLines1 = new List<im_GoodsReceiptLines>();
                im_GoodsReceiptHeaders im_GoodsReceiptHeaders = new im_GoodsReceiptHeaders();
                var exisitng_purchase = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
                var ap_vendor = await _context.ap_Vendors.FirstOrDefaultAsync(a => a.vendor_id == exisitng_purchase.vendor_id);
                var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == exisitng_purchase.site_id);
                var table_3 = "im_purchase_listing_details";
                var table_key_3 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table_3 && a.business_id == st_store.company_id);
                var key_3 = Convert.ToInt16(table_key_3.next_key);
                im_GoodsReceiptHeaders.business_id = st_store?.company_id ?? Guid.Empty;///
                im_GoodsReceiptHeaders.store_id = exisitng_purchase?.site_id ?? Guid.Empty;
                im_GoodsReceiptHeaders.supplier_id = exisitng_purchase?.vendor_id;
                im_GoodsReceiptHeaders.purchase_order_id = exisitng_purchase.listing_id;
                im_GoodsReceiptHeaders.purchase_entry_id = exisitng_purchase.listing_id;
                im_GoodsReceiptHeaders.Goods_recipt_code= "GH" + year + "-" + Convert.ToString(key_3 + 1);
                im_GoodsReceiptHeaders.warehouse_id = exisitng_purchase.edit_user_id;
                im_GoodsReceiptHeaders.received_by = exisitng_purchase.vendor_id;
                im_GoodsReceiptHeaders.created_by = exisitng_purchase.vendor_id;
                im_GoodsReceiptHeaders.posted_by = exisitng_purchase.vendor_id;
                im_GoodsReceiptHeaders.cancelled_by = exisitng_purchase.vendor_id;
                im_GoodsReceiptHeaders.receipt_no = exisitng_purchase?.listing_code;
                im_GoodsReceiptHeaders.receipt_date = DateTime.Now;
                im_GoodsReceiptHeaders.supplier_invoice_no = exisitng_purchase.supplier_invoice_no;
                im_GoodsReceiptHeaders.supplier_do_no = im_GoodsReceiptHeaders.supplier_do_no;
                im_GoodsReceiptHeaders.subtotal = Convert.ToDecimal(exisitng_purchase.sub_total);
                im_GoodsReceiptHeaders.discount_amount = Convert.ToDecimal(exisitng_purchase.discount_amount);
                im_GoodsReceiptHeaders.tax_amount = Convert.ToDecimal(exisitng_purchase.tax_amount);
                im_GoodsReceiptHeaders.total_amount = Convert.ToDecimal(exisitng_purchase.doc_total);
                im_GoodsReceiptHeaders.created_at = DateTime.Now;
                im_GoodsReceiptHeaders.updated_at = DateTime.Now;
                im_GoodsReceiptHeaders.posted_at = DateTime.Now;
                im_GoodsReceiptHeaders.remarks = im_GoodsReceiptHeaders.remarks;
                im_GoodsReceiptHeaders.status = "POSTED";
                im_GoodsReceiptHeaders.is_posted = "T";
                im_GoodsReceiptHeaders.is_cancelled = "F";
                foreach (var item in exisitng_purchase.im_purchase_listing_details)
                {
                    var unmo = await _context.im_UnitsOfMeasures.FirstOrDefaultAsync(a => a.name == item.uom_name);
                    im_GoodsReceiptLines im_GoodsReceiptLines = new im_GoodsReceiptLines();
                    im_GoodsReceiptLines.business_id = st_store?.company_id ?? Guid.Empty;
                    im_GoodsReceiptLines.store_id = exisitng_purchase.site_id ?? Guid.Empty;
                    im_GoodsReceiptLines.supplier_id = exisitng_purchase.vendor_id;
                    im_GoodsReceiptLines.variant_id = item.sub_variant_id ?? Guid.Empty;
                    im_GoodsReceiptLines.product_id = item.product_id ?? Guid.Empty;
                    im_GoodsReceiptLines.uom_id = unmo.uom_id;
                    im_GoodsReceiptLines.uom_code = item.uom_name;
                    im_GoodsReceiptLines.line_no = 1;
                    im_GoodsReceiptLines.ordered_qty = Convert.ToDecimal(item.quantity);
                    im_GoodsReceiptLines.received_qty = Convert.ToDecimal(item.quantity);
                    im_GoodsReceiptLines.free_qty = 0;
                    im_GoodsReceiptLines.rejected_qty = 0;
                    im_GoodsReceiptLines.trans_type = "PURCHASE";

                    im_GoodsReceiptLines.accepted_qty = Convert.ToDecimal(item.quantity);
                    im_GoodsReceiptLines.unit_cost = Convert.ToDecimal(item.unit_price);
                    im_GoodsReceiptLines.discount_amount = Convert.ToDecimal(item.discount_amount);
                    im_GoodsReceiptLines.discount_percent = item.quantity != 0 ? Convert.ToDecimal(item.discount_amount) / Convert.ToDecimal(item.quantity) * 100 : 0m;
                    im_GoodsReceiptLines.tax_amount = Convert.ToDecimal(item.tax_amount);
                    im_GoodsReceiptLines.tax_percent = 0;
                    im_GoodsReceiptLines.line_amount = Convert.ToDecimal(item.line_net_cost) * Convert.ToDecimal(item.quantity);
                    im_GoodsReceiptLines.net_unit_cost = Convert.ToDecimal(item.line_net_cost);
                    im_GoodsReceiptLines.net_amount = Convert.ToDecimal(item.selling_price);
                    im_GoodsReceiptLines.batch_no = item.batch_no;
                    im_GoodsReceiptLines.expiry_date = item.expiry_date;
                    im_GoodsReceiptLines.manufacture_date = item.expiry_date;
                    im_GoodsReceiptLines.remarks = im_GoodsReceiptLines.remarks;
                    im_GoodsReceiptLines.created_at = DateTime.Now;
                    im_GoodsReceiptLines.updated_at = DateTime.Now;
                    if (im_GoodsReceiptLines.expiry_date != null)
                    {
                        im_GoodsReceiptLineBatches im_GoodsReceiptLineBatches = new im_GoodsReceiptLineBatches();

                        im_GoodsReceiptLineBatches.business_id = st_store?.company_id ?? Guid.Empty;
                        im_GoodsReceiptLineBatches.store_id = exisitng_purchase.site_id ?? Guid.Empty;
                        im_GoodsReceiptLineBatches.variant_id = item.sub_variant_id ?? Guid.Empty;
                        im_GoodsReceiptLineBatches.batch_no = item.batch_no;
                        im_GoodsReceiptLineBatches.expiry_date = item.expiry_date;
                        im_GoodsReceiptLineBatches.manufacture_date = item.expiry_date;
                        im_GoodsReceiptLineBatches.received_qty = Convert.ToDecimal(item.quantity);
                        im_GoodsReceiptLineBatches.free_qty = Convert.ToDecimal(item.quantity);
                        im_GoodsReceiptLineBatches.rejected_qty = 0;
                        im_GoodsReceiptLineBatches.accepted_qty = Convert.ToDecimal(item.quantity);
                        im_GoodsReceiptLineBatches.unit_cost = Convert.ToDecimal(item.unit_price);
                        im_GoodsReceiptLineBatches.net_unit_cost = Convert.ToDecimal(item.selling_price);
                        im_GoodsReceiptLineBatches.remarks = "";
                        im_GoodsReceiptLineBatches.created_at = DateTime.Now;
                        im_GoodsReceiptLineBatches.updated_at = DateTime.Now;
                        _context.im_GoodsReceiptLineBatches.Add(im_GoodsReceiptLineBatches);

                    }
                    _context.im_GoodsReceiptLines.Add(im_GoodsReceiptLines);
                    im_GoodsReceiptLines1.AddRange(im_GoodsReceiptLines);
                    im_GoodsReceiptHeaders.im_GoodsReceiptLines = im_GoodsReceiptLines1;

                }
                if (table_3 != null)
                {
                    table_key_3.next_key = key_3 + 1;
                    _context.am_table_next_key.Update(table_key_3);
                    await _context.SaveChangesAsync();
                }
                _context.im_GoodsReceiptHeaders.Add(im_GoodsReceiptHeaders);
                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();
                return new ServiceResult<im_GoodsReceiptHeaders>
                {
                    Status = 200,
                    Success = true,
                    Data = im_GoodsReceiptHeaders
                };

            }
            catch (Exception ex)
            {
                //transaction.RollbackAsync();
                return new ServiceResult<im_GoodsReceiptHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<im_purchase_listing>> Update_purchase_calculation(Guid listing_id, im_purchase_listing im_Purchase_)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

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
                    existing_purchase.gst = im_Purchase_.gst;
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
                decimal taxableAmount = sub_total - (existing_purchase.discount_amount ?? 0) + (existing_purchase.freight_amount ?? 0) + other_expense + ((existing_purchase.plastic_bag ?? 0) * (existing_purchase.exchange_rate ?? 1));
                decimal gstPercent = Convert.ToDecimal(existing_purchase.tax_amount ?? 0);
                decimal gstAmount = taxableAmount * gstPercent / 100;
                existing_purchase.tax_amount = gstAmount;
                existing_purchase.sub_total = Convert.ToDecimal(sub_total);
                existing_purchase.doc_total = taxableAmount + gstAmount;
                _context.im_purchase_listing.Update(existing_purchase);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 200,
                    Success = true,
                    Data = existing_purchase
                };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

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
            var transaction = await _context.Database.BeginTransactionAsync();

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
                await transaction.CommitAsync();
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
                await transaction.RollbackAsync();

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
                if (existing_batch.Any())
                {
                    _context.im_itemBatches.RemoveRange(existing_batch);
                    await _context.SaveChangesAsync();
                }
                var existing_temp_variant = await _context.temp_im_variants.Where(a => a.detail_id == detail_id).ToListAsync();
                if (existing_temp_variant.Any())
                {
                    _context.temp_im_variants.RemoveRange(existing_temp_variant);
                    await _context.SaveChangesAsync();
                }
                if (existing_purchase_listing != null)
                {
                    var im_purchase_listing_details = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_purchase_listing.listing_id).ToListAsync();
                    if (im_purchase_listing_details.Count == 0)
                    {
                        var im_purchase = await _context.im_purchase_listing.FirstOrDefaultAsync(a => a.listing_id == existing_purchase_listing.listing_id);
                        if (im_purchase != null)
                        {
                            _context.im_purchase_listing.Remove(im_purchase);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return new ServiceResult<im_purchase_listing_details>
                {
                    Status = 200,
                    Success = true,
                    Message = "Deleted successfully",
                };
            }
            catch (Exception ex)
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
                    if (purchase_details.Count == 0)
                    {
                        _context.im_purchase_listing.Remove(existing_purchase);
                    }
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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

        public async Task<ServiceResult<im_ItemBatches>> update_item_batch(Guid item_batch_id, im_ItemBatches im_ItemBatches)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

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
                var store_inv = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == existing_item.store_variant_inventory_id);
                if (existing_item != null)
                {
                    existing_item.expiry_date = im_ItemBatches.expiry_date;
                    existing_item.batch_number = im_ItemBatches.batch_number;
                    store_inv.batch_number = existing_item.batch_number;
                    existing_item.batch_promo_price = im_ItemBatches.batch_promo_price;
                    existing_item.promo_from_date = im_ItemBatches.promo_from_date;
                    existing_item.promo_to_date = im_ItemBatches.promo_to_date;
                    existing_item.batch_on_hold = im_ItemBatches.batch_on_hold;
                    _context.im_itemBatches.Update(existing_item);
                    _context.im_StoreVariantInventory.Update(store_inv);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                return new ServiceResult<im_ItemBatches>
                {
                    Status = 200,
                    Success = true,
                    Message = "Updated",
                    Data = existing_item
                };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

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
            var transaction = await _context.Database.BeginTransactionAsync();

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
                im_ItemBatches im_ItemBatches = new im_ItemBatches();

                var existing_im_purchase = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == im_Purchase_Listing.listing_id);

                Decimal sub_total = 0;
                Decimal other_expense = 0;
                Guid? product_id = null;
                Guid? variant_id = null;
                Guid? store_variant_inventory_id = null;
                if (existing_im_purchase == null)
                {
                    var table = "im_ItemBatches";
                    var table_key = await _context.super_abi.FirstOrDefaultAsync(a => a.description == table);
                    var key = Convert.ToInt16(table_key.next_key);

                    var table_2 = "im_purchase_listing";
                    var table_key_2 = await _context.am_table_next_key.FindAsync(table_2);
                    var key_2 = Convert.ToInt16(table_key_2.next_key);
                    var year = DateTime.Now.Year;

                    var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);

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
                        var tax_class = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_name == item.tax_class_name);
                        var im_product_cat = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_name == item.Category);
                        var im_product_sub_cat = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_name == item.Sub_Category);
                        var im_product_sub_sub_cat = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_name == item.Sub_sub_Category);
                        if (item.sku != "")
                        {
                            var im_varient_2 = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.sku == item.sku);

                            var im_store_varient = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.variant_id == im_varient_2.variant_id && a.store_id == im_Purchase_Listing.site_id);
                            product_id = im_varient_2.product_id;
                            variant_id = im_varient_2.variant_id;
                            store_variant_inventory_id = im_store_varient?.store_variant_inventory_id;
                        }

                        item.detail_id = Guid.CreateVersion7();
                        item.listing_id = im_Purchase_Listing.listing_id;
                        item.category_id = im_product_cat?.category_id;
                        item.sub_category_id = im_product_sub_cat?.category_id;
                        item.sub_sub_category_id = im_product_sub_sub_cat?.category_id;
                        item.tax_class_id = tax_class?.tax_class_id;
                        item.product_id = product_id;
                        item.sub_variant_id = variant_id;
                        item.quantity = item.quantity;
                        item.unit_price = item.unit_price;
                        item.discount_amount = item.discount_amount;
                        item.tax_amount = item.tax_amount;
                        item.freight_amount = item.freight_amount;
                        item.other_expenses = item.other_expenses;
                        item.line_total = item.quantity * item.unit_price;
                        item.notes = item.notes;
                        item.batch_no = item.batch_no;
                        item.base_price = item.base_price;
                        item.bin_no = item.bin_no;
                        item.expiry_date = item.expiry_date;
                        item.uom_name = item.uom_name;
                        item.barcode = item.barcode;
                        item.sku = item.sku;
                        if (item.sku == null || item.sku == "")
                        {
                            item.new_item = "T";
                        }
                        else
                        {
                            item.new_item = "F";
                        }
                        item.Product_title = item.Product_title;
                        item.Product_Brand = item.Product_Brand;
                        item.store_variant_inventory_id = store_variant_inventory_id;
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
                            im_ItemBatches.product_description = item.Product_title;
                            im_ItemBatches.created_at = DateTime.Now;
                            if (table_key != null)
                            {
                                table_key.next_key = key + 1;
                                _context.super_abi.Update(table_key);
                                await _context.SaveChangesAsync();
                            }
                            _context.im_itemBatches.Add(im_ItemBatches);
                        }


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
                    await transaction.CommitAsync();

                    return new ServiceResult<im_purchase_listing>
                    {
                        Status = 201,
                        Success = true,
                        Message = "Inserted",
                        Data = im_Purchase_Listing

                    };
                }
                else
                {
                    existing_im_purchase.notes = im_Purchase_Listing.notes;
                    existing_im_purchase.supplier_invoice_date = im_Purchase_Listing.supplier_invoice_date;
                    existing_im_purchase.supplier_invoice_no = im_Purchase_Listing.supplier_invoice_no;
                    existing_im_purchase.purchase_type = im_Purchase_Listing.purchase_type;
                    existing_im_purchase.edited_date_time = DateTime.Now;
                    foreach (var item in im_Purchase_Listing.im_purchase_listing_details)
                    {
                        var existing_purchase_deatils = await _context.im_purchase_listing_details.FirstOrDefaultAsync(a => a.detail_id == item.detail_id);
                        if (existing_purchase_deatils != null)
                        {

                            existing_purchase_deatils.discount_amount = item.discount_amount;
                            existing_purchase_deatils.tax_amount = item.tax_amount;
                            existing_purchase_deatils.freight_amount = item.freight_amount;
                            existing_purchase_deatils.other_expenses = item.other_expenses;
                            existing_purchase_deatils.notes = item.notes;
                            existing_purchase_deatils.batch_no = item.batch_no;
                            existing_purchase_deatils.bin_no = item.bin_no;
                            existing_purchase_deatils.Product_title = item.Product_title;
                            existing_purchase_deatils.Product_Brand = item.Product_Brand;
                            existing_purchase_deatils.uom_name = item.uom_name;
                            existing_purchase_deatils.barcode = item.barcode;
                            existing_purchase_deatils.sku = item.sku;
                            existing_purchase_deatils.quantity = item.quantity;
                            existing_purchase_deatils.expiry_date = item.expiry_date;
                            existing_purchase_deatils.unit_price = item.unit_price;
                            existing_purchase_deatils.line_total = item.quantity * item.unit_price;
                            if (existing_purchase_deatils.expiry_date != null)
                            {
                                var existing_batches = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.variant_id == existing_purchase_deatils.sub_variant_id && a.store_id == existing_im_purchase.site_id);
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
                                    existing_batches.product_description = item.Product_title;
                                    existing_batches.reference_doc = im_Purchase_Listing.supplier_invoice_no;
                                    existing_batches.notes = im_ItemBatches.notes;
                                    existing_batches.product_description = existing_batches.product_description;
                                    existing_batches.barcode = existing_purchase_deatils.barcode;
                                    existing_batches.sku = existing_purchase_deatils.sku;
                                    _context.im_itemBatches.Update(existing_batches);
                                    await _context.SaveChangesAsync();
                                }

                            }
                            other_expense += Convert.ToDecimal(item.other_expenses);
                            sub_total += Convert.ToDecimal(item.line_total);
                            _context.im_purchase_listing_details.Update(existing_purchase_deatils);
                        }
                        else
                        {
                            var tax_class = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_name == item.tax_class_name);
                            var im_product_cat = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_name == item.Category);
                            var im_product_sub_cat = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_name == item.Sub_Category);
                            var im_product_sub_sub_cat = await _context.im_ProductCategories.FirstOrDefaultAsync(a => a.category_name == item.Sub_sub_Category);

                            var im_site = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Purchase_Listing.site_id);

                            if (item.sku != "")
                            {
                                var im_varient_2 = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.sku == item.sku);

                                var im_store_varient = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.variant_id == im_varient_2.variant_id && a.store_id == im_Purchase_Listing.site_id);
                                product_id = im_varient_2.product_id;
                                variant_id = im_varient_2.variant_id;
                                store_variant_inventory_id = im_store_varient?.store_variant_inventory_id;
                            }


                            item.detail_id = Guid.CreateVersion7();
                            item.listing_id = im_Purchase_Listing.listing_id;
                            item.category_id = im_product_cat?.category_id;
                            item.sub_category_id = im_product_sub_cat?.category_id;
                            item.sub_sub_category_id = im_product_sub_sub_cat?.category_id;
                            item.tax_class_id = tax_class?.tax_class_id;
                            item.product_id = product_id;
                            item.sub_variant_id = variant_id;
                            item.quantity = item.quantity;
                            item.unit_price = item.unit_price;
                            item.discount_amount = item.discount_amount;
                            item.tax_amount = item.tax_amount;
                            item.freight_amount = item.freight_amount;
                            item.other_expenses = item.other_expenses;
                            item.line_total = item.quantity * item.unit_price;
                            item.notes = item.notes;
                            item.batch_no = item.batch_no;
                            item.base_price = item.base_price;
                            item.bin_no = item.bin_no;
                            item.expiry_date = item.expiry_date;
                            item.uom_name = item.uom_name;
                            item.barcode = item.barcode;
                            item.sku = item.sku;
                            if (item.sku == null || item.sku == "")
                            {
                                item.new_item = "T";
                            }
                            else
                            {
                                item.new_item = "F";
                            }
                            item.Product_title = item.Product_title;
                            item.Product_Brand = item.Product_Brand;
                            item.store_variant_inventory_id = store_variant_inventory_id;
                            if (item.expiry_date != null)
                            {
                                var table = "im_ItemBatches";
                                var table_key = await _context.super_abi.FirstOrDefaultAsync(a => a.description == table);
                                var key = Convert.ToInt16(table_key.next_key);

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
                                im_ItemBatches.product_description = item.Product_title;
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

                            other_expense += Convert.ToDecimal(item.other_expenses);
                            sub_total += Convert.ToDecimal(item.line_total);
                            _context.im_purchase_listing_details.Add(item);
                            existing_im_purchase.im_purchase_listing_details.Add(item);

                            //var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.sku == item.sku);

                        }
                    }
                    _context.im_purchase_listing.Update(existing_im_purchase);
                    await _context.SaveChangesAsync();
                    var im_purchase = await _context.im_purchase_listing_details.Where(a => a.listing_id == existing_im_purchase.listing_id).ToListAsync();
                    if (im_purchase.Any())
                    {
                        sub_total = 0;
                        other_expense = 0;
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
                    await transaction.CommitAsync();
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
                await transaction.RollbackAsync();

                _logger.LogInformation("Erro while Add_purchase_listing_excel");
                return new ServiceResult<im_purchase_listing>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }



        }

        public async Task<ServiceResult<im_Products>> Get_product_data(Guid product_id)
        {
            if (product_id == null)
            {
                return new ServiceResult<im_Products>
                {
                    Status = 400,
                };
            }
            var im_product = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).FirstOrDefaultAsync(a => a.product_id == product_id);
            return new ServiceResult<im_Products>
            {
                Data = im_product,
            };
        }

        public async Task<ServiceResult<List<im_purchase_listing>>> Get_purchase_list(string searchText)
        {
            try
            {
                if (searchText == null)
                {
                    _logger.LogInformation("No data found searchText");
                    return new ServiceResult<List<im_purchase_listing>>
                    {
                        Status = 400,
                        Message = "NO searchText found",
                        Success = false
                    };
                }
                var jsonResult = _context.Database.SqlQueryRaw<string>("EXEC dbo.im_purchase_list @SearchText=@SearchText,@opr=@opr",
                    new SqlParameter("@SearchText", searchText),
                    new SqlParameter("@opr",1)).AsEnumerable().FirstOrDefault();
                var list_deatils = JsonConvert.DeserializeObject<List<im_purchase_listing>>(jsonResult);
                return new ServiceResult<List<im_purchase_listing>>
                {
                    Status = 200,
                    Message = "Success",
                    Success = true,
                    Data = list_deatils
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error While Get_purchase_list");
                return new ServiceResult<List<im_purchase_listing>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<im_purchase_listing_dto>> Add_retuen_purchase(Guid listing_id, im_purchase_listing_dto _Purchase_Listing)
        {
            try
            {
                im_purchase_return_header im_Purchase_Return_Header = new im_purchase_return_header();
                List<im_purchase_return_details_line> im_Purchase_Return_Details_Lines = new List<im_purchase_return_details_line>();
                List<im_InventoryTransactions> im_InventoryTransactions1 = new List<im_InventoryTransactions>();

                im_GoodsReceiptLines im_GoodsReceiptLines = new im_GoodsReceiptLines();
                List<im_GoodsReceiptLines> im_GoodsReceiptLines1 = new List<im_GoodsReceiptLines>();

                var existing_list = await _context.im_purchase_listing.Include(a => a.im_purchase_listing_details).FirstOrDefaultAsync(a => a.listing_id == listing_id);
                var im_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == existing_list.site_id);
                var table = "im_purchase_return_header";
                var am_table = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == im_store.company_id);
                var key = Convert.ToInt16(am_table.next_key);
                var goods_header = await _context.im_GoodsReceiptHeaders.Include(a => a.im_GoodsReceiptLines).FirstOrDefaultAsync(a => a.purchase_order_id == existing_list.listing_id);

                im_Purchase_Return_Header.return_id = Guid.CreateVersion7();
                im_Purchase_Return_Header.return_code = "RE-" + im_store.store_code + "-" + Convert.ToString(key + 1);
                im_Purchase_Return_Header.listing_id = existing_list.listing_id;
                im_Purchase_Return_Header.site_id = existing_list.site_id;
                im_Purchase_Return_Header.vendor_id = existing_list.vendor_id;
                im_Purchase_Return_Header.return_date = DateOnly.MaxValue;
                im_Purchase_Return_Header.created_at = DateTime.Now;
                im_Purchase_Return_Header.supplier_return_ref = "";
                im_Purchase_Return_Header.reason = "";
                im_Purchase_Return_Header.orginal_sub_total = existing_list.sub_total;
                im_Purchase_Return_Header.freight_amount = existing_list.freight_amount;
                im_Purchase_Return_Header.other_expenses = existing_list.other_expenses;
                im_Purchase_Return_Header.plastic_bag = existing_list.plastic_bag;
                im_Purchase_Return_Header.exchange_rate = existing_list.exchange_rate;
                im_Purchase_Return_Header.orginal_discount_amount = existing_list.discount_amount;
                im_Purchase_Return_Header.orginal_tax_amount = existing_list.tax_amount;
                im_Purchase_Return_Header.orginal_total_amount = existing_list.doc_total;
                im_Purchase_Return_Header.status = "completed";
                Decimal? sub_total = 0;
                Decimal? other_expenses = 0;
                Decimal? tax_amount = 0;
                Decimal? doc_total = 0;
                foreach (var item in _Purchase_Listing.im_purchase_listing_details)
                {
                    im_purchase_return_details_line im_Purchase_Return_Details_Line = new im_purchase_return_details_line();
                    im_Purchase_Return_Details_Line.return_detail_id = Guid.CreateVersion7();
                    im_Purchase_Return_Details_Line.return_id = im_Purchase_Return_Header.return_id;
                    im_Purchase_Return_Details_Line.product_id = item.product_id;
                    im_Purchase_Return_Details_Line.sub_variant_id = item.sub_variant_id;
                    im_Purchase_Return_Details_Line.store_variant_inventory_id = item.store_variant_inventory_id;
                    im_Purchase_Return_Details_Line.uom_name = item.uom_name;
                    im_Purchase_Return_Details_Line.return_qty = item.return_quantity;
                    im_Purchase_Return_Details_Line.orginal_quantity = item.quantity;
                    im_Purchase_Return_Details_Line.orginal_unit_price = item.unit_price;
                    im_Purchase_Return_Details_Line.orginal_line_total = item.line_total;
                    im_Purchase_Return_Details_Line.unit_price = item.unit_price;
                    im_Purchase_Return_Details_Line.line_total = item.line_total * im_Purchase_Return_Details_Line.orginal_unit_price;
                    sub_total += im_Purchase_Return_Details_Line.line_total;
                    im_Purchase_Return_Details_Line.batch_no = item.batch_no;
                    im_Purchase_Return_Details_Line.other_expenses = item.other_expenses;
                    other_expenses += im_Purchase_Return_Details_Line.other_expenses;
                    im_Purchase_Return_Details_Line.return_reason = item.return_reason;
                    im_Purchase_Return_Details_Line.product_brand = item.Product_Brand;
                    im_Purchase_Return_Details_Line.product_title = item.Product_title;
                    im_Purchase_Return_Details_Line.barcode = item.barcode;
                    im_Purchase_Return_Details_Line.sku = item.sku;

                    im_GoodsReceiptLines.goods_receipt_id = Guid.CreateVersion7();
                    im_GoodsReceiptLines.business_id = goods_header.business_id;
                    im_GoodsReceiptLines.store_id = goods_header.store_id;
                    im_GoodsReceiptLines.supplier_id = goods_header.supplier_id;
                    im_GoodsReceiptLines.variant_id = im_Purchase_Return_Details_Line?.sub_variant_id??Guid.Empty;
                    im_GoodsReceiptLines.product_id = im_Purchase_Return_Details_Line?.product_id??Guid.Empty;
                    im_GoodsReceiptLines.uom_code = im_Purchase_Return_Details_Line.uom_name;
                    im_GoodsReceiptLines.line_no = 1;
                    im_GoodsReceiptLines.trans_type= "RETURN_PURCHASE";
                    im_GoodsReceiptLines.ordered_qty = 0;
                    im_GoodsReceiptLines.received_qty = 0;
                    im_GoodsReceiptLines.free_qty = 0;
                    im_GoodsReceiptLines.rejected_qty = Convert.ToDecimal(im_Purchase_Return_Details_Line.return_qty);
                    im_GoodsReceiptLines.accepted_qty = Convert.ToDecimal(im_Purchase_Return_Details_Line.orginal_quantity);
                    im_GoodsReceiptLines.unit_cost = Convert.ToDecimal(im_Purchase_Return_Details_Line.unit_price);
                    im_GoodsReceiptLines.discount_percent = im_GoodsReceiptLines.discount_percent = im_GoodsReceiptLines.rejected_qty != 0 ? Convert.ToDecimal(item.discount_amount) / Convert.ToDecimal(im_GoodsReceiptLines.rejected_qty) * 100 : 0m;
                    im_GoodsReceiptLines.discount_amount = Convert.ToDecimal(item.discount_amount);
                    im_GoodsReceiptLines.tax_amount = Convert.ToDecimal(item.tax_amount);
                    im_GoodsReceiptLines.tax_percent =0;
                    im_GoodsReceiptLines.line_amount = Convert.ToDecimal(im_GoodsReceiptLines.unit_cost) * Convert.ToDecimal(im_GoodsReceiptLines.rejected_qty);
                    im_GoodsReceiptLines.net_unit_cost = Convert.ToDecimal(item.line_net_cost);
                    im_GoodsReceiptLines.net_amount = Convert.ToDecimal(item.selling_price);
                    im_GoodsReceiptLines.batch_no = item.batch_no;
                    im_GoodsReceiptLines.expiry_date = item.expiry_date;
                    im_GoodsReceiptLines.created_at = DateTime.Now;
                    im_GoodsReceiptLines.updated_at = DateTime.Now;


                    foreach (var items in _Purchase_Listing.im_purchase_listing_details)
                    {
                        var im_InventoryTransactions = new im_InventoryTransactions()
                        {
                            listing_id = items.listing_id,
                            store_id = im_Purchase_Return_Header.site_id,
                            variant_id = items.sub_variant_id,
                            trans_type = "RETURN_PURCHASE",
                            quantity_change = im_Purchase_Return_Details_Line.return_qty,
                            unit_cost = im_Purchase_Return_Details_Line.unit_price,
                            total_cost = im_Purchase_Return_Details_Line.line_total,
                            created_date_time = DateTime.Now
                        };

                        im_InventoryTransactions1.Add(im_InventoryTransactions);

                    }
                    _context.im_GoodsReceiptLines.Add(im_GoodsReceiptLines);
                    goods_header.im_GoodsReceiptLines.Add(im_GoodsReceiptLines);

                    _context.im_InventoryTransactions.AddRange(im_InventoryTransactions1);
                    _context.im_purchase_return_details_line.Add(im_Purchase_Return_Details_Line);
                    im_Purchase_Return_Details_Lines.AddRange(im_Purchase_Return_Details_Line);
                    im_Purchase_Return_Header.im_purchase_return_details_line = im_Purchase_Return_Details_Lines;
                }
                foreach (var temp_item in _Purchase_Listing.temp_Im_Purchase_Listing_Details)
                {
                    im_purchase_return_details_line im_Purchase_Return_Details_Line = new im_purchase_return_details_line();
                    im_Purchase_Return_Details_Line.return_detail_id = Guid.CreateVersion7();
                    im_Purchase_Return_Details_Line.return_id = im_Purchase_Return_Header.return_id;
                    im_Purchase_Return_Details_Line.product_id = temp_item.product_id;
                    im_Purchase_Return_Details_Line.sub_variant_id = temp_item.sub_variant_id;
                    im_Purchase_Return_Details_Line.store_variant_inventory_id = temp_item.store_variant_inventory_id;
                    im_Purchase_Return_Details_Line.uom_name = temp_item.uom_name;
                    im_Purchase_Return_Details_Line.orginal_quantity = temp_item.quantity;
                    im_Purchase_Return_Details_Line.return_qty = temp_item.return_quantity;
                    im_Purchase_Return_Details_Line.orginal_unit_price = temp_item.unit_price;
                    im_Purchase_Return_Details_Line.orginal_line_total = temp_item.line_total;
                    im_Purchase_Return_Details_Line.other_expenses = temp_item.other_expenses;
                    im_Purchase_Return_Details_Line.unit_price = temp_item.unit_price;
                    im_Purchase_Return_Details_Line.line_total = temp_item.line_total * im_Purchase_Return_Details_Line.orginal_unit_price;
                    sub_total += im_Purchase_Return_Details_Line.line_total;
                    other_expenses += im_Purchase_Return_Details_Line.other_expenses;
                    im_Purchase_Return_Details_Line.batch_no = temp_item.batch_no;
                    im_Purchase_Return_Details_Line.return_reason = temp_item.return_reason;
                    im_Purchase_Return_Details_Line.product_brand = temp_item.Product_Brand;
                    im_Purchase_Return_Details_Line.product_title = temp_item.Product_title;
                    im_Purchase_Return_Details_Line.barcode = temp_item.barcode;
                    im_Purchase_Return_Details_Line.sku = temp_item.sku;

                    im_GoodsReceiptLines.goods_receipt_id = Guid.CreateVersion7();
                    im_GoodsReceiptLines.business_id = goods_header.business_id;
                    im_GoodsReceiptLines.store_id = goods_header.store_id;
                    im_GoodsReceiptLines.supplier_id = goods_header.supplier_id;
                    im_GoodsReceiptLines.variant_id = im_Purchase_Return_Details_Line?.sub_variant_id ?? Guid.Empty;
                    im_GoodsReceiptLines.product_id = im_Purchase_Return_Details_Line?.product_id ?? Guid.Empty;
                    im_GoodsReceiptLines.uom_code = im_Purchase_Return_Details_Line.uom_name;
                    im_GoodsReceiptLines.line_no = 1;
                    im_GoodsReceiptLines.trans_type = "RETURN_PURCHASE";
                    im_GoodsReceiptLines.ordered_qty = 0;
                    im_GoodsReceiptLines.received_qty = 0;
                    im_GoodsReceiptLines.free_qty = 0;
                    im_GoodsReceiptLines.rejected_qty = Convert.ToDecimal(im_Purchase_Return_Details_Line.return_qty);
                    im_GoodsReceiptLines.accepted_qty = Convert.ToDecimal(im_Purchase_Return_Details_Line.orginal_quantity);
                    im_GoodsReceiptLines.unit_cost = Convert.ToDecimal(im_Purchase_Return_Details_Line.unit_price);
                    im_GoodsReceiptLines.discount_percent = im_GoodsReceiptLines.discount_percent = im_GoodsReceiptLines.rejected_qty != 0 ? Convert.ToDecimal(temp_item.discount_amount) / Convert.ToDecimal(im_GoodsReceiptLines.rejected_qty) * 100 : 0m;
                    im_GoodsReceiptLines.discount_amount = Convert.ToDecimal(temp_item.discount_amount);
                    im_GoodsReceiptLines.tax_amount = Convert.ToDecimal(temp_item.tax_amount);
                    im_GoodsReceiptLines.tax_percent = 0;
                    im_GoodsReceiptLines.line_amount = Convert.ToDecimal(im_GoodsReceiptLines.unit_cost) * Convert.ToDecimal(im_GoodsReceiptLines.rejected_qty);
                    im_GoodsReceiptLines.net_unit_cost = Convert.ToDecimal(temp_item.line_net_cost);
                    im_GoodsReceiptLines.net_amount = Convert.ToDecimal(temp_item.selling_price);
                    im_GoodsReceiptLines.batch_no = temp_item.batch_no;
                    im_GoodsReceiptLines.expiry_date = temp_item.expiry_date;
                    im_GoodsReceiptLines.created_at = DateTime.Now;
                    im_GoodsReceiptLines.updated_at = DateTime.Now;


                    foreach (var items in _Purchase_Listing.temp_Im_Purchase_Listing_Details)
                    {
                        var im_InventoryTransactions = new im_InventoryTransactions()
                        {
                            listing_id = items.listing_id,
                            store_id = im_Purchase_Return_Header.site_id,
                            variant_id = items.sub_variant_id,
                            trans_type = "RETURN_PURCHASE",
                            quantity_change = im_Purchase_Return_Details_Line.return_qty,
                            unit_cost = im_Purchase_Return_Details_Line.unit_price,
                            total_cost = im_Purchase_Return_Details_Line.line_total,
                            created_date_time = DateTime.Now
                        };

                        im_InventoryTransactions1.Add(im_InventoryTransactions);

                    }

                    _context.im_GoodsReceiptLines.Add(im_GoodsReceiptLines);
                    goods_header.im_GoodsReceiptLines.Add(im_GoodsReceiptLines);

                    _context.im_InventoryTransactions.AddRange(im_InventoryTransactions1);
                    _context.im_purchase_return_details_line.Add(im_Purchase_Return_Details_Line);
                    im_Purchase_Return_Details_Lines.AddRange(im_Purchase_Return_Details_Line);
                    im_Purchase_Return_Header.im_purchase_return_details_line = im_Purchase_Return_Details_Lines;
                }

                
                decimal taxableAmount =Convert.ToDecimal( sub_total - (im_Purchase_Return_Header.discount_amount ?? 0) + (im_Purchase_Return_Header.freight_amount ?? 0) + other_expenses + ((im_Purchase_Return_Header.plastic_bag ?? 0) * (im_Purchase_Return_Header.exchange_rate ?? 1)));
                decimal gstPercent = Convert.ToDecimal(im_Purchase_Return_Header.tax_amount ?? 0);
                decimal gstAmount = taxableAmount * gstPercent / 100;
                im_Purchase_Return_Header.tax_amount = gstAmount;
                im_Purchase_Return_Header.sub_total = Convert.ToDecimal(sub_total);
                im_Purchase_Return_Header.total_amount = taxableAmount + gstAmount;
                im_Purchase_Return_Header.sub_total = sub_total;
                im_Purchase_Return_Header.discount_amount = im_Purchase_Return_Header.discount_amount;
                sub_total =0;
                other_expenses = 0;
                am_table.next_key = key + 1;
                _context.am_table_next_key.Update(am_table);
                _context.im_GoodsReceiptHeaders.Update(goods_header);
                _context.im_purchase_return_header.Add(im_Purchase_Return_Header);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_purchase_listing_dto>
                {
                    Status = 200,
                    Success = true
                };


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Add_retuen_purchase");
                return new ServiceResult<im_purchase_listing_dto>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<ServiceResult<Dictionary<string,List<im_GoodsReceiptLines>>>> Get_inventory(Guid store_id, Guid variant_id)
        {
            try
            {
                if(store_id==null|| variant_id == null)
                {
                    return new ServiceResult<Dictionary<string, List<im_GoodsReceiptLines>>>
                    {
                        Status = 300,
                        Success = false,
                    };
                }
                var transaction_list = await _context.im_GoodsReceiptLines.FromSqlRaw<im_GoodsReceiptLines>(
                    "EXEC dbo.im_purchase_list @opr=@opr,@store_id=@store_id,@variant_id=@variant_id",
                    new SqlParameter("@opr",2),
                    new SqlParameter("@store_id",store_id),
                    new SqlParameter("@variant_id",variant_id)).ToListAsync();

                if(transaction_list.Count==0 || transaction_list == null)
                {
                    return new ServiceResult<Dictionary<string, List<im_GoodsReceiptLines>>>
                    {
                        Status = 300,
                        Success = false,
                        Message="No data found"
                    };
                }
                var grouped_=transaction_list.GroupBy(a=>a.trans_type).ToDictionary(a=>a.Key,a=>a.ToList());
                return new ServiceResult<Dictionary<string, List<im_GoodsReceiptLines>>>
                {
                    Success = true,
                    Status = 200,
                    Data = grouped_
                };

            }catch(Exception ex)
            {
                _logger.LogInformation("Error while Get_inventory");
                return new ServiceResult<Dictionary<string, List<im_GoodsReceiptLines>>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<im_purchase_listing>>> Get_inventory_report(Guid store_id, DateTime? start_date, DateTime? end_date, Guid? vendor_id, string searchText)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<List<im_purchase_listing>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
               

               
                var jsonResult = _context.Database.SqlQueryRaw<string>("EXEC dbo.im_purchase_list @opr=@opr, @store_id=@store_id, @StartDate=@StartDate, @EndDate=@EndDate,@vendor_id=@vendor_id,@searchText=@searchText",
                     new SqlParameter("@opr", 3),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@StartDate", start_date.HasValue ? (object)start_date.Value : DBNull.Value),
                                     new SqlParameter("@EndDate", end_date.HasValue ? (object)end_date.Value : DBNull.Value),
                                     new SqlParameter("@vendor_id", vendor_id.HasValue ? (object)vendor_id.Value : DBNull.Value),
                                     new SqlParameter("@searchText", (object?)searchText ?? DBNull.Value) 
                                     ).AsEnumerable().FirstOrDefault();

                var list_deatils = string.IsNullOrEmpty(jsonResult)? new List<im_purchase_listing>(): JsonConvert.DeserializeObject<List<im_purchase_listing>>(jsonResult);

                if (list_deatils.Count == 0)
                {
                    return new ServiceResult<List<im_purchase_listing>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<im_purchase_listing>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = list_deatils
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<List<im_purchase_listing>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

    }

}
