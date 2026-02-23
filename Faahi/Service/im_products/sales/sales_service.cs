using AutoMapper.Configuration.Annotations;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Faahi.Model.sales;
using Faahi.Model.st_sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                var table_key = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table);
                var key = Convert.ToInt16(table_key.next_key);

                var table2 = "so_SalesHeaders_invoice_no";
                var table_key2 = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table2);
                var key2 = Convert.ToInt16(table_key2.next_key);

                var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == so_SalesHeaders.store_id);

                Decimal discount_total = 0m;
                Decimal total_taxable_value = 0m;
                Decimal total_zero_value = 0m;
                Decimal total_exempted_value = 0m;

                so_SalesHeaders.business_id = so_SalesHeaders.business_id;
                so_SalesHeaders.store_id = so_SalesHeaders.store_id;
                so_SalesHeaders.customer_id = so_SalesHeaders.customer_id;
                so_SalesHeaders.payment_term_id = so_SalesHeaders.payment_term_id;
                so_SalesHeaders.membership_id = so_SalesHeaders.membership_id;
                so_SalesHeaders.sales_no = Convert.ToInt64(key + 1);
                so_SalesHeaders.invoice_no = Convert.ToString(key2 + 1);
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
                so_SalesHeaders.fx_rate_to_base = 1;
                so_SalesHeaders.fx_rate_date = so_SalesHeaders.fx_rate_date;
                so_SalesHeaders.fx_source = so_SalesHeaders.fx_source;
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
                so_SalesHeaders.sub_total_base = so_SalesHeaders.sub_total_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.discount_total_base = so_SalesHeaders.discount_total_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.tax_total_base = so_SalesHeaders.tax_total_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.grand_total_base = so_SalesHeaders.grand_total_base * so_SalesHeaders.fx_rate_to_base;
                so_SalesHeaders.total_taxable_base = so_SalesHeaders.total_taxable_base * so_SalesHeaders.fx_rate_to_base;
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
                so_SalesHeaders.sales_on_hold = so_SalesHeaders.sales_on_hold;
                so_SalesHeaders.status = so_SalesHeaders.status;
                foreach (var item in so_SalesHeaders.so_SalesLines)
                {

                    var im_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == item.variant_id);
                    var item_batch = await _context.im_itemBatches.FirstOrDefaultAsync(a => a.item_batch_id == item.batch_id);
                    var im_store_inv = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == item.store_variant_inventory_id);

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
                    item.line_total = item.quantity * item.unit_price;

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
                if (so_SalesHeaders.status == "POSTED")
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
                            trans_type = "SALES",
                            quantity_change = item.quantity,
                            unit_cost = item.unit_price,
                            total_cost = item.line_total,
                            created_date_time = DateTime.Now
                        };

                        im_InventoryTransactions1.Add(im_InventoryTransactions);

                    }
                    _context.im_InventoryTransactions.AddRange(im_InventoryTransactions1);
                    await _context.SaveChangesAsync();
                    var spResults = _context.Database.SqlQueryRaw<string>(
                                     "EXEC dbo.sp_PostSales @sales_id=@sales_id ,@store_id=@store_id",
                                      new SqlParameter("@sales_id", so_SalesHeaders.sales_id),
                                      new SqlParameter("@store_id ", st_store.store_id)).AsEnumerable().ToList();
                    var spResponse = spResults.FirstOrDefault();
                }





                await transaction.CommitAsync();
                return new ServiceResult<so_SalesHeaders>
                {
                    Status = 200,
                    Message="Success",
                    Success=true,
                    Data= so_SalesHeaders
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
                existing_sal_header.status = so_SalesHeaders.status;
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
                existing_sal_header.discount_total = discount_total;
                existing_sal_header.total_taxable_value = total_taxable_value;
                existing_sal_header.total_zero_value = total_zero_value;
                existing_sal_header.discount_total_base = so_SalesHeaders.discount_total * so_SalesHeaders.fx_rate_to_base;
                existing_sal_header.total_taxable_base = so_SalesHeaders.total_taxable_value * so_SalesHeaders.fx_rate_to_base;
                existing_sal_header.total_zero_base = so_SalesHeaders.total_zero_value * so_SalesHeaders.fx_rate_to_base;


                _context.so_SalesHeaders.Update(existing_sal_header);
                await _context.SaveChangesAsync();
                if (so_SalesHeaders.status == "POSTED")
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
                    await _context.SaveChangesAsync();
                    var spResults = _context.Database.SqlQueryRaw<string>(
                                     "EXEC dbo.sp_PostSales @sales_id=@sales_id ,@store_id=@store_id",
                                      new SqlParameter("@sales_id", existing_sal_header.sales_id),
                                      new SqlParameter("@store_id ", existing_sal_header.store_id)).AsEnumerable().ToList();
                    var spResponse = spResults.FirstOrDefault();
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
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
        public async Task<ServiceResult<List<so_SalesHeaders>>> Get_sales(Guid company_id)
        {
            try
            {
                if (company_id == null)
                {
                    _logger.LogInformation("No company_id found");
                    return new ServiceResult<List<so_SalesHeaders>>
                    {
                        Status = 400,
                        Message = "No company_id found",
                        Success = false
                    };
                }
                var so_sales = await _context.so_SalesHeaders.Include(a => a.so_SalesLines).Where(a => a.business_id == company_id).OrderByDescending(a => a.sales_no).ToListAsync();
                if (so_sales.Count == 0)
                {
                    return new ServiceResult<List<so_SalesHeaders>>
                    {
                        Status = 300,
                        Message = "No so_SalesHeaders found",
                        Success = false
                    };
                }
                return new ServiceResult<List<so_SalesHeaders>>
                {
                    Success = true,
                    Status = 200,
                    Message = "Success",
                    Data = so_sales
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro while Get_sales company_id");
                return new ServiceResult<List<so_SalesHeaders>>
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
    }
}
