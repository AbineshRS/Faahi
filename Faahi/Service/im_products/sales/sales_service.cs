using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper.Configuration.Annotations;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.sales_dto;
using Faahi.Migrations;
using Faahi.Model.Accounts;
using Faahi.Model.im_products;
using Faahi.Model.Order;
using Faahi.Model.pos_tables;
using Faahi.Model.sales;
using Faahi.Model.Shared_tables;
using Faahi.Model.st_sellers;
using Faahi.Service.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Utilities;

namespace Faahi.Service.im_products.sales
{
    public class sales_service : Isales
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<sales_service> _logger;
        private readonly IHubContext<signalr_hub> _hubContext;

        public sales_service(ApplicationDbContext applicationDb, ILogger<sales_service> sales_log,IHubContext<signalr_hub> hubContext)
        {
            _context = applicationDb;
            _logger = sales_log;
            _hubContext=hubContext;
            
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
                var exisiting = await _context.so_Payment_Types.AnyAsync(a => a.PayTypeCode == so_payment_type.PayTypeCode && a.business_id == so_payment_type.business_id);
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
                so_payment_type.business_id = so_payment_type.business_id;
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
                var date = await _context.so_Payment_Types.Where(a => a.business_id == company_id).OrderByDescending(a => a.Order).ToListAsync();
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

        public async Task<ServiceResult<so_payment_type>> Get_payment(Guid payment_type_id)
        {
            try
            {
                if (payment_type_id == null)
                {
                    _logger.LogInformation("NO data payTypeCode found");
                    return new ServiceResult<so_payment_type>
                    {
                        Success = false,
                        Status = 400,
                        Message = "No Data foun payTypeCode"
                    };
                }
                var date = await _context.so_Payment_Types.FirstOrDefaultAsync(a => a.payment_type_id == payment_type_id);
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

        public async Task<ServiceResult<so_payment_type>> Update_payment(Guid payment_type_id, so_payment_type so_payment)
        {
            var transation = await _context.Database.BeginTransactionAsync();
            try
            {
                if (payment_type_id == null)
                {
                    _logger.LogInformation($"payTypeCode {payment_type_id}");
                    return new ServiceResult<so_payment_type>
                    {
                        Success = false,
                        Status = 400,
                        Message = "NO data found"
                    };
                }
                var existing = await _context.so_Payment_Types.FirstOrDefaultAsync(a => a.payment_type_id == payment_type_id);
                existing.business_id = so_payment.business_id;
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

        public async Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches_list(Guid variant_id)
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



            var expiryBatches = await _context.im_itemBatches.Where(a => a.variant_id == variant_id && a.expiry_date > today && a.on_hand_quantity >= 0).OrderBy(a => a.expiry_date).ToListAsync();
            //decimal remainingQty = requiredQuantity;

            //List<im_ItemBatches> itemBatches = new List<im_ItemBatches>();
            //foreach (var batch in expiryBatches)
            //{
            //    if (remainingQty <= 0)
            //        break;

            //    decimal availableQty = batch.on_hand_quantity ?? 0;

            //    if (availableQty <= 0)
            //        continue;

            //    decimal usedQty = 0;

            //    if (availableQty >= remainingQty)
            //    {
            //        usedQty = remainingQty;
            //    }
            //    else
            //    {
            //        usedQty = availableQty;
            //    }

            //    batch.on_hand_quantity = usedQty;

            //    itemBatches.Add(batch);

            //    remainingQty -= usedQty;
            //}



            return new ServiceResult<List<im_ItemBatches>>
            {
                Status = 200,
                Message = "Success",
                Success = true,
                Data = expiryBatches

            };

        }

        public async Task<ActionResult<ServiceResult<so_SalesHeaders>>> Add_sales(so_SalesHeaders so_SalesHeaders)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (so_SalesHeaders == null)
                {
                    _logger.LogInformation("No data found to insert");
                    return new ServiceResult<so_SalesHeaders>
                    {
                        Status = 400,
                        Message = "No data found to insert",
                        Success = false,

                    };
                }
                var table = "so_SalesHeaders_sales_no";
                var table_key = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == so_SalesHeaders.business_id);
                var key = Convert.ToInt16(table_key.next_key);

                var table2 = "so_SalesHeaders_invoice_no";
                var table_key2 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table2 && a.business_id == so_SalesHeaders.business_id);
                var key2 = Convert.ToInt16(table_key2.next_key);

                var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == so_SalesHeaders.store_id);


                Decimal discount_total = 0m;
                Decimal total_taxable_value = 0m;
                Decimal total_zero_value = 0m;
                Decimal total_exempted_value = 0m;
                string? docType = so_SalesHeaders.doc_type;
                string[]? parts = docType?.Split('-');
                so_SalesHeaders.business_id = so_SalesHeaders.business_id;
                so_SalesHeaders.store_id = so_SalesHeaders.store_id;
                so_SalesHeaders.customer_id = so_SalesHeaders.customer_id;
                so_SalesHeaders.payment_term_id = so_SalesHeaders.payment_term_id;
                so_SalesHeaders.membership_id = so_SalesHeaders.membership_id;
                so_SalesHeaders.sales_no = Convert.ToInt64(key + 1);
                so_SalesHeaders.invoice_no = st_store.default_invoice_init + "-" + so_SalesHeaders.invoice_no + "-" + Convert.ToString(key2 + 1);

                if (so_SalesHeaders.doc_type == "QUOTATION")
                {
                    so_SalesHeaders.quot_no = st_store.default_quote_init + "-" + Convert.ToString(key2 + 1);
                }
                else
                {
                    so_SalesHeaders.quot_no = "";
                }
                so_SalesHeaders.purchase_order_no = so_SalesHeaders.purchase_order_no;
                so_SalesHeaders.sales_date = so_SalesHeaders.sales_date;
                so_SalesHeaders.doc_type = so_SalesHeaders.doc_type;
                so_SalesHeaders.due_date = so_SalesHeaders.due_date;
                so_SalesHeaders.tax_percent = so_SalesHeaders.tax_percent;
                so_SalesHeaders.service_charge_percent = so_SalesHeaders.service_charge_percent;
                so_SalesHeaders.sales_mode = so_SalesHeaders.sales_mode;
                so_SalesHeaders.id_card_no = so_SalesHeaders.id_card_no;
                so_SalesHeaders.age = so_SalesHeaders.age;
                so_SalesHeaders.table_no = so_SalesHeaders.table_no;
                so_SalesHeaders.number_of_pax = so_SalesHeaders.number_of_pax;
                so_SalesHeaders.doc_currency_code = so_SalesHeaders.doc_currency_code;
                so_SalesHeaders.base_currency_code = st_store?.default_currency;
                so_SalesHeaders.fx_rate_to_base = so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.fx_rate_date = so_SalesHeaders.fx_rate_date;
                so_SalesHeaders.fx_source = so_SalesHeaders.fx_source;
                so_SalesHeaders.purchase_order_no = so_SalesHeaders.purchase_order_no;
                so_SalesHeaders.sub_total = so_SalesHeaders.sub_total;
                so_SalesHeaders.discount_total = so_SalesHeaders.discount_total;
                so_SalesHeaders.service_charge = so_SalesHeaders.service_charge;
                so_SalesHeaders.tax_total = so_SalesHeaders.tax_total;
                so_SalesHeaders.grand_total = so_SalesHeaders.grand_total;
                so_SalesHeaders.quick_customer = so_SalesHeaders.quick_customer;
                so_SalesHeaders.reference_no = so_SalesHeaders.reference_no;
                so_SalesHeaders.total_plastic_bag = so_SalesHeaders.total_plastic_bag;
                so_SalesHeaders.total_taxable_value = so_SalesHeaders.total_taxable_value;
                so_SalesHeaders.total_zero_value = so_SalesHeaders.total_zero_value;
                so_SalesHeaders.total_exempted_value = so_SalesHeaders.total_exempted_base;
                so_SalesHeaders.total_charge_customer = so_SalesHeaders.total_charge_customer;
                so_SalesHeaders.total_plastic_bag_tax = so_SalesHeaders.total_plastic_bag * Convert.ToDecimal(st_store.plastic_bag_tax_amount);
                so_SalesHeaders.sub_total_base = so_SalesHeaders.sub_total * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.balance_base = so_SalesHeaders.balance_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.discount_total_base = so_SalesHeaders.discount_total_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.tax_total_base = so_SalesHeaders.tax_total * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.grand_total_base = so_SalesHeaders.grand_total * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_taxable_base = so_SalesHeaders.tax_total_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_zero_base = so_SalesHeaders.total_zero_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_exempted_base = so_SalesHeaders.total_exempted_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_charge_customer_base = so_SalesHeaders.total_charge_customer_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.service_charge_base = so_SalesHeaders.service_charge_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_plastic_bag_tax_base = so_SalesHeaders.total_plastic_bag_tax_base;
                so_SalesHeaders.total_charge_bank_marchant = so_SalesHeaders.total_charge_bank_marchant;
                so_SalesHeaders.transaction_cost = so_SalesHeaders.transaction_cost;
                so_SalesHeaders.amount_paid_base = so_SalesHeaders.amount_paid_base;
                so_SalesHeaders.change_given_base = so_SalesHeaders.change_given_base;
                so_SalesHeaders.change_given_doc = so_SalesHeaders.change_given_doc;
                so_SalesHeaders.balance_base = so_SalesHeaders.balance_base;
                so_SalesHeaders.notes = so_SalesHeaders.notes;
                so_SalesHeaders.created_at = so_SalesHeaders.created_at;
                so_SalesHeaders.datetime = DateTime.Now;
                so_SalesHeaders.created_by = so_SalesHeaders.created_by;
                so_SalesHeaders.qo_validity = so_SalesHeaders.qo_validity;
                so_SalesHeaders.qo_delivery = so_SalesHeaders.qo_delivery;
                so_SalesHeaders.qo_attention = so_SalesHeaders.qo_attention;
                so_SalesHeaders.sales_on_hold = so_SalesHeaders.sales_on_hold;
                so_SalesHeaders.is_mutiple_payment = so_SalesHeaders.is_mutiple_payment;
                so_SalesHeaders.created_user_id = so_SalesHeaders.created_user_id;
                so_SalesHeaders.status = so_SalesHeaders.status;
                foreach (var item in so_SalesHeaders.so_SalesLines)
                {

                    var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == item.variant_id);
                    var item_batch = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.item_batch_id == item.batch_id);
                    var im_store_inv = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == item.store_variant_inventory_id);
                    if (item_batch?.on_hand_quantity == 0 || im_store_inv?.on_hand_quantity == 0)
                    {
                        return new ServiceResult<so_SalesHeaders>
                        {
                            Status = 400,
                            Message = "selected Item  is out of stock",
                            Success = false
                        };
                    }
                    if (st_store.sales_mode == "ROUNDOFF")
                    {
                        var resultof_roundoff = await Roundoff_CalculationResult(item.product_id, item.variant_id, item.quantity,item.discount_percent);
                        if (resultof_roundoff.success == true)
                        {
                            item.unit_price = resultof_roundoff.price_rate ?? 0m;
                            item.tax_amount = resultof_roundoff.gst ?? 0m;
                            item.line_total = resultof_roundoff.total_price ?? 0m;
                            item.discount_amount = resultof_roundoff.discount_amount ?? 0m;
                        }

                    }
                    if (st_store.sales_mode == "ROUND OFF 4")
                    {
                        var resultof_roundoff = await Roundoff4_CalculationResult(item.product_id, item.variant_id, item.quantity, item.discount_percent);
                        if (resultof_roundoff.success == true)
                        {
                            item.unit_price = resultof_roundoff.price_rate ?? 0m;
                            item.tax_amount = resultof_roundoff.gst ?? 0m;
                            item.line_total = resultof_roundoff.total_price ?? 0m;
                            item.discount_amount = resultof_roundoff.discount_amount ?? 0m;
                        }
                    }
                    if (st_store.sales_mode == "STANDARD")
                    {
                        var resultof_roundoff = await Standard_CalculationResult(item.product_id, item.variant_id, item.quantity, item.discount_percent);
                        if (resultof_roundoff.success == true)
                        {
                            item.unit_price = resultof_roundoff.price_rate ?? 0m;
                            item.tax_amount = resultof_roundoff.gst ?? 0m;
                            item.line_total = resultof_roundoff.total_price ?? 0m;
                            item.discount_amount = resultof_roundoff.discount_amount ?? 0m;
                        }
                    }
                    if (st_store.sales_mode == "RATE PLUS TAX")
                    {
                        var resultof_roundoff = await Rateplus_tax_CalculationResult(item.product_id, item.variant_id, item.quantity, item.discount_percent);
                        if (resultof_roundoff.success == true)
                        {
                            item.unit_price = resultof_roundoff.price_rate ?? 0m;
                            item.tax_amount = resultof_roundoff.gst ?? 0m;
                            item.line_total = resultof_roundoff.total_price ?? 0m;
                            item.discount_amount = resultof_roundoff.discount_amount ?? 0m;
                        }
                    }
                    item.sales_id = so_SalesHeaders.sales_id;
                    item.business_id = so_SalesHeaders.business_id;
                    item.store_id = so_SalesHeaders.store_id;
                    item.product_id = item.product_id;
                    item.variant_id = item.variant_id;
                    item.barcode = im_varient?.barcode;
                    item.product_sku = im_varient?.sku;
                    item.track_expiry = item.track_expiry;
                    item.item_description = item.item_description;
                    item.line_discount_amount = item.line_discount_amount;
                    item.stock_item = item.stock_item;
                    item.consignment_id = item.consignment_id;
                    item.consignment_det_id = item.consignment_det_id;
                    item.consignment_billed = item.consignment_billed; ;
                    item.batch_id = item.batch_id;
                    item.batch_id_int = item_batch?.batch_id;
                    item.batch_name = item_batch?.batch_number;
                    item.expiry_date = item_batch?.expiry_date;
                    item.insurance_code = item.insurance_code;
                    item.doctor_consent = item.doctor_consent;

                    item.quantity = item.quantity;
                    item.discount_amount = item.discount_amount;
                    item.discount_percent = item.discount_percent;
                    //item.unit_price = item.unit_price;
                    //item.tax_amount = item.tax_amount;
                    //item.line_total = item.quantity * item.unit_price;
                    item.original_price_base = im_varient?.base_price ?? 0m;
                    item.tax_class = item.tax_class;
                    item.returned_quantity = item.returned_quantity;
                    item.original_quantity = im_store_inv?.on_hand_quantity ?? 0m;
                    item.doc_currency_code = item.doc_currency_code;
                    item.base_currency_code = st_store?.default_currency;
                    item.fx_rate_to_base = item.fx_rate_to_base;
                    item.detected_qty = item.quantity;
                    item.tax_amount_base = item.tax_amount * item.fx_rate_to_base;
                    item.discount_amount_base = item.discount_amount * item.fx_rate_to_base;
                    item.unit_price_base = item.unit_price * item.fx_rate_to_base;
                    item.unit_discount_amount_base = (item.unit_price * (item.discount_percent / 100)) * item.fx_rate_to_base;
                    item.line_total_base = item.line_total * item.fx_rate_to_base;
                    item.remarks = item.remarks;
                    item.created_at = DateTime.Now;

                    discount_total += item.discount_amount;
                    var im_product = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == item.product_id);
                    var tax_clas = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == im_product.tax_class_id);
                    if (tax_clas.rate_percent >= 0)
                    {
                        total_taxable_value += item.unit_price;
                    }
                    else if (tax_clas.rate_percent == 0)
                    {
                        total_zero_value += item.unit_price;
                    }

                }

                so_SalesHeaders.discount_total = discount_total;
                so_SalesHeaders.total_taxable_value = total_taxable_value;
                so_SalesHeaders.total_zero_value = total_zero_value;
                so_SalesHeaders.discount_total_base = so_SalesHeaders.discount_total * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_taxable_base = so_SalesHeaders.total_taxable_value * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_zero_base = so_SalesHeaders.total_zero_value * so_SalesHeaders.fx_rate_to_base;
                if (table_key != null)
                {
                    table_key.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key);
                    await _context.SaveChangesAsync();
                }
                if (table_key2 != null)
                {
                    table_key2.next_key = key2 + 1;
                    _context.am_table_next_key.Update(table_key2);
                    await _context.SaveChangesAsync();
                }

                _context.so_SalesHeaders.Add(so_SalesHeaders);
                await _context.SaveChangesAsync();
                if (so_SalesHeaders.status == "POSTED" || so_SalesHeaders.status == "HOLD" && so_SalesHeaders.doc_type == "SALE")
                {
                    int index = 0;

                    foreach (var so_item in so_SalesHeaders.pos_SalePayments)
                    {
                        so_item.business_id = so_SalesHeaders.business_id;
                        so_item.store_id = so_SalesHeaders.store_id;
                        so_item.sale_id = so_SalesHeaders.sales_id;
                        so_item.payment_method_id = so_item.payment_method_id;
                        so_item.receipt_no = so_SalesHeaders.invoice_no;
                        so_item.line_no = index + 1;
                        so_item.currency_code = so_SalesHeaders.base_currency_code;
                        so_item.fx_rate = 1;
                        so_item.amount = so_item.amount;
                        so_item.base_amount = so_item.base_amount;
                        so_item.reference_no = so_SalesHeaders.reference_no;
                        so_item.notes = so_SalesHeaders.notes;
                        so_item.is_voided = so_item.is_voided;
                        so_item.created_by = so_item.created_by;
                        so_item.voided_at = DateTime.Now;
                        so_item.created_at = DateTime.Now;

                        index++;
                        _context.pos_SalePayments.Add(so_item);

                    }
                }
                if (so_SalesHeaders.status == "POSTED" || so_SalesHeaders.status == "HOLD" && so_SalesHeaders.doc_type == "SALE" || so_SalesHeaders.doc_type == "MARKETPLACE")
                {
                    var so_lines = await _context.so_SalesLines.Where(a => a.sales_id == so_SalesHeaders.sales_id).ToListAsync();
                    List<im_InventoryTransactions> im_InventoryTransactions1 = new List<im_InventoryTransactions>();
                    foreach (var item in so_lines)
                    {
                        var im_InventoryTransactions = new im_InventoryTransactions()
                        {
                            sales_line_id = item.sales_line_id,
                            store_id = so_SalesHeaders.store_id,
                            variant_id = item.variant_id,
                            trans_type = so_SalesHeaders.doc_type,
                            quantity_change = item.quantity,
                            unit_cost = item.unit_price,
                            total_cost = item.line_total,
                            created_date_time = DateTime.Now
                        };

                        im_InventoryTransactions1.Add(im_InventoryTransactions);

                    }
                    _context.im_InventoryTransactions.AddRange(im_InventoryTransactions1);
                    if (so_SalesHeaders.doc_type == "MARKETPLACE")
                    {
                        var order = await Add_order(so_SalesHeaders?.sales_id, so_SalesHeaders.address_id, so_SalesHeaders.source_id, so_SalesHeaders.urget_delivery);
                        {
                            if (!order.Success)
                            {
                                await transaction.RollbackAsync();

                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Status = 500,
                                    Success = false,
                                    Message = order.Message
                                };
                            }
                            
                        }
                        var Fullfilment = await Add_Fullfilment(order?.Data.customer_order_id, so_SalesHeaders.login_user_id);
                        {
                            if (!Fullfilment.Success)
                            {
                                await transaction.RollbackAsync();

                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Status = 500,
                                    Success = false,
                                    Message = Fullfilment.Message
                                };
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    if (so_SalesHeaders.status == "POSTED" && so_SalesHeaders.doc_type == "SALE")
                    {
                        if (so_SalesHeaders.payment_mode == "Cash")
                        {
                            var journal = await Add_Journal_header(so_SalesHeaders.sales_id);
                            if (!journal.Success)
                            {
                                await transaction.RollbackAsync();
                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Success = false,
                                    Status = 300,
                                    Message = "Error"
                                };
                            }
                        }

                        if (so_SalesHeaders.payment_mode == "CREDIT")
                        {
                            var journal_credit = await Add_Journal_header_credit(so_SalesHeaders.sales_id);
                            if (!journal_credit.Success)
                            {
                                await transaction.RollbackAsync();
                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Success = false,
                                    Status = 300,
                                    Message = "Error"
                                };
                            }
                        }

                        var spResults = _context.Database.SqlQueryRaw<string>(
                                                             "EXEC dbo.sp_PostSales @sales_id=@sales_id ,@store_id=@store_id",
                                                              new SqlParameter("@sales_id", so_SalesHeaders.sales_id),
                                                              new SqlParameter("@store_id ", st_store.store_id)).AsEnumerable().ToList();
                        var spResponse = spResults.FirstOrDefault();
                    }

                }





                await transaction.CommitAsync();
                if (so_SalesHeaders.doc_type == "MARKETPLACE" && so_SalesHeaders.status == "HOLD")
                {
                    await _hubContext.Clients.Group(so_SalesHeaders.business_id.ToString()).SendAsync(
                                "MarketplaceSale", new
                                {
                                    salesId = so_SalesHeaders.sales_id,
                                    storeId = so_SalesHeaders.store_id,
                                    total = so_SalesHeaders.grand_total,
                                    items = so_SalesHeaders.so_SalesLines.Select(x => new
                                    {
                                        productId = x.product_id,
                                        variantId = x.variant_id,
                                        qty = x.quantity
                                    })
                                });
                }
                else if(so_SalesHeaders.doc_type== "SALE"&& so_SalesHeaders.status == "POSTED")
                {
                    await _hubContext.Clients.Group(so_SalesHeaders.business_id.ToString())
                                        .SendAsync("SaleCreated", new
                                        {
                                            salesId = so_SalesHeaders.sales_id,
                                            storeId = so_SalesHeaders.store_id,
                                            total = so_SalesHeaders.grand_total,
                                            items = so_SalesHeaders.so_SalesLines.Select(x => new
                                            {
                                                productId = x.product_id,
                                                variantId = x.variant_id,
                                                qty = x.quantity
                                            })
                                        });
                }
                    
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 200,
                    Message = "Success",
                    Success = true,
                    Data = so_SalesHeaders
                };



            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation("Error while Add_sales");
                return new ServiceResult<so_SalesHeaders>
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message
                };
            }



        }

        public async Task<sales_CalculationResult> Roundoff_CalculationResult(Guid? product_id, Guid? vareint_id, decimal quantity,decimal discount_percent)
        {
            try
            {
                var im_product = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == product_id);

                var tax_class = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == im_product.tax_class_id);

                var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == vareint_id);


                decimal base_price = (im_varient.base_price ?? 0m) * quantity;
                base_price = Math.Round(base_price, 2, MidpointRounding.AwayFromZero);

                decimal rate_percent = tax_class.rate_percent ?? 0m;

                decimal gst = (rate_percent / 100) + 1;
                gst = Math.Round(gst, 2, MidpointRounding.AwayFromZero);


                decimal rate = base_price / gst;
                rate = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                gst = base_price - rate;



                decimal total_price = rate + gst;

                decimal discount = (discount_percent / 100);
                discount = total_price * discount;
                total_price = total_price - discount;

                decimal price_rate = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                total_price = Math.Round(total_price, 2, MidpointRounding.AwayFromZero);
                discount = Math.Round(discount, 1, MidpointRounding.AwayFromZero);

                return new sales_CalculationResult
                {
                    price_rate = price_rate,
                    total_price = total_price,
                    discount_amount = discount,
                    gst = gst,
                    success = true
                };
            }
            catch (Exception)
            {
                return new sales_CalculationResult
                {
                    success = false
                };
            }
        }
        public async Task<sales_CalculationResult> Roundoff4_CalculationResult(Guid? product_id, Guid? vareint_id, decimal quantity,decimal discount_percent)
        {
            try
            {
                var im_product = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == product_id);

                var tax_class = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == im_product.tax_class_id);

                var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == vareint_id);


                decimal base_price = (im_varient.base_price ?? 0m) * quantity;
                base_price = Math.Round(base_price, 4, MidpointRounding.AwayFromZero);

                decimal rate_percent = tax_class.rate_percent ?? 0m;

                decimal gst = (rate_percent / 100) + 1;
                gst = Math.Round(gst, 4, MidpointRounding.AwayFromZero);


                decimal rate = base_price / gst;
                rate = Math.Round(rate, 4, MidpointRounding.AwayFromZero);
                gst = base_price - rate;



                decimal total_price = rate + gst;

                decimal discount = (discount_percent / 100);
                discount = total_price * discount;
                total_price = total_price - discount;

                decimal price_rate = Math.Round(rate, 4, MidpointRounding.AwayFromZero);
                total_price = Math.Round(total_price, 4, MidpointRounding.AwayFromZero);
                discount = Math.Round(discount, 1, MidpointRounding.AwayFromZero);

                return new sales_CalculationResult
                {
                    price_rate = price_rate,
                    total_price = total_price,
                    discount_amount = discount,
                    gst = gst,
                    success = true
                };
            }
            catch (Exception)
            {
                return new sales_CalculationResult
                {
                    success = false
                };
            }
        }

        public async Task<sales_CalculationResult> Standard_CalculationResult(Guid? product_id, Guid? vareint_id, decimal quantity, decimal discount_percent)
        {
            try
            {
                var im_product = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == product_id);

                var tax_class = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == im_product.tax_class_id);

                var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == vareint_id);


                decimal base_price = (im_varient.base_price ?? 0m) * quantity;
                base_price = Math.Round(base_price, 2, MidpointRounding.AwayFromZero);

                decimal rate_percent = tax_class.rate_percent ?? 0m;

                decimal gst = (rate_percent / 100);
                decimal gst_2 = (rate_percent / 100) + 1;
                gst = Math.Round(gst, 2, MidpointRounding.AwayFromZero);
                gst_2 = Math.Round(gst_2, 2, MidpointRounding.AwayFromZero);


                decimal rate = base_price / gst_2;
                rate = Math.Round(rate, 2, MidpointRounding.AwayFromZero);



                decimal final_gst = rate * gst;
                decimal total_price = rate + final_gst;

                decimal discount = (discount_percent / 100);
                discount = total_price * discount;
                total_price = total_price - discount;

                decimal price_rate = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                total_price = Math.Round(total_price, 2, MidpointRounding.AwayFromZero);
                discount = Math.Round(discount, 1, MidpointRounding.AwayFromZero);

                return new sales_CalculationResult
                {
                    price_rate = price_rate,
                    total_price = total_price,
                    discount_amount = discount,
                    gst = final_gst,
                    success = true
                };
            }
            catch (Exception)
            {
                return new sales_CalculationResult
                {
                    success = false
                };
            }
        }
        public async Task<sales_CalculationResult> Rateplus_tax_CalculationResult(Guid? product_id, Guid? vareint_id, decimal quantity, decimal discount_percent)
        {
            try
            {
                var im_product = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == product_id);

                var tax_class = await _context.tx_TaxClasses.FirstOrDefaultAsync(a => a.tax_class_id == im_product.tax_class_id);

                var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == vareint_id);


                decimal base_price = (im_varient.base_price ?? 0m) * quantity;
                base_price = Math.Round(base_price, 2, MidpointRounding.AwayFromZero);

                decimal rate_percent = tax_class.rate_percent ?? 0m;

                decimal gst = (rate_percent / 100);
                gst = Math.Round(gst, 2, MidpointRounding.AwayFromZero);


                decimal rate = base_price;
                rate = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                gst = base_price * gst;
                gst = Math.Round(gst, 2, MidpointRounding.AwayFromZero);



                decimal total_price = rate + gst;

                decimal discount = (discount_percent / 100);
                discount = total_price * discount;
                total_price = total_price - discount;

                decimal price_rate = Math.Round(rate, 2, MidpointRounding.AwayFromZero);
                total_price = Math.Round(total_price, 2, MidpointRounding.AwayFromZero);
                discount = Math.Round(discount, 1, MidpointRounding.AwayFromZero);

                return new sales_CalculationResult
                {
                    price_rate = price_rate,
                    total_price = total_price,
                    discount_amount = discount,
                    gst = gst,
                    success = true
                };
            }
            catch (Exception)
            {
                return new sales_CalculationResult
                {
                    success = false
                };
            }
        }

        public async Task<ServiceResult<so_SalesHeaders>> Update_sales(Guid salesId, so_SalesHeaders so_SalesHeaders)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (salesId == null)
                {
                    _logger.LogInformation("Not sales_id found");
                    return new ServiceResult<so_SalesHeaders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Not sales_id found"
                    };
                }
                Decimal discount_total = 0m;
                Decimal total_taxable_value = 0m;
                Decimal total_zero_value = 0m;
                Decimal total_exempted_value = 0m;
                var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == so_SalesHeaders.store_id);

                var existing_sal_header = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == salesId);
                existing_sal_header.purchase_order_no = so_SalesHeaders.purchase_order_no;
                existing_sal_header.sales_date = so_SalesHeaders.sales_date;
                existing_sal_header.doc_type = so_SalesHeaders.doc_type;
                existing_sal_header.due_date = so_SalesHeaders.due_date;
                existing_sal_header.tax_percent = so_SalesHeaders.tax_percent;
                existing_sal_header.service_charge_percent = so_SalesHeaders.service_charge_percent;
                existing_sal_header.payment_mode = so_SalesHeaders.payment_mode;
                existing_sal_header.id_card_no = so_SalesHeaders.id_card_no;
                existing_sal_header.age = so_SalesHeaders.age;
                existing_sal_header.quick_customer = so_SalesHeaders.quick_customer;
                existing_sal_header.reference_no = so_SalesHeaders.reference_no;
                existing_sal_header.table_no = so_SalesHeaders.table_no;
                existing_sal_header.number_of_pax = so_SalesHeaders.number_of_pax;
                existing_sal_header.doc_currency_code = so_SalesHeaders.doc_currency_code;
                existing_sal_header.fx_rate_to_base = 1;
                existing_sal_header.fx_rate_date = so_SalesHeaders.fx_rate_date;
                existing_sal_header.balance_base = so_SalesHeaders.balance_base;
                existing_sal_header.fx_source = so_SalesHeaders.fx_source;
                existing_sal_header.sub_total = so_SalesHeaders.sub_total;
                existing_sal_header.discount_total = so_SalesHeaders.discount_total;
                existing_sal_header.service_charge = so_SalesHeaders.service_charge;
                existing_sal_header.tax_total = so_SalesHeaders.tax_total;
                existing_sal_header.grand_total = so_SalesHeaders.grand_total;
                existing_sal_header.total_plastic_bag = so_SalesHeaders.total_plastic_bag;
                existing_sal_header.total_taxable_value = so_SalesHeaders.total_taxable_value;
                existing_sal_header.total_zero_value = so_SalesHeaders.total_zero_value;
                existing_sal_header.total_exempted_value = so_SalesHeaders.total_exempted_base;
                existing_sal_header.total_charge_customer = so_SalesHeaders.total_charge_customer;
                existing_sal_header.total_plastic_bag_tax = so_SalesHeaders.total_plastic_bag * Convert.ToDecimal(st_store.plastic_bag_tax_amount);
                existing_sal_header.sub_total_base = so_SalesHeaders.sub_total_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.discount_total_base = so_SalesHeaders.discount_total_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.tax_total_base = so_SalesHeaders.tax_total_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.grand_total_base = so_SalesHeaders.grand_total_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.total_taxable_base = so_SalesHeaders.total_taxable_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.total_zero_base = so_SalesHeaders.total_zero_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.total_exempted_base = so_SalesHeaders.total_exempted_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.total_charge_customer_base = so_SalesHeaders.total_charge_customer_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.service_charge_base = so_SalesHeaders.service_charge_base * existing_sal_header.fx_rate_to_base;
                existing_sal_header.total_plastic_bag_tax_base = so_SalesHeaders.total_plastic_bag_tax_base;
                existing_sal_header.total_charge_bank_marchant = so_SalesHeaders.total_charge_bank_marchant;
                existing_sal_header.transaction_cost = so_SalesHeaders.transaction_cost;
                existing_sal_header.amount_paid_base = so_SalesHeaders.amount_paid_base;
                existing_sal_header.change_given_base = so_SalesHeaders.change_given_base;
                existing_sal_header.change_given_doc = so_SalesHeaders.change_given_doc;
                existing_sal_header.balance_base = so_SalesHeaders.balance_base;
                existing_sal_header.notes = so_SalesHeaders.notes;
                existing_sal_header.created_at = so_SalesHeaders.created_at;
                existing_sal_header.datetime = DateTime.Now;
                existing_sal_header.created_by = so_SalesHeaders.created_by;
                existing_sal_header.sales_on_hold = so_SalesHeaders.sales_on_hold;
                existing_sal_header.is_mutiple_payment = so_SalesHeaders.is_mutiple_payment;
                existing_sal_header.status = so_SalesHeaders.status;
                if (so_SalesHeaders.doc_type == "QUOTATION")
                {
                    string invoiceNo = existing_sal_header.invoice_no;

                    string number = new string(invoiceNo.Where(char.IsDigit).ToArray());

                    existing_sal_header.quot_no = st_store.default_quote_init + "-" + number;
                }
                else
                {
                    existing_sal_header.quot_no = "";
                }
                foreach (var item in so_SalesHeaders.so_SalesLines)
                {
                    var existing_sales_line = await _context.so_SalesLines.FirstOrDefaultAsync(a => a.sales_line_id == item.sales_line_id);
                    var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == item.variant_id);
                    var item_batch = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.item_batch_id == item.batch_id);
                    var im_store_inv = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == item.store_variant_inventory_id);
                    if (existing_sales_line != null)
                    {

                        existing_sales_line.line_discount_amount = item.line_discount_amount;
                        existing_sales_line.stock_item = item.stock_item;
                        existing_sales_line.consignment_id = item.consignment_id;
                        existing_sales_line.consignment_det_id = item.consignment_det_id;
                        existing_sales_line.consignment_billed = item.consignment_billed; ;
                        existing_sales_line.batch_id = item.batch_id;
                        existing_sales_line.batch_id_int = item_batch?.batch_id;
                        existing_sales_line.batch_name = item_batch?.batch_number;
                        existing_sales_line.expiry_date = item_batch?.expiry_date;
                        existing_sales_line.insurance_code = item.insurance_code;
                        existing_sales_line.doctor_consent = item.doctor_consent;

                        existing_sales_line.quantity = item.quantity;
                        existing_sales_line.unit_price = item.unit_price;
                        existing_sales_line.discount_amount = item.discount_amount;
                        existing_sales_line.discount_percent = item.discount_percent;
                        existing_sales_line.tax_amount = item.tax_amount;
                        existing_sales_line.original_price_base = im_varient?.base_price ?? 0m;
                        existing_sales_line.tax_class = item.tax_class;
                        existing_sales_line.returned_quantity = item.returned_quantity;
                        existing_sales_line.original_quantity = im_store_inv?.on_hand_quantity ?? 0m;
                        existing_sales_line.doc_currency_code = item.doc_currency_code;
                        existing_sales_line.fx_rate_to_base = 1;
                        existing_sales_line.detected_qty = item.quantity;
                        existing_sales_line.line_total = item.quantity * item.unit_price;

                        existing_sales_line.unit_price_base = item.unit_price * item.fx_rate_to_base;
                        existing_sales_line.unit_discount_amount_base = (item.unit_price * (item.discount_percent / 100)) * item.fx_rate_to_base;
                        existing_sales_line.line_total_base = item.quantity * item.unit_price;
                        existing_sales_line.remarks = item.remarks;
                        existing_sales_line.created_at = DateTime.Now;

                        discount_total += item.discount_amount;
                        if (item.tax_class == "TAXABLE")
                        {
                            total_taxable_value += item.unit_price;
                        }
                        else if (item.tax_class == "NO Taxable")
                        {
                            total_zero_value += item.unit_price;
                        }
                        _context.so_SalesLines.Update(existing_sales_line);
                    }
                    else
                    {
                        item.sales_id = so_SalesHeaders.sales_id;
                        item.business_id = so_SalesHeaders.business_id;
                        item.store_id = so_SalesHeaders.store_id;
                        item.product_id = item.product_id;
                        item.variant_id = item.variant_id;
                        item.barcode = im_varient?.barcode;
                        item.product_sku = im_varient?.sku;
                        item.track_expiry = item.track_expiry;
                        item.item_description = item.item_description;
                        item.line_discount_amount = item.line_discount_amount;
                        item.stock_item = item.stock_item;
                        item.consignment_id = item.consignment_id;
                        item.consignment_det_id = item.consignment_det_id;
                        item.consignment_billed = item.consignment_billed; ;
                        item.batch_id = item.batch_id;
                        item.batch_id_int = item_batch?.batch_id;
                        item.batch_name = item_batch?.batch_number;
                        item.expiry_date = item_batch?.expiry_date;
                        item.insurance_code = item.insurance_code;
                        item.doctor_consent = item.doctor_consent;

                        item.quantity = item.quantity;
                        item.unit_price = item.unit_price;
                        item.discount_amount = item.discount_amount;
                        item.discount_percent = item.discount_percent;
                        item.tax_amount = item.tax_amount;
                        item.original_price_base = im_varient?.base_price ?? 0m;
                        item.tax_class = item.tax_class;
                        item.returned_quantity = item.returned_quantity;
                        item.original_quantity = im_store_inv?.on_hand_quantity ?? 0m;
                        item.doc_currency_code = item.doc_currency_code;
                        item.base_currency_code = st_store?.default_currency;
                        item.fx_rate_to_base = 1;
                        item.detected_qty = item.quantity;
                        item.line_total = item.line_total;

                        item.unit_price_base = item.unit_price * item.fx_rate_to_base;
                        item.unit_discount_amount_base = (item.unit_price * (item.discount_percent / 100)) * item.fx_rate_to_base;
                        item.line_total_base = item.quantity * item.unit_price;
                        item.remarks = item.remarks;
                        item.created_at = DateTime.Now;

                        discount_total += item.discount_amount;
                        if (item.tax_class == "TAXABLE")
                        {
                            total_taxable_value += item.unit_price;
                        }
                        else if (item.tax_class == "NO Taxable")
                        {
                            total_zero_value += item.unit_price;
                        }
                        _context.so_SalesLines.Add(item);
                        existing_sal_header.so_SalesLines.Add(item);

                    }


                }
                if (so_SalesHeaders.status == "POSTED" && so_SalesHeaders.doc_type == "SALES")
                {
                    int index = 0;

                    foreach (var so_item in so_SalesHeaders.pos_SalePayments)
                    {
                        so_item.business_id = so_SalesHeaders.business_id;
                        so_item.store_id = so_SalesHeaders.store_id;
                        so_item.sale_id = so_SalesHeaders.sales_id;
                        so_item.payment_method_id = so_SalesHeaders.payment_term_id;
                        so_item.receipt_no = so_SalesHeaders.invoice_no;
                        so_item.line_no = index + 1;
                        so_item.currency_code = so_SalesHeaders.base_currency_code;
                        so_item.fx_rate = 1;
                        so_item.amount = so_item.amount;
                        so_item.base_amount = so_item.base_amount;
                        so_item.reference_no = so_SalesHeaders.reference_no;
                        so_item.notes = so_SalesHeaders.notes;
                        so_item.is_voided = so_item.is_voided;
                        so_item.created_by = so_item.created_by;
                        so_item.voided_at = DateTime.Now;
                        so_item.created_at = DateTime.Now;

                        index++;
                        _context.pos_SalePayments.Add(so_item);

                    }
                }
                existing_sal_header.discount_total = discount_total;
                existing_sal_header.total_taxable_value = total_taxable_value;
                existing_sal_header.total_zero_value = total_zero_value;
                existing_sal_header.discount_total_base = so_SalesHeaders.discount_total * so_SalesHeaders.fx_rate_to_base;
                existing_sal_header.total_taxable_base = so_SalesHeaders.total_taxable_value * so_SalesHeaders.fx_rate_to_base;
                existing_sal_header.total_zero_base = so_SalesHeaders.total_zero_value * so_SalesHeaders.fx_rate_to_base;


                _context.so_SalesHeaders.Update(existing_sal_header);
                await _context.SaveChangesAsync();
                if (so_SalesHeaders.status == "POSTED" && so_SalesHeaders.doc_type == "SALES" || so_SalesHeaders.doc_type == "MARKETPLACE")
                {
                    var so_lines = await _context.so_SalesLines.Where(a => a.sales_id == existing_sal_header.sales_id).ToListAsync();
                    List<im_InventoryTransactions> im_InventoryTransactions1 = new List<im_InventoryTransactions>();
                    foreach (var item in so_lines)
                    {
                        var im_InventoryTransactions = new im_InventoryTransactions()
                        {
                            sales_line_id = item.sales_line_id,
                            store_id = so_SalesHeaders.store_id,
                            variant_id = item.variant_id,
                            trans_type = "SALES",
                            quantity_change = item.quantity,
                            unit_cost = item.unit_price,
                            total_cost = item.line_total,
                            created_date_time = DateTime.Now
                        };

                        im_InventoryTransactions1.Add(im_InventoryTransactions);

                    }
                    _context.im_InventoryTransactions.AddRange(im_InventoryTransactions1);
                    if (so_SalesHeaders.doc_type == "MARKETPLACE" && so_SalesHeaders.status != "HOLD")
                    {
                        var order = await Add_order(so_SalesHeaders?.sales_id, so_SalesHeaders.address_id, so_SalesHeaders.source_id, so_SalesHeaders.urget_delivery);
                        {
                            if (!order.Success)
                            {
                                await transaction.RollbackAsync();

                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Status = 500,
                                    Success = false,
                                    Message = order.Message
                                };
                            }
                        }
                        var Fullfilment = await Add_Fullfilment(order?.Data.customer_order_id, so_SalesHeaders.login_user_id);
                        {
                            if (!Fullfilment.Success)
                            {
                                await transaction.RollbackAsync();

                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Status = 500,
                                    Success = false,
                                    Message = Fullfilment.Message
                                };
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                    if (so_SalesHeaders.status == "POSTED" && so_SalesHeaders.doc_type == "SALE")
                    {
                        if (so_SalesHeaders.payment_mode == "Cash")
                        {
                            var journal = await Add_Journal_header(so_SalesHeaders.sales_id);
                            if (!journal.Success)
                            {
                                await transaction.RollbackAsync();
                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Success = false,
                                    Status = 300,
                                    Message = "Error"
                                };
                            }
                        }

                        if (so_SalesHeaders.payment_mode == "CREDIT")
                        {
                            var journal_credit = await Add_Journal_header_credit(so_SalesHeaders.sales_id);
                            if (!journal_credit.Success)
                            {
                                await transaction.RollbackAsync();
                                return new ServiceResult<so_SalesHeaders>
                                {
                                    Success = false,
                                    Status = 300,
                                    Message = "Error"
                                };
                            }
                        }

                        var spResults = _context.Database.SqlQueryRaw<string>(
                                                             "EXEC dbo.sp_PostSales @sales_id=@sales_id ,@store_id=@store_id",
                                                              new SqlParameter("@sales_id", so_SalesHeaders.sales_id),
                                                              new SqlParameter("@store_id ", st_store.store_id)).AsEnumerable().ToList();
                        var spResponse = spResults.FirstOrDefault();
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                if (so_SalesHeaders.doc_type == "MARKETPLACE" && so_SalesHeaders.status == "HOLD")
                {
                    await _hubContext.Clients.Group(so_SalesHeaders.business_id.ToString()).SendAsync(
                                "MarketplaceSale", new
                                {
                                    salesId = so_SalesHeaders.sales_id,
                                    storeId = so_SalesHeaders.store_id,
                                    total = so_SalesHeaders.grand_total,
                                    items = so_SalesHeaders.so_SalesLines.Select(x => new
                                    {
                                        productId = x.product_id,
                                        variantId = x.variant_id,
                                        qty = x.quantity
                                    })
                                });
                }
                else if (so_SalesHeaders.doc_type == "SALE" && so_SalesHeaders.status == "POSTED")
                {
                    await _hubContext.Clients.Group(so_SalesHeaders.business_id.ToString())
                                        .SendAsync("SaleCreated", new
                                        {
                                            salesId = so_SalesHeaders.sales_id,
                                            storeId = so_SalesHeaders.store_id,
                                            total = so_SalesHeaders.grand_total,
                                            items = so_SalesHeaders.so_SalesLines.Select(x => new
                                            {
                                                productId = x.product_id,
                                                variantId = x.variant_id,
                                                qty = x.quantity
                                            })
                                        });
                }
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Updated",
                    Data = existing_sal_header
                };



            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation("Error While Update_sales(Guid salesId, so_SalesHeaders so_SalesHeaders)");
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false

                };
            }

        }

        public async Task<ServiceResult<List<so_SalesHeaders_dto>>> Get_sales(Guid company_id)
        {
            try
            {
                if (company_id == null)
                {
                    _logger.LogInformation("No company_id found");
                    return new ServiceResult<List<so_SalesHeaders_dto>>
                    {
                        Status = 400,
                        Message = "No company_id found",
                        Success = false
                    };
                }
                var result = await _context.Database.SqlQueryRaw<so_SalesHeaders_dto>(
                    "EXEC dbo.sp_sales_report @opr=@opr, @business_id=@business_id",
                    new SqlParameter("@opr", 10),
                    new SqlParameter("@business_id", company_id)
                ).ToListAsync();

                if (result.Count == 0)
                {
                    return new ServiceResult<List<so_SalesHeaders_dto>>
                    {
                        Status = 300,
                        Message = "No so_SalesHeaders found",
                        Success = false
                    };
                }
                return new ServiceResult<List<so_SalesHeaders_dto>>
                {
                    Success = true,
                    Status = 200,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro while Get_sales company_id");
                return new ServiceResult<List<so_SalesHeaders_dto>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<ServiceResult<so_SalesHeaders>> Get_sales_salesId(Guid salesId)
        {
            try
            {
                if (salesId == null)
                {
                    _logger.LogInformation("No salesId found");
                    return new ServiceResult<so_SalesHeaders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No salesId found"

                    };
                }
                var so_sales = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == salesId);
                if (so_sales == null)
                {
                    return new ServiceResult<so_SalesHeaders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = so_sales
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error While Get_sales_salesId");
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message

                };
            }

        }

        public async Task<ServiceResult<SalesReportResponseDTO>> Get_sales_report_by_date(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<SalesReportResponseDTO>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                if (start_date == null)
                {
                    _logger.LogInformation("Start date or end date not found");
                    return new ServiceResult<SalesReportResponseDTO>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Start date or end date not found"
                    };
                }
                var result_report = await _context.Database.SqlQueryRaw<SalesReportDTO>
                                 (
                                     "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                                     new SqlParameter("@opr", 1),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@StartDate", start_date),
                                     new SqlParameter("@EndDate", end_date)
                                 )
                                 .ToListAsync();
                var result_total = (await _context.Database.SqlQueryRaw<SalesTotalDTO>
                                     (
                                         "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                                         new SqlParameter("@opr", 2),
                                         new SqlParameter("@store_id", store_id),
                                         new SqlParameter("@StartDate", start_date),
                                         new SqlParameter("@EndDate", end_date)
                                     ).ToListAsync())
                                      .FirstOrDefault();
                var result = new SalesReportResponseDTO
                {
                    Totals = result_total,
                    Sales = result_report
                };

                if (result.Sales.Count == 0 || result.Totals == null)
                {
                    return new ServiceResult<SalesReportResponseDTO>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<SalesReportResponseDTO>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<SalesReportResponseDTO>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<ServiceResult<Dictionary<string, List<SalesLineDTO>>>> Get_sales_detailed_by_date(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == Guid.Empty)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<Dictionary<string, List<SalesLineDTO>>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }

                if (start_date == null || end_date == null)
                {
                    _logger.LogInformation("Start date or end date not found");
                    return new ServiceResult<Dictionary<string, List<SalesLineDTO>>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Start date or end date not found"
                    };
                }

                // Execute SP
                var result = await _context.Database.SqlQueryRaw<SalesLineDTO>(
                     "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                     new SqlParameter("@opr", 3),
                     new SqlParameter("@store_id", store_id),
                     new SqlParameter("@StartDate", start_date),
                     new SqlParameter("@EndDate", end_date)
                ).ToListAsync();

                if (result.Count == 0)
                {
                    return new ServiceResult<Dictionary<string, List<SalesLineDTO>>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }

                // Group by payment method
                var grouped = result
                    .GroupBy(a => a.payment_method)
                    .ToDictionary(g => g.Key, g => g.ToList());

                return new ServiceResult<Dictionary<string, List<SalesLineDTO>>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = grouped
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Get_sales_detailed_by_date");
                return new ServiceResult<Dictionary<string, List<SalesLineDTO>>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<DailySalesSummaryDto>>> Get_sales_detailed_by_day_report(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<List<DailySalesSummaryDto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                if (start_date == null)
                {
                    _logger.LogInformation("Start date or end date not found");
                    return new ServiceResult<List<DailySalesSummaryDto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Start date or end date not found"
                    };
                }
                var result_report = await _context.Database.SqlQueryRaw<DailySalesSummaryDto>
                                 (
                                     "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                                     new SqlParameter("@opr", 4),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@StartDate", start_date),
                                     new SqlParameter("@EndDate", end_date)
                                 )
                                 .ToListAsync();


                if (result_report.Count == 0)
                {
                    return new ServiceResult<List<DailySalesSummaryDto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<DailySalesSummaryDto>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = result_report
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<List<DailySalesSummaryDto>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<ServiceResult<Dictionary<string, List<CustomerSalesDetailDto>>>> Get_sales_detailed_by_customer(Guid store_id, string? customer, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == Guid.Empty)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<Dictionary<string, List<CustomerSalesDetailDto>>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }



                // Execute SP
                var result = await _context.Database.SqlQueryRaw<CustomerSalesDetailDto>(
                     "EXEC dbo.sp_sales_report @opr=@opr, @store_id=@store_id, @CustomerName=@CustomerName, @StartDate=@StartDate, @EndDate=@EndDate",
                     new SqlParameter("@opr", 5),
                     new SqlParameter("@store_id", store_id),
                     new SqlParameter("@CustomerName", string.IsNullOrWhiteSpace(customer) ? (object)DBNull.Value : customer),
                     new SqlParameter("@StartDate", start_date.HasValue ? (object)start_date.Value : DBNull.Value),
                     new SqlParameter("@EndDate", end_date.HasValue ? (object)end_date.Value : DBNull.Value)
                ).ToListAsync();


                if (result.Count == 0)
                {
                    return new ServiceResult<Dictionary<string, List<CustomerSalesDetailDto>>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }

                // Group by payment method
                var grouped = result
                    .GroupBy(a => a.payment_method)
                    .ToDictionary(g => g.Key, g => g.ToList());

                return new ServiceResult<Dictionary<string, List<CustomerSalesDetailDto>>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = grouped
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Get_sales_detailed_by_date");
                return new ServiceResult<Dictionary<string, List<CustomerSalesDetailDto>>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<ProductSalesDetailDto>>> Get_sales_detailed_by_product(Guid store_id, string? ProductSku, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<List<ProductSalesDetailDto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }

                var result_report = await _context.Database.SqlQueryRaw<ProductSalesDetailDto>
                                 (
                                     "EXEC dbo.sp_sales_report @opr=@opr, @store_id=@store_id,@ProductSku=@ProductSku, @StartDate=@StartDate, @EndDate=@EndDate",
                                     new SqlParameter("@opr", 6),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@ProductSku", string.IsNullOrWhiteSpace(ProductSku) ? (object)DBNull.Value : ProductSku),
                                     new SqlParameter("@StartDate", start_date.HasValue ? (object)start_date.Value : DBNull.Value),
                                     new SqlParameter("@EndDate", end_date.HasValue ? (object)end_date.Value : DBNull.Value)

                                 )
                                 .ToListAsync();


                if (result_report.Count == 0)
                {
                    return new ServiceResult<List<ProductSalesDetailDto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<ProductSalesDetailDto>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = result_report
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<List<ProductSalesDetailDto>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<ServiceResult<List<TaxClassReportDTO>>> Get_sales_tax_report(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<List<TaxClassReportDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                if (start_date == null)
                {
                    _logger.LogInformation("Start date or end date not found");
                    return new ServiceResult<List<TaxClassReportDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Start date or end date not found"
                    };
                }
                var result_report = await _context.Database.SqlQueryRaw<TaxClassReportDTO>
                                 (
                                     "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                                     new SqlParameter("@opr", 7),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@StartDate", start_date),
                                     new SqlParameter("@EndDate", end_date)
                                 )
                                 .ToListAsync();


                if (result_report.Count == 0)
                {
                    return new ServiceResult<List<TaxClassReportDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<TaxClassReportDTO>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = result_report
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<List<TaxClassReportDTO>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<ServiceResult<List<OutstandingSalesDTO>>> Get_sales_out_standing(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<List<OutstandingSalesDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                if (start_date == null)
                {
                    _logger.LogInformation("Start date or end date not found");
                    return new ServiceResult<List<OutstandingSalesDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Start date or end date not found"
                    };
                }
                var result_report = await _context.Database.SqlQueryRaw<OutstandingSalesDTO>
                                 (
                                     "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                                     new SqlParameter("@opr", 8),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@StartDate", start_date),
                                     new SqlParameter("@EndDate", end_date)
                                 )
                                 .ToListAsync();


                if (result_report.Count == 0)
                {
                    return new ServiceResult<List<OutstandingSalesDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<OutstandingSalesDTO>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = result_report
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<List<OutstandingSalesDTO>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<ServiceResult<List<HourlySalesReportDTO>>> Get_sales_hourly_base(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            try
            {
                if (store_id == null)
                {
                    _logger.LogInformation("No store_id found");
                    return new ServiceResult<List<HourlySalesReportDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No store_id found"
                    };
                }
                if (start_date == null)
                {
                    _logger.LogInformation("Start date or end date not found");
                    return new ServiceResult<List<HourlySalesReportDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Start date or end date not found"
                    };
                }
                var result_report = await _context.Database.SqlQueryRaw<HourlySalesReportDTO>
                                 (
                                     "EXEC dbo.sp_sales_report @opr, @store_id, @StartDate, @EndDate",
                                     new SqlParameter("@opr", 9),
                                     new SqlParameter("@store_id", store_id),
                                     new SqlParameter("@StartDate", start_date),
                                     new SqlParameter("@EndDate", end_date)
                                 )
                                 .ToListAsync();


                if (result_report.Count == 0)
                {
                    return new ServiceResult<List<HourlySalesReportDTO>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<HourlySalesReportDTO>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = result_report
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Get_sales_report_by_date");
                return new ServiceResult<List<HourlySalesReportDTO>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<ServiceResult<so_SalesHeaders>> Add_sales_return(Guid salesId, so_SalesHeaders so_SalesHeaders)
        {
            var transactio = await _context.Database.BeginTransactionAsync();

            try
            {
                var existing_sales = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == salesId);
                if (existing_sales == null)
                {
                    _logger.LogInformation("No sales found with the provided salesId");
                    return new ServiceResult<so_SalesHeaders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No sales found with the provided salesId"
                    };
                }
                var table = "so_SalesReturnLines";
                var table_key = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == so_SalesHeaders.business_id);
                var key = Convert.ToInt16(table_key.next_key);
                var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == so_SalesHeaders.store_id);

                so_SalesReturnHeaders so_SalesReturnHeaders = new so_SalesReturnHeaders();
                //so_SalesReturnLines so_SalesReturnLines1 = new so_SalesReturnLines();
                List<so_SalesReturnLines> so_SalesReturnLines = new List<so_SalesReturnLines>();
                pos_ReturnsalePayments pos_ReturnsalePayments = new pos_ReturnsalePayments();
                bool is_retunqty = false;
                bool is_inserted = false;
                Guid? sales_line_id = null;
                var existing_grand_total = existing_sales.grand_total;
                existing_sales.tax_percent = so_SalesHeaders.tax_percent;
                existing_sales.service_charge_percent = so_SalesHeaders.service_charge_percent;
                existing_sales.sub_total = so_SalesHeaders.sub_total;
                existing_sales.tax_total = so_SalesHeaders.tax_total;
                existing_sales.grand_total = so_SalesHeaders.grand_total;
                existing_sales.tax_total_base = so_SalesHeaders.tax_total_base;
                existing_sales.grand_total_base = so_SalesHeaders.grand_total_base;
                existing_sales.amount_paid_base = so_SalesHeaders.amount_paid_base;
                foreach (var item in so_SalesHeaders.so_SalesLines)
                {
                    var sales_line = existing_sales.so_SalesLines.FirstOrDefault(a => a.sales_line_id == item.sales_line_id);
                    if (sales_line != null)
                    {
                        var st_store_inv = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == item.store_variant_inventory_id);
                        var item_batches = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.item_batch_id == item.batch_id);
                        var po_payment = await _context.pos_SalePayments.Where(a => a.sale_id == sales_line.sales_id).ToListAsync();
                        sales_line.quantity = item.quantity - item.return_qty;
                        sales_line.unit_price = item.unit_price;
                        sales_line.tax_amount = item.tax_amount;
                        sales_line.unit_price_base = item.unit_price_base;
                        sales_line.tax_amount_base = item.tax_amount_base;
                        sales_line.line_total_base = item.line_total_base;
                        sales_line.remarks = item.remarks;
                        sales_line.line_total = item.line_total;
                        sales_line.return_qty = item.return_qty;
                        if (sales_line.return_qty != 0)
                        {
                            if (st_store_inv != null)
                            {
                                st_store_inv.on_hand_quantity += sales_line.return_qty;
                                _context.im_StoreVariantInventory.Update(st_store_inv);
                            }
                            if (item_batches != null)
                            {
                                item_batches.on_hand_quantity += sales_line.return_qty;
                                _context.im_itemBatches.Update(item_batches);
                            }
                            if (po_payment.Count > 0)
                            {
                                foreach (var payment in po_payment)
                                {
                                    var pos_sub = await _context.pos_SalePayments.FirstOrDefaultAsync(a => a.sale_payment_id == payment.sale_payment_id);
                                    if (pos_sub != null)
                                    {
                                        pos_sub.amount = pos_sub.amount - existing_grand_total;
                                        _context.pos_SalePayments.Update(pos_sub);
                                    }
                                    if (pos_sub != null)
                                    {
                                        pos_ReturnsalePayments.business_id = pos_sub.business_id;
                                        pos_ReturnsalePayments.store_id = pos_sub.store_id;
                                        pos_ReturnsalePayments.payment_method_id = so_SalesReturnHeaders.payment_term_id;
                                        pos_ReturnsalePayments.terminal_id = pos_sub.terminal_id;
                                        pos_ReturnsalePayments.shift_id = pos_sub.shift_id;
                                        pos_ReturnsalePayments.drawer_session_id = pos_sub.drawer_session_id;
                                        pos_ReturnsalePayments.amount = pos_sub.amount;
                                        pos_ReturnsalePayments.base_amount = pos_sub.base_amount;
                                        pos_ReturnsalePayments.change_given = pos_sub.change_given;
                                        pos_ReturnsalePayments.reference_no = pos_sub.reference_no;
                                        pos_ReturnsalePayments.notes = pos_sub.notes;
                                        pos_ReturnsalePayments.is_voided = pos_sub.is_voided;
                                        pos_ReturnsalePayments.voided_by = pos_sub.voided_by;
                                        pos_ReturnsalePayments.currency_code = pos_sub.currency_code;
                                        _context.pos_ReturnsalePayments.Add(pos_ReturnsalePayments);

                                    }


                                }
                            }

                            is_retunqty = true;
                            is_inserted = true;
                        }

                        sales_line_id = sales_line.sales_line_id;
                        _context.so_SalesLines.Update(sales_line);
                    }

                }
                if (is_retunqty)
                {
                    if (is_inserted)
                    {
                        so_SalesReturnHeaders.sales_return_id = Guid.CreateVersion7();
                        so_SalesReturnHeaders.sales_id = salesId;
                        so_SalesReturnHeaders.business_id = existing_sales.business_id;
                        so_SalesReturnHeaders.store_id = existing_sales.store_id;
                        so_SalesReturnHeaders.customer_id = existing_sales.customer_id;
                        so_SalesReturnHeaders.return_no = st_store?.default_invoice_init + "-" + Convert.ToString(key + 1);
                        so_SalesReturnHeaders.return_date = DateTime.Now;
                        so_SalesReturnHeaders.doc_type = existing_sales.doc_type;
                        so_SalesReturnHeaders.return_type = "RETURN";
                        so_SalesReturnHeaders.return_reason = so_SalesHeaders.notes;
                        so_SalesReturnHeaders.doc_currency_code = existing_sales.doc_currency_code;
                        so_SalesReturnHeaders.base_currency_code = existing_sales.base_currency_code;
                        so_SalesReturnHeaders.fx_rate_to_base = existing_sales.fx_rate_to_base;
                        so_SalesReturnHeaders.sub_total = so_SalesHeaders.sub_total;
                        so_SalesReturnHeaders.discount_total = so_SalesHeaders.discount_total;
                        so_SalesReturnHeaders.tax_total = so_SalesHeaders.tax_total;
                        so_SalesReturnHeaders.grand_total = so_SalesHeaders.grand_total;
                        so_SalesReturnHeaders.sub_total_base = so_SalesHeaders.sub_total_base;
                        so_SalesReturnHeaders.discount_total_base = so_SalesHeaders.discount_total_base;
                        so_SalesReturnHeaders.tax_total_base = so_SalesHeaders.tax_total_base;
                        so_SalesReturnHeaders.created_at = DateTime.Now;
                        is_inserted = false;
                    }

                    foreach (var item_2 in so_SalesHeaders.so_SalesLines)
                    {
                        if (item_2.return_qty != 0)
                        {
                            var so_SalesReturnLines1 = new so_SalesReturnLines();

                            so_SalesReturnLines1.sales_return_line_id = Guid.CreateVersion7();
                            so_SalesReturnLines1.sales_return_id = so_SalesReturnHeaders.sales_return_id;
                            so_SalesReturnLines1.business_id = existing_sales.business_id;
                            so_SalesReturnLines1.store_id = existing_sales.store_id;
                            so_SalesReturnLines1.product_id = item_2.product_id;
                            so_SalesReturnLines1.variant_id = item_2.variant_id;
                            so_SalesReturnLines1.store_variant_inventory_id = item_2.store_variant_inventory_id;
                            so_SalesReturnLines1.batch_id = item_2.batch_id;
                            so_SalesReturnLines1.barcode = item_2.barcode;
                            so_SalesReturnLines1.product_sku = item_2.product_sku;
                            so_SalesReturnLines1.track_expiry = item_2.track_expiry;
                            so_SalesReturnLines1.item_description = item_2.item_description;
                            so_SalesReturnLines1.return_qty = item_2.return_qty;
                            so_SalesReturnLines1.unit_price = item_2.unit_price;
                            so_SalesReturnLines1.discount_amount = item_2.discount_amount;
                            so_SalesReturnLines1.tax_amount = item_2.tax_amount;
                            so_SalesReturnLines1.line_total = item_2.line_total;
                            so_SalesReturnLines1.unit_price_base = item_2.unit_price_base;
                            so_SalesReturnLines1.discount_amount_base = item_2.discount_amount_base;
                            so_SalesReturnLines1.tax_amount_base = item_2.tax_amount_base;
                            so_SalesReturnLines1.line_total_base = item_2.line_total_base;
                            so_SalesReturnLines1.return_reason = item_2.remarks;
                            so_SalesReturnLines.Add(so_SalesReturnLines1);
                        }

                        so_SalesReturnHeaders.so_SalesReturnLines = so_SalesReturnLines;
                    }
                    if (existing_sales.payment_mode == "Cash")
                    {
                        var Sales_Return = await Add_Journal_header_Sales_Return(so_SalesReturnHeaders.sales_return_id);
                        if (!Sales_Return.Success)
                        {
                            await transactio.RollbackAsync();
                            return new ServiceResult<so_SalesHeaders>
                            {
                                Status = 300,
                                Success = false,
                                Message = "Error while Add_Journal_header_Sales_Return"
                            };
                        }

                    }
                    else if (existing_sales.payment_mode == "CREDIT")
                    {
                        var Sales_Return = await Add_Journal_header_Sales_Return_credit(so_SalesReturnHeaders.sales_return_id);
                        if (!Sales_Return.Success)
                        {
                            await transactio.RollbackAsync();
                            return new ServiceResult<so_SalesHeaders>
                            {
                                Status = 300,
                                Success = false,
                                Message = "Error while Add_Journal_header_Sales_Return_credit"
                            };
                        }
                    }
                    
                    if (table_key != null)
                    {
                        table_key.next_key = key + 1;
                        _context.am_table_next_key.Update(table_key);
                        await _context.SaveChangesAsync();
                    }
                    await _context.so_SalesReturnHeaders.AddAsync(so_SalesReturnHeaders);
                }
                await _context.SaveChangesAsync();
                await transactio.CommitAsync();

                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = so_SalesHeaders
                };
            }
            catch (Exception ex)
            {
                await transactio.RollbackAsync();
                _logger.LogInformation("Error while Add_sales_return");
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<om_CustomerOrders>> Add_order(Guid? salesId, Guid? address_id, Guid? source_id, string urget_delivery)
        {
            try
            {
                var existing_sales = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == salesId);
                if (existing_sales == null)
                {
                    return new ServiceResult<om_CustomerOrders>
                    {
                        Status = 300,
                        Success = false,
                    };
                }
                var table = "om_CustomerOrders";
                var table_key = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == existing_sales.business_id);
                var key = Convert.ToInt16(table_key.next_key);
                var ar_customer = await _context.ar_Customers.FirstOrDefaultAsync(a => a.customer_id == existing_sales.customer_id);
                om_OrderStatusHistory history = new om_OrderStatusHistory();
                om_CustomerOrders om_Customer = new om_CustomerOrders();
                List<om_CustomerOrderLines> om_CustomerOrderLines = new List<om_CustomerOrderLines>();
                om_Customer.customer_order_id = Guid.CreateVersion7();
                om_Customer.business_id = existing_sales.business_id ?? Guid.Empty;
                om_Customer.store_id = existing_sales.store_id;
                om_Customer.sales_id = existing_sales.sales_id ?? Guid.Empty;
                om_Customer.source_id = source_id;
                om_Customer.order_no = key + 1;
                om_Customer.customer_id = existing_sales.customer_id;
                om_Customer.party_id = ar_customer?.party_id;
                om_Customer.order_reference_no = existing_sales.reference_no;
                om_Customer.order_date = DateTime.Now;
                om_Customer.expected_payment_method = om_Customer.expected_payment_method;
                om_Customer.payment_status = "UNPAID";
                om_Customer.urget_delivery = urget_delivery ?? "F";
                om_Customer.order_status = "NEW";
                om_Customer.fulfillment_status = "PENDING";
                om_Customer.delivery_status = "PENDING";
                om_Customer.currency_code = existing_sales.doc_currency_code;
                om_Customer.exchange_rate = existing_sales.change_given_doc;
                om_Customer.sub_total = existing_sales.sub_total;
                om_Customer.discount_amount = existing_sales.discount_total;
                om_Customer.tax_amount = existing_sales.tax_total;
                om_Customer.other_charges = 0;
                var mk_address = await _context.mk_customer_addresses.FirstOrDefaultAsync(a => a.address_id == address_id);
                var zones = await _context.mk_business_zones.FirstOrDefaultAsync(a => a.zone_id == mk_address.zone_id);
                om_Customer.delivery_contact_name = mk_address.contact_name;
                om_Customer.delivery_contact_no = mk_address.contact_phone;
                om_Customer.delivery_address1 = mk_address.address_line1;
                om_Customer.delivery_area = mk_address.Land_mark;
                om_Customer.delivery_postal_code = mk_address.postal_code;
                om_Customer.delivery_latitude = mk_address.latitude;
                om_Customer.delivery_longitude = mk_address.longitude;
                om_Customer.delevery_end_time = mk_address.delevery_end_time;
                om_Customer.delevery_start_time = mk_address.delevery_start_time;
                om_Customer.delevery_date = mk_address.delevery_date;
                om_Customer.delivery_city = mk_address.city;
                om_Customer.created_by = existing_sales.created_by;
                om_Customer.created_user_id = existing_sales.created_user_id;
                om_Customer.zone_name = zones?.zone_name;
                om_Customer.confirmed_at = DateTime.Now;
                om_Customer.created_at = DateTime.Now;
                foreach (var item in existing_sales.so_SalesLines)
                {
                    int i = 0;
                    im_InventoryReservations im_Inventory = new im_InventoryReservations();
                    om_CustomerOrderLines lines = new om_CustomerOrderLines();
                    var store_varient = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == item.store_variant_inventory_id);
                    if (store_varient != null)
                    {
                        store_varient.committed_quantity += item.quantity;
                        _context.im_StoreVariantInventory.Update(store_varient);
                    }
                    lines.customer_order_line_id = Guid.CreateVersion7();
                    lines.customer_order_id = om_Customer.customer_order_id;
                    lines.line_no += i;
                    lines.product_id = item.product_id ?? Guid.Empty;
                    lines.variant_id = item.variant_id ?? Guid.Empty;
                    lines.store_variant_inventory_id = item.store_variant_inventory_id;
                    lines.batch_id = item.batch_id;
                    //lines.uom_id = Guid.Empty;
                    lines.ordered_qty = item.quantity;
                    lines.reserved_qty = item.quantity;
                    lines.picked_qty = item.quantity;
                    lines.packed_qty = item.quantity;
                    lines.dispatched_qty = item.quantity;
                    lines.delivered_qty = item.quantity;
                    lines.reserved_qty = 0;
                    lines.cancelled_qty = 0;
                    lines.unit_price = item.unit_price;
                    lines.discount_amount = item.discount_amount;
                    lines.tax_amount = item.tax_amount;
                    lines.line_total = item.line_total;
                    lines.created_at = DateTime.Now;
                    lines.updated_at = DateTime.Now;
                    lines.line_status = "OPEN";
                    i++;

                    im_Inventory.reservation_id = Guid.CreateVersion7();
                    im_Inventory.business_id = item.business_id ?? Guid.Empty;
                    im_Inventory.store_id = item.store_id;
                    im_Inventory.customer_order_id = lines.customer_order_id;
                    im_Inventory.customer_order_line_id = lines.customer_order_line_id;
                    im_Inventory.product_id = lines.product_id;
                    im_Inventory.variant_id = lines.variant_id;
                    im_Inventory.batch_id = lines.batch_id;
                    im_Inventory.reserved_qty = lines.reserved_qty;
                    im_Inventory.released_qty = 0;
                    im_Inventory.consumed_qty = 0;
                    im_Inventory.reserved_at = DateTime.Now;
                    im_Inventory.remarks = "";
                    im_Inventory.created_at = DateTime.Now;
                    im_Inventory.created_by = om_Customer.created_by;
                    im_Inventory.created_user_id = om_Customer.created_user_id;
                    im_Inventory.reservation_status = "ACTIVE";
                    _context.im_InventoryReservations.Add(im_Inventory);
                    _context.om_CustomerOrderLines.Add(lines);
                    om_CustomerOrderLines.Add(lines);
                }

                history.order_status_history_id = Guid.CreateVersion7();
                history.customer_order_id = om_Customer.customer_order_id;
                history.old_status = "PENDING";
                history.new_status = "";
                history.status_type = "PENDING";
                history.changed_by = Guid.Empty;
                history.changed_at = DateTime.Now;
                _context.om_OrderStatusHistories.Add(history);

                om_Customer.om_CustomerOrderLines = om_CustomerOrderLines;
                _context.om_CustomerOrders.Add(om_Customer);
                if (table_key != null)
                {
                    table_key.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key);
                }
                await _context.SaveChangesAsync();

                return new ServiceResult<om_CustomerOrders>
                {
                    Status = 200,
                    Success = true,
                    Data = om_Customer
                };




            }
            catch (Exception ex)
            {
                return new ServiceResult<om_CustomerOrders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<om_FulfillmentOrders>> Add_Fullfilment(Guid? customer_order_id, Guid? login_user_id)
        {
            try
            {

                if (customer_order_id == null)
                {
                    return new ServiceResult<om_FulfillmentOrders>
                    {
                        Status = 300,
                        Success = false
                    };
                }
                var existing = await _context.om_CustomerOrders.Include(a => a.om_CustomerOrderLines).FirstOrDefaultAsync(a => a.customer_order_id == customer_order_id);
                if (existing == null)
                {
                    return new ServiceResult<om_FulfillmentOrders>
                    {
                        Status = 300,
                        Success = false
                    };
                }
                var table = "om_FulfillmentOrders";
                var table_key = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == existing.business_id);
                var key = Convert.ToInt16(table_key.next_key);
                Decimal ordered_qty = 0;
                var am_user = await _context.am_users.FirstOrDefaultAsync(a => a.userId == login_user_id);
                om_FulfillmentOrders fulfillmentOrders = new om_FulfillmentOrders();
                List<om_FulfillmentLines> om_FulfillmentLines1 = new List<om_FulfillmentLines>();
                fulfillmentOrders.fulfillment_id = Guid.CreateVersion7();
                fulfillmentOrders.business_id = existing.business_id;
                fulfillmentOrders.store_id = existing.store_id;
                fulfillmentOrders.customer_order_id = existing.customer_order_id;
                fulfillmentOrders.created_by = am_user.fullName;
                fulfillmentOrders.created_user_id = am_user.userId;
                //fulfillmentOrders.grand_total = existing.grand_total;
                fulfillmentOrders.fulfillment_no = key + 1;
                fulfillmentOrders.total_ordered_qty = 0;
                fulfillmentOrders.total_delivered_qty = 0;
                fulfillmentOrders.total_rejected_qty = 0;
                fulfillmentOrders.total_reserved_qty = 0;
                fulfillmentOrders.total_returned_qty = 0;
                fulfillmentOrders.collected_amount = 0;
                fulfillmentOrders.created_at = DateTime.Now;
                fulfillmentOrders.fulfillment_status = "PENDING";
                foreach (var item in existing.om_CustomerOrderLines)
                {
                    int i = 0;
                    om_FulfillmentLines om_FulfillmentLines = new om_FulfillmentLines();
                    om_FulfillmentLines.fulfillment_line_id = Guid.CreateVersion7();
                    om_FulfillmentLines.fulfillment_id = fulfillmentOrders.fulfillment_id;
                    om_FulfillmentLines.customer_order_line_id = item.customer_order_line_id;
                    om_FulfillmentLines.product_id = item.product_id;
                    om_FulfillmentLines.variant_id = item.variant_id;
                    om_FulfillmentLines.store_variant_inventory_id = item.store_variant_inventory_id;
                    om_FulfillmentLines.batch_id = item.batch_id;
                    om_FulfillmentLines.line_no = i;
                    om_FulfillmentLines.ordered_qty = item.ordered_qty;
                    ordered_qty += om_FulfillmentLines.ordered_qty;
                    om_FulfillmentLines.reserved_qty = item.reserved_qty;
                    om_FulfillmentLines.packed_qty = item.ordered_qty;
                    om_FulfillmentLines.delivered_qty = 0;
                    om_FulfillmentLines.returned_qty = 0;
                    om_FulfillmentLines.rejected_qty = 0;
                    om_FulfillmentLines.remarks = "";
                    om_FulfillmentLines.line_status = "PENDING";
                    om_FulfillmentLines1.Add(om_FulfillmentLines);

                    i++;
                }
                fulfillmentOrders.total_ordered_qty = ordered_qty;
                fulfillmentOrders.om_FulfillmentLines = om_FulfillmentLines1;
                _context.om_FulfillmentOrders.Add(fulfillmentOrders);
                if (table_key != null)
                {
                    table_key.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key);
                }
                await _context.SaveChangesAsync();

                return new ServiceResult<om_FulfillmentOrders>
                {
                    Status = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<om_FulfillmentOrders>
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message
                };
            }


        }

        //This function Adding sales cash
        //Insert Account Cash on Hand && Tax Payable
        public async Task<ServiceResult<gl_JournalHeaders>> Add_Journal_header(Guid? sales_id)
        {
            gl_JournalHeaders gl_Journal_Header = new gl_JournalHeaders();
            List<gl_JournalLines> gl_Journal_Lines_List = new List<gl_JournalLines>();
            try
            {
                Decimal ToalCedit = 0;
                Decimal ToalDebit = 0;
                Decimal ToalDebit_CH = 0;
                Decimal ToalCedit_TX = 0;
                bool isTaxInserted = false;
                var existing_list = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == sales_id);
                var year = DateTime.Now.Year;

                var table = "gl_JournalHeaders";
                var table_key_ = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == existing_list.business_id);
                var key = Convert.ToInt16(table_key_.next_key);


                gl_Journal_Header.JournalId = Guid.CreateVersion7();
                gl_Journal_Header.BusinessId = existing_list?.business_id ?? Guid.Empty;
                gl_Journal_Header.JournalDate = DateTime.Now;
                gl_Journal_Header.PostingDate = DateTime.Now;
                gl_Journal_Header.JournalNo = "JE" + year + "-" + Convert.ToString(key + 1); ;
                gl_Journal_Header.SourceId = existing_list.sales_id;
                gl_Journal_Header.SourceType = "POS";
                gl_Journal_Header.JournalMemo = $"Journal entry for Sales order {existing_list.sales_no}";
                gl_Journal_Header.Status = "POSTED";
                gl_Journal_Header.CreatedAt = DateTime.Now;
                gl_Journal_Header.CreatedBy = "";
                gl_Journal_Header.BaseCurrencyCode = existing_list.doc_currency_code;
                gl_Journal_Header.ExchangeRate = existing_list.fx_rate_to_base;
                gl_Journal_Header.IsSystemGenerated = true;
                gl_Journal_Header.PostedAt = DateTime.Now;
                gl_Journal_Header.PostedBy = existing_list.created_by;
                gl_Journal_Header.ReferenceNo = existing_list.invoice_no;
                gl_Journal_Header.Remarks = $"Journal entry for Sales order {existing_list.sales_no} with invoice number {existing_list.invoice_no}";
                gl_Journal_Header.StoreId = existing_list.store_id;
                //gl_Journal_Header.TotalCreditBC = existing_list.grand_total;
                //gl_Journal_Header.TotalCreditFC = existing_list.grand_total;
                gl_Journal_Header.TotalDebitBC = existing_list.grand_total;
                gl_Journal_Header.TotalDebitFC = existing_list.grand_total;
                foreach (var item in existing_list.so_SalesLines)
                {
                    gl_JournalLines gl_Journal_Lines = new gl_JournalLines();

                    if (item.tax_amount > 0)
                    {
                        var gl_account = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Tax Payable" && a.CompanyId == existing_list.business_id);
                        var current_balance = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account.GlAccountId);
                        var gl_account_2 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Cash on Hand" && a.CompanyId == existing_list.business_id);
                        var current_balance_2 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_2.GlAccountId);

                        gl_Journal_Lines.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines.StoreId = existing_list.store_id;
                        gl_Journal_Lines.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        //gl_Journal_Lines.Description = item.Product_title;
                        ToalDebit_CH = Convert.ToDecimal(item.line_total);
                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.DebitAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.DebitAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines.SourceLineId = item.sales_line_id;
                        gl_Journal_Lines.SourceType = "Input Tax";
                        //gl_Journal_Lines.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger = new gl_Ledger();
                        gl_Ledger.LedgerId = Guid.CreateVersion7();
                        gl_Ledger.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger.StoreId = existing_list.store_id;
                        gl_Ledger.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger.JournalLineId = gl_Journal_Lines.JournalLineId;
                        gl_Ledger.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        gl_Ledger.PostingDate = DateTime.Now;
                        gl_Ledger.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger.CreatedBy = existing_list.created_by;
                        gl_Ledger.Module = "PURCHASE";
                        gl_Ledger.Description = $"Ledger entry for Sales order {existing_list.sales_no} with invoice number {existing_list.invoice_no}";
                        gl_Ledger.CreatedAt = DateTime.Now;
                        gl_Ledger.CreatedBy = "";
                        gl_Ledger.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Ledger.SourceId = existing_list.sales_id;
                        gl_Ledger.SourceType = "Input Tax";
                        gl_Ledger.SourceLineId = item.sales_line_id;
                        gl_Ledger.TransactionDate = DateTime.Now;
                        gl_Ledger.UpdatedAt = DateTime.Now;
                        gl_Ledger.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger.DebitAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.DebitAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger);

                        _context.gl_JournalLines.Add(gl_Journal_Lines);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines);

                        ToalCedit_TX = Convert.ToDecimal(existing_list.tax_total);

                        if (current_balance != null)
                        {
                            current_balance.CurrentBalance += ToalCedit_TX;
                            _context.gl_AccountCurrentBalances.Update(current_balance);
                        }
                        if (current_balance_2 != null)
                        {
                            current_balance_2.CurrentBalance += ToalDebit_CH;
                            _context.gl_AccountCurrentBalances.Update(current_balance_2);
                        }
                        ToalDebit_CH = 0;
                        ToalCedit_TX = 0;
                    }
                    if (item.tax_amount == 0)
                    {
                        var gl_account_3 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Cash on Hand" && a.CompanyId == existing_list.business_id);
                        var current_balance_3 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_3.GlAccountId);

                        gl_JournalLines gl_Journal_Lines2 = new gl_JournalLines();

                        gl_Journal_Lines2.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines2.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines2.StoreId = existing_list.store_id;
                        gl_Journal_Lines2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Journal_Lines2.DebitAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Journal_Lines2.DebitAmountFC = Convert.ToDecimal(item.line_total);
                        ToalDebit_CH = Convert.ToDecimal(item.line_total);
                        //gl_Journal_Lines2.Description = item.Product_title;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines2.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines2.SourceLineId = item.sales_line_id;
                        gl_Journal_Lines2.SourceType = "Cash on Hand";
                        //gl_Journal_Lines2.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger2 = new gl_Ledger();
                        gl_Ledger2.LedgerId = Guid.CreateVersion7();
                        gl_Ledger2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger2.StoreId = existing_list.store_id;
                        gl_Ledger2.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger2.JournalLineId = gl_Journal_Lines2.JournalLineId;
                        gl_Ledger2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Ledger2.PostingDate = DateTime.Now;
                        gl_Ledger2.CreatedBy = existing_list.created_by;
                        gl_Ledger2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger2.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger2.Module = "POS";
                        gl_Ledger2.Description = $"Ledger entry for Sales order {existing_list.sales_no} with invoice number {existing_list.invoice_no}";
                        gl_Ledger2.CreatedAt = DateTime.Now;
                        gl_Ledger2.CreatedBy = "";
                        gl_Ledger2.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Ledger2.SourceId = existing_list.source_id;
                        gl_Ledger2.SourceType = "Cash on Hands";
                        gl_Ledger2.SourceLineId = item.sales_line_id;
                        gl_Ledger2.TransactionDate = DateTime.Now;
                        gl_Ledger2.UpdatedAt = DateTime.Now;
                        gl_Ledger2.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger2.DebitAmountFC = Convert.ToDecimal(item.line_total);
                        gl_Ledger2.DebitAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Ledger2.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger2);

                        _context.gl_JournalLines.Add(gl_Journal_Lines2);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines2);

                        if (current_balance_3 != null)
                        {
                            current_balance_3.CurrentBalance += ToalDebit_CH;
                            _context.gl_AccountCurrentBalances.Update(current_balance_3);
                        }
                    }

                }


                if (table != null)
                {
                    table_key_.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key_);
                }
                gl_Journal_Header.JournalLines = gl_Journal_Lines_List;
                _context.gl_JournalHeaders.Add(gl_Journal_Header);
                await _context.SaveChangesAsync();
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Journal header added successfully",
                    Data = gl_Journal_Header
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Add_Journal_header");
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        //this function Adding sales credit 
        //Insert Account Receivable (A/R) && Tax Payable
        //Update ar_customer curren_balance
        public async Task<ServiceResult<gl_JournalHeaders>> Add_Journal_header_credit(Guid? sales_id)
        {
            gl_JournalHeaders gl_Journal_Header = new gl_JournalHeaders();
            List<gl_JournalLines> gl_Journal_Lines_List = new List<gl_JournalLines>();
            try
            {
                Decimal ToalCedit = 0;
                Decimal ToalCedit_Tax = 0;
                Decimal ToalDebit = 0;
                Decimal ToalDebit_A_R = 0;
                bool isTaxInserted = false;
                var existing_list = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == sales_id);
                var ar_customer = await _context.ar_Customers.FirstOrDefaultAsync(a => a.customer_id == existing_list.customer_id);

                var year = DateTime.Now.Year;

                var table = "gl_JournalHeaders";
                var table_key_ = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == existing_list.business_id);
                var key = Convert.ToInt16(table_key_.next_key);


                gl_Journal_Header.JournalId = Guid.CreateVersion7();
                gl_Journal_Header.BusinessId = existing_list?.business_id ?? Guid.Empty;
                gl_Journal_Header.JournalDate = DateTime.Now;
                gl_Journal_Header.PostingDate = DateTime.Now;
                gl_Journal_Header.JournalNo = "JE" + year + "-" + Convert.ToString(key + 1); ;
                gl_Journal_Header.SourceId = existing_list.sales_id;
                gl_Journal_Header.SourceType = "POS";
                gl_Journal_Header.JournalMemo = $"Journal entry for Sales order {existing_list.sales_no}";
                gl_Journal_Header.Status = "POSTED";
                gl_Journal_Header.CreatedAt = DateTime.Now;
                gl_Journal_Header.CreatedBy = "";
                gl_Journal_Header.BaseCurrencyCode = existing_list.doc_currency_code;
                gl_Journal_Header.ExchangeRate = existing_list.fx_rate_to_base;
                gl_Journal_Header.IsSystemGenerated = true;
                gl_Journal_Header.PostedAt = DateTime.Now;
                gl_Journal_Header.PostedBy = existing_list.created_by;
                gl_Journal_Header.ReferenceNo = existing_list.invoice_no;
                gl_Journal_Header.Remarks = $"Journal entry for Sales order {existing_list.sales_no} with invoice number {existing_list.invoice_no}";
                gl_Journal_Header.StoreId = existing_list.store_id;
                //gl_Journal_Header.TotalCreditBC = existing_list.grand_total;
                //gl_Journal_Header.TotalCreditFC = existing_list.grand_total;
                gl_Journal_Header.TotalDebitBC = existing_list.grand_total;
                gl_Journal_Header.TotalDebitFC = existing_list.grand_total;
                foreach (var item in existing_list.so_SalesLines)
                {
                    gl_JournalLines gl_Journal_Lines = new gl_JournalLines();

                    if (item.tax_amount > 0)
                    {
                        var gl_account = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Tax Payable" && a.CompanyId == existing_list.business_id);
                        var current_balance = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account.GlAccountId);
                        var gl_account_2 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Account Receivable (A/R)" && a.CompanyId == existing_list.business_id);
                        var current_balance_2 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_2.GlAccountId);
                        gl_Journal_Lines.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines.StoreId = existing_list.store_id;
                        gl_Journal_Lines.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        //gl_Journal_Lines.Description = item.Product_title;
                        ToalDebit_A_R = Convert.ToDecimal(item.line_total);

                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.DebitAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.DebitAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines.SourceLineId = item.sales_line_id;
                        gl_Journal_Lines.SourceType = "Input Tax";
                        //gl_Journal_Lines.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger = new gl_Ledger();
                        gl_Ledger.LedgerId = Guid.CreateVersion7();
                        gl_Ledger.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger.StoreId = existing_list.store_id;
                        gl_Ledger.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger.JournalLineId = gl_Journal_Lines.JournalLineId;
                        gl_Ledger.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        gl_Ledger.PostingDate = DateTime.Now;
                        gl_Ledger.CreatedBy = existing_list.created_by;
                        gl_Ledger.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger.Module = "PURCHASE";
                        gl_Ledger.Description = $"Ledger entry for Sales order {existing_list.sales_no} with invoice number {existing_list.invoice_no}";
                        gl_Ledger.CreatedAt = DateTime.Now;
                        gl_Ledger.CreatedBy = "";
                        gl_Ledger.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Ledger.SourceId = existing_list.sales_id;
                        gl_Ledger.SourceType = "Input Tax";
                        gl_Ledger.SourceLineId = item.sales_line_id;
                        gl_Ledger.TransactionDate = DateTime.Now;
                        gl_Ledger.UpdatedAt = DateTime.Now;
                        gl_Ledger.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger.DebitAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.DebitAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger);

                        _context.gl_JournalLines.Add(gl_Journal_Lines);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines);

                        ToalCedit_Tax = Convert.ToDecimal(existing_list.tax_total);

                        if (current_balance != null)
                        {
                            current_balance.CurrentBalance += ToalCedit_Tax;
                            _context.gl_AccountCurrentBalances.Update(current_balance);
                        }
                        if (current_balance_2 != null)
                        {
                            current_balance_2.CurrentBalance += ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_2);
                        }
                        ToalDebit_A_R = 0;
                        ToalCedit_Tax = 0;
                    }
                    if (item.tax_amount == 0)
                    {
                        var gl_account_3 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Account Receivable (A/R)" && a.CompanyId == existing_list.business_id);
                        var current_balance_3 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_3.GlAccountId);

                        gl_JournalLines gl_Journal_Lines2 = new gl_JournalLines();

                        gl_Journal_Lines2.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines2.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines2.StoreId = existing_list.store_id;
                        gl_Journal_Lines2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Journal_Lines2.DebitAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Journal_Lines2.DebitAmountFC = Convert.ToDecimal(item.line_total);
                        ToalDebit_A_R = Convert.ToDecimal(item.line_total);
                        //gl_Journal_Lines2.Description = item.Product_title;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines2.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines2.SourceLineId = item.sales_line_id;
                        gl_Journal_Lines2.SourceType = "Account Receivable (A/R)";
                        //gl_Journal_Lines2.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger2 = new gl_Ledger();
                        gl_Ledger2.LedgerId = Guid.CreateVersion7();
                        gl_Ledger2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger2.StoreId = existing_list.store_id;
                        gl_Ledger2.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger2.JournalLineId = gl_Journal_Lines2.JournalLineId;
                        gl_Ledger2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Ledger2.PostingDate = DateTime.Now;
                        gl_Ledger2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger2.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger2.Module = "POS";
                        gl_Ledger2.Description = $"Ledger entry for Sales order {existing_list.sales_no} with invoice number {existing_list.invoice_no}";
                        gl_Ledger2.CreatedAt = DateTime.Now;
                        gl_Ledger2.CreatedBy = existing_list.created_by;
                        gl_Ledger2.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Ledger2.SourceId = existing_list.source_id;
                        gl_Ledger2.SourceType = "Account Receivable (A/R)";
                        gl_Ledger2.SourceLineId = item.sales_line_id;
                        gl_Ledger2.TransactionDate = DateTime.Now;
                        gl_Ledger2.UpdatedAt = DateTime.Now;
                        gl_Ledger2.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger2.DebitAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Ledger2.DebitAmountFC = Convert.ToDecimal(item.line_total);
                        gl_Ledger2.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger2);

                        _context.gl_JournalLines.Add(gl_Journal_Lines2);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines2);

                        if (current_balance_3 != null)
                        {
                            current_balance_3.CurrentBalance += ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_3);
                        }
                    }

                }
                if (ar_customer != null)
                {
                    ar_customer.curren_balance += existing_list.grand_total;
                    _context.ar_Customers.Update(ar_customer);
                }

                if (table != null)
                {
                    table_key_.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key_);
                }
                gl_Journal_Header.JournalLines = gl_Journal_Lines_List;
                _context.gl_JournalHeaders.Add(gl_Journal_Header);
                await _context.SaveChangesAsync();
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Journal header added successfully",
                    Data = gl_Journal_Header
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Add_Journal_header");
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // this function adding Sales Return cash 
        //Insert  Sales Return
        //Update  Cash on Hand && Tax Payable 
        public async Task<ServiceResult<gl_JournalHeaders>> Add_Journal_header_Sales_Return(Guid? sales_id)
        {
            gl_JournalHeaders gl_Journal_Header = new gl_JournalHeaders();
            List<gl_JournalLines> gl_Journal_Lines_List = new List<gl_JournalLines>();
            try
            {
                Decimal ToalCedit = 0;
                Decimal ToalCedit_Tax = 0;
                Decimal ToalDebit = 0;
                Decimal ToalDebit_A_R = 0;
                bool isTaxInserted = false;
                var existing_list = await _context.so_SalesReturnHeaders.Include(a => a.so_SalesReturnLines).FirstOrDefaultAsync(a => a.sales_return_id == sales_id);

                var year = DateTime.Now.Year;

                var table = "gl_JournalHeaders";
                var table_key_ = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == existing_list.business_id);
                var key = Convert.ToInt16(table_key_.next_key);


                gl_Journal_Header.JournalId = Guid.CreateVersion7();
                gl_Journal_Header.BusinessId = existing_list?.business_id ?? Guid.Empty;
                gl_Journal_Header.JournalDate = DateTime.Now;
                gl_Journal_Header.PostingDate = DateTime.Now;
                gl_Journal_Header.JournalNo = "JE" + year + "-" + Convert.ToString(key + 1); ;
                gl_Journal_Header.SourceId = existing_list.sales_id;
                gl_Journal_Header.SourceType = "POS";
                gl_Journal_Header.JournalMemo = $"Journal entry for Retuen Sales order ";
                gl_Journal_Header.Status = "POSTED";
                gl_Journal_Header.CreatedAt = DateTime.Now;
                gl_Journal_Header.CreatedBy = "";
                gl_Journal_Header.BaseCurrencyCode = existing_list.doc_currency_code;
                gl_Journal_Header.ExchangeRate = existing_list.fx_rate_to_base;
                gl_Journal_Header.IsSystemGenerated = true;
                gl_Journal_Header.PostedAt = DateTime.Now;
                gl_Journal_Header.PostedBy = existing_list.created_by;
                //gl_Journal_Header.ReferenceNo = existing_list.invoice_no;
                gl_Journal_Header.Remarks = $"Journal entry for Retuen Sales order ";
                gl_Journal_Header.StoreId = existing_list.store_id;
                gl_Journal_Header.TotalCreditBC = existing_list.grand_total;
                gl_Journal_Header.TotalCreditFC = existing_list.grand_total;
                //gl_Journal_Header.TotalDebitBC = existing_list.grand_total;
                //gl_Journal_Header.TotalDebitFC = existing_list.grand_total;
                foreach (var item in existing_list.so_SalesReturnLines)
                {
                    gl_JournalLines gl_Journal_Lines = new gl_JournalLines();

                    if (item.tax_amount > 0)
                    {
                        var gl_account = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Tax Payable" && a.CompanyId == existing_list.business_id);
                        var current_balance = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account.GlAccountId);
                        var gl_account_2 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Cash on Hand" && a.CompanyId == existing_list.business_id);
                        var current_balance_2 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_2.GlAccountId);
                        var gl_account_3 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Sales Return" && a.CompanyId == existing_list.business_id);
                        var current_balance_3 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_3.GlAccountId);
                        gl_Journal_Lines.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines.StoreId = existing_list.store_id;
                        gl_Journal_Lines.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        //gl_Journal_Lines.Description = item.Product_title;
                        ToalDebit_A_R = Convert.ToDecimal(item.line_total);

                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.CreditAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.CreditAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines.SourceLineId = item.sales_return_line_id;
                        gl_Journal_Lines.SourceType = "Sales Return";
                        //gl_Journal_Lines.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger = new gl_Ledger();
                        gl_Ledger.LedgerId = Guid.CreateVersion7();
                        gl_Ledger.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger.StoreId = existing_list.store_id;
                        gl_Ledger.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger.JournalLineId = gl_Journal_Lines.JournalLineId;
                        gl_Ledger.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        gl_Ledger.PostingDate = DateTime.Now;
                        gl_Ledger.CreatedBy = existing_list.created_by;
                        gl_Ledger.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        //gl_Ledger.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger.Module = "POS";
                        gl_Ledger.Description = $"Ledger entry for Return Sales ";
                        gl_Ledger.CreatedAt = DateTime.Now;
                        gl_Ledger.CreatedBy = "";
                        gl_Ledger.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Ledger.SourceId = existing_list.sales_id;
                        gl_Ledger.SourceType = "Sales Return";
                        //gl_Ledger.SourceLineId = item.sales_line_id;
                        gl_Ledger.TransactionDate = DateTime.Now;
                        gl_Ledger.UpdatedAt = DateTime.Now;
                        gl_Ledger.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger.CreditAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.CreditAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger);

                        _context.gl_JournalLines.Add(gl_Journal_Lines);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines);

                        ToalCedit_Tax = Convert.ToDecimal(existing_list.tax_total);

                        if (current_balance != null)
                        {
                            current_balance.CurrentBalance -= ToalCedit_Tax;
                            _context.gl_AccountCurrentBalances.Update(current_balance);
                        }
                        if (current_balance_2 != null)
                        {
                            current_balance_2.CurrentBalance -= ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_2);
                        }
                        if (current_balance_3 != null)
                        {
                            current_balance_3.CurrentBalance += ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_3);
                        }
                        ToalDebit_A_R = 0;
                        ToalCedit_Tax = 0;
                    }
                    if (item.tax_amount == 0)
                    {
                        var gl_account_3 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Cash on Hand" && a.CompanyId == existing_list.business_id);
                        var current_balance_3 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_3.GlAccountId);
                        var gl_account_4 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Sales Return" && a.CompanyId == existing_list.business_id);
                        var current_balance_4 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_4.GlAccountId);

                        gl_JournalLines gl_Journal_Lines2 = new gl_JournalLines();

                        gl_Journal_Lines2.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines2.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines2.StoreId = existing_list.store_id;
                        gl_Journal_Lines2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Journal_Lines2.CreditAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Journal_Lines2.CreditAmountFC = Convert.ToDecimal(item.line_total);
                        ToalDebit_A_R = Convert.ToDecimal(item.line_total);
                        //gl_Journal_Lines2.Description = item.Product_title;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines2.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines2.SourceLineId = item.sales_return_line_id;
                        gl_Journal_Lines2.SourceType = "Sales Return";
                        //gl_Journal_Lines2.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger2 = new gl_Ledger();
                        gl_Ledger2.LedgerId = Guid.CreateVersion7();
                        gl_Ledger2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger2.StoreId = existing_list.store_id;
                        gl_Ledger2.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger2.JournalLineId = gl_Journal_Lines2.JournalLineId;
                        gl_Ledger2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Ledger2.PostingDate = DateTime.Now;
                        gl_Ledger2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        //gl_Ledger2.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger2.Module = "POS";
                        gl_Ledger2.Description = $"Ledger entry for Retun Sales ";
                        gl_Ledger2.CreatedAt = DateTime.Now;
                        gl_Ledger2.CreatedBy = existing_list.created_by;
                        gl_Ledger2.ExchangeRate = existing_list.fx_rate_to_base;
                        //gl_Ledger2.SourceId = existing_list.source_id;
                        gl_Ledger2.SourceType = "Sales Return";
                        gl_Ledger2.SourceLineId = item.sales_return_line_id;
                        gl_Ledger2.TransactionDate = DateTime.Now;
                        gl_Ledger2.UpdatedAt = DateTime.Now;
                        gl_Ledger2.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger2.CreditAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Ledger2.CreditAmountFC = Convert.ToDecimal(item.line_total);
                        gl_Ledger2.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger2);

                        _context.gl_JournalLines.Add(gl_Journal_Lines2);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines2);

                        if (current_balance_3 != null)
                        {
                            current_balance_3.CurrentBalance -= ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_3);
                        }
                        if (current_balance_4 != null)
                        {
                            current_balance_4.CurrentBalance += ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_4);
                        }
                    }

                }
                

                if (table != null)
                {
                    table_key_.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key_);
                }
                gl_Journal_Header.JournalLines = gl_Journal_Lines_List;
                _context.gl_JournalHeaders.Add(gl_Journal_Header);
                await _context.SaveChangesAsync();
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Journal header added successfully",
                    Data = gl_Journal_Header
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Add_Journal_header");
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // this function adding Sales Return Credit and updating corresponed values and ar_customer -> curren_balance 
        //Insert  Sales Return
        //Update  Account Receivable (A/R) && Tax Payable 
        public async Task<ServiceResult<gl_JournalHeaders>> Add_Journal_header_Sales_Return_credit(Guid? sales_id)
        {
            gl_JournalHeaders gl_Journal_Header = new gl_JournalHeaders();
            List<gl_JournalLines> gl_Journal_Lines_List = new List<gl_JournalLines>();
            try
            {
                Decimal ToalCedit = 0;
                Decimal ToalCedit_Tax = 0;
                Decimal ToalDebit = 0;
                Decimal ToalDebit_A_R = 0;
                bool isTaxInserted = false;
                var existing_list = await _context.so_SalesReturnHeaders.Include(a => a.so_SalesReturnLines).FirstOrDefaultAsync(a => a.sales_return_id == sales_id);
                var ar_customer = await _context.ar_Customers.FirstOrDefaultAsync(a => a.customer_id == existing_list.customer_id);

                var year = DateTime.Now.Year;

                var table = "gl_JournalHeaders";
                var table_key_ = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == existing_list.business_id);
                var key = Convert.ToInt16(table_key_.next_key);


                gl_Journal_Header.JournalId = Guid.CreateVersion7();
                gl_Journal_Header.BusinessId = existing_list?.business_id ?? Guid.Empty;
                gl_Journal_Header.JournalDate = DateTime.Now;
                gl_Journal_Header.PostingDate = DateTime.Now;
                gl_Journal_Header.JournalNo = "JE" + year + "-" + Convert.ToString(key + 1); ;
                gl_Journal_Header.SourceId = existing_list.sales_id;
                gl_Journal_Header.SourceType = "POS";
                gl_Journal_Header.JournalMemo = $"Journal entry for Retuen Sales order ";
                gl_Journal_Header.Status = "POSTED";
                gl_Journal_Header.CreatedAt = DateTime.Now;
                gl_Journal_Header.CreatedBy = "";
                gl_Journal_Header.BaseCurrencyCode = existing_list.doc_currency_code;
                gl_Journal_Header.ExchangeRate = existing_list.fx_rate_to_base;
                gl_Journal_Header.IsSystemGenerated = true;
                gl_Journal_Header.PostedAt = DateTime.Now;
                gl_Journal_Header.PostedBy = existing_list.created_by;
                //gl_Journal_Header.ReferenceNo = existing_list.invoice_no;
                gl_Journal_Header.Remarks = $"Journal entry for Retuen Sales order ";
                gl_Journal_Header.StoreId = existing_list.store_id;
                gl_Journal_Header.TotalCreditBC = existing_list.grand_total;
                gl_Journal_Header.TotalCreditFC = existing_list.grand_total;
                //gl_Journal_Header.TotalDebitBC = existing_list.grand_total;
                //gl_Journal_Header.TotalDebitFC = existing_list.grand_total;
                foreach (var item in existing_list.so_SalesReturnLines)
                {
                    gl_JournalLines gl_Journal_Lines = new gl_JournalLines();

                    if (item.tax_amount > 0)
                    {
                        var gl_account = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Tax Payable" && a.CompanyId == existing_list.business_id);
                        var current_balance = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account.GlAccountId);
                        var gl_account_2 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Account Receivable (A/R)" && a.CompanyId == existing_list.business_id);
                        var current_balance_2 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_2.GlAccountId);
                        var gl_account_3 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Sales Return" && a.CompanyId == existing_list.business_id);
                        var current_balance_3 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_3.GlAccountId);
                        gl_Journal_Lines.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines.StoreId = existing_list.store_id;
                        gl_Journal_Lines.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        //gl_Journal_Lines.Description = item.Product_title;
                        ToalDebit_A_R = Convert.ToDecimal(item.line_total);

                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.CreditAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.CreditAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Journal_Lines.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines.SourceLineId = item.sales_return_line_id;
                        gl_Journal_Lines.SourceType = "Sales Return";
                        //gl_Journal_Lines.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines.CreatedAt = DateTime.Now;
                        gl_Journal_Lines.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger = new gl_Ledger();
                        gl_Ledger.LedgerId = Guid.CreateVersion7();
                        gl_Ledger.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger.StoreId = existing_list.store_id;
                        gl_Ledger.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger.JournalLineId = gl_Journal_Lines.JournalLineId;
                        gl_Ledger.GlAccountId = gl_account?.GlAccountId ?? Guid.Empty;
                        gl_Ledger.PostingDate = DateTime.Now;
                        gl_Ledger.CreatedBy = existing_list.created_by;
                        gl_Ledger.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        //gl_Ledger.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger.Module = "POS";
                        gl_Ledger.Description = $"Ledger entry for Return Sales ";
                        gl_Ledger.CreatedAt = DateTime.Now;
                        gl_Ledger.CreatedBy = "";
                        gl_Ledger.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Ledger.SourceId = existing_list.sales_id;
                        gl_Ledger.SourceType = "Sales Return";
                        //gl_Ledger.SourceLineId = item.sales_line_id;
                        gl_Ledger.TransactionDate = DateTime.Now;
                        gl_Ledger.UpdatedAt = DateTime.Now;
                        gl_Ledger.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger.CreditAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.CreditAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger);

                        _context.gl_JournalLines.Add(gl_Journal_Lines);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines);

                        ToalCedit_Tax = Convert.ToDecimal(existing_list.tax_total);

                        if (current_balance != null)
                        {
                            current_balance.CurrentBalance -= ToalCedit_Tax;
                            _context.gl_AccountCurrentBalances.Update(current_balance);
                        }
                        if (current_balance_2 != null)
                        {
                            current_balance_2.CurrentBalance -= ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_2);
                        }
                        if (current_balance_3 != null)
                        {
                            current_balance_3.CurrentBalance += ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_3);
                        }
                        ToalDebit_A_R = 0;
                        ToalCedit_Tax = 0;
                    }
                    if (item.tax_amount == 0)
                    {
                        var gl_account_3 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Account Receivable (A/R)" && a.CompanyId == existing_list.business_id);
                        var current_balance_3 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_3.GlAccountId);
                        var gl_account_4 = await _context.gl_AccountMapping.FirstOrDefaultAsync(a => a.Module == "POS" && a.PurposeCode == "Sales Return" && a.CompanyId == existing_list.business_id);
                        var current_balance_4 = await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a => a.AccountId == gl_account_4.GlAccountId);

                        gl_JournalLines gl_Journal_Lines2 = new gl_JournalLines();

                        gl_Journal_Lines2.JournalLineId = Guid.CreateVersion7();
                        gl_Journal_Lines2.JournalId = gl_Journal_Header.JournalId;
                        gl_Journal_Lines2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Journal_Lines2.StoreId = existing_list.store_id;
                        gl_Journal_Lines2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Journal_Lines2.CreditAmountBC = Convert.ToDecimal(item.line_total);
                        gl_Journal_Lines2.CreditAmountFC = Convert.ToDecimal(item.line_total);
                        ToalDebit_A_R = Convert.ToDecimal(item.line_total);
                        //gl_Journal_Lines2.Description = item.Product_title;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Journal_Lines2.ExchangeRate = existing_list.fx_rate_to_base;
                        gl_Journal_Lines2.SourceLineId = item.sales_return_line_id;
                        gl_Journal_Lines2.SourceType = "Sales Return";
                        //gl_Journal_Lines2.SupplierId = existing_list.vendor_id;
                        gl_Journal_Lines2.CreatedAt = DateTime.Now;
                        gl_Journal_Lines2.UpdatedAt = DateTime.Now;

                        gl_Ledger gl_Ledger2 = new gl_Ledger();
                        gl_Ledger2.LedgerId = Guid.CreateVersion7();
                        gl_Ledger2.BusinessId = existing_list?.business_id ?? Guid.Empty;
                        gl_Ledger2.StoreId = existing_list.store_id;
                        gl_Ledger2.JournalId = gl_Journal_Header.JournalId;
                        gl_Ledger2.JournalLineId = gl_Journal_Lines2.JournalLineId;
                        gl_Ledger2.GlAccountId = gl_account_3?.GlAccountId ?? Guid.Empty;
                        gl_Ledger2.PostingDate = DateTime.Now;
                        gl_Ledger2.CurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        //gl_Ledger2.ReferenceNo = existing_list.invoice_no;
                        gl_Ledger2.Module = "POS";
                        gl_Ledger2.Description = $"Ledger entry for Retun Sales ";
                        gl_Ledger2.CreatedAt = DateTime.Now;
                        gl_Ledger2.CreatedBy = existing_list.created_by;
                        gl_Ledger2.ExchangeRate = existing_list.fx_rate_to_base;
                        //gl_Ledger2.SourceId = existing_list.source_id;
                        gl_Ledger2.SourceType = "Sales Return";
                        gl_Ledger2.SourceLineId = item.sales_return_line_id;
                        gl_Ledger2.TransactionDate = DateTime.Now;
                        gl_Ledger2.UpdatedAt = DateTime.Now;
                        gl_Ledger2.BaseCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        gl_Ledger2.CreditAmountBC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger2.CreditAmountFC = Convert.ToDecimal(existing_list.tax_total);
                        gl_Ledger2.TransactionCurrencyCode = gl_Journal_Header.BaseCurrencyCode;
                        _context.gl_ledger.Add(gl_Ledger2);

                        _context.gl_JournalLines.Add(gl_Journal_Lines2);
                        gl_Journal_Lines_List.Add(gl_Journal_Lines2);

                        if (current_balance_3 != null)
                        {
                            current_balance_3.CurrentBalance -= ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_3);
                        }
                        if (current_balance_4 != null)
                        {
                            current_balance_4.CurrentBalance += ToalDebit_A_R;
                            _context.gl_AccountCurrentBalances.Update(current_balance_4);
                        }
                    }

                }
                if (ar_customer != null)
                {
                    ar_customer.curren_balance -= existing_list.grand_total;
                    _context.ar_Customers.Update(ar_customer);
                }

                if (table != null)
                {
                    table_key_.next_key = key + 1;
                    _context.am_table_next_key.Update(table_key_);
                }
                gl_Journal_Header.JournalLines = gl_Journal_Lines_List;
                _context.gl_JournalHeaders.Add(gl_Journal_Header);
                await _context.SaveChangesAsync();
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Journal header added successfully",
                    Data = gl_Journal_Header
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while Add_Journal_header");
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        //public async Task<ServiceResult<gl_JournalHeaders>> Update_current_balace(Guid? sales_id)
        //{
        //    try
        //    {
        //        if (sales_id == null)
        //        {
        //            return new ServiceResult<gl_JournalHeaders>
        //            {
        //                Status = 300,
        //                Success = false,
        //                Message = "No data found"
        //            };
        //        }
        //        var existing_sales = await _context.so_SalesHeaders.FirstOrDefaultAsync(a => a.sales_id == sales_id);
        //        var gl_account = await _context.gl_JournalHeaders.FirstOrDefaultAsync(a => a.SourceId == existing_sales.sales_id);
        //        var gl_balace= await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(a=>a.AccountId==gl_account.acc)
        //    }catch(Exception ex)
        //    {
        //        return new ServiceResult<gl_JournalHeaders>
        //        {
        //            Status = 500,
        //            Success = false,
        //            Message = ex.Message
        //        };
        //    }
        //}
       

        public async Task<ServiceResult<so_SalesHeaders>> update_market_places(Guid salesId, Guid userId)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (salesId == null)
                {
                    _logger.LogInformation("not salesId found");
                    return new ServiceResult<so_SalesHeaders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "not salesId found"
                    };
                }
                var existing_sales = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).FirstOrDefaultAsync(a => a.sales_id == salesId);
                var om_order = await _context.om_CustomerOrders.Include(a => a.om_CustomerOrderLines).FirstOrDefaultAsync(a => a.sales_id == existing_sales.sales_id);
                var am_user = await _context.am_users.FirstOrDefaultAsync(a => a.userId == userId);
                if (existing_sales != null)
                {
                    existing_sales.status = "POSTED";
                    var journal = await Add_Journal_header(existing_sales.sales_id);
                    if (!journal.Success)
                    {
                        await transaction.RollbackAsync();
                        return new ServiceResult<so_SalesHeaders>
                        {
                            Success = false,
                            Status = 300,
                            Message = "Error"
                        };
                    }
                    foreach (var item in om_order.om_CustomerOrderLines)
                    {
                        im_InventoryReservations im_Inventory = new im_InventoryReservations();
                        im_Inventory.reservation_id = Guid.CreateVersion7();
                        im_Inventory.business_id = existing_sales.business_id ?? Guid.Empty;
                        im_Inventory.store_id = existing_sales.store_id;
                        im_Inventory.customer_order_id = item.customer_order_id;
                        im_Inventory.customer_order_line_id = item.customer_order_line_id;
                        im_Inventory.product_id = item.product_id;
                        im_Inventory.variant_id = item.variant_id;
                        im_Inventory.batch_id = item.batch_id;
                        im_Inventory.reserved_qty = item.reserved_qty;
                        im_Inventory.released_qty = 0;
                        im_Inventory.consumed_qty = 0;
                        im_Inventory.reserved_at = DateTime.Now;
                        im_Inventory.remarks = "";
                        im_Inventory.created_at = DateTime.Now;
                        im_Inventory.created_by = am_user.fullName;
                        im_Inventory.created_user_id = am_user.userId;
                        im_Inventory.reservation_status = "ACTIVE";
                        _context.im_InventoryReservations.Add(im_Inventory);
                    }

                    var spResults = _context.Database.SqlQueryRaw<string>(
                                         "EXEC dbo.sp_PostSales @sales_id=@sales_id ,@store_id=@store_id",
                                          new SqlParameter("@sales_id", existing_sales.sales_id),
                                          new SqlParameter("@store_id ", existing_sales.store_id)).AsEnumerable().ToList();
                    var spResponse = spResults.FirstOrDefault();
                }
                _context.so_SalesHeaders.Update(existing_sales);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<so_SalesHeaders>
                {
                    Success = true,
                    Status = 200,
                    Data = existing_sales
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while update_market_places");
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
