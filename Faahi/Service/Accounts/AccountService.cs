using Amazon.S3;
using Amazon.S3.Model;
using Dapper;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.Accounts;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace Faahi.Service.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountService> _logger;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configure;

        private static bool IsUniqueViolation(DbUpdateException ex)
        {
            return ex.InnerException is SqlException sqlEx &&
                   (sqlEx.Number == 2601 || sqlEx.Number == 2627);
        }

        public AccountService(ApplicationDbContext context, ILogger<AccountService> logger, IAmazonS3 s3Client, IConfiguration configure)
        {
            _context = context;
            _logger = logger;
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _configure = configure;
        }

        public async Task<ServiceResult<List<AccountType>>> GetAccountTypes()
        {
            try
            {
                var AccountTypes = await _context.AccountTypes.ToListAsync();
                if (AccountTypes.Count == 0)
                {
                    _logger.LogInformation("No AccountTypes data found");
                    return new ServiceResult<List<AccountType>>
                    {
                        Status = 200,
                        Success = true,
                        Message = "No data found",
                        Data = new List<AccountType>()
                    };
                }
                return new ServiceResult<List<AccountType>>
                {
                    Status = 200,
                    Success = true,
                    Data = AccountTypes
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro while GetAccountType");
                return new ServiceResult<List<AccountType>>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        public AccountType CreateAccountType(AccountType dto)
        {
            var account = new AccountType
            {
                AccountId = Guid.CreateVersion7(),
                AccountName = dto.AccountName,
                AccountNumber = dto.AccountNumber,
                AccountParentId = dto.AccountParentId
            };

            _context.AccountTypes.Add(account);
            _context.SaveChanges();

            return account;
        }

        public async Task<ServiceResult<List<gl_Accounts>>> GetGl_Accounts(Guid company_id)
        {
            try
            {
                if (company_id == Guid.Empty)
                {
                    _logger.LogInformation("NO company_id found");
                    return new ServiceResult<List<gl_Accounts>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "NO company_id found"
                    };
                }


                //var gl_account = await _context.gl_Accounts.Where(c => c.CompanyId == company_id).ToListAsync();



                var gl_account = await (from a in _context.gl_Accounts
                                        join b in _context.gl_AccountCurrentBalances on new { A = a.GlAccountId, C = a.CompanyId } equals new { A = b.AccountId ?? Guid.Empty, C = b.BusinessId } into bal
                                        from b in bal.DefaultIfEmpty()
                                        where a.CompanyId == company_id
                                        select new gl_Accounts
                                        {
                                            GlAccountId = a.GlAccountId,
                                            CompanyId = a.CompanyId,
                                            AccountNumber = a.AccountNumber,
                                            AccountName = a.AccountName,
                                            AccountType = a.AccountType,
                                            BalanceType = a.BalanceType,
                                            NormalBalance = a.NormalBalance,
                                            DetailType = a.DetailType,
                                            ParentAccountId = a.ParentAccountId,
                                            IsPostable = a.IsPostable,
                                            IsActive = a.IsActive,
                                            CurrencyCode = a.CurrencyCode,
                                            OpeningBalance = a.OpeningBalance,
                                            AsOfDate = a.AsOfDate,
                                            Description = a.Description,
                                            CreatedAt = a.CreatedAt,
                                            UpdatedAt = a.UpdatedAt,
                                            CurrentBalance = b != null ? b.CurrentBalance : a.OpeningBalance
                                        }).ToListAsync();


                if (gl_account.Count == 0)
                {
                    _logger.LogInformation("No gl_Accounts data found");
                    return new ServiceResult<List<gl_Accounts>>
                    {
                        Status = 200,
                        Success = true,
                        Message = "No data found",
                        Data = new List<gl_Accounts>()
                    };
                }
                return new ServiceResult<List<gl_Accounts>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Data found",
                    Data = gl_account
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro while GetGl_Accounts");
                return new ServiceResult<List<gl_Accounts>>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                };
            }


        }
        public async Task<ServiceResult<gl_Accounts>> CreateGl_Accounts(gl_Accounts accounts)
        {
            if (accounts == null)
            {
                return new ServiceResult<gl_Accounts> { Success = false, Message = "No data", Status = 400 };
            }

            if (accounts.CompanyId == Guid.Empty)
            {
                return new ServiceResult<gl_Accounts> { Success = false, Message = "CompanyId is required", Status = 400 };
            }

            if (accounts.OpeningBalance > 0m && !accounts.AsOfDate.HasValue)
            {
                return new ServiceResult<gl_Accounts>
                {
                    Success = false,
                    Message = "AsOfDate is required when OpeningBalance is greater than zero",
                    Status = 400
                };
            }

            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var exists = await _context.gl_Accounts
                    .AnyAsync(x => x.CompanyId == accounts.CompanyId && x.AccountNumber == accounts.AccountNumber);

                if (exists)
                {
                    return new ServiceResult<gl_Accounts>
                    {
                        Success = false,
                        Status = 400,
                        Message = "Account number already exists"
                    };
                }

                accounts.GlAccountId = Guid.CreateVersion7();
                accounts.IsPostable ??= "T";
                accounts.IsActive ??= "T";
                accounts.NormalBalance = ResolveNormalBalance(accounts.NormalBalance, accounts.AccountType);
                accounts.BalanceType = ResolveBalanceType(accounts.BalanceType, accounts.AccountType);
                accounts.CurrencyCode = string.IsNullOrWhiteSpace(accounts.CurrencyCode) ? "MVR" : accounts.CurrencyCode;
                accounts.CreatedAt = DateTime.UtcNow;
                accounts.UpdatedAt = DateTime.UtcNow;

                _context.gl_Accounts.Add(accounts);
                await _context.SaveChangesAsync();

                var isBs = string.Equals(accounts.BalanceType, "BS", StringComparison.OrdinalIgnoreCase);

                if (isBs)
                {
                    if (accounts.OpeningBalance != 0m)
                    {
                        var obeAccount = await EnsureOpeningBalanceEquityAccountAsync(accounts.CompanyId);
                        var amount = Math.Abs(accounts.OpeningBalance);
                        var isDebitNormal = IsDebitNormal(accounts.NormalBalance, accounts.AccountType);

                        decimal line1Debit, line1Credit, line2Debit, line2Credit;
                        Guid line1AccountId = accounts.GlAccountId;
                        Guid line2AccountId = obeAccount.GlAccountId;

                        if (isDebitNormal)
                        {
                            line1Debit = amount; line1Credit = 0m;
                            line2Debit = 0m; line2Credit = amount;
                        }
                        else
                        {
                            line1Debit = 0m; line1Credit = amount;
                            line2Debit = amount; line2Credit = 0m;
                        }

                        var journalDate = accounts.AsOfDate?.Date ?? DateTime.UtcNow.Date;

                        var header = new gl_JournalHeaders
                        {
                            JournalId = Guid.CreateVersion7(),
                            BusinessId = accounts.CompanyId,
                            JournalDate = journalDate,
                            PostingDate = journalDate,
                            JournalNo = await GenerateJournalNumber(accounts.CompanyId),
                            SourceType = "OPENING_BALANCE",
                            SourceId = accounts.GlAccountId,
                            JournalMemo = $"Opening balance for {accounts.AccountName}",
                            Status = "POSTED",
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = "SYSTEM"
                        };
                        _context.gl_JournalHeaders.Add(header);

                        var jl1 = new gl_JournalLines
                        {
                            JournalLineId = Guid.CreateVersion7(),
                            JournalId = header.JournalId,
                            BusinessId = accounts.CompanyId,
                            StoreId = null,
                            GlAccountId = line1AccountId,
                            DebitAmountBC = line1Debit,
                            CreditAmountBC = line1Credit,
                            LineNo = 1,
                            Description = "Opening balance",
                            CreatedAt = DateTime.UtcNow
                        };

                        var jl2 = new gl_JournalLines
                        {
                            JournalLineId = Guid.CreateVersion7(),
                            JournalId = header.JournalId,
                            BusinessId = accounts.CompanyId,
                            StoreId = null,
                            GlAccountId = line2AccountId,
                            DebitAmountBC = line2Debit,
                            CreditAmountBC = line2Credit,
                            LineNo = 2,
                            Description = "Opening balance offset",
                            CreatedAt = DateTime.UtcNow
                        };

                        _context.gl_JournalLines.AddRange(jl1, jl2);

                        var ledger1 = new gl_Ledger
                        {
                            LedgerId = Guid.CreateVersion7(),
                            BusinessId = accounts.CompanyId,
                            StoreId = null,
                            JournalId = header.JournalId,
                            JournalLineId = jl1.JournalLineId,
                            GlAccountId = jl1.GlAccountId,
                            TransactionDate = journalDate,
                            PostingDate = journalDate,
                            DebitAmountBC = jl1.DebitAmountBC,
                            CreditAmountBC = jl1.CreditAmountBC,
                            CurrencyCode = accounts.CurrencyCode,
                            ExchangeRate = 1m,
                            ReferenceNo = header.JournalNo,
                            Module = "OPENING_BALANCE",
                            Description = jl1.Description,
                            SourceId = accounts.GlAccountId,
                            SourceLineId = jl1.JournalLineId,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        var ledger2 = new gl_Ledger
                        {
                            LedgerId = Guid.CreateVersion7(),
                            BusinessId = accounts.CompanyId,
                            StoreId = null,
                            JournalId = header.JournalId,
                            JournalLineId = jl2.JournalLineId,
                            GlAccountId = jl2.GlAccountId,
                            TransactionDate = journalDate,
                            PostingDate = journalDate,
                            DebitAmountBC = jl2.DebitAmountBC,
                            CreditAmountBC = jl2.CreditAmountBC,
                            CurrencyCode = accounts.CurrencyCode,
                            ExchangeRate = 1m,
                            ReferenceNo = header.JournalNo,
                            Module = "OPENING_BALANCE",
                            Description = jl2.Description,
                            SourceId = accounts.GlAccountId,
                            SourceLineId = jl2.JournalLineId,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                        _context.gl_ledger.AddRange(ledger1, ledger2);

                        // ONLY balance writes for >0 path
                        await UpsertCurrentBalanceAsync(accounts.CompanyId, jl1.GlAccountId, accounts.CurrencyCode!, jl1.DebitAmountBC, jl1.CreditAmountBC);
                        await UpsertCurrentBalanceAsync(accounts.CompanyId, jl2.GlAccountId, accounts.CurrencyCode!, jl2.DebitAmountBC, jl2.CreditAmountBC);
                    }
                    else
                    {
                        // <= 0 path: only ensure balance row exists at zero
                        await UpsertCurrentBalanceAsync(accounts.CompanyId, accounts.GlAccountId, accounts.CurrencyCode!, 0m, 0m);
                    }
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                return new ServiceResult<gl_Accounts> { Success = true, Status = 200, Data = accounts };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();

                var inner = ex.InnerException?.InnerException?.Message
                            ?? ex.InnerException?.Message
                            ?? ex.Message;

                _logger.LogError(ex, "Error while creating GL account: {Inner}", inner);

                return new ServiceResult<gl_Accounts>
                {
                    Success = false,
                    Status = 500,
                    Message = inner
                };
            }
        }

        public async Task<ServiceResult<gl_Accounts>> UpdateGl_Accounts(Guid glAccountId, gl_Accounts accounts)
        {
            if (accounts == null)
            {
                _logger.LogWarning("Update called with null gl_Accounts");

                return new ServiceResult<gl_Accounts>
                {
                    Success = false,
                    Message = "No data found to update",
                    Status = 400
                };
            }

            try
            {

                var existing = await _context.gl_Accounts
                    .FirstOrDefaultAsync(x => x.GlAccountId == glAccountId);

                if (existing == null)
                {
                    return new ServiceResult<gl_Accounts>
                    {
                        Success = false,
                        Message = "Account not found",
                        Status = 404
                    };
                }

                // Update fields
                existing.AccountNumber = accounts.AccountNumber;
                existing.AccountName = accounts.AccountName;
                existing.IsActive = accounts.IsActive;
                existing.UpdatedAt = DateTime.UtcNow;

                _context.gl_Accounts.Update(existing);

                await _context.SaveChangesAsync();

                return new ServiceResult<gl_Accounts>
                {
                    Success = true,
                    Status = 200,
                    Message = "Success",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating gl account");

                return new ServiceResult<gl_Accounts>
                {
                    Success = false,
                    Message = "Error while updating",
                    Status = 500
                };
            }
        }

        // Get all mappings for a company
        public async Task<ServiceResult<List<gl_AccountMapping>>> GetAccountMappings(Guid companyId)
        {
            try
            {
                var mappings = await _context.gl_AccountMapping
                    .Include(x => x.GlAccount)
                    .Include(x => x.Store)
                    .Where(x => x.CompanyId == companyId)
                    .ToListAsync();

                if (mappings.Count == 0)
                {
                    _logger.LogInformation("No gl_AccountMapping data found");
                    return new ServiceResult<List<gl_AccountMapping>>
                    {
                        Status = 200,
                        Success = true,
                        Message = "No data found",
                        Data = new List<gl_AccountMapping>()
                    };
                }

                return new ServiceResult<List<gl_AccountMapping>>
                {
                    Status = 200,
                    Success = true,
                    Data = mappings
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching account mappings");
                return new ServiceResult<List<gl_AccountMapping>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResult<gl_AccountMapping>> CreateAccountMapping(gl_AccountMapping dto)
        {
            if (dto == null)
            {
                return new ServiceResult<gl_AccountMapping>
                {
                    Status = 400,
                    Success = false,
                    Message = "No data provided"
                };
            }

            try
            {
                if (dto.CompanyId == Guid.Empty)
                {
                    return new ServiceResult<gl_AccountMapping>
                    {
                        Status = 400,
                        Success = false,
                        Message = "CompanyId is required"
                    };
                }

                dto.Module = (dto.Module ?? string.Empty).Trim().ToUpperInvariant();
                dto.PurposeCode = (dto.PurposeCode ?? string.Empty).Trim().ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(dto.Module) || string.IsNullOrWhiteSpace(dto.PurposeCode))
                {
                    return new ServiceResult<gl_AccountMapping>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Module and PurposeCode are required"
                    };
                }

                // duplicate check
                var exists = await _context.gl_AccountMapping.AnyAsync(x =>
                    x.CompanyId == dto.CompanyId &&
                    x.StoreId == dto.StoreId &&
                    x.Module == dto.Module &&
                    x.PurposeCode == dto.PurposeCode);

                if (exists)
                {
                    return new ServiceResult<gl_AccountMapping>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Mapping already exists for this company/store/module/purpose"
                    };
                }

                // if GlAccountId is provided, verify it belongs to same company
                if (dto.GlAccountId.HasValue)
                {
                    var validGl = await _context.gl_Accounts.AnyAsync(a =>
                        a.GlAccountId == dto.GlAccountId.Value &&
                        a.CompanyId == dto.CompanyId);

                    if (!validGl)
                    {
                        return new ServiceResult<gl_AccountMapping>
                        {
                            Status = 400,
                            Success = false,
                            Message = "Selected GL account is invalid for this company"
                        };
                    }
                }

                dto.DefaultId = Guid.CreateVersion7();
                dto.IsRequired = string.IsNullOrWhiteSpace(dto.IsRequired) ? "T" : dto.IsRequired;
                dto.IsActive = string.IsNullOrWhiteSpace(dto.IsActive) ? "T" : dto.IsActive;
                dto.CreatedAt = DateTime.UtcNow;
                dto.UpdatedAt = DateTime.UtcNow;

                _context.gl_AccountMapping.Add(dto);
                await _context.SaveChangesAsync();

                return new ServiceResult<gl_AccountMapping>
                {
                    Status = 200,
                    Success = true,
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating account mapping");
                return new ServiceResult<gl_AccountMapping>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Update existing mapping
        public async Task<ServiceResult<gl_AccountMapping>> UpdateAccountMapping(Guid defaultId, gl_AccountMapping dto)
        {
            if (dto == null)
            {
                return new ServiceResult<gl_AccountMapping>
                {
                    Status = 400,
                    Success = false,
                    Message = "No data provided"
                };
            }

            try
            {
                var existing = await _context.gl_AccountMapping.FirstOrDefaultAsync(x => x.DefaultId == defaultId);

                if (existing == null)
                {
                    return new ServiceResult<gl_AccountMapping>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Mapping not found"
                    };
                }

                // Update fields
                existing.GlAccountId = dto.GlAccountId;
                existing.Module = dto.Module;
                existing.PurposeCode = dto.PurposeCode;
                existing.StoreId = dto.StoreId;
                existing.IsActive = dto.IsActive;
                existing.IsRequired = dto.IsRequired;
                existing.UpdatedAt = DateTime.UtcNow;

                _context.gl_AccountMapping.Update(existing);
                await _context.SaveChangesAsync();

                return new ServiceResult<gl_AccountMapping>
                {
                    Status = 200,
                    Success = true,
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating account mapping");
                return new ServiceResult<gl_AccountMapping>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Get ledger entries with optional filters
        public async Task<ServiceResult<List<gl_Ledger>>> GetLedgerEntries(Guid companyId)
        {
            try
            {
                if (companyId == Guid.Empty)
                {
                    _logger.LogInformation("No companyId found");
                    return new ServiceResult<List<gl_Ledger>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Company ID is required"
                    };
                }

                var query = _context.gl_ledger
                    .Include(x => x.GlAccount)
                    .Include(x => x.JournalHeader)
                    .Include(x => x.JournalLine)
                    .Where(x => x.BusinessId == companyId);



                var ledgerEntries = await query.OrderByDescending(x => x.PostingDate).ToListAsync();

                if (ledgerEntries.Count == 0)
                {
                    _logger.LogInformation("No ledger entries found");
                    return new ServiceResult<List<gl_Ledger>>
                    {
                        Status = 200,
                        Success = true,
                        Message = "No data found",
                        Data = new List<gl_Ledger>()
                    };
                }

                return new ServiceResult<List<gl_Ledger>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Data found",
                    Data = ledgerEntries
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ledger entries");
                return new ServiceResult<List<gl_Ledger>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Create new ledger entry
        public async Task<ServiceResult<gl_Ledger>> CreateLedgerEntry(gl_Ledger dto)
        {
            if (dto == null)
            {
                return new ServiceResult<gl_Ledger>
                {
                    Status = 400,
                    Success = false,
                    Message = "No data provided"
                };
            }

            try
            {
                // Validate that debit or credit has value (not both zero)
                if (dto.DebitAmountBC == 0 && dto.CreditAmountBC == 0)
                {
                    return new ServiceResult<gl_Ledger>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Either Debit or Credit must have a value"
                    };
                }

                // Validate that both are not set at same time
                if (dto.DebitAmountBC > 0 && dto.CreditAmountBC > 0)
                {
                    return new ServiceResult<gl_Ledger>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Cannot have both Debit and Credit on same entry"
                    };
                }

                dto.LedgerId = Guid.CreateVersion7();
                dto.CreatedAt = DateTime.UtcNow;

                _context.gl_ledger.Add(dto);
                await _context.SaveChangesAsync();

                return new ServiceResult<gl_Ledger>
                {
                    Status = 200,
                    Success = true,
                    Message = "Ledger entry created successfully",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ledger entry");
                return new ServiceResult<gl_Ledger>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Update ledger entry (use with caution - normally ledger should be immutable)
        public async Task<ServiceResult<gl_Ledger>> UpdateLedgerEntry(Guid ledgerId, gl_Ledger dto)
        {
            if (dto == null)
            {
                return new ServiceResult<gl_Ledger>
                {
                    Status = 400,
                    Success = false,
                    Message = "No data provided"
                };
            }

            try
            {
                var existing = await _context.gl_ledger.FirstOrDefaultAsync(x => x.LedgerId == ledgerId);

                if (existing == null)
                {
                    return new ServiceResult<gl_Ledger>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Ledger entry not found"
                    };
                }

                // Validate that debit or credit has value (not both zero)
                if (dto.DebitAmountBC == 0 && dto.CreditAmountBC == 0)
                {
                    return new ServiceResult<gl_Ledger>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Either Debit or Credit must have a value"
                    };
                }

                // Validate that both are not set at same time
                if (dto.DebitAmountBC > 0 && dto.CreditAmountBC > 0)
                {
                    return new ServiceResult<gl_Ledger>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Cannot have both Debit and Credit on same entry"
                    };
                }

                // Update fields (be careful - ledger should normally be immutable)
                existing.GlAccountId = dto.GlAccountId;
                existing.PostingDate = dto.PostingDate;
                existing.DebitAmountBC = dto.DebitAmountBC;
                existing.CreditAmountBC = dto.CreditAmountBC;
                existing.CurrencyCode = dto.CurrencyCode;
                existing.ReferenceNo = dto.ReferenceNo;
                existing.Module = dto.Module;
                existing.Description = dto.Description;

                _context.gl_ledger.Update(existing);
                await _context.SaveChangesAsync();

                return new ServiceResult<gl_Ledger>
                {
                    Status = 200,
                    Success = true,
                    Message = "Ledger entry updated successfully",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ledger entry");
                return new ServiceResult<gl_Ledger>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Generate automatic journal number in format JE-2026-0001
        public async Task<string> GenerateJournalNumber(Guid company_id)
        {
            try
            {
                var currentYear = DateTime.UtcNow.Year;
                var prefix = $"JE-{currentYear}-";

                // Get the last journal number for this company and year
                var lastJournal = await _context.gl_JournalHeaders
                    .Where(x => x.BusinessId == company_id && x.JournalNo != null && x.JournalNo.StartsWith(prefix))
                    .OrderByDescending(x => x.JournalNo)
                    .FirstOrDefaultAsync();

                int nextNumber = 1;

                if (lastJournal != null && !string.IsNullOrEmpty(lastJournal.JournalNo))
                {
                    // Extract the number part from the last journal number
                    var lastNumberPart = lastJournal.JournalNo.Substring(prefix.Length);
                    if (int.TryParse(lastNumberPart, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }

                // Format: JE-2026-0001
                return $"{prefix}{nextNumber:D4}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating journal number");
                // Fallback to a basic number if error occurs
                return $"JE-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
            }
        }

        // Get journal entries with filters
        public async Task<ServiceResult<List<gl_JournalHeaders>>> GetJournalEntries(Guid company_id)
        {
            try
            {
                if (company_id == Guid.Empty)
                {
                    return new ServiceResult<List<gl_JournalHeaders>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Company ID is required"
                    };
                }

                var query = _context.gl_JournalHeaders
                    .Include(x => x.JournalLines)
                        .ThenInclude(l => l.Account)
                    .Include(x => x.Attachments)
                    .Where(x => x.BusinessId == company_id);


                var journals = await query.OrderByDescending(x => x.JournalDate).ToListAsync();

                if (journals.Count == 0)
                {
                    return new ServiceResult<List<gl_JournalHeaders>>
                    {
                        Status = 200,
                        Success = true,
                        Message = "No journal entries found",
                        Data = new List<gl_JournalHeaders>()
                    };
                }

                return new ServiceResult<List<gl_JournalHeaders>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Journal entries retrieved successfully",
                    Data = journals
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching journal entries");
                return new ServiceResult<List<gl_JournalHeaders>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Get single journal entry by ID
        public async Task<ServiceResult<gl_JournalHeaders>> GetJournalEntryById(Guid journalId)
        {
            try
            {
                var journal = await _context.gl_JournalHeaders
                    .Include(x => x.JournalLines)
                        .ThenInclude(l => l.Account)
                    .Include(x => x.Attachments)
                    .FirstOrDefaultAsync(x => x.JournalId == journalId);

                if (journal == null)
                {
                    return new ServiceResult<gl_JournalHeaders>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Journal entry not found"
                    };
                }

                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Data = journal
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching journal entry");
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResult<gl_JournalHeaders>> CreateJournalEntry(gl_JournalHeaders dto)
        {
            if (dto == null)
            {
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 400,
                    Success = false,
                    Message = "No data provided"
                };
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (dto.JournalLines == null || dto.JournalLines.Count < 2)
                {
                    return new ServiceResult<gl_JournalHeaders>
                    {
                        Status = 400,
                        Success = false,
                        Message = "Journal entry must have at least 2 lines"
                    };
                }

                // Header defaults
                if (dto.JournalDate == default) dto.JournalDate = DateTime.UtcNow.Date;
                if (dto.PostingDate == default) dto.PostingDate = dto.JournalDate;

                dto.BaseCurrencyCode = string.IsNullOrWhiteSpace(dto.BaseCurrencyCode) ? "MVR" : dto.BaseCurrencyCode;
                dto.TransactionCurrencyCode = string.IsNullOrWhiteSpace(dto.TransactionCurrencyCode) ? dto.BaseCurrencyCode : dto.TransactionCurrencyCode;
                dto.ExchangeRate ??= 1m;

                if (string.IsNullOrWhiteSpace(dto.Status))
                    dto.Status = "DRAFT";

                var shouldPost = dto.Status.Equals("POSTED", StringComparison.OrdinalIgnoreCase);

                dto.JournalNo = await GenerateJournalNumber(dto.BusinessId);
                dto.JournalId = Guid.CreateVersion7();
                dto.CreatedAt = DateTime.UtcNow;
                if (shouldPost) dto.PostedAt = DateTime.UtcNow;

                var linesToProcess = dto.JournalLines.ToList();
                var attachmentsToProcess = dto.Attachments?.ToList() ?? new List<gl_JournalAttachments>();

                // Prevent EF tracking from re-attaching incoming graph unexpectedly
                dto.JournalLines = new List<gl_JournalLines>();
                dto.Attachments = new List<gl_JournalAttachments>();

                _context.gl_JournalHeaders.Add(dto);

                var ledgerIds = new List<Guid>();
                var preparedLines = new List<gl_JournalLines>();
                int lineNo = 1;

                foreach (var line in linesToProcess)
                {
                    var accountExists = await _context.gl_Accounts.AnyAsync(a => a.GlAccountId == line.GlAccountId);
                    if (!accountExists)
                    {
                        await transaction.RollbackAsync();
                        return new ServiceResult<gl_JournalHeaders>
                        {
                            Status = 400,
                            Success = false,
                            Message = $"Account ID {line.GlAccountId} does not exist in GL Accounts"
                        };
                    }

                    line.CurrencyCode = string.IsNullOrWhiteSpace(line.CurrencyCode) ? dto.TransactionCurrencyCode : line.CurrencyCode;
                    line.ExchangeRate ??= dto.ExchangeRate;

                    // FC/BC normalization
                    var rate = line.ExchangeRate ?? 1m;

                    // If only BC provided, copy to FC
                    if (line.DebitAmountFC == 0m && line.DebitAmountBC != 0m) line.DebitAmountFC = line.DebitAmountBC;
                    if (line.CreditAmountFC == 0m && line.CreditAmountBC != 0m) line.CreditAmountFC = line.CreditAmountBC;

                    // If only FC provided, calculate BC
                    if (line.DebitAmountBC == 0m && line.DebitAmountFC != 0m) line.DebitAmountBC = Math.Round(line.DebitAmountFC * rate, 4);
                    if (line.CreditAmountBC == 0m && line.CreditAmountFC != 0m) line.CreditAmountBC = Math.Round(line.CreditAmountFC * rate, 4);

                    // Basic line validation
                    if (line.DebitAmountBC < 0m || line.CreditAmountBC < 0m)
                    {
                        return new ServiceResult<gl_JournalHeaders>
                        {
                            Status = 400,
                            Success = false,
                            Message = "Debit/Credit amounts cannot be negative"
                        };
                    }

                    if (line.DebitAmountBC > 0m && line.CreditAmountBC > 0m)
                    {
                        return new ServiceResult<gl_JournalHeaders>
                        {
                            Status = 400,
                            Success = false,
                            Message = "A line cannot have both Debit and Credit amounts"
                        };
                    }

                    line.JournalLineId = Guid.CreateVersion7();
                    line.JournalId = dto.JournalId;
                    line.BusinessId = dto.BusinessId;
                    line.LineNo = lineNo++;
                    line.CreatedAt = DateTime.UtcNow;

                    _context.gl_JournalLines.Add(line);
                    preparedLines.Add(line);
                }

                // Validate balance after normalization
                var totalDebitBC = preparedLines.Sum(l => l.DebitAmountBC);
                var totalCreditBC = preparedLines.Sum(l => l.CreditAmountBC);
                if (Math.Abs(totalDebitBC - totalCreditBC) > 0.01m)
                {
                    return new ServiceResult<gl_JournalHeaders>
                    {
                        Status = 400,
                        Success = false,
                        Message = $"Journal entry is not balanced. Debits: {totalDebitBC:N2}, Credits: {totalCreditBC:N2}"
                    };
                }

                dto.TotalDebitBC = totalDebitBC;
                dto.TotalCreditBC = totalCreditBC;
                dto.TotalDebitFC = preparedLines.Sum(l => l.DebitAmountFC);
                dto.TotalCreditFC = preparedLines.Sum(l => l.CreditAmountFC);

                await _context.SaveChangesAsync();

                if (shouldPost)
                {
                    foreach (var line in preparedLines)
                    {
                        var ledgerEntry = new gl_Ledger
                        {
                            LedgerId = Guid.CreateVersion7(),
                            BusinessId = line.BusinessId,
                            StoreId = line.StoreId,
                            JournalId = dto.JournalId,
                            JournalLineId = line.JournalLineId,
                            GlAccountId = line.GlAccountId,
                            TransactionDate = dto.JournalDate.Date,
                            PostingDate = dto.PostingDate.Date,

                            BaseCurrencyCode = dto.BaseCurrencyCode,
                            TransactionCurrencyCode = dto.TransactionCurrencyCode,
                            ExchangeRate = line.ExchangeRate ?? dto.ExchangeRate ?? 1m,

                            DebitAmountFC = line.DebitAmountFC == 0m ? line.DebitAmountBC : line.DebitAmountFC,
                            CreditAmountFC = line.CreditAmountFC == 0m ? line.CreditAmountBC : line.CreditAmountFC,
                            DebitAmountBC = line.DebitAmountBC,
                            CreditAmountBC = line.CreditAmountBC,

                            CurrencyCode = string.IsNullOrWhiteSpace(line.CurrencyCode) ? dto.TransactionCurrencyCode : line.CurrencyCode,
                            Module = line.SourceType ?? dto.SourceType ?? "JOURNAL",
                            ReferenceNo = dto.ReferenceNo ?? dto.JournalNo,
                            Description = line.Description ?? dto.JournalMemo,
                            SourceId = dto.SourceId ?? dto.JournalId,
                            SourceLineId = line.SourceLineId ?? line.JournalLineId,

                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            CreatedBy = dto.CreatedBy
                        };

                        _context.gl_ledger.Add(ledgerEntry);
                        ledgerIds.Add(ledgerEntry.LedgerId);

                        // Keep current balances in BASE currency
                        await UpsertCurrentBalanceAsync(
                            line.BusinessId,
                            line.GlAccountId,
                            dto.BaseCurrencyCode ?? "MVR",
                            line.DebitAmountBC,
                            line.CreditAmountBC
                        );
                    }
                }

                foreach (var attachment in attachmentsToProcess)
                {
                    attachment.AttachmentId = Guid.CreateVersion7();
                    attachment.JournalId = dto.JournalId;
                    attachment.UploadedAt = DateTime.UtcNow;
                    _context.gl_JournalAttachments.Add(attachment);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation(
                    "Created journal entry {JournalNo} for business {BusinessId} with {LedgerCount} ledger entries",
                    dto.JournalNo, dto.BusinessId, ledgerIds.Count);

                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = $"Journal entry created successfully with number: {dto.JournalNo}",
                    Data = dto
                };
            }
            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();
            //    _logger.LogError(ex, "Error creating journal entry for business {BusinessId}", dto.BusinessId);

            //    return new ServiceResult<gl_JournalHeaders>
            //    {
            //        Status = 500,
            //        Success = false,
            //        Message = ex.Message
            //    };
            //}
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                await transaction.RollbackAsync();
                _logger.LogWarning(ex, "Duplicate JournalNo for business {BusinessId}", dto.BusinessId);

                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 409,
                    Success = false,
                    Message = "Journal number conflict. Please retry."
                };
            }
        }

        private async Task RepostLedgerForJournalAsync(gl_JournalHeaders header, List<gl_JournalLines> lines, string currencyCode, string? userName = null)
        {
            // 1) Reverse old balance impact
            var oldLedgerRows = await _context.gl_ledger
                .Where(x => x.JournalId == header.JournalId)
                .ToListAsync();

            foreach (var old in oldLedgerRows)
            {
                // reverse old delta (debit-credit) by swapping values
                await UpsertCurrentBalanceAsync(
                    old.BusinessId,
                    old.GlAccountId,
                    string.IsNullOrWhiteSpace(currencyCode) ? "MVR" : currencyCode,
                    old.CreditAmountBC,
                    old.DebitAmountBC
                );
            }

            // 2) Delete old derived ledger rows (QuickBooks style)
            if (oldLedgerRows.Count > 0)
            {
                _context.gl_ledger.RemoveRange(oldLedgerRows);
                await _context.SaveChangesAsync();
            }

            // 3) Recreate ledger rows from latest journal lines
            var newLedgerRows = new List<gl_Ledger>();

            foreach (var line in lines)
            {
                var ledger = new gl_Ledger
                {
                    LedgerId = Guid.CreateVersion7(),
                    BusinessId = line.BusinessId,
                    StoreId = line.StoreId,
                    JournalId = header.JournalId,
                    JournalLineId = line.JournalLineId,
                    GlAccountId = line.GlAccountId,
                    TransactionDate = header.JournalDate.Date,
                    PostingDate = header.PostingDate.Date,

                    // keep your BC/FC values
                    DebitAmountFC = line.DebitAmountFC == 0 ? line.DebitAmountBC : line.DebitAmountFC,
                    CreditAmountFC = line.CreditAmountFC == 0 ? line.CreditAmountBC : line.CreditAmountFC,
                    DebitAmountBC = line.DebitAmountBC,
                    CreditAmountBC = line.CreditAmountBC,
                    // add these 3 (you asked to keep them)
                    CurrencyCode = string.IsNullOrWhiteSpace(line.CurrencyCode) ? (header.TransactionCurrencyCode ?? header.BaseCurrencyCode ?? "MVR") : line.CurrencyCode,
                    Module = line.SourceType ?? header.SourceType ?? "JOURNAL",
                    UpdatedAt = DateTime.UtcNow,

                    BaseCurrencyCode = header.BaseCurrencyCode,
                    TransactionCurrencyCode = header.TransactionCurrencyCode,
                    ExchangeRate = line.ExchangeRate ?? header.ExchangeRate ?? 1m,
                    //DebitAmountFC = line.DebitAmountFC,
                    //CreditAmountFC = line.CreditAmountFC,
                    //DebitAmountBC = line.DebitAmountBC,
                    //CreditAmountBC = line.CreditAmountBC,
                    ReferenceNo = header.ReferenceNo ?? header.JournalNo,
                    SourceType = line.SourceType ?? header.SourceType ?? "JOURNAL",
                    SourceId = header.SourceId ?? header.JournalId,
                    SourceLineId = line.SourceLineId ?? line.JournalLineId,
                    Description = line.Description ?? header.JournalMemo,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userName
                };

                newLedgerRows.Add(ledger);

                // Apply new balance impact (BC only)
                await UpsertCurrentBalanceAsync(
                    line.BusinessId,
                    line.GlAccountId,
                    string.IsNullOrWhiteSpace(currencyCode) ? "MVR" : currencyCode,
                    line.DebitAmountBC,
                    line.CreditAmountBC
                );
            }

            _context.gl_ledger.AddRange(newLedgerRows);
        }

        public async Task<ServiceResult<gl_JournalHeaders>> UpdateJournalEntry(Guid journalId, gl_JournalHeaders dto)
        {
            if (dto == null)
                return new ServiceResult<gl_JournalHeaders> { Status = 400, Success = false, Message = "No data provided" };

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existing = await _context.gl_JournalHeaders
                    .Include(x => x.JournalLines)
                    .FirstOrDefaultAsync(x => x.JournalId == journalId);

                if (existing == null)
                    return new ServiceResult<gl_JournalHeaders> { Status = 404, Success = false, Message = "Journal entry not found" };

                if (dto.JournalLines == null || dto.JournalLines.Count < 2)
                    return new ServiceResult<gl_JournalHeaders> { Status = 400, Success = false, Message = "Journal entry must have at least 2 lines" };

                // Validate balanced in base currency
                var totalDrBc = dto.JournalLines.Sum(x => x.DebitAmountBC);
                var totalCrBc = dto.JournalLines.Sum(x => x.CreditAmountBC);
                if (Math.Abs(totalDrBc - totalCrBc) > 0.01m)
                    return new ServiceResult<gl_JournalHeaders> { Status = 400, Success = false, Message = "Journal is not balanced in base currency" };

                // Header updates
                existing.JournalDate = dto.JournalDate;
                existing.PostingDate = dto.PostingDate == default ? dto.JournalDate : dto.PostingDate;
                existing.JournalMemo = dto.JournalMemo;
                existing.ReferenceNo = dto.ReferenceNo;
                existing.SourceType = dto.SourceType;
                existing.SourceId = dto.SourceId;
                existing.StoreId = dto.StoreId;
                existing.BaseCurrencyCode = string.IsNullOrWhiteSpace(dto.BaseCurrencyCode) ? "MVR" : dto.BaseCurrencyCode;
                existing.TransactionCurrencyCode = string.IsNullOrWhiteSpace(dto.TransactionCurrencyCode) ? existing.BaseCurrencyCode : dto.TransactionCurrencyCode;
                existing.ExchangeRate = dto.ExchangeRate ?? 1m;
                existing.UpdatedAt = DateTime.UtcNow;
                existing.UpdatedBy = dto.UpdatedBy;

                // Replace lines
                _context.gl_JournalLines.RemoveRange(existing.JournalLines);

                int lineNo = 1;
                var newLines = new List<gl_JournalLines>();
                foreach (var line in dto.JournalLines)
                {
                    var newLine = new gl_JournalLines
                    {
                        JournalLineId = Guid.CreateVersion7(),
                        JournalId = existing.JournalId,
                        BusinessId = existing.BusinessId,
                        StoreId = line.StoreId,
                        GlAccountId = line.GlAccountId,
                        CurrencyCode = string.IsNullOrWhiteSpace(line.CurrencyCode) ? existing.TransactionCurrencyCode : line.CurrencyCode,
                        ExchangeRate = line.ExchangeRate ?? existing.ExchangeRate ?? 1m,
                        DebitAmountFC = line.DebitAmountFC,
                        CreditAmountFC = line.CreditAmountFC,
                        DebitAmountBC = line.DebitAmountBC,
                        CreditAmountBC = line.CreditAmountBC,
                        LineNo = lineNo++,
                        Description = line.Description,
                        SourceType = line.SourceType ?? existing.SourceType,
                        SourceLineId = line.SourceLineId,
                        CustomerId = line.CustomerId,
                        SupplierId = line.SupplierId,
                        CostCenterId = line.CostCenterId,
                        DepartmentId = line.DepartmentId,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = dto.UpdatedBy
                    };

                    newLines.Add(newLine);
                }

                _context.gl_JournalLines.AddRange(newLines);

                // Update header totals
                existing.TotalDebitFC = newLines.Sum(x => x.DebitAmountFC);
                existing.TotalCreditFC = newLines.Sum(x => x.CreditAmountFC);
                existing.TotalDebitBC = newLines.Sum(x => x.DebitAmountBC);
                existing.TotalCreditBC = newLines.Sum(x => x.CreditAmountBC);

                // Posted status => delete/repost ledger rows
                var shouldPost = string.Equals(existing.Status, "POSTED", StringComparison.OrdinalIgnoreCase)
                              || string.Equals(dto.Status, "POSTED", StringComparison.OrdinalIgnoreCase);

                existing.Status = dto.Status ?? existing.Status;
                if (shouldPost)
                {
                    existing.PostedAt ??= DateTime.UtcNow;
                    existing.PostedBy ??= dto.UpdatedBy;
                }

                await _context.SaveChangesAsync();

                if (shouldPost)
                {
                    await RepostLedgerForJournalAsync(existing, newLines, existing.BaseCurrencyCode ?? "MVR", dto.UpdatedBy);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Journal updated successfully",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating journal entry");
                return new ServiceResult<gl_JournalHeaders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        // Upload multiple attachments for journal entry
        public async Task<ActionResult<ServiceResult<List<gl_JournalAttachments>>>> UploadJournalAttachments(IFormFile[] formFiles, Guid journalId)
        {
            if (formFiles == null || formFiles.Length == 0)
            {
                _logger.LogWarning("UploadJournalAttachments: No files uploaded");
                return new ServiceResult<List<gl_JournalAttachments>>
                {
                    Status = 400,
                    Success = false,
                    Message = "No files uploaded",
                    Data = null
                };
            }

            try
            {
                // Get journal header with business info
                var journal = await _context.gl_JournalHeaders
                    .Include(x => x.Attachments)
                    .FirstOrDefaultAsync(x => x.JournalId == journalId);

                if (journal == null)
                {
                    _logger.LogWarning("UploadJournalAttachments: Invalid journal ID");
                    return new ServiceResult<List<gl_JournalAttachments>>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Journal entry not found",
                        Data = null
                    };
                }

                // Get company info for folder structure
                var business = await _context.co_business
                    .FirstOrDefaultAsync(c => c.company_id == journal.BusinessId);

                if (business == null)
                {
                    return new ServiceResult<List<gl_JournalAttachments>>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Company not found",
                        Data = null
                    };
                }

                var bucketName = _configure["Wasabi:BucketName"];
                var uploadedAttachments = new List<gl_JournalAttachments>();

                int fileNumber = 0;

                foreach (var file in formFiles)
                {
                    fileNumber++;

                    FileInfo fileInfo = new FileInfo(file.FileName);
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string safeFileName = $"attachment_{fileNumber}_{timestamp}{fileInfo.Extension}";

                    // Folder structure: faahi/company/{company_code}/journal_{journal_id}/attachments/
                    string key = $"faahi/company/{business.company_code}/journal_{journalId}/attachments/{safeFileName}";

                    // Upload to Wasabi
                    using var stream = file.OpenReadStream();
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        InputStream = stream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead,
                        Headers = { CacheControl = "public,max-age=604800" } // 1 week caching
                    };

                    await _s3Client.PutObjectAsync(request);

                    // Save canonical URL in DB
                    string canonicalUrl = $"https://cdn.faahi.com/{key}";

                    var attachment = new gl_JournalAttachments
                    {
                        AttachmentId = Guid.CreateVersion7(),
                        JournalId = journalId,
                        FileName = file.FileName, // Original filename
                        image_url = canonicalUrl,
                        UploadedAt = DateTime.UtcNow
                    };

                    _context.gl_JournalAttachments.Add(attachment);
                    journal.Attachments.Add(attachment);
                    uploadedAttachments.Add(attachment);
                }

                _context.gl_JournalHeaders.Update(journal);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Uploaded {Count} attachments for journal {JournalNo}",
                    uploadedAttachments.Count, journal.JournalNo);

                return new ServiceResult<List<gl_JournalAttachments>>
                {
                    Status = 200,
                    Success = true,
                    Message = $"{uploadedAttachments.Count} file(s) uploaded successfully",
                    Data = uploadedAttachments
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadJournalAttachments: Error uploading files for journal {JournalId}", journalId);
                return new ServiceResult<List<gl_JournalAttachments>>
                {
                    Status = 500,
                    Success = false,
                    Message = "An error occurred while uploading files",
                    Data = null
                };
            }
        }

        // Delete journal attachment
        public async Task<ServiceResult<bool>> DeleteJournalAttachment(Guid attachmentId)
        {
            try
            {
                var attachment = await _context.gl_JournalAttachments
                    .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId);

                if (attachment == null)
                {
                    return new ServiceResult<bool>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Attachment not found"
                    };
                }

                // Delete from Wasabi
                if (!string.IsNullOrEmpty(attachment.image_url))
                {
                    try
                    {
                        var bucketName = _configure["Wasabi:BucketName"];
                        var key = new Uri(attachment.image_url).AbsolutePath.TrimStart('/');
                        await _s3Client.DeleteObjectAsync(bucketName, key);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete attachment from S3, continuing with DB deletion");
                    }
                }

                // Delete from database
                _context.gl_JournalAttachments.Remove(attachment);
                await _context.SaveChangesAsync();

                return new ServiceResult<bool>
                {
                    Status = 200,
                    Success = true,
                    Message = "Attachment deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting journal attachment {AttachmentId}", attachmentId);
                return new ServiceResult<bool>
                {
                    Status = 500,
                    Success = false,
                    Message = "An error occurred while deleting attachment"
                };
            }
        }

        public async Task<ServiceResult<List<gl_AccountCurrentBalances>>> GetAccountCurrentBalances(Guid businessId)
        {
            try
            {
                if (businessId == Guid.Empty)
                {
                    return new ServiceResult<List<gl_AccountCurrentBalances>>
                    {
                        Success = false,
                        Status = 400,
                        Message = "Business ID is required"
                    };
                }

                //var balances = await _context.gl_AccountCurrentBalances
                //    .Where(x => x.BusinessId == businessId)
                //    .OrderBy(x => x.AccountId)
                //    .ToListAsync();
                var balances = await _context.gl_AccountCurrentBalances
                .Where(x => x.BusinessId == businessId)
                .OrderBy(x => x.AccountId)
                .ToListAsync();

                return new ServiceResult<List<gl_AccountCurrentBalances>>
                {
                    Success = true,
                    Status = 200,
                    Message = balances.Count == 0 ? "No data found" : "Data found",
                    Data = balances
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting account current balances");

                return new ServiceResult<List<gl_AccountCurrentBalances>>
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<gl_AccountCurrentBalances>> CreateAccountCurrentBalances(gl_AccountCurrentBalances dto)
        {
            if (dto == null)
            {
                return new ServiceResult<gl_AccountCurrentBalances>
                {
                    Success = false,
                    Status = 400,
                    Message = "No data provided"
                };
            }

            try
            {
                //var exists = await _context.gl_AccountCurrentBalances
                //    .AnyAsync(x => x.BusinessId == dto.BusinessId &&
                //                   x.AccountId == dto.AccountId);

                //if (exists)
                //{
                //    return new ServiceResult<gl_AccountCurrentBalances>
                //    {
                //        Success = false,
                //        Status = 400,
                //        Message = "Balance already exists for this account"
                //    };
                //}

                if (dto.BusinessId == Guid.Empty || dto.AccountId == null || dto.AccountId == Guid.Empty)
                {
                    return new ServiceResult<gl_AccountCurrentBalances>
                    {
                        Success = false,
                        Status = 400,
                        Message = "BusinessId and AccountId are required"
                    };
                }
                var existing = await _context.gl_AccountCurrentBalances
                    .FirstOrDefaultAsync(x => x.BusinessId == dto.BusinessId && x.AccountId == dto.AccountId);
                if (existing != null)
                {
                    return new ServiceResult<gl_AccountCurrentBalances>
                    {
                        Success = false,
                        Status = 400,
                        Message = "Balance already exists for this account"
                    };
                }
                dto.gl_account_current_id ??= Guid.CreateVersion7();
                dto.CurrencyCode = string.IsNullOrWhiteSpace(dto.CurrencyCode) ? "MVR" : dto.CurrencyCode;
                dto.LastUpdated = DateTime.UtcNow;

                _context.gl_AccountCurrentBalances.Add(dto);
                await _context.SaveChangesAsync();

                return new ServiceResult<gl_AccountCurrentBalances>
                {
                    Success = true,
                    Status = 200,
                    Data = dto,
                    Message = "Account current balance created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account current balance");

                return new ServiceResult<gl_AccountCurrentBalances>
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<bool>> SeedAccountCurrentBalances(Guid businessId)
        {
            try
            {
                if (businessId == Guid.Empty)
                {
                    return new ServiceResult<bool>
                    {
                        Success = false,
                        Status = 400,
                        Message = "Business ID is required",
                        Data = false
                    };
                }

                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC dbo.sp_SeedAccountCurrentBalances @CompanyId={businessId}"
                );

                return new ServiceResult<bool>
                {
                    Success = true,
                    Status = 200,
                    Message = "Current balances seeded/updated successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding account current balances for businessId {BusinessId}", businessId);

                return new ServiceResult<bool>
                {
                    Success = false,
                    Status = 500,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

        private bool IsDebitNormal(string? normalBalance, string? accountType)
        {
            if (!string.IsNullOrWhiteSpace(normalBalance))
            {
                return normalBalance.Equals("DEBIT", StringComparison.OrdinalIgnoreCase);
            }

            // fallback from account type
            var type = accountType?.Trim().ToUpperInvariant();
            return type == "ASSETS" || type == "EXPENSES";
        }

        private string ResolveNormalBalance(string? normalBalance, string? accountType)
        {
            if (!string.IsNullOrWhiteSpace(normalBalance))
                return normalBalance;

            var type = accountType?.Trim().ToUpperInvariant();
            return (type == "LIABILITIES" || type == "EQUITY" || type == "INCOME") ? "Credit" : "Debit";
        }

        private string ResolveBalanceType(string? balanceType, string? accountType)
        {
            if (!string.IsNullOrWhiteSpace(balanceType))
                return balanceType;

            var type = accountType?.Trim().ToUpperInvariant();
            return (type == "INCOME" || type == "EXPENSES") ? "PNL" : "BS";
        }

        private async Task<gl_Accounts> EnsureOpeningBalanceEquityAccountAsync(Guid companyId)
        {
            var obe = await _context.gl_Accounts.FirstOrDefaultAsync(a =>
                a.CompanyId == companyId &&
                a.AccountName == "Opening Balance Equity");

            if (obe != null) return obe;

            // choose available number in equity range
            int nextNumber = 3999;
            while (await _context.gl_Accounts.AnyAsync(a =>
                a.CompanyId == companyId && a.AccountNumber == nextNumber.ToString()))
            {
                nextNumber--;
                if (nextNumber < 3900)
                    throw new Exception("Could not allocate account number for Opening Balance Equity.");
            }

            obe = new gl_Accounts
            {
                GlAccountId = Guid.CreateVersion7(),
                CompanyId = companyId,
                AccountNumber = nextNumber.ToString(),
                AccountName = "Opening Balance Equity",
                AccountType = "Equity",
                BalanceType = "BS",
                NormalBalance = "Credit",
                DetailType = "Equity",
                ParentAccountId = null,
                IsPostable = "T",
                IsActive = "T",
                CurrencyCode = "MVR",
                OpeningBalance = 0,
                AsOfDate = null,
                Description = "System account for opening balances",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.gl_Accounts.Add(obe);
            await _context.SaveChangesAsync();

            return obe;
        }
        private async Task UpsertCurrentBalanceAsync(Guid businessId, Guid accountId, string currencyCode, decimal debitAmount, decimal creditAmount, Guid? userId = null)
        {
            var delta = debitAmount - creditAmount;
            var balanceRow = _context.gl_AccountCurrentBalances.Local.FirstOrDefault(x => x.BusinessId == businessId && x.AccountId == accountId) ?? await _context.gl_AccountCurrentBalances.FirstOrDefaultAsync(x => x.BusinessId == businessId && x.AccountId == accountId);

            if (balanceRow == null)
            {
                balanceRow = new gl_AccountCurrentBalances
                {
                    gl_account_current_id = Guid.CreateVersion7(),
                    BusinessId = businessId,
                    AccountId = accountId,
                    CurrencyCode = string.IsNullOrWhiteSpace(currencyCode) ? "MVR" : currencyCode,
                    CurrentBalance = delta,
                    LastUpdated = DateTime.UtcNow,
                    LastUpdatedByUserId = userId
                };
                _context.gl_AccountCurrentBalances.Add(balanceRow);
            }
            else
            {
                balanceRow.CurrentBalance += delta;
                balanceRow.LastUpdated = DateTime.UtcNow;
                balanceRow.LastUpdatedByUserId = userId;
                if (string.IsNullOrWhiteSpace(balanceRow.CurrencyCode))
                    balanceRow.CurrencyCode = string.IsNullOrWhiteSpace(currencyCode) ? "MVR" : currencyCode;
            }
        }
        public async Task<ServiceResult<List<ap_Expenses>>> GetExpenses(Guid businessId)
        {
            try
            {
                if (businessId == Guid.Empty)
                {
                    return new ServiceResult<List<ap_Expenses>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "BusinessId is required"
                    };
                }
                var expenses = await _context.ap_Expenses
                    .Include(x => x.ExpenseLines)
                    .Where(x => x.BusinessId == businessId)
                    .OrderByDescending(x => x.ExpenseDate)
                    .ToListAsync();
                return new ServiceResult<List<ap_Expenses>>
                {
                    Status = 200,
                    Success = true,
                    Message = expenses.Count == 0 ? "No data found" : "Data found",
                    Data = expenses
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching expenses for business {BusinessId}", businessId);
                return new ServiceResult<List<ap_Expenses>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        // ─────────────────────────────────────────────
        //  GET BY ID
        // ─────────────────────────────────────────────
        public async Task<ServiceResult<ap_Expenses>> GetExpenseById(Guid expenseId)
        {
            try
            {
                var expense = await _context.ap_Expenses
                    .Include(x => x.ExpenseLines)
                    .FirstOrDefaultAsync(x => x.ExpenseId == expenseId);
                if (expense == null)
                {
                    return new ServiceResult<ap_Expenses>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Expense not found"
                    };
                }
                return new ServiceResult<ap_Expenses>
                {
                    Status = 200,
                    Success = true,
                    Data = expense
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching expense {ExpenseId}", expenseId);
                return new ServiceResult<ap_Expenses>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        // ─────────────────────────────────────────────
        //  CREATE
        // ─────────────────────────────────────────────
        public async Task<ServiceResult<ap_Expenses>> CreateExpense(ap_Expenses dto)
        {
            if (dto == null)
                return new ServiceResult<ap_Expenses> { Status = 400, Success = false, Message = "No data provided" };
            if (dto.BusinessId == Guid.Empty)
                return new ServiceResult<ap_Expenses> { Status = 400, Success = false, Message = "BusinessId is required" };
            if (dto.PaymentAccountId == Guid.Empty)
                return new ServiceResult<ap_Expenses> { Status = 400, Success = false, Message = "PaymentAccountId is required" };
            if (dto.ExpenseLines == null || dto.ExpenseLines.Count == 0)
                return new ServiceResult<ap_Expenses> { Status = 400, Success = false, Message = "At least one expense line is required" };
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // ── Header defaults ──────────────────────────────
                dto.ExpenseId = Guid.CreateVersion7();
                dto.ExpenseNo = await GenerateExpenseNumber(dto.BusinessId);
                dto.CurrencyCode = string.IsNullOrWhiteSpace(dto.CurrencyCode) ? "MVR" : dto.CurrencyCode;
                dto.ExchangeRate = dto.ExchangeRate <= 0 ? 1m : dto.ExchangeRate;
                if (string.IsNullOrWhiteSpace(dto.Status))
                    dto.Status = "DRAFT";
                dto.CreatedAt = DateTime.UtcNow;
                // ── Lines ────────────────────────────────────────
                var linesToProcess = dto.ExpenseLines.ToList();
                dto.ExpenseLines = new List<ap_ExpenseLines>();
                // Recalculate totals from lines to avoid client tampering
                var totalAmount = linesToProcess.Sum(l => l.Amount);
                var rate = dto.ExchangeRate;
                dto.TotalAmount = totalAmount;
                dto.BaseTotalAmount = Math.Round(totalAmount * rate, 2);
                _context.ap_Expenses.Add(dto);
                int sortOrder = 1;
                var preparedLines = new List<ap_ExpenseLines>();
                foreach (var line in linesToProcess)
                {
                    if (line.AccountId == Guid.Empty)
                    {
                        await tx.RollbackAsync();
                        return new ServiceResult<ap_Expenses>
                        {
                            Status = 400,
                            Success = false,
                            Message = "Each expense line must have a valid AccountId"
                        };
                    }
                    var accountExists = await _context.gl_Accounts.AnyAsync(a => a.GlAccountId == line.AccountId);
                    if (!accountExists)
                    {
                        await tx.RollbackAsync();
                        return new ServiceResult<ap_Expenses>
                        {
                            Status = 400,
                            Success = false,
                            Message = $"Account ID {line.AccountId} does not exist in GL Accounts"
                        };
                    }
                    line.ExpenseLineId = Guid.CreateVersion7();
                    line.ExpenseId = dto.ExpenseId;
                    line.BaseAmount = Math.Round(line.Amount * rate, 2);
                    line.SortOrder = sortOrder++;
                    line.CreatedAt = DateTime.UtcNow;
                    _context.ap_ExpenseLines.Add(line);
                    preparedLines.Add(line);
                }
                await _context.SaveChangesAsync();
                // ── Journal Entry ────────────────────────────────
                // Only post journal when status is POSTED
                var shouldPost = dto.Status.Equals("POSTED", StringComparison.OrdinalIgnoreCase);
                if (shouldPost)
                {
                    await PostExpenseJournalAsync(dto, preparedLines);
                }
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                _logger.LogInformation("Created expense {ExpenseNo} for business {BusinessId}", dto.ExpenseNo, dto.BusinessId);
                return new ServiceResult<ap_Expenses>
                {
                    Status = 200,
                    Success = true,
                    Message = $"Expense created successfully with number: {dto.ExpenseNo}",
                    Data = dto
                };
            }
            //catch (Exception ex)
            //{
            //    await tx.RollbackAsync();
            //    var inner = ex.InnerException?.InnerException?.Message
            //                ?? ex.InnerException?.Message
            //                ?? ex.Message;
            //    _logger.LogError(ex, "Error creating expense: {Inner}", inner);
            //    return new ServiceResult<ap_Expenses>
            //    {
            //        Status = 500,
            //        Success = false,
            //        Message = inner
            //    };
            //}
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                await tx.RollbackAsync();
                _logger.LogWarning(ex, "Duplicate ExpenseNo for business {BusinessId}", dto.BusinessId);

                return new ServiceResult<ap_Expenses>
                {
                    Status = 409,
                    Success = false,
                    Message = "Expense number conflict. Please retry."
                };
            }
        }
        // ─────────────────────────────────────────────
        //  UPDATE
        // ─────────────────────────────────────────────
        //public async Task<ServiceResult<ap_Expenses>> UpdateExpense(Guid expenseId, ap_Expenses dto)
        //{
        //    if (dto == null)
        //        return new ServiceResult<ap_Expenses> { Status = 400, Success = false, Message = "No data provided" };
        //    await using var tx = await _context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var existing = await _context.ap_Expenses
        //            .Include(x => x.ExpenseLines)
        //            .FirstOrDefaultAsync(x => x.ExpenseId == expenseId);
        //        if (existing == null)
        //            return new ServiceResult<ap_Expenses> { Status = 404, Success = false, Message = "Expense not found" };
        //        if (existing.Status == "POSTED")
        //            return new ServiceResult<ap_Expenses>
        //            {
        //                Status = 400,
        //                Success = false,
        //                Message = "Cannot edit a posted expense. Void and re-enter instead."
        //            };
        //        // ── Update header ────────────────────────────────
        //        existing.PayeeId = dto.PayeeId;
        //        existing.PayeeName = dto.PayeeName;
        //        existing.PaymentAccountId = dto.PaymentAccountId;
        //        existing.PaymentMethod = dto.PaymentMethod;
        //        existing.ReferenceNo = dto.ReferenceNo;
        //        existing.ExpenseDate = dto.ExpenseDate;
        //        existing.CurrencyCode = string.IsNullOrWhiteSpace(dto.CurrencyCode) ? "MVR" : dto.CurrencyCode;
        //        existing.ExchangeRate = dto.ExchangeRate <= 0 ? 1m : dto.ExchangeRate;
        //        existing.Memo = dto.Memo;
        //        existing.StoreId = dto.StoreId;
        //        existing.UpdatedAt = DateTime.UtcNow;
        //        existing.UpdatedBy = dto.UpdatedBy;
        //        // ── Replace lines ────────────────────────────────
        //        _context.ap_ExpenseLines.RemoveRange(existing.ExpenseLines);
        //        var linesToProcess = dto.ExpenseLines?.ToList() ?? new List<ap_ExpenseLines>();
        //        var rate = existing.ExchangeRate;
        //        var totalAmount = linesToProcess.Sum(l => l.Amount);
        //        existing.TotalAmount = totalAmount;
        //        existing.BaseTotalAmount = Math.Round(totalAmount * rate, 2);
        //        var preparedLines = new List<ap_ExpenseLines>();
        //        int sortOrder = 1;
        //        foreach (var line in linesToProcess)
        //        {
        //            var newLine = new ap_ExpenseLines
        //            {
        //                ExpenseLineId = Guid.CreateVersion7(),
        //                ExpenseId = existing.ExpenseId,
        //                AccountId = line.AccountId,
        //                Description = line.Description,
        //                Amount = line.Amount,
        //                BaseAmount = Math.Round(line.Amount * rate, 2),
        //                SortOrder = sortOrder++,
        //                CreatedAt = DateTime.UtcNow
        //            };
        //            _context.ap_ExpenseLines.Add(newLine);
        //            preparedLines.Add(newLine);
        //        }
        //        // ── Re-post journal if changing to POSTED ────────
        //        var shouldPost = dto.Status?.Equals("POSTED", StringComparison.OrdinalIgnoreCase) == true
        //                      && !existing.Status.Equals("POSTED", StringComparison.OrdinalIgnoreCase);
        //        existing.Status = dto.Status ?? existing.Status;
        //        await _context.SaveChangesAsync();
        //        if (shouldPost)
        //        {
        //            await PostExpenseJournalAsync(existing, preparedLines);
        //            await _context.SaveChangesAsync();
        //        }
        //        await tx.CommitAsync();
        //        return new ServiceResult<ap_Expenses>
        //        {
        //            Status = 200,
        //            Success = true,
        //            Message = "Expense updated successfully",
        //            Data = existing
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        await tx.RollbackAsync();
        //        _logger.LogError(ex, "Error updating expense {ExpenseId}", expenseId);
        //        return new ServiceResult<ap_Expenses>
        //        {
        //            Status = 500,
        //            Success = false,
        //            Message = ex.Message
        //        };
        //    }
        //}

        public async Task<ServiceResult<ap_Expenses>> UpdateExpense(Guid expenseId, ap_Expenses dto)
        {
            if (dto == null)
                return new ServiceResult<ap_Expenses> { Status = 400, Success = false, Message = "No data provided" };

            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.ap_Expenses
                    .Include(x => x.ExpenseLines)
                    .FirstOrDefaultAsync(x => x.ExpenseId == expenseId);

                if (existing == null)
                    return new ServiceResult<ap_Expenses> { Status = 404, Success = false, Message = "Expense not found" };

                // If previously posted → reverse the old journal first
                if (existing.Status == "POSTED")
                {
                    await ReverseExpenseJournalAsync(existing);
                    await _context.SaveChangesAsync();
                }

                // Update header fields
                existing.PayeeId = dto.PayeeId;
                existing.PayeeName = dto.PayeeName;
                existing.PaymentAccountId = dto.PaymentAccountId;
                existing.PaymentMethod = dto.PaymentMethod;
                existing.ReferenceNo = dto.ReferenceNo;
                existing.ExpenseDate = dto.ExpenseDate;
                existing.CurrencyCode = string.IsNullOrWhiteSpace(dto.CurrencyCode) ? "MVR" : dto.CurrencyCode;
                existing.ExchangeRate = dto.ExchangeRate <= 0 ? 1m : dto.ExchangeRate;
                existing.Memo = dto.Memo;
                existing.StoreId = dto.StoreId;
                existing.UpdatedAt = DateTime.UtcNow;

                // Replace lines
                _context.ap_ExpenseLines.RemoveRange(existing.ExpenseLines);
                var linesToProcess = dto.ExpenseLines?.ToList() ?? new List<ap_ExpenseLines>();
                var rate = existing.ExchangeRate;
                existing.TotalAmount = linesToProcess.Sum(l => l.Amount);
                existing.BaseTotalAmount = Math.Round(existing.TotalAmount * rate, 2);

                var preparedLines = new List<ap_ExpenseLines>();
                int sortOrder = 1;
                foreach (var line in linesToProcess)
                {
                    var newLine = new ap_ExpenseLines
                    {
                        ExpenseLineId = Guid.CreateVersion7(),
                        ExpenseId = existing.ExpenseId,
                        AccountId = line.AccountId,
                        Description = line.Description,
                        Amount = line.Amount,
                        BaseAmount = Math.Round(line.Amount * rate, 2),
                        SortOrder = sortOrder++,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.ap_ExpenseLines.Add(newLine);
                    preparedLines.Add(newLine);
                }

                // Always re-post (frontend always sends POSTED)
                existing.Status = "POSTED";
                await _context.SaveChangesAsync();

                await PostExpenseJournalAsync(existing, preparedLines);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                return new ServiceResult<ap_Expenses>
                {
                    Status = 200,
                    Success = true,
                    Message = "Expense updated and re-posted successfully",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error updating expense {ExpenseId}", expenseId);
                return new ServiceResult<ap_Expenses> { Status = 500, Success = false, Message = ex.Message };
            }
        }



        private async Task ReverseExpenseJournalAsync(ap_Expenses expense)
        {
            // Find the original journal for this expense
            var originalJournal = await _context.gl_JournalHeaders
                .Include(j => j.JournalLines)
                .FirstOrDefaultAsync(j => j.SourceType == "EXPENSE" && j.SourceId == expense.ExpenseId
                                          && j.Status == "POSTED");

            if (originalJournal == null) return;

            var reversalNo = await GenerateJournalNumberAsync(expense.BusinessId);
            var now = DateTime.UtcNow;

            var reversalHeader = new gl_JournalHeaders
            {
                JournalId = Guid.CreateVersion7(),
                BusinessId = expense.BusinessId,
                StoreId = expense.StoreId,
                JournalDate = now,
                PostingDate = now,
                JournalNo = reversalNo,
                ReferenceNo = expense.ExpenseNo,
                SourceType = "EXPENSE_REVERSAL",
                SourceId = expense.ExpenseId,
                ReversalOfJournalId = originalJournal.JournalId,
                JournalMemo = $"Reversal of {originalJournal.JournalNo}",
                Status = "POSTED",
                BaseCurrencyCode = originalJournal.BaseCurrencyCode,
                TransactionCurrencyCode = originalJournal.TransactionCurrencyCode,
                ExchangeRate = originalJournal.ExchangeRate,
                IsSystemGenerated = true,
                CreatedAt = now,
                PostedAt = now
            };
            _context.gl_JournalHeaders.Add(reversalHeader);

            var reversalLines = new List<gl_JournalLines>();
            int lineNo = 1;

            foreach (var ol in originalJournal.JournalLines)
            {
                var rl = new gl_JournalLines
                {
                    JournalLineId = Guid.CreateVersion7(),
                    JournalId = reversalHeader.JournalId,
                    BusinessId = ol.BusinessId,
                    StoreId = ol.StoreId,
                    GlAccountId = ol.GlAccountId,
                    CurrencyCode = ol.CurrencyCode,
                    ExchangeRate = ol.ExchangeRate,
                    // Swap debit ↔ credit
                    DebitAmountFC = ol.CreditAmountFC,
                    CreditAmountFC = ol.DebitAmountFC,
                    DebitAmountBC = ol.CreditAmountBC,
                    CreditAmountBC = ol.DebitAmountBC,
                    LineNo = lineNo++,
                    Description = $"Reversal: {ol.Description}",
                    SourceType = "EXPENSE_REVERSAL",
                    CreatedAt = now
                };
                _context.gl_JournalLines.Add(rl);
                reversalLines.Add(rl);
            }

            reversalHeader.TotalDebitBC = reversalLines.Sum(x => x.DebitAmountBC);
            reversalHeader.TotalCreditBC = reversalLines.Sum(x => x.CreditAmountBC);

            await _context.SaveChangesAsync();

            // Reversal ledger entries + undo balance impact
            foreach (var rl in reversalLines)
            {
                _context.gl_ledger.Add(new gl_Ledger
                {
                    LedgerId = Guid.CreateVersion7(),
                    BusinessId = rl.BusinessId,
                    StoreId = rl.StoreId,
                    JournalId = reversalHeader.JournalId,
                    JournalLineId = rl.JournalLineId,
                    GlAccountId = rl.GlAccountId,
                    TransactionDate = now,
                    PostingDate = now,
                    BaseCurrencyCode = reversalHeader.BaseCurrencyCode,
                    TransactionCurrencyCode = reversalHeader.TransactionCurrencyCode,
                    ExchangeRate = rl.ExchangeRate ?? 1m,
                    DebitAmountFC = rl.DebitAmountFC,
                    CreditAmountFC = rl.CreditAmountFC,
                    DebitAmountBC = rl.DebitAmountBC,
                    CreditAmountBC = rl.CreditAmountBC,
                    CurrencyCode = rl.CurrencyCode,
                    Module = "EXPENSE_REVERSAL",
                    ReferenceNo = reversalHeader.JournalNo,
                    Description = rl.Description,
                    SourceId = expense.ExpenseId,
                    SourceLineId = rl.JournalLineId,
                    CreatedAt = now,
                    UpdatedAt = now
                });

                await UpsertCurrentBalanceAsync(
                    rl.BusinessId, rl.GlAccountId, "MVR",
                    rl.DebitAmountBC, rl.CreditAmountBC
                );
            }

            // Mark original journal as reversed
            originalJournal.Status = "REVERSED";
        }


        // ─────────────────────────────────────────────
        //  DELETE
        // ─────────────────────────────────────────────
        //public async Task<ServiceResult<bool>> DeleteExpense(Guid expenseId)
        //{
        //    try
        //    {
        //        var expense = await _context.ap_Expenses
        //            .Include(x => x.ExpenseLines)
        //            .FirstOrDefaultAsync(x => x.ExpenseId == expenseId);
        //        if (expense == null)
        //            return new ServiceResult<bool> { Status = 404, Success = false, Message = "Expense not found" };
        //        if (expense.Status == "POSTED")
        //            return new ServiceResult<bool>
        //            {
        //                Status = 400,
        //                Success = false,
        //                Message = "Cannot delete a posted expense. Void it instead."
        //            };
        //        _context.ap_ExpenseLines.RemoveRange(expense.ExpenseLines);
        //        _context.ap_Expenses.Remove(expense);
        //        await _context.SaveChangesAsync();
        //        return new ServiceResult<bool>
        //        {
        //            Status = 200,
        //            Success = true,
        //            Message = "Expense deleted successfully",
        //            Data = true
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error deleting expense {ExpenseId}", expenseId);
        //        return new ServiceResult<bool>
        //        {
        //            Status = 500,
        //            Success = false,
        //            Message = ex.Message
        //        };
        //    }
        //}

        public async Task<ServiceResult<bool>> DeleteExpense(Guid expenseId)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var expense = await _context.ap_Expenses
                    .Include(x => x.ExpenseLines)
                    .FirstOrDefaultAsync(x => x.ExpenseId == expenseId);

                if (expense == null)
                    return new ServiceResult<bool> { Status = 404, Success = false, Message = "Expense not found" };

                if (expense.Status == "POSTED")
                {
                    // Reverse the journal, then soft-delete
                    await ReverseExpenseJournalAsync(expense);
                    await _context.SaveChangesAsync();
                    expense.Status = "VOID";
                    expense.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // DRAFT — hard delete is fine
                    _context.ap_ExpenseLines.RemoveRange(expense.ExpenseLines);
                    _context.ap_Expenses.Remove(expense);
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                return new ServiceResult<bool>
                {
                    Status = 200,
                    Success = true,
                    Message = "Expense deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error deleting expense {ExpenseId}", expenseId);
                return new ServiceResult<bool> { Status = 500, Success = false, Message = ex.Message };
            }
        }

        //public async Task<ServiceResult<ap_Cheques>> UpdateCheque(Guid chequeId, ap_Cheques dto)
        //{
        //    if (dto == null)
        //        return new ServiceResult<ap_Cheques> { Status = 400, Success = false, Message = "No data provided" };

        //    await using var tx = await _context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var existing = await _context.ap_Cheques
        //            .Include(x => x.ChequeLines)
        //            .FirstOrDefaultAsync(x => x.ChequeId == chequeId);

        //        if (existing == null)
        //            return new ServiceResult<ap_Cheques> { Status = 404, Success = false, Message = "Cheque not found" };

        //        existing.CheckNumber = dto.CheckNumber;
        //        existing.PayeeId = dto.PayeeId;
        //        existing.PayeeName = dto.PayeeName;
        //        existing.PaymentAccountId = dto.PaymentAccountId;
        //        existing.PaymentDate = dto.PaymentDate;
        //        existing.Memo = dto.Memo;
        //        existing.UpdatedAt = DateTime.UtcNow;

        //        _context.ap_ChequeLines.RemoveRange(existing.ChequeLines);

        //        var totalAmount = dto.ChequeLines?.Sum(l => l.Amount) ?? 0m;
        //        existing.TotalAmount = totalAmount;

        //        int sortOrder = 1;
        //        foreach (var line in dto.ChequeLines ?? new List<ap_ChequeLines>())
        //        {
        //            _context.ap_ChequeLines.Add(new ap_ChequeLines
        //            {
        //                ChequeLineId = Guid.CreateVersion7(),
        //                ChequeId = existing.ChequeId,
        //                AccountId = line.AccountId,
        //                Description = line.Description,
        //                Amount = line.Amount,
        //                BaseAmount = line.Amount,
        //                SortOrder = sortOrder++,
        //                CreatedAt = DateTime.UtcNow
        //            });
        //        }

        //        await _context.SaveChangesAsync();
        //        await tx.CommitAsync();

        //        return new ServiceResult<ap_Cheques>
        //        {
        //            Status = 200,
        //            Success = true,
        //            Message = "Cheque updated successfully",
        //            Data = existing
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        await tx.RollbackAsync();
        //        _logger.LogError(ex, "Error updating cheque {ChequeId}", chequeId);
        //        return new ServiceResult<ap_Cheques> { Status = 500, Success = false, Message = ex.Message };
        //    }
        //}

        public async Task<ServiceResult<ap_Cheques>> UpdateCheque(Guid chequeId, ap_Cheques dto)
        {
            if (dto == null)
                return new ServiceResult<ap_Cheques> { Status = 400, Success = false, Message = "No data provided" };

            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.ap_Cheques
                    .Include(x => x.ChequeLines)
                    .FirstOrDefaultAsync(x => x.ChequeId == chequeId);

                if (existing == null)
                    return new ServiceResult<ap_Cheques> { Status = 404, Success = false, Message = "Cheque not found" };

                // If previously posted → reverse the old journal first
                if (existing.Status == "POSTED")
                {
                    await ReverseChequeJournalAsync(existing);
                    await _context.SaveChangesAsync();
                }

                // Update header fields
                existing.CheckNumber = dto.CheckNumber;
                existing.PayeeId = dto.PayeeId;
                existing.PayeeName = dto.PayeeName;
                existing.PaymentAccountId = dto.PaymentAccountId;
                existing.PaymentDate = dto.PaymentDate;
                existing.Memo = dto.Memo;
                existing.StoreId = dto.StoreId;
                existing.UpdatedAt = DateTime.UtcNow;

                // Replace lines
                _context.ap_ChequeLines.RemoveRange(existing.ChequeLines);
                var linesToProcess = dto.ChequeLines?.ToList() ?? new List<ap_ChequeLines>();
                existing.TotalAmount = linesToProcess.Sum(l => l.Amount);

                var preparedLines = new List<ap_ChequeLines>();
                int sortOrder = 1;
                foreach (var line in linesToProcess)
                {
                    var newLine = new ap_ChequeLines
                    {
                        ChequeLineId = Guid.CreateVersion7(),
                        ChequeId = existing.ChequeId,
                        AccountId = line.AccountId,
                        Description = line.Description,
                        Amount = line.Amount,
                        BaseAmount = line.Amount,
                        SortOrder = sortOrder++,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.ap_ChequeLines.Add(newLine);
                    preparedLines.Add(newLine);
                }

                // Always re-post
                existing.Status = "POSTED";
                await _context.SaveChangesAsync();

                await PostChequeJournalAsync(existing, preparedLines);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                return new ServiceResult<ap_Cheques>
                {
                    Status = 200,
                    Success = true,
                    Message = "Cheque updated and re-posted successfully",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error updating cheque {ChequeId}", chequeId);
                return new ServiceResult<ap_Cheques> { Status = 500, Success = false, Message = ex.Message };
            }
        }

        // ─────────────────────────────────────────────
        //  GENERATE EXPENSE NUMBER  (EXP-2026-0001)
        // ─────────────────────────────────────────────
        public async Task<string> GenerateExpenseNumber(Guid businessId)
        {
            try
            {
                var currentYear = DateTime.UtcNow.Year;
                var prefix = $"EXP-{currentYear}-";
                var last = await _context.ap_Expenses
                    .Where(x => x.BusinessId == businessId
                             && x.ExpenseNo != null
                             && x.ExpenseNo.StartsWith(prefix))
                    .OrderByDescending(x => x.ExpenseNo)
                    .FirstOrDefaultAsync();
                int nextNumber = 1;
                if (last != null && !string.IsNullOrEmpty(last.ExpenseNo))
                {
                    var lastPart = last.ExpenseNo.Substring(prefix.Length);
                    if (int.TryParse(lastPart, out int lastNumber))
                        nextNumber = lastNumber + 1;
                }
                return $"{prefix}{nextNumber:D4}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating expense number");
                return $"EXP-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString()[..4].ToUpper()}";
            }
        }
        // ─────────────────────────────────────────────
        //  PRIVATE: Post Expense to Journal + Ledger
        //  DEBIT  → each expense line account
        //  CREDIT → payment account (total)
        // ─────────────────────────────────────────────
        private async Task PostExpenseJournalAsync(ap_Expenses expense, List<ap_ExpenseLines> lines)
        {
            // Resolve journal number
            var journalNumber = await GenerateJournalNumberAsync(expense.BusinessId);
            var journalDate = expense.ExpenseDate.Date;
            var rate = expense.ExchangeRate;
            var currency = expense.CurrencyCode ?? "MVR";
            // Build journal header
            var header = new gl_JournalHeaders
            {
                JournalId = Guid.CreateVersion7(),
                BusinessId = expense.BusinessId,
                StoreId = expense.StoreId,
                JournalDate = journalDate,
                PostingDate = journalDate,
                JournalNo = journalNumber,
                ReferenceNo = expense.ReferenceNo ?? expense.ExpenseNo,
                SourceType = "EXPENSE",
                SourceId = expense.ExpenseId,
                JournalMemo = expense.Memo ?? $"Expense {expense.ExpenseNo}",
                Status = "POSTED",
                BaseCurrencyCode = "MVR",
                TransactionCurrencyCode = currency,
                ExchangeRate = rate,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = expense.CreatedBy?.ToString()
            };
            _context.gl_JournalHeaders.Add(header);
            var journalLines = new List<gl_JournalLines>();
            int lineNo = 1;
            // DEBIT each expense line account
            foreach (var line in lines)
            {
                var jl = new gl_JournalLines
                {
                    JournalLineId = Guid.CreateVersion7(),
                    JournalId = header.JournalId,
                    BusinessId = expense.BusinessId,
                    StoreId = expense.StoreId,
                    GlAccountId = line.AccountId,
                    CurrencyCode = currency,
                    ExchangeRate = rate,
                    DebitAmountFC = line.Amount,
                    CreditAmountFC = 0m,
                    DebitAmountBC = line.BaseAmount,
                    CreditAmountBC = 0m,
                    LineNo = lineNo++,
                    Description = line.Description ?? expense.Memo,
                    SourceType = "EXPENSE",
                    SourceLineId = line.ExpenseLineId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.gl_JournalLines.Add(jl);
                journalLines.Add(jl);
            }
            // CREDIT the payment account (full total)
            var creditLine = new gl_JournalLines
            {
                JournalLineId = Guid.CreateVersion7(),
                JournalId = header.JournalId,
                BusinessId = expense.BusinessId,
                StoreId = expense.StoreId,
                GlAccountId = expense.PaymentAccountId,
                CurrencyCode = currency,
                ExchangeRate = rate,
                DebitAmountFC = 0m,
                CreditAmountFC = expense.TotalAmount,
                DebitAmountBC = 0m,
                CreditAmountBC = expense.BaseTotalAmount,
                LineNo = lineNo,
                Description = $"Payment for {expense.ExpenseNo}",
                SourceType = "EXPENSE",
                SourceLineId = null,
                CreatedAt = DateTime.UtcNow
            };
            _context.gl_JournalLines.Add(creditLine);
            journalLines.Add(creditLine);
            // Totals on header
            header.TotalDebitBC = journalLines.Sum(x => x.DebitAmountBC);
            header.TotalCreditBC = journalLines.Sum(x => x.CreditAmountBC);
            header.TotalDebitFC = journalLines.Sum(x => x.DebitAmountFC);
            header.TotalCreditFC = journalLines.Sum(x => x.CreditAmountFC);
            header.PostedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            // Ledger entries + current balance update
            foreach (var jl in journalLines)
            {
                var ledger = new gl_Ledger
                {
                    LedgerId = Guid.CreateVersion7(),
                    BusinessId = jl.BusinessId,
                    StoreId = jl.StoreId,
                    JournalId = header.JournalId,
                    JournalLineId = jl.JournalLineId,
                    GlAccountId = jl.GlAccountId,
                    TransactionDate = journalDate,
                    PostingDate = journalDate,
                    BaseCurrencyCode = "MVR",
                    TransactionCurrencyCode = currency,
                    ExchangeRate = rate,
                    DebitAmountFC = jl.DebitAmountFC,
                    CreditAmountFC = jl.CreditAmountFC,
                    DebitAmountBC = jl.DebitAmountBC,
                    CreditAmountBC = jl.CreditAmountBC,
                    CurrencyCode = currency,
                    Module = "EXPENSE",
                    ReferenceNo = header.ReferenceNo ?? header.JournalNo,
                    Description = jl.Description,
                    SourceId = expense.ExpenseId,
                    SourceLineId = jl.SourceLineId ?? jl.JournalLineId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = expense.CreatedBy?.ToString()
                };
                _context.gl_ledger.Add(ledger);
                await UpsertCurrentBalanceAsync(
                    jl.BusinessId,
                    jl.GlAccountId,
                    "MVR",
                    jl.DebitAmountBC,
                    jl.CreditAmountBC
                );
            }
        }
        private async Task<string> GenerateJournalNumberAsync(Guid businessId)
        {
            var currentYear = DateTime.UtcNow.Year;
            var prefix = $"JE-{currentYear}-";
            var last = await _context.gl_JournalHeaders
                .Where(x => x.BusinessId == businessId
                         && x.JournalNo != null
                         && x.JournalNo.StartsWith(prefix))
                .OrderByDescending(x => x.JournalNo)
                .FirstOrDefaultAsync();
            int nextNumber = 1;
            if (last != null && !string.IsNullOrEmpty(last.JournalNo))
            {
                var lastPart = last.JournalNo.Substring(prefix.Length);
                if (int.TryParse(lastPart, out int lastNumber))
                    nextNumber = lastNumber + 1;
            }
            return $"{prefix}{nextNumber:D4}";
        }

        //public async Task<ServiceResult<ap_Cheques>> Create_cheque(ap_Cheques cheque)
        //{
        //    if (cheque == null)
        //        return new ServiceResult<ap_Cheques> { Success = false, Message = "No data found", Status = -1 };

        //    try
        //    {
        //        var random = new Random();
        //        string chequeNo;
        //        bool exists;
        //        do
        //        {
        //            chequeNo = random.Next(100000, 999999).ToString();
        //            exists = await _context.ap_Cheques.AnyAsync(c => c.ChequeNo == "CHQ-" + chequeNo);
        //        } while (exists);

        //        cheque.ChequeId = Guid.CreateVersion7();
        //        cheque.ChequeNo = "CHQ-" + chequeNo;
        //        cheque.Status = "PENDING";
        //        cheque.CreatedAt = DateTime.UtcNow;

        //        foreach (var line in cheque.ChequeLines)
        //        {
        //            line.ChequeLineId = Guid.CreateVersion7();
        //            line.ChequeId = cheque.ChequeId;
        //            line.BaseAmount = line.Amount;
        //            line.CreatedAt = DateTime.UtcNow;
        //        }

        //        await _context.ap_Cheques.AddAsync(cheque);
        //        await _context.SaveChangesAsync();

        //        return new ServiceResult<ap_Cheques> { Success = true, Message = "Success", Status = 1, Data = cheque };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error creating cheque.");
        //        return new ServiceResult<ap_Cheques> { Success = false, Message = "Failed to create cheque.", Status = -1 };
        //    }
        //}

        public async Task<ServiceResult<ap_Cheques>> Create_cheque(ap_Cheques cheque)
        {
            if (cheque == null)
                return new ServiceResult<ap_Cheques> { Success = false, Message = "No data provided", Status = 400 };
            if (cheque.BusinessId == Guid.Empty)
                return new ServiceResult<ap_Cheques> { Success = false, Message = "BusinessId is required", Status = 400 };
            if (cheque.PaymentAccountId == Guid.Empty)
                return new ServiceResult<ap_Cheques> { Success = false, Message = "PaymentAccountId is required", Status = 400 };
            if (cheque.ChequeLines == null || !cheque.ChequeLines.Any())
                return new ServiceResult<ap_Cheques> { Success = false, Message = "At least one cheque line is required", Status = 400 };

            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // Generate unique ChequeNo
                var random = new Random();
                string chequeNo;
                bool exists;
                do
                {
                    chequeNo = random.Next(100000, 999999).ToString();
                    exists = await _context.ap_Cheques.AnyAsync(c => c.ChequeNo == "CHQ-" + chequeNo);
                } while (exists);

                cheque.ChequeId = Guid.CreateVersion7();
                cheque.ChequeNo = "CHQ-" + chequeNo;
                cheque.Status = "POSTED";
                cheque.CreatedAt = DateTime.UtcNow;

                var linesToProcess = cheque.ChequeLines.ToList();
                cheque.ChequeLines = new List<ap_ChequeLines>();

                var totalAmount = linesToProcess.Sum(l => l.Amount);
                cheque.TotalAmount = totalAmount;

                _context.ap_Cheques.Add(cheque);

                var preparedLines = new List<ap_ChequeLines>();
                int sortOrder = 1;
                foreach (var line in linesToProcess)
                {
                    line.ChequeLineId = Guid.CreateVersion7();
                    line.ChequeId = cheque.ChequeId;
                    line.BaseAmount = line.Amount;
                    line.SortOrder = sortOrder++;
                    line.CreatedAt = DateTime.UtcNow;
                    _context.ap_ChequeLines.Add(line);
                    preparedLines.Add(line);
                }

                await _context.SaveChangesAsync();

                // Auto-post journal
                await PostChequeJournalAsync(cheque, preparedLines);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();

                _logger.LogInformation("Created cheque {ChequeNo} for business {BusinessId}", cheque.ChequeNo, cheque.BusinessId);

                return new ServiceResult<ap_Cheques>
                {
                    Success = true,
                    Status = 200,
                    Message = $"Cheque created successfully: {cheque.ChequeNo}",
                    Data = cheque
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                var inner = ex.InnerException?.InnerException?.Message
                            ?? ex.InnerException?.Message
                            ?? ex.Message;
                _logger.LogError(ex, "Error creating cheque: {Inner}", inner);
                return new ServiceResult<ap_Cheques> { Success = false, Status = 500, Message = inner };
            }
        }

        public async Task<ServiceResult<List<ap_Cheques>>> Get_all_cheques(Guid businessId)
        {
            var cheques = await _context.ap_Cheques
                .Include(c => c.ChequeLines)
                .Where(c => c.BusinessId == businessId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return new ServiceResult<List<ap_Cheques>>
            {
                Success = true,
                Status = cheques.Any() ? 1 : 0,
                Message = cheques.Any() ? "Success" : "No cheques found",
                Data = cheques
            };
        }

        public async Task<ServiceResult<string>> Upload_cheque_attachments(Guid chequeId, List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return new ServiceResult<string> { Success = false, Message = "No files provided", Status = -1 };

            var cheque = await _context.ap_Cheques
                .FirstOrDefaultAsync(c => c.ChequeId == chequeId);

            if (cheque == null)
                return new ServiceResult<string> { Success = false, Message = "Invalid cheque ID", Status = 404 };

            var business = await _context.co_business
                .FirstOrDefaultAsync(c => c.company_id == cheque.BusinessId);

            if (business == null)
                return new ServiceResult<string> { Success = false, Message = "Company not found", Status = 404 };

            var bucketName = _configure["Wasabi:BucketName"];
            int fileNumber = 0;

            foreach (var file in files)
            {
                fileNumber++;
                var fileInfo = new FileInfo(file.FileName);
                string timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
                string safeFileName = $"attachment_{fileNumber}_{timestamp}{fileInfo.Extension}";
                string key = $"faahi/company/{business.company_code}/cheque_{chequeId}/attachments/{safeFileName}";

                using var stream = file.OpenReadStream();
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    Headers = { CacheControl = "public,max-age=604800" }
                };

                await _s3Client.PutObjectAsync(request);

                string canonicalUrl = $"https://cdn.faahi.com/{key}";

                var attachment = new ap_ChequesAttachments
                {
                    AttachmentId = Guid.CreateVersion7(),
                    ChequeId = chequeId,
                    FileName = file.FileName,
                    image_url = canonicalUrl,
                    UploadedAt = DateTime.UtcNow
                };
                await _context.ap_ChequesAttachments.AddAsync(attachment);
            }

            await _context.SaveChangesAsync();
            return new ServiceResult<string> { Success = true, Status = 1, Message = "Attachments uploaded" };
        }


        public async Task<ServiceResult<string>> Upload_expense_attachments(Guid expenseId, List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return new ServiceResult<string> { Success = false, Message = "No files provided", Status = -1 };

            var expense = await _context.ap_Expenses
                .FirstOrDefaultAsync(e => e.ExpenseId == expenseId);

            if (expense == null)
                return new ServiceResult<string> { Success = false, Message = "Invalid expense ID", Status = 404 };

            var business = await _context.co_business
                .FirstOrDefaultAsync(c => c.company_id == expense.BusinessId);

            if (business == null)
                return new ServiceResult<string> { Success = false, Message = "Company not found", Status = 404 };

            var bucketName = _configure["Wasabi:BucketName"];
            int fileNumber = 0;

            foreach (var file in files)
            {
                fileNumber++;
                var fileInfo = new FileInfo(file.FileName);
                string timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
                string safeFileName = $"attachment_{fileNumber}_{timestamp}{fileInfo.Extension}";
                string key = $"faahi/company/{business.company_code}/expense_{expenseId}/attachments/{safeFileName}";

                using var stream = file.OpenReadStream();
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    Headers = { CacheControl = "public,max-age=604800" }
                };

                await _s3Client.PutObjectAsync(request);

                string canonicalUrl = $"https://cdn.faahi.com/{key}";

                var attachment = new ap_ExpensesAttachments
                {
                    AttachmentId = Guid.CreateVersion7(),
                    ExpenseId = expenseId,
                    FileName = file.FileName,
                    image_url = canonicalUrl,
                    UploadedAt = DateTime.UtcNow
                };
                await _context.ap_ExpensesAttachments.AddAsync(attachment);
            }

            await _context.SaveChangesAsync();
            return new ServiceResult<string> { Success = true, Status = 1, Message = "Attachments uploaded" };
        }

        public async Task<ServiceResult<object>> Delete_expense(Guid expenseId)
        {
            try
            {
                var expense = await _context.ap_Expenses
                    .Include(e => e.ExpenseLines)
                    .Include(e => e.Attachments)
                    .FirstOrDefaultAsync(e => e.ExpenseId == expenseId);

                if (expense == null)
                    return new ServiceResult<object> { Success = false, Status = 404, Message = "Expense not found." };

                _context.ap_Expenses.Remove(expense);
                await _context.SaveChangesAsync();

                return new ServiceResult<object> { Success = true, Status = 200, Message = "Expense deleted successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting expense {ExpenseId}", expenseId);
                return new ServiceResult<object> { Success = false, Status = 500, Message = ex.Message };
            }
        }

        //public async Task<ServiceResult<object>> DeleteCheque(Guid chequeId)
        //{
        //    try
        //    {
        //        var cheque = await _context.ap_Cheques
        //            .Include(c => c.ChequeLines)
        //            .Include(c => c.Attachments)
        //            .FirstOrDefaultAsync(c => c.ChequeId == chequeId);

        //        if (cheque == null)
        //            return new ServiceResult<object> { Success = false, Status = 404, Message = "Cheque not found." };

        //        _context.ap_Cheques.Remove(cheque);
        //        await _context.SaveChangesAsync();

        //        return new ServiceResult<object> { Success = true, Status = 200, Message = "Cheque deleted successfully." };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error deleting cheque {ChequeId}", chequeId);
        //        return new ServiceResult<object> { Success = false, Status = 500, Message = ex.Message };
        //    }
        //}

        public async Task<ServiceResult<object>> DeleteCheque(Guid chequeId)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var cheque = await _context.ap_Cheques
                    .Include(c => c.ChequeLines)
                    .FirstOrDefaultAsync(c => c.ChequeId == chequeId);

                if (cheque == null)
                    return new ServiceResult<object> { Status = 404, Success = false, Message = "Cheque not found." };

                if (cheque.Status == "POSTED")
                {
                    // Reverse journal, then soft-delete
                    await ReverseChequeJournalAsync(cheque);
                    await _context.SaveChangesAsync();
                    cheque.Status = "VOID";
                    cheque.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Not posted — hard delete
                    _context.ap_ChequeLines.RemoveRange(cheque.ChequeLines);
                    _context.ap_Cheques.Remove(cheque);
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                return new ServiceResult<object> { Success = true, Status = 200, Message = "Cheque deleted successfully." };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error deleting cheque {ChequeId}", chequeId);
                return new ServiceResult<object> { Success = false, Status = 500, Message = ex.Message };
            }
        }

        private async Task PostChequeJournalAsync(ap_Cheques cheque, List<ap_ChequeLines> lines)
        {
            var journalNumber = await GenerateJournalNumberAsync(cheque.BusinessId);
            var journalDate = cheque.PaymentDate.Date;
            var currency = "MVR";

            var header = new gl_JournalHeaders
            {
                JournalId = Guid.CreateVersion7(),
                BusinessId = cheque.BusinessId,
                StoreId = cheque.StoreId,
                JournalDate = journalDate,
                PostingDate = journalDate,
                JournalNo = journalNumber,
                ReferenceNo = cheque.CheckNumber ?? cheque.ChequeNo,
                SourceType = "CHEQUE",
                SourceId = cheque.ChequeId,
                JournalMemo = cheque.Memo ?? $"Cheque {cheque.ChequeNo}",
                Status = "POSTED",
                BaseCurrencyCode = "MVR",
                TransactionCurrencyCode = currency,
                ExchangeRate = 1m,
                CreatedAt = DateTime.UtcNow,
                PostedAt = DateTime.UtcNow,
                CreatedBy = cheque.CreatedBy?.ToString()
            };
            _context.gl_JournalHeaders.Add(header);

            var journalLines = new List<gl_JournalLines>();
            int lineNo = 1;

            // DEBIT each cheque line account
            foreach (var line in lines)
            {
                var jl = new gl_JournalLines
                {
                    JournalLineId = Guid.CreateVersion7(),
                    JournalId = header.JournalId,
                    BusinessId = cheque.BusinessId,
                    StoreId = cheque.StoreId,
                    GlAccountId = line.AccountId,
                    CurrencyCode = currency,
                    ExchangeRate = 1m,
                    DebitAmountFC = line.Amount,
                    CreditAmountFC = 0m,
                    DebitAmountBC = line.BaseAmount,
                    CreditAmountBC = 0m,
                    LineNo = lineNo++,
                    Description = line.Description ?? cheque.Memo,
                    SourceType = "CHEQUE",
                    SourceLineId = line.ChequeLineId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.gl_JournalLines.Add(jl);
                journalLines.Add(jl);
            }

            // CREDIT the payment account (full total)
            var creditLine = new gl_JournalLines
            {
                JournalLineId = Guid.CreateVersion7(),
                JournalId = header.JournalId,
                BusinessId = cheque.BusinessId,
                StoreId = cheque.StoreId,
                GlAccountId = cheque.PaymentAccountId,
                CurrencyCode = currency,
                ExchangeRate = 1m,
                DebitAmountFC = 0m,
                CreditAmountFC = cheque.TotalAmount,
                DebitAmountBC = 0m,
                CreditAmountBC = cheque.TotalAmount,
                LineNo = lineNo,
                Description = $"Payment for {cheque.ChequeNo}",
                SourceType = "CHEQUE",
                SourceLineId = null,
                CreatedAt = DateTime.UtcNow
            };
            _context.gl_JournalLines.Add(creditLine);
            journalLines.Add(creditLine);

            // Header totals
            header.TotalDebitBC = journalLines.Sum(x => x.DebitAmountBC);
            header.TotalCreditBC = journalLines.Sum(x => x.CreditAmountBC);
            header.TotalDebitFC = journalLines.Sum(x => x.DebitAmountFC);
            header.TotalCreditFC = journalLines.Sum(x => x.CreditAmountFC);

            await _context.SaveChangesAsync();

            // Ledger entries + balance updates
            foreach (var jl in journalLines)
            {
                _context.gl_ledger.Add(new gl_Ledger
                {
                    LedgerId = Guid.CreateVersion7(),
                    BusinessId = jl.BusinessId,
                    StoreId = jl.StoreId,
                    JournalId = header.JournalId,
                    JournalLineId = jl.JournalLineId,
                    GlAccountId = jl.GlAccountId,
                    TransactionDate = journalDate,
                    PostingDate = journalDate,
                    BaseCurrencyCode = "MVR",
                    TransactionCurrencyCode = currency,
                    ExchangeRate = 1m,
                    DebitAmountFC = jl.DebitAmountFC,
                    CreditAmountFC = jl.CreditAmountFC,
                    DebitAmountBC = jl.DebitAmountBC,
                    CreditAmountBC = jl.CreditAmountBC,
                    CurrencyCode = currency,
                    Module = "CHEQUE",
                    ReferenceNo = header.ReferenceNo ?? header.JournalNo,
                    Description = jl.Description,
                    SourceId = cheque.ChequeId,
                    SourceLineId = jl.SourceLineId ?? jl.JournalLineId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = cheque.CreatedBy?.ToString()
                });

                await UpsertCurrentBalanceAsync(
                    jl.BusinessId, jl.GlAccountId, "MVR",
                    jl.DebitAmountBC, jl.CreditAmountBC
                );
            }
        }

        private async Task ReverseChequeJournalAsync(ap_Cheques cheque)
        {
            var originalJournal = await _context.gl_JournalHeaders
                .Include(j => j.JournalLines)
                .FirstOrDefaultAsync(j => j.SourceType == "CHEQUE"
                                        && j.SourceId == cheque.ChequeId
                                        && j.Status == "POSTED");

            if (originalJournal == null) return;

            var reversalNo = await GenerateJournalNumberAsync(cheque.BusinessId);
            var now = DateTime.UtcNow;

            var reversalHeader = new gl_JournalHeaders
            {
                JournalId = Guid.CreateVersion7(),
                BusinessId = cheque.BusinessId,
                StoreId = cheque.StoreId,
                JournalDate = now,
                PostingDate = now,
                JournalNo = reversalNo,
                ReferenceNo = cheque.ChequeNo,
                SourceType = "CHEQUE_REVERSAL",
                SourceId = cheque.ChequeId,
                ReversalOfJournalId = originalJournal.JournalId,
                JournalMemo = $"Reversal of {originalJournal.JournalNo}",
                Status = "POSTED",
                BaseCurrencyCode = originalJournal.BaseCurrencyCode,
                TransactionCurrencyCode = originalJournal.TransactionCurrencyCode,
                ExchangeRate = originalJournal.ExchangeRate,
                IsSystemGenerated = true,
                CreatedAt = now,
                PostedAt = now
            };
            _context.gl_JournalHeaders.Add(reversalHeader);

            var reversalLines = new List<gl_JournalLines>();
            int lineNo = 1;

            foreach (var ol in originalJournal.JournalLines)
            {
                var rl = new gl_JournalLines
                {
                    JournalLineId = Guid.CreateVersion7(),
                    JournalId = reversalHeader.JournalId,
                    BusinessId = ol.BusinessId,
                    StoreId = ol.StoreId,
                    GlAccountId = ol.GlAccountId,
                    CurrencyCode = ol.CurrencyCode,
                    ExchangeRate = ol.ExchangeRate,
                    DebitAmountFC = ol.CreditAmountFC,   // swapped
                    CreditAmountFC = ol.DebitAmountFC,
                    DebitAmountBC = ol.CreditAmountBC,   // swapped
                    CreditAmountBC = ol.DebitAmountBC,
                    LineNo = lineNo++,
                    Description = $"Reversal: {ol.Description}",
                    SourceType = "CHEQUE_REVERSAL",
                    CreatedAt = now
                };
                _context.gl_JournalLines.Add(rl);
                reversalLines.Add(rl);
            }

            reversalHeader.TotalDebitBC = reversalLines.Sum(x => x.DebitAmountBC);
            reversalHeader.TotalCreditBC = reversalLines.Sum(x => x.CreditAmountBC);

            await _context.SaveChangesAsync();

            foreach (var rl in reversalLines)
            {
                _context.gl_ledger.Add(new gl_Ledger
                {
                    LedgerId = Guid.CreateVersion7(),
                    BusinessId = rl.BusinessId,
                    StoreId = rl.StoreId,
                    JournalId = reversalHeader.JournalId,
                    JournalLineId = rl.JournalLineId,
                    GlAccountId = rl.GlAccountId,
                    TransactionDate = now,
                    PostingDate = now,
                    BaseCurrencyCode = reversalHeader.BaseCurrencyCode,
                    TransactionCurrencyCode = reversalHeader.TransactionCurrencyCode,
                    ExchangeRate = rl.ExchangeRate ?? 1m,
                    DebitAmountFC = rl.DebitAmountFC,
                    CreditAmountFC = rl.CreditAmountFC,
                    DebitAmountBC = rl.DebitAmountBC,
                    CreditAmountBC = rl.CreditAmountBC,
                    CurrencyCode = rl.CurrencyCode,
                    Module = "CHEQUE_REVERSAL",
                    ReferenceNo = reversalHeader.JournalNo,
                    Description = rl.Description,
                    SourceId = cheque.ChequeId,
                    SourceLineId = rl.JournalLineId,
                    CreatedAt = now,
                    UpdatedAt = now
                });

                await UpsertCurrentBalanceAsync(
                    rl.BusinessId, rl.GlAccountId, "MVR",
                    rl.DebitAmountBC, rl.CreditAmountBC
                );
            }

            originalJournal.Status = "REVERSED";
        }

        public async Task<ServiceResult<object>> GetTemplates()
        {
            try
            {
                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var sql = @"
            SELECT
                TemplateId, AccountNumber, AccountName, AccountType, DetailType,
                ParentTemplateId, IsPostable, IsActive, CurrencyCode, OpeningBalance,
                Description, BalanceType, NormalBalance, SortOrder, CreatedAt, UpdatedAt
            FROM dbo.gl_AccountTemplates
            ORDER BY SortOrder, AccountNumber";

                var rows = await con.QueryAsync(sql);
                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 200, data = rows }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching templates");
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> DeleteTemplate(Guid templateId)
        {
            try
            {
                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var exists = await con.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
                    new { templateId });

                if (exists == 0)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 404,
                        Data = new { status = 404, message = "Template not found." }
                    };
                }

                var childCount = await con.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE ParentTemplateId = @templateId",
                    new { templateId });

                if (childCount > 0)
                {
                    await con.ExecuteAsync(
                        "UPDATE dbo.gl_AccountTemplates SET IsActive='F', UpdatedAt=GETDATE() WHERE TemplateId=@templateId",
                        new { templateId });

                    return new ServiceResult<object>
                    {
                        Success = true,
                        Status = 200,
                        Data = new { status = 200, message = "Template has children; marked inactive." }
                    };
                }

                await con.ExecuteAsync(
                    "DELETE FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
                    new { templateId });

                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 200, message = "Template deleted successfully." }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting template {TemplateId}", templateId);
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> UpdateTemplate(Guid templateId, JsonElement body)
        {
            try
            {
                if (templateId == Guid.Empty)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 400,
                        Data = new { status = 400, message = "Invalid template id." }
                    };
                }

                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var exists = await con.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
                    new { templateId });

                if (exists == 0)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 404,
                        Data = new { status = 404, message = "Template not found." }
                    };
                }

                var accountNumber = GetString(body, "AccountNumber") ?? "";
                var accountName = GetString(body, "AccountName") ?? "";
                var accountType = GetString(body, "AccountType") ?? "";
                var isPostable = GetString(body, "IsPostable") ?? "T";
                var isActive = GetString(body, "IsActive") ?? "T";
                var openingBalance = GetDecimal(body, "OpeningBalance") ?? 0m;
                var description = GetString(body, "Description");
                var sortOrder = GetInt(body, "SortOrder") ?? 0;
                var detailType = GetString(body, "DetailType");
                var currencyCode = GetString(body, "CurrencyCode");
                var balanceType = GetString(body, "BalanceType");
                var normalBalance = GetString(body, "NormalBalance");

                Guid? parentTemplateId = null;
                var parentRaw = GetString(body, "ParentTemplateId");
                if (!string.IsNullOrWhiteSpace(parentRaw) && Guid.TryParse(parentRaw, out var parentGuid))
                {
                    parentTemplateId = parentGuid;
                }

                if (parentTemplateId.HasValue && parentTemplateId.Value == templateId)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 400,
                        Data = new { status = 400, message = "Template cannot be its own parent." }
                    };
                }

                var sql = @"
            UPDATE dbo.gl_AccountTemplates
            SET
                AccountNumber = @AccountNumber,
                AccountName = @AccountName,
                AccountType = @AccountType,
                DetailType = @DetailType,
                ParentTemplateId = @ParentTemplateId,
                IsPostable = @IsPostable,
                IsActive = @IsActive,
                CurrencyCode = @CurrencyCode,
                OpeningBalance = @OpeningBalance,
                Description = @Description,
                BalanceType = @BalanceType,
                NormalBalance = @NormalBalance,
                SortOrder = @SortOrder,
                UpdatedAt = GETDATE()
            WHERE TemplateId = @TemplateId";

                await con.ExecuteAsync(sql, new
                {
                    TemplateId = templateId,
                    AccountNumber = accountNumber,
                    AccountName = accountName,
                    AccountType = accountType,
                    DetailType = detailType,
                    ParentTemplateId = parentTemplateId,
                    IsPostable = isPostable,
                    IsActive = isActive,
                    CurrencyCode = currencyCode,
                    OpeningBalance = openingBalance,
                    Description = description,
                    BalanceType = balanceType,
                    NormalBalance = normalBalance,
                    SortOrder = sortOrder
                });

                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 200, message = "Template updated successfully." }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating template {TemplateId}", templateId);
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> CreateTemplate(JsonElement body)
        {
            try
            {
                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var accountNumber = GetString(body, "AccountNumber") ?? "";
                var accountName = GetString(body, "AccountName") ?? "";
                var accountType = GetString(body, "AccountType") ?? "";

                if (string.IsNullOrWhiteSpace(accountNumber) ||
                    string.IsNullOrWhiteSpace(accountName) ||
                    string.IsNullOrWhiteSpace(accountType))
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 400,
                        Data = new { status = 400, message = "AccountNumber, AccountName, and AccountType are required." }
                    };
                }

                Guid? parentTemplateId = null;
                var parentRaw = GetString(body, "ParentTemplateId");
                if (!string.IsNullOrWhiteSpace(parentRaw) && Guid.TryParse(parentRaw, out var parentGuid))
                {
                    parentTemplateId = parentGuid;
                }

                var isPostable = GetString(body, "IsPostable") ?? "T";
                var isActive = GetString(body, "IsActive") ?? "T";
                var openingBalance = GetDecimal(body, "OpeningBalance") ?? 0m;
                var description = GetString(body, "Description");
                var sortOrder = GetInt(body, "SortOrder") ?? 0;
                var detailType = GetString(body, "DetailType");
                var currencyCode = GetString(body, "CurrencyCode");
                var balanceType = GetString(body, "BalanceType");
                var normalBalance = GetString(body, "NormalBalance");

                var sql = @"INSERT INTO dbo.gl_AccountTemplates
            (
                AccountNumber, AccountName, AccountType, DetailType,
                ParentTemplateId, IsPostable, IsActive, CurrencyCode, OpeningBalance,
                Description, BalanceType, NormalBalance, SortOrder, CreatedAt, UpdatedAt
            )
            VALUES
            (
                @AccountNumber, @AccountName, @AccountType, @DetailType,
                @ParentTemplateId, @IsPostable, @IsActive, @CurrencyCode, @OpeningBalance,
                @Description, @BalanceType, @NormalBalance, @SortOrder, GETDATE(), GETDATE()
            )";

                await con.ExecuteAsync(sql, new
                {
                    AccountNumber = accountNumber,
                    AccountName = accountName,
                    AccountType = accountType,
                    DetailType = detailType,
                    ParentTemplateId = parentTemplateId,
                    IsPostable = isPostable,
                    IsActive = isActive,
                    CurrencyCode = currencyCode,
                    OpeningBalance = openingBalance,
                    Description = description,
                    BalanceType = balanceType,
                    NormalBalance = normalBalance,
                    SortOrder = sortOrder
                });

                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 201, message = "Template created successfully." }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating template");
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> GetMappingTemplates()
        {
            try
            {
                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var sql = @"
            SELECT
            TemplateId, Module, PurposeCode, AccountNumber,
            IsRequired, SortOrder, Description, CreatedAt, UpdatedAt
            FROM dbo.gl_AccountMappingTemplate
            ORDER BY SortOrder, Module, PurposeCode, AccountNumber";

                var rows = await con.QueryAsync(sql);
                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 200, data = rows }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mapping templates");
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> CreateMappingTemplate(JsonElement body)
        {
            try
            {
                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var module = GetString(body, "Module") ?? "";
                var purposeCode = GetString(body, "PurposeCode") ?? "";
                var accountNumber = GetString(body, "AccountNumber") ?? "";

                if (string.IsNullOrWhiteSpace(module) ||
                    string.IsNullOrWhiteSpace(purposeCode) ||
                    string.IsNullOrWhiteSpace(accountNumber))
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 400,
                        Data = new { status = 400, message = "Module, PurposeCode and AccountNumber are required." }
                    };
                }

                var isRequired = GetString(body, "IsRequired") ?? "T";
                var sortOrder = GetInt(body, "SortOrder") ?? 0;
                var description = GetString(body, "Description");

                var sql = @"
                INSERT INTO dbo.gl_AccountMappingTemplate
                (
                    Module, PurposeCode, AccountNumber, IsRequired,
                    SortOrder, Description, CreatedAt, UpdatedAt
                )
                VALUES
                (
                    @Module, @PurposeCode, @AccountNumber, @IsRequired,
                    @SortOrder, @Description, GETDATE(), GETDATE()
                )";

                await con.ExecuteAsync(sql, new
                {
                    Module = module,
                    PurposeCode = purposeCode,
                    AccountNumber = accountNumber,
                    IsRequired = isRequired,
                    SortOrder = sortOrder,
                    Description = description
                });

                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 201, message = "Mapping template created successfully." }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating mapping template");
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> UpdateMappingTemplate(Guid templateId, JsonElement body)
        {
            try
            {
                if (templateId == Guid.Empty)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 400,
                        Data = new { status = 400, message = "Invalid template id." }
                    };
                }

                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var exists = await con.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.gl_AccountMappingTemplate WHERE TemplateId = @templateId",
                    new { templateId });

                if (exists == 0)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 404,
                        Data = new { status = 404, message = "Template not found." }
                    };
                }

                var module = GetString(body, "Module") ?? "";
                var purposeCode = GetString(body, "PurposeCode") ?? "";
                var accountNumber = GetString(body, "AccountNumber") ?? "";
                var isRequired = GetString(body, "IsRequired") ?? "T";
                var sortOrder = GetInt(body, "SortOrder") ?? 0;
                var description = GetString(body, "Description");

                if (string.IsNullOrWhiteSpace(module) ||
                    string.IsNullOrWhiteSpace(purposeCode) ||
                    string.IsNullOrWhiteSpace(accountNumber))
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 400,
                        Data = new { status = 400, message = "Module, PurposeCode and AccountNumber are required." }
                    };
                }

                var sql = @"
            UPDATE dbo.gl_AccountMappingTemplate
            SET
                Module = @Module,
                PurposeCode = @PurposeCode,
                AccountNumber = @AccountNumber,
                IsRequired = @IsRequired,
                SortOrder = @SortOrder,
                Description = @Description,
                UpdatedAt = GETDATE()
            WHERE TemplateId = @TemplateId";

                await con.ExecuteAsync(sql, new
                {
                    TemplateId = templateId,
                    Module = module,
                    PurposeCode = purposeCode,
                    AccountNumber = accountNumber,
                    IsRequired = isRequired,
                    SortOrder = sortOrder,
                    Description = description
                });

                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 200, message = "Mapping template updated successfully." }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating mapping template {TemplateId}", templateId);
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        public async Task<ServiceResult<object>> DeleteMappingTemplate(Guid templateId)
        {
            try
            {
                using var con = new SqlConnection(_configure.GetConnectionString("DefaultConnection"));

                var exists = await con.ExecuteScalarAsync<int>(
                    "SELECT COUNT(1) FROM dbo.gl_AccountMappingTemplate WHERE TemplateId = @templateId",
                    new { templateId });

                if (exists == 0)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        Status = 404,
                        Data = new { status = 404, message = "Template not found." }
                    };
                }

                await con.ExecuteAsync(
                    "DELETE FROM dbo.gl_AccountMappingTemplate WHERE TemplateId = @templateId",
                    new { templateId });

                return new ServiceResult<object>
                {
                    Success = true,
                    Status = 200,
                    Data = new { status = 200, message = "Mapping template deleted successfully." }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting mapping template {TemplateId}", templateId);
                return new ServiceResult<object>
                {
                    Success = false,
                    Status = 500,
                    Data = new { status = 500, message = ex.Message }
                };
            }
        }

        private static string? GetString(JsonElement body, string name)
        {
            if (!body.TryGetProperty(name, out var p)) return null;
            if (p.ValueKind == JsonValueKind.Null || p.ValueKind == JsonValueKind.Undefined) return null;
            return p.ValueKind == JsonValueKind.String ? p.GetString() : p.ToString();
        }

        private static int? GetInt(JsonElement body, string name)
        {
            if (!body.TryGetProperty(name, out var p)) return null;
            if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var v)) return v;
            if (p.ValueKind == JsonValueKind.String && int.TryParse(p.GetString(), out v)) return v;
            return null;
        }

        private static decimal? GetDecimal(JsonElement body, string name)
        {
            if (!body.TryGetProperty(name, out var p)) return null;
            if (p.ValueKind == JsonValueKind.Number && p.TryGetDecimal(out var v)) return v;
            if (p.ValueKind == JsonValueKind.String && decimal.TryParse(p.GetString(), out v)) return v;
            return null;
        }
    }
}
