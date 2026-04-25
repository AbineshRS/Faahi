using Amazon.S3;
using Amazon.S3.Model;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.sales_dto;
using Faahi.Model.am_vcos;
using Faahi.Model.pos_tables;
using Faahi.Model.Shared_tables;
using Faahi.Model.st_sellers;
using Faahi.Service.im_products.sales;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Numerics;

namespace Faahi.Service.Users
{
    public class User_service : IUser
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<User_service> _logger;
        private readonly Isales _sales_serv;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configure;
        public User_service(ApplicationDbContext context, ILogger<User_service> logger,Isales sales_serv, IAmazonS3 s3Client, IConfiguration configure)
        {
            _context = context;
            _logger = logger;
            _sales_serv = sales_serv;
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _configure = configure;
        }
        public async Task<ServiceResult<st_Parties>> Create_vendors(st_Parties st_Parties)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            if (st_Parties == null)
            {
                _logger.LogWarning("Create_vendors: No data found");
                return new ServiceResult<st_Parties>
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

                st_Parties.party_id = Guid.CreateVersion7();
                st_Parties.company_id = st_Parties.company_id;
                st_Parties.party_type = "vendor";
                st_Parties.display_name = st_Parties.display_name;
                st_Parties.legal_name = st_Parties.legal_name;
                st_Parties.payable_name = st_Parties.payable_name;
                st_Parties.tax_id = st_Parties.tax_id;
                st_Parties.email = st_Parties.email;
                st_Parties.phone = st_Parties.phone;
                st_Parties.created_at = DateTime.Now;
                st_Parties.updated_at = DateTime.Now;
                foreach (var vendors in st_Parties.ap_Vendors)
                {
                    vendors.vendor_id = Guid.CreateVersion7();
                    vendors.party_id = st_Parties.party_id;
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
                    vendors.contact_name = vendors.contact_name;
                    vendors.contact_phone1 = vendors.contact_phone1;
                    vendors.contact_phone2 = vendors.contact_phone2;
                    vendors.contact_email = vendors.contact_email;
                    vendors.contact_website = vendors.contact_website;
                    vendors.tex_identification_number = vendors.tex_identification_number;


                }
                await _context.st_Parties.AddAsync(st_Parties);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<st_Parties>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = st_Parties
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while creating a vendor.");
                return new ServiceResult<st_Parties>
                {
                    Success = false,
                    Message = "An error occurred while creating the vendor.",
                    Status = -1
                };
            }



        }
        public async Task<ServiceResult<st_Parties>> Create_customer(st_Parties st_Parties)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            if (st_Parties == null)
            {
                _logger.LogWarning("Create_customer: No data found");
                return new ServiceResult<st_Parties>
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

                st_Parties.party_id = Guid.CreateVersion7();
                st_Parties.company_id = st_Parties.company_id;
                st_Parties.party_type = "customer";
                st_Parties.display_name = st_Parties.display_name;
                st_Parties.legal_name = st_Parties.legal_name;
                st_Parties.payable_name = st_Parties.payable_name;
                st_Parties.tax_id = st_Parties.tax_id;
                st_Parties.email = st_Parties.email;
                st_Parties.phone = st_Parties.phone;
                st_Parties.created_at = DateTime.Now;
                st_Parties.updated_at = DateTime.Now;

                foreach(var ar_Customers in st_Parties.ar_Customers)
                {
                    ar_Customers.customer_id = Guid.CreateVersion7();
                    ar_Customers.customer_code = "C-" + customerCode;
                    ar_Customers.price_tier_id = ar_Customers.price_tier_id;
                    ar_Customers.party_id = st_Parties.party_id;
                    ar_Customers.payment_term_id = ar_Customers.payment_term_id;
                    ar_Customers.credit_limit = ar_Customers.credit_limit;
                    ar_Customers.default_shipping_address_id = ar_Customers.default_shipping_address_id;
                    ar_Customers.default_billing_address_id = ar_Customers.default_billing_address_id;
                    ar_Customers.loyalty_level = ar_Customers.loyalty_level;
                    ar_Customers.curren_balance = ar_Customers.curren_balance;
                    ar_Customers.loyalty_points = ar_Customers.loyalty_points;
                    ar_Customers.note = ar_Customers.note;
                    ar_Customers.created_at = DateTime.Now;
                    ar_Customers.updated_at = DateTime.Now;
                    ar_Customers.credit_hold = ar_Customers.credit_hold;
                    ar_Customers.tax_exempt = ar_Customers.tax_exempt;
                    ar_Customers.company_id = ar_Customers.company_id;
                    ar_Customers.status = ar_Customers.status;
                    ar_Customers.contact_name = ar_Customers.contact_name;
                    ar_Customers.contact_email = ar_Customers.contact_email;
                    ar_Customers.contact_phone1 = ar_Customers.contact_phone1;
                    ar_Customers.contact_phone2 = ar_Customers.contact_phone2;
                    ar_Customers.tex_identification_number = ar_Customers.tex_identification_number;
                    ar_Customers.credit_hold = "T";
                }
                await _context.st_Parties.AddAsync(st_Parties);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<st_Parties>
                {
                    Success = true,
                    Status = 1,
                    Message = "Success",
                    Data = st_Parties
                };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while creating a customer.");
                return new ServiceResult<st_Parties>
                {
                    Success = false,
                    Message = "An error occurred while creating the customer.",
                    Status = -1
                };
            }

        }
        public async Task<ServiceResult<ar_Customers>> Update_arcustomer(Guid customer_id, ar_Customers ar_Customers)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (customer_id == null || ar_Customers == null)
                {
                    _logger.LogWarning("No data to found insert");
                    return new ServiceResult<ar_Customers>
                    {
                        Status = -1,
                        Success = false,
                        Message = "No data found to insert",
                        Data = null
                    };
                }
                var customer = await _context.ar_Customers.Include(a => a.fin_PartyBankAccounts).Include(a => a.st_PartyAddresses).FirstOrDefaultAsync(a => a.customer_id == customer_id);

                customer.contact_name = ar_Customers.contact_name;
                customer.contact_phone1 = ar_Customers.contact_phone1;
                customer.contact_phone2 = ar_Customers.contact_phone2;
                customer.contact_email = ar_Customers.contact_email;
                customer.credit_limit = ar_Customers.credit_limit;
                customer.tex_identification_number = ar_Customers.tex_identification_number;
                customer.default_currency = ar_Customers.default_currency;
                customer.updated_at = DateTime.Now;

                var existing_bank = await _context.fin_PartyBankAccounts.Where(a => a.customer_id == customer_id).ToListAsync();
                var newbank = ar_Customers.fin_PartyBankAccounts.Select(a => a.party_account_id).ToList();
                var delete_bank = existing_bank.Where(a => !newbank.Contains(a.party_account_id)).ToList();
                if (delete_bank.Any())
                {
                    foreach (var bank in delete_bank)
                    {
                        _context.fin_PartyBankAccounts.Remove(bank);
                    }
                }

                var exisitng_add = await _context.st_PartyAddresses.Where(a => a.customer_id == customer_id).ToListAsync();
                var newAddressIds = ar_Customers.st_PartyAddresses.Select(c => c.address_id).ToList();
                var delete_address = exisitng_add.Where(a => !newAddressIds.Contains(a.address_id)).ToList();
                if (delete_address.Any())
                {
                    foreach (var addre in delete_address)
                    {
                        _context.st_PartyAddresses.Remove(addre);
                    }
                    await _context.SaveChangesAsync();
                }
                foreach (var bank in ar_Customers.fin_PartyBankAccounts)
                {
                    var existing_bank_details = await _context.fin_PartyBankAccounts.FirstOrDefaultAsync(a => a.party_account_id == bank.party_account_id);
                    if (existing_bank_details != null)
                    {
                        existing_bank_details.customer_id = customer.customer_id;
                        existing_bank_details.bank_name = bank.bank_name;
                        existing_bank_details.account_holder_name = bank.account_holder_name;
                        existing_bank_details.account_number = bank.account_number;
                        existing_bank_details.routing_number = bank.routing_number;
                        existing_bank_details.swift_code = bank.swift_code;
                        existing_bank_details.iban = bank.iban;
                        existing_bank_details.currency = bank.currency;
                        existing_bank_details.created_at = bank.created_at;
                        existing_bank_details.is_default = bank.is_default;
                        _context.fin_PartyBankAccounts.Update(existing_bank_details);
                        customer.fin_PartyBankAccounts.Add(existing_bank_details);
                    }
                    else
                    {
                        bank.party_account_id = Guid.CreateVersion7();
                        bank.customer_id = customer.customer_id;
                        bank.bank_name = bank.bank_name;
                        bank.account_holder_name = bank.account_holder_name;
                        bank.account_number = bank.account_number;
                        bank.routing_number = bank.routing_number;
                        bank.swift_code = bank.swift_code;
                        bank.iban = bank.iban;
                        bank.currency = bank.currency;
                        bank.created_at = bank.created_at;
                        bank.is_default = bank.is_default;
                        _context.fin_PartyBankAccounts.Add(bank);
                        customer.fin_PartyBankAccounts.Add(bank);
                    }

                }
                foreach (var address in ar_Customers.st_PartyAddresses)
                {

                    var existing_address = await _context.st_PartyAddresses.FirstOrDefaultAsync(a => a.address_id == address.address_id);
                    if (existing_address == null)
                    {
                        st_PartyAddresses st_PartyAddresses = new st_PartyAddresses();
                        st_PartyAddresses.address_id = Guid.CreateVersion7();
                        st_PartyAddresses.customer_id = customer_id;
                        st_PartyAddresses.address_type = address.address_type;
                        st_PartyAddresses.line1 = address.line1;
                        st_PartyAddresses.line2 = address.line2;
                        st_PartyAddresses.region = address.region;
                        st_PartyAddresses.postal_code = address.postal_code;
                        st_PartyAddresses.country = address.country;
                        st_PartyAddresses.created_at = DateTime.Now;
                        st_PartyAddresses.updated_at = DateTime.Now;
                        st_PartyAddresses.is_default = address.is_default;
                        if (address.address_type == "shipping" && address.is_default == "T")
                        {
                            customer.default_shipping_address_id = st_PartyAddresses.address_id;

                        }
                        else if (address.address_type == "shipping" && address.is_default == "F")
                        {
                            customer.default_shipping_address_id = null;
                        }
                        if (address.address_type == "billing" && address.is_default == "T")
                        {
                            customer.default_billing_address_id = st_PartyAddresses.address_id;

                        }
                        else if (address.address_type == "billing" && address.is_default == "F")
                        {
                            customer.default_billing_address_id = null;
                        }

                        _context.st_PartyAddresses.Add(st_PartyAddresses);
                        customer.st_PartyAddresses.Add(st_PartyAddresses);
                    }
                    else
                    {
                        existing_address.address_type = address.address_type;
                        existing_address.line1 = address.line1;
                        existing_address.line2 = address.line2;
                        existing_address.region = address.region;
                        existing_address.postal_code = address.postal_code;
                        existing_address.country = address.country;
                        existing_address.updated_at = DateTime.Now;
                        existing_address.is_default = address.is_default;

                        if (address.address_type == "shipping" && address.is_default == "T")
                        {
                            customer.default_shipping_address_id = existing_address.address_id;

                        }
                        else if (address.address_type == "shipping" && address.is_default == "F")
                        {
                            customer.default_shipping_address_id = null;
                        }

                        if (address.address_type == "billing" && address.is_default == "T")
                        {
                            customer.default_billing_address_id = existing_address.address_id;

                        }
                        else if (address.address_type == "billing" && address.is_default == "F")
                        {
                            customer.default_billing_address_id = null;
                        }



                        _context.st_PartyAddresses.Update(existing_address);
                        customer.st_PartyAddresses.Add(existing_address);

                    }


                }
                _context.ar_Customers.Update(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<ar_Customers>
                {
                    Status = 1,
                    Success = true,
                    Message = "updated",
                    Data = customer
                };
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while updating a customer.");
                return new ServiceResult<ar_Customers>
                {
                    Success = false,
                    Message = "An error occurred while updating the customer.",
                    Status = -1
                };
            }
            
        }
        public async Task<ServiceResult<ap_Vendors>> Update_apvendor(Guid vendor_id, ap_Vendors ap_Vendors)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (vendor_id == null || ap_Vendors == null)
                {
                    _logger.LogWarning("No data to found insert");
                    return new ServiceResult<ap_Vendors>
                    {
                        Status = -1,
                        Success = false,
                        Message = "No data found to insert",
                        Data = null
                    };
                }
                var vendor = await _context.ap_Vendors.Include(a => a.fin_PartyBankAccounts).Include(a => a.st_PartyAddresses).FirstOrDefaultAsync(a => a.vendor_id == vendor_id);

                vendor.contact_name = ap_Vendors.contact_name;
                vendor.contact_phone1 = ap_Vendors.contact_phone1;
                vendor.contact_phone2 = ap_Vendors.contact_phone2;
                vendor.contact_email = ap_Vendors.contact_email;
                vendor.tex_identification_number = ap_Vendors.tex_identification_number;
                vendor.preferred_payment_method = ap_Vendors.preferred_payment_method;
                vendor.default_currency = ap_Vendors.default_currency;
                vendor.updated_at = DateTime.Now;

                //Bank Details
                var existing_bank = await _context.fin_PartyBankAccounts.Where(a => a.vendor_id == vendor_id).ToListAsync();
                var newbank = ap_Vendors.fin_PartyBankAccounts.Select(a => a.party_account_id).ToList();
                var delete_bank = existing_bank.Where(a => !newbank.Contains(a.party_account_id)).ToList();
                if (delete_bank.Any())
                {
                    foreach (var bank in delete_bank)
                    {
                        _context.fin_PartyBankAccounts.Remove(bank);
                    }
                }

                //Address
                var exisitng_add = await _context.st_PartyAddresses.Where(a => a.vendor_id == vendor_id).ToListAsync();
                var newAddressIds = ap_Vendors.st_PartyAddresses.Select(c => c.address_id).ToList();
                var delete_address = exisitng_add.Where(a => !newAddressIds.Contains(a.address_id)).ToList();
                if (delete_address.Any())
                {
                    foreach (var addre in delete_address)
                    {
                        _context.st_PartyAddresses.Remove(addre);
                    }
                    await _context.SaveChangesAsync();
                }


                foreach (var bank in ap_Vendors.fin_PartyBankAccounts)
                {
                    var existing_bank_details = await _context.fin_PartyBankAccounts.FirstOrDefaultAsync(a => a.party_account_id == bank.party_account_id);
                    if (existing_bank_details != null)
                    {
                        existing_bank_details.vendor_id = vendor_id;
                        existing_bank_details.party_id = vendor.vendor_id;
                        existing_bank_details.bank_name = bank.bank_name;
                        existing_bank_details.account_holder_name = bank.account_holder_name;
                        existing_bank_details.account_number = bank.account_number;
                        existing_bank_details.routing_number = bank.routing_number;
                        existing_bank_details.swift_code = bank.swift_code;
                        existing_bank_details.iban = bank.iban;
                        existing_bank_details.currency = bank.currency;
                        existing_bank_details.created_at = bank.created_at;
                        existing_bank_details.is_default = bank.is_default;
                        _context.fin_PartyBankAccounts.Update(existing_bank_details);
                        vendor.fin_PartyBankAccounts.Add(existing_bank_details);
                    }
                    else
                    {
                        bank.party_account_id = Guid.CreateVersion7();
                        bank.vendor_id = vendor_id;
                        bank.party_id = vendor.vendor_id;
                        bank.bank_name = bank.bank_name;
                        bank.account_holder_name = bank.account_holder_name;
                        bank.account_number = bank.account_number;
                        bank.routing_number = bank.routing_number;
                        bank.swift_code = bank.swift_code;
                        bank.iban = bank.iban;
                        bank.currency = bank.currency;
                        bank.created_at = bank.created_at;
                        bank.is_default = bank.is_default;
                        _context.fin_PartyBankAccounts.Add(bank);
                        vendor.fin_PartyBankAccounts.Add(bank);
                    }

                }

                foreach (var address in ap_Vendors.st_PartyAddresses)
                {

                    var existing_address = await _context.st_PartyAddresses.FirstOrDefaultAsync(a => a.address_id == address.address_id);
                    if (existing_address == null)
                    {
                        st_PartyAddresses st_PartyAddresses = new st_PartyAddresses();
                        st_PartyAddresses.address_id = Guid.CreateVersion7();
                        st_PartyAddresses.vendor_id = vendor_id;
                        st_PartyAddresses.address_type = address.address_type;
                        st_PartyAddresses.line1 = address.line1;
                        st_PartyAddresses.line2 = address.line2;
                        st_PartyAddresses.region = address.region;
                        st_PartyAddresses.postal_code = address.postal_code;
                        st_PartyAddresses.country = address.country;
                        st_PartyAddresses.created_at = DateTime.Now;
                        st_PartyAddresses.updated_at = DateTime.Now;
                        st_PartyAddresses.is_default = address.is_default;
                        _context.st_PartyAddresses.Add(st_PartyAddresses);
                        vendor.st_PartyAddresses.Add(st_PartyAddresses);
                    }
                    else
                    {
                        existing_address.address_type = address.address_type;
                        existing_address.line1 = address.line1;
                        existing_address.line2 = address.line2;
                        existing_address.region = address.region;
                        existing_address.postal_code = address.postal_code;
                        existing_address.country = address.country;
                        existing_address.updated_at = DateTime.Now;
                        existing_address.is_default = address.is_default;
                        _context.st_PartyAddresses.Update(existing_address);
                        vendor.st_PartyAddresses.Add(existing_address);

                    }
                }
                _context.ap_Vendors.Update(vendor);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<ap_Vendors>
                {
                    Status = 1,
                    Success = true,
                    Message = "updated",
                    Data = vendor
                };
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while updating a vendor.");
                return new ServiceResult<ap_Vendors>
                {
                    Success = false,
                    Message = "An error occurred while updating the vendor.",
                    Status = -1
                };
            }
            
        }
        public async Task<ServiceResult<ar_Customers>> Get_customer(Guid customer_id)
        {
            if (customer_id == null)
            {
                _logger.LogWarning("customer_id  not found");
                return new ServiceResult<ar_Customers>
                {
                    Success = false,
                    Status = -1,
                    Data = null
                };
            }
            try
            {
                var jsonresult =  _context.Database
                            .SqlQueryRaw<string>(
                                "EXEC dbo.sp_Getusers @customer_id=@customer_id,@opr=@opr",
                                new SqlParameter("@customer_id", customer_id),
                                new SqlParameter("@opr", 4)
                            )
                            .AsEnumerable().FirstOrDefault(); 
                var customer = JsonConvert.DeserializeObject<ar_Customers>(jsonresult);
                //var customer = await _context.ar_Customers.Include(a=>a.fin_PartyBankAccounts).Include(a => a.st_PartyAddresses).FirstOrDefaultAsync(a => a.customer_id == customer_id);
                if (customer == null)
                {
                    _logger.LogWarning("no data found in ar_customer table");
                    return new ServiceResult<ar_Customers>
                    {
                        Success = false,
                        Status = -2,
                        Message = "no data found in ar_customer table",
                        Data = null
                    };
                }
                return new ServiceResult<ar_Customers>
                {
                    Success = true,
                    Status = 1,
                    Message = "success",
                    Data = customer
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning a customer.");
                return new ServiceResult<ar_Customers>
                {
                    Success = false,
                    Message = "An error occurred while returning the customer.",
                    Status = -1
                };
            }

        }
        public async Task<ServiceResult<ap_Vendors>> Get_vendor (Guid vendor_id)
        {
            if (vendor_id == null)
            {
                _logger.LogWarning("customer_id  not found");
                return new ServiceResult<ap_Vendors>
                {
                    Success = false,
                    Status = -1,
                    Data = null
                };
            }
            try
            {
                var jsonList = await _context.Database
                                .SqlQueryRaw<string>(
                                    "EXEC dbo.sp_Getusers @VendorId=@VendorId,@opr=@opr",
                                    new SqlParameter("@VendorId", vendor_id),
                                    new SqlParameter("@opr", 1)
                                )
                                .AsNoTracking()
                                .ToListAsync();

                var jsonresult = jsonList.FirstOrDefault();

                var vendor = JsonConvert.DeserializeObject<ap_Vendors>(jsonresult);

                //var customer = await _context.ap_Vendors.Include(a => a.fin_PartyBankAccounts).Include(a => a.st_PartyAddresses).FirstOrDefaultAsync(a => a.vendor_id == vendor_id);
                if (vendor == null)
                {
                    _logger.LogWarning("no data found in ar_customer table");
                    return new ServiceResult<ap_Vendors>
                    {
                        Success = false,
                        Status = -2,
                        Message = "no data found in ar_customer table",
                        Data = null
                    };
                }
                return new ServiceResult<ap_Vendors>
                {
                    Success = true,
                    Status = 1,
                    Message = "success",
                    Data = vendor
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while returning a customer.");
                return new ServiceResult<ap_Vendors>
                {
                    Success = false,
                    Message = "An error occurred while returning the customer.",
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
            var jsonList = await _context.Database
                                .SqlQueryRaw<string>(
                                    "EXEC dbo.sp_Getusers @company_id=@company_id,@opr=@opr",
                                    new SqlParameter("@company_id", company_id),
                                    new SqlParameter("@opr", 6)
                                )
                                .AsNoTracking()
                                .ToListAsync();

            var jsonresult = jsonList.FirstOrDefault();
            if (jsonresult == null)
            {
                return new ServiceResult<List<ar_Customers>>
                {
                    Status = 300,
                    Success = false
                };
            }

            var customers = JsonConvert.DeserializeObject<List<ar_Customers>>(jsonresult);
            //var im_site = await _context.im_site.FirstOrDefaultAsync(s => s.site_id == company_id || s.company_id == company_id);
            //var customers = await _context.ar_Customers.Where(c => c.company_id == company_id).ToListAsync();
            if (customers == null || customers.Count == 0)
            {
                return new ServiceResult<List<ar_Customers>>
                {
                    Success = false,
                    Message = "No customers found for the given site_id",
                    Status = 0,
                    Data = customers
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

        public async Task<ServiceResult<List<ar_Customers>>> get_all_customer_search(Guid company_id,string search_text)
        {
            try
            {
                if (company_id == null || search_text == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<List<ar_Customers>>
                    {
                        Status = 300,
                        Message = "No data found",
                        Success = false,
                    };
                }

                var jsonresult = _context.Database.SqlQueryRaw<string>(
                    "EXEC dbo.sp_Getusers @company_id=@company_id,@search_text=@search_text,@opr=@opr",
                    new SqlParameter("@company_id", company_id),
                    new SqlParameter("@search_text", search_text),
                    new SqlParameter("@opr",2)).AsEnumerable().FirstOrDefault();
                var customer = JsonConvert.DeserializeObject<List<ar_Customers>>(jsonresult);
                if(customer == null)
                {
                    return new ServiceResult<List<ar_Customers>>
                    {
                        Status = 300,
                        Message = "No data found",
                        Success = false,
                    };
                }
                return new ServiceResult<List<ar_Customers>>
                {
                    Success = true,
                    Status = 200,
                    Data = customer
                };
                     

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error While get_all_customer_search");
                return new ServiceResult<List<ar_Customers>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
            
        }

        public async Task<ServiceResult<List<ap_Vendors>>> get_all_vendors_search(Guid company_id,string search_text)
        {
            try
            {
                if (company_id == null || search_text == null)
                {
                    _logger.LogInformation("No data found");
                    return new ServiceResult<List<ap_Vendors>>
                    {
                        Status = 300,
                        Message = "No data found",
                        Success = false,
                    };
                }

                var jsonresult = _context.Database.SqlQueryRaw<string>(
                    "EXEC dbo.sp_Getusers @company_id=@company_id,@search_text=@search_text,@opr=@opr",
                    new SqlParameter("@company_id", company_id),
                    new SqlParameter("@search_text", search_text),
                    new SqlParameter("@opr",3)).AsEnumerable().FirstOrDefault();
                var vendors = JsonConvert.DeserializeObject<List<ap_Vendors>>(jsonresult);
                if(vendors == null)
                {
                    return new ServiceResult<List<ap_Vendors>>
                    {
                        Status = 300,
                        Message = "No data found",
                        Success = false,
                    };
                }
                return new ServiceResult<List<ap_Vendors>>
                {
                    Success = true,
                    Status = 200,
                    Data = vendors
                };
                     

            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error While get_all_customer_search");
                return new ServiceResult<List<ap_Vendors>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
            
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
            var jsonList = await _context.Database
                                .SqlQueryRaw<string>(
                                    "EXEC dbo.sp_Getusers @company_id=@company_id,@opr=@opr",
                                    new SqlParameter("@company_id", company_id),
                                    new SqlParameter("@opr", 5)
                                )
                                .AsNoTracking()
                                .ToListAsync();

            var jsonresult = jsonList.FirstOrDefault();
            if (jsonresult == null)
            {
                return new ServiceResult<List<ap_Vendors>>
                {
                    Status = 300,
                    Success = false
                };
            }
            var vendors = JsonConvert.DeserializeObject<List<ap_Vendors>>(jsonresult);
            //var vendors = await _context.ap_Vendors.Include(a=>a.st_PartyAddresses).Include(a=>a.fin_PartyBankAccounts).Where(v => v.company_id == company_id).ToListAsync();
            if (vendors == null || vendors.Count == 0)
            {
                return new ServiceResult<List<ap_Vendors>>
                {
                    Success = false,
                    Message = "No vendors found for the given site_id",
                    Status = 0,
                    Data = vendors
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

        public async Task<ServiceResult<st_Parties>> Create_other_party(st_Parties st_Parties)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            if (st_Parties == null)
            {
                _logger.LogWarning("Create_other_party: No data found");
                return new ServiceResult<st_Parties>
                {
                    Success = false,
                    Message = "No data found",
                    Status = -1
                };
            }
            try
            {
                st_Parties.party_id = Guid.CreateVersion7();
                st_Parties.party_type = "other";
                st_Parties.display_name = st_Parties.display_name;
                st_Parties.legal_name = st_Parties.legal_name;
                st_Parties.payable_name = st_Parties.payable_name;
                st_Parties.email = st_Parties.email;
                st_Parties.phone = st_Parties.phone;
                st_Parties.default_currency = st_Parties.default_currency;
                st_Parties.tax_id = st_Parties.tax_id;
                st_Parties.status = st_Parties.status ?? "T";
                st_Parties.created_at = DateTime.Now;
                st_Parties.updated_at = DateTime.Now;

                // No ap_Vendors or ar_Customers — only st_Parties is saved
                st_Parties.ap_Vendors = null;
                st_Parties.ar_Customers = null;

                await _context.st_Parties.AddAsync(st_Parties);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<st_Parties>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = st_Parties
                };
            }
            catch (Exception ex)
            {
                    await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while creating other party.");
                return new ServiceResult<st_Parties>
                {
                    Success = false,
                    Message = "An error occurred while creating the party.",
                    Status = -1
                };
            }
        }

        public async Task<ServiceResult<List<st_Parties>>> Get_all_parties(Guid company_id)
        {
            if (company_id == Guid.Empty)
            {
                return new ServiceResult<List<st_Parties>>
                {
                    Success = false,
                    Message = "No company ID provided",
                    Status = -1
                };
            }

            var parties = await _context.st_Parties
                .Where(p => p.company_id == company_id && p.status == "T")
                .OrderBy(p => p.display_name)
                .ToListAsync();

            if (parties == null || parties.Count == 0)
            {
                return new ServiceResult<List<st_Parties>>
                {
                    Success = false,
                    Message = "No parties found",
                    Status = 0,
                    Data = parties
                };
            }

            return new ServiceResult<List<st_Parties>>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = parties
            };
        }

        public async Task<ServiceResult<List<so_sales_header_customer>>> Order_list_customer(Guid customer_id)
        {
            try
            {
                if (customer_id == null)
                {
                    return new ServiceResult<List<so_sales_header_customer>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var sales = await _context.Set<so_sales_header_customer>()
                    .FromSqlRaw("EXEC dbo.sp_Getusers  @opr=@opr, @customer_id=@customer_id",
                    new SqlParameter("@customer_id", customer_id),
                    new SqlParameter("@opr", 7)).ToListAsync();

                if(sales==null || sales.Count == 0)
                {
                    return new ServiceResult<List<so_sales_header_customer>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                return new ServiceResult<List<so_sales_header_customer>>
                {
                    Status = 200,
                    Success = true,
                    Data = sales
                };

            }catch(Exception ex)
            {
                _logger.LogInformation("Error while Order_list_customer");
                return new ServiceResult<List<so_sales_header_customer>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServiceResult<sales_customer_update_payment_dto>> Update_sales_payment(sales_customer_update_payment_dto sales_Customer)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (sales_Customer == null)
                {
                    return new ServiceResult<sales_customer_update_payment_dto>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var exisitng_sales = await _context.so_SalesHeaders.FirstOrDefaultAsync(a => a.sales_id == sales_Customer.sales_id);
                foreach (var payment in sales_Customer.pos_SalePayments_dto)
                {
                    pos_SalePayments pos_Sale = new pos_SalePayments();
                    int i = 1;
                    pos_Sale.sale_payment_id = Guid.CreateVersion7();
                    pos_Sale.business_id = exisitng_sales.business_id;
                    pos_Sale.store_id = exisitng_sales.store_id;
                    pos_Sale.payment_method_id = payment.payment_method_id;
                    pos_Sale.terminal_id = payment.terminal_id;
                    pos_Sale.shift_id = payment.shift_id;
                    pos_Sale.drawer_session_id = payment.drawer_session_id;
                    pos_Sale.receipt_no = exisitng_sales.invoice_no;
                    pos_Sale.line_no = i;
                    pos_Sale.currency_code = exisitng_sales.doc_currency_code;
                    pos_Sale.fx_rate = exisitng_sales.fx_rate_to_base;
                    pos_Sale.amount = payment.amount;
                    pos_Sale.base_amount = payment.base_amount;
                    pos_Sale.change_given = 0;
                    pos_Sale.reference_no = exisitng_sales.reference_no;
                    pos_Sale.notes = exisitng_sales.notes;
                    pos_Sale.is_voided = payment.is_voided;
                    pos_Sale.voided_by = payment.voided_by;
                    pos_Sale.created_by = payment.created_by;
                    pos_Sale.created_at = DateTime.Now;
                    i++;
                    _context.pos_SalePayments.Add(pos_Sale);

                    foreach (var img in payment.sys_Images_dto)
                    {
                        if (img.file != null)
                        {

                            var file = await UploadImage(img.file, exisitng_sales.sales_id,exisitng_sales.invoice_no, "PAYMENT", exisitng_sales.business_id ?? Guid.Empty);
                            if (!file.Success)
                            {
                                await transaction.RollbackAsync();
                                return new ServiceResult<sales_customer_update_payment_dto>
                                {
                                    Status = 300,
                                    Success = false,
                                    Message = file.Message
                                };
                            }
                        }
                    }

                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<sales_customer_update_payment_dto>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = sales_Customer
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation("Error Update_sales_payment");
                return new ServiceResult<sales_customer_update_payment_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }

        }
        public async Task<ServiceResult<string>> UploadImage(IFormFile file, Guid? sales_id,string invoice_no, string source_type, Guid business_id)
        {
            try
            {
                var co_business = await _context.co_business.FirstOrDefaultAsync(c => c.company_id == business_id);
                long planLimitInBytes;

                if (co_business.plan_type == "Basic")
                {
                    planLimitInBytes = 5L * 1024 * 1024 * 1024; // 5 GB 
                }
                else if (co_business.plan_type == "Intermediate")
                {
                    planLimitInBytes = 20L * 1024 * 1024 * 1024; // 20 GB
                }
                else if (co_business.plan_type == "Advanced")
                {
                    planLimitInBytes = 50L * 1024 * 1024 * 1024; // 50 GB
                }
                else
                {
                    planLimitInBytes = 5L * 1024 * 1024 * 1024; // default 5 GB
                }


                string storeName = co_business.business_name;
                long storeFolderSize = await GetStoreFolderSizeAsync(_configure["Wasabi:BucketName"], storeName);
                if (storeFolderSize + file.Length > planLimitInBytes)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Store storage limit reached",
                        Data = null
                    };
                }
                var bucketName = _configure["Wasabi:BucketName"];
                var fileInfo = new FileInfo(file.FileName);
                var newFileName = $"{source_type}_{sales_id}_{Guid.NewGuid()}{fileInfo.Extension}";

                // folder structure → faahi/company/{company_code}/{source_type}/{source_id}/
                var key = $"faahi/company/{co_business.company_code}/{source_type}/{invoice_no}/{newFileName}";

                // 5. upload to Wasabi S3
                using var stream = file.OpenReadStream();
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    Headers = { CacheControl = "public,max-age=604800" } // 1 week
                };
                await _s3Client.PutObjectAsync(request);

                var imageUrl = $"https://cdn.faahi.com/{key}";

                _context.sys_Images.Add(new sys_Images
                {
                    image_id = Guid.NewGuid(),
                    source_id = sales_id ?? Guid.Empty,
                    source_type = source_type,
                    business_id = business_id,
                    image_url = imageUrl,
                    status = "T",
                    created_at = DateTime.Now
                });
                await _context.SaveChangesAsync();

                return new ServiceResult<string>
                {
                    Success = true,
                    Status = 1,
                    Message = "image uploaded",
                    Data = imageUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image");
                return new ServiceResult<string>
                {
                    Success = false,
                    Status = -1,
                    Message = ex.Message
                };
            }
        }
        public async Task<long> GetStoreFolderSizeAsync(string bucketName, string storeName)
        {
            long totalSize = 0;

            var request = new Amazon.S3.Model.ListObjectsV2Request
            {
                BucketName = bucketName,
                Prefix = $"faahi/company/{storeName}/"
                // All objects under this store
            };

            ListObjectsV2Response response;
            do
            {
                response = await _s3Client.ListObjectsV2Async(request);

                foreach (var obj in response.S3Objects)
                {
                    totalSize += obj.Size;
                }

                request.ContinuationToken = response.NextContinuationToken;

            } while (response.IsTruncated);

            return totalSize;
        }
    }
}
