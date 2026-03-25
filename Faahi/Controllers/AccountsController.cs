using Faahi.Dto;
using Dapper;
using Microsoft.Extensions.Configuration;
using Faahi.Model.Accounts;
using Faahi.Service.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Text.Json;

namespace Faahi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }

        [HttpGet("types")]
        public async Task< IActionResult> GetAccountTypes()
        {
            //return Ok(_accountService.GetAccountTypes());
            var result = await _accountService.GetAccountTypes();

            return Ok(result);
        }

        [HttpPost("createTypes")]
        public IActionResult CreateAccountType([FromBody] AccountType dto)
        {
            var result = _accountService.CreateAccountType(dto);
            return Ok(result);
        }

        [HttpGet("glAccounts/{company_id}")]
        public async Task<IActionResult> GetGlAccounts(Guid company_id)
        {
            var result = await _accountService.GetGl_Accounts(company_id);
            return Ok(result);
        }

        //[HttpPost("createGlAccount")]
        //public async Task<IActionResult> CreateGl_Account([FromBody] gl_Accounts dto)
        //{
        //    var result = await _accountService.CreateGl_Accounts(dto);
        //    return Ok(result);
        //}

        [HttpPost("createGlAccount")]
        public async Task<IActionResult> CreateGl_Account([FromBody] gl_Accounts dto)
        {
            var result = await _accountService.CreateGl_Accounts(dto);

            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("updateGlAccount/{glAccountId}")]
        public async Task<IActionResult> UpdateGlAccount(Guid glAccountId, [FromBody] gl_Accounts gl_Accounts)
        {
            var result = await _accountService.UpdateGl_Accounts(glAccountId, gl_Accounts);

            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpPost("seedCurrentBalances/{businessId}")]
        public async Task<IActionResult> SeedCurrentBalances(Guid businessId)
        {
            var result = await _accountService.SeedAccountCurrentBalances(businessId);

            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpGet("accountMappings/{companyId}")]
        public async Task<IActionResult> GetAccountMappings(Guid companyId)
        {
            var result = await _accountService.GetAccountMappings(companyId);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpPost("createAccountMapping")]
        public async Task<IActionResult> CreateAccountMapping([FromBody] gl_AccountMapping dto)
        {
            var result = await _accountService.CreateAccountMapping(dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpPost("updateAccountMapping/{defaultId}")]
        public async Task<IActionResult> UpdateAccountMapping(Guid defaultId, [FromBody] gl_AccountMapping dto)
        {
            var result = await _accountService.UpdateAccountMapping(defaultId, dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        // GL Ledger Endpoints

        [HttpGet("ledgerEntries/{companyId}")]
        public async Task<IActionResult> GetLedgerEntries(Guid companyId)

        {
            var responce = await _accountService.GetLedgerEntries(companyId);
          return Ok(responce);

        }

        [HttpPost("createLedgerEntry")]
        public async Task<IActionResult> CreateLedgerEntry([FromBody] gl_Ledger dto)
        {
            var result = await _accountService.CreateLedgerEntry(dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("updateLedgerEntry/{ledgerId}")]
        public async Task<IActionResult> UpdateLedgerEntry(Guid ledgerId, [FromBody] gl_Ledger dto)
        {
            var result = await _accountService.UpdateLedgerEntry(ledgerId, dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        // Journal Entry Endpoints

        [HttpGet("journalEntries/{company_id}")]
        public async Task<IActionResult> GetJournalEntries(Guid company_id)
         
        {
            var result = await _accountService.GetJournalEntries(company_id);
          
            return Ok(result);
        }

        [HttpGet("journalEntry/{journalId}")]
        public async Task<IActionResult> GetJournalEntryById(Guid journalId)
        {
            var result = await _accountService.GetJournalEntryById(journalId);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpPost("createJournalEntry")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] gl_JournalHeaders dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid data", errors = ModelState });
                }

                var result = await _accountService.CreateJournalEntry(dto);
                
                if (!result.Success)
                {
                    {
                        return result.Status switch
                        {
                            400 => BadRequest(result),
                            404 => NotFound(result),
                            401 => Unauthorized(result),
                            _ => StatusCode(500, result)
                        };
                    }
                }

                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = new
                    {
                        journalId = result.Data.JournalId,
                        journalNo = result.Data.JournalNo,
                        journalDate = result.Data.JournalDate,
                        status = result.Data.Status,
                        totalDebit = result.Data.JournalLines?.Sum(l => l.DebitAmountBC) ?? 0,
                        totalCredit = result.Data.JournalLines?.Sum(l => l.CreditAmountBC) ?? 0,
                        linesCount = result.Data.JournalLines?.Count ?? 0
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("updateJournalEntry/{journalId}")]
        public async Task<IActionResult> UpdateJournalEntry(Guid journalId, [FromBody] gl_JournalHeaders dto)
        {
            var result = await _accountService.UpdateJournalEntry(journalId, dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    401 => Unauthorized(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpGet("generateJournalNumber/{company_id}")]
        public async Task<IActionResult> GenerateJournalNumber(Guid company_id)
        {
            try
            {
                if (company_id == Guid.Empty)
                {
                    return BadRequest(new { success = false, message = "Invalid company ID" });
                }

                var journalNumber = await _accountService.GenerateJournalNumber(company_id);

                return Ok(new
                {
                    success = true,
                    data = journalNumber,
                    journalNumber = journalNumber
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // Journal Attachments Endpoints

        [HttpPost("uploadJournalAttachments/{journalId}")]
        public async Task<IActionResult> UploadJournalAttachments(Guid journalId, [FromForm] IFormFile[] files)
        {
            try
            {
                if (journalId == Guid.Empty)
                {
                    return BadRequest(new { success = false, message = "Journal ID is required" });
                }

                if (files == null || files.Length == 0)
                {
                    return BadRequest(new { success = false, message = "No files provided" });
                }

                var result = await _accountService.UploadJournalAttachments(files, journalId);

                if (result.Value == null || !result.Value.Success)
                {
                    return StatusCode(result.Value?.Status ?? 500, result.Value);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("deleteJournalAttachment/{attachmentId}")]
        public async Task<IActionResult> DeleteJournalAttachment(Guid attachmentId)
        {
            try
            {
                if (attachmentId == Guid.Empty)
                {
                    return BadRequest(new { success = false, message = "Attachment ID is required" });
                }

                var result = await _accountService.DeleteJournalAttachment(attachmentId);

                if (!result.Success)
                {
                    return result.Status switch
                    {
                        404 => NotFound(result),
                        _ => StatusCode(result.Status, result)
                    };
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("currentBalances/{businessId}")]
        public async Task<IActionResult> GetAccountCurrentBalances(Guid businessId)
        {
            var result = await _accountService.GetAccountCurrentBalances(businessId);

            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpPost("createCurrentBalance")]
        public async Task<IActionResult> CreateAccountCurrentBalance(
        [FromBody] gl_AccountCurrentBalances dto)
        {
            var result = await _accountService.CreateAccountCurrentBalances(dto);

            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }

            return Ok(result);
        }

        [HttpGet("expenses/{businessId}")]
        public async Task<IActionResult> GetExpenses(Guid businessId)
        {
            var result = await _accountService.GetExpenses(businessId);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }
            return Ok(result);
        }
        [HttpGet("expenses/detail/{expenseId}")]
        public async Task<IActionResult> GetExpenseById(Guid expenseId)
        {
            var result = await _accountService.GetExpenseById(expenseId);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }
            return Ok(result);
        }
        [HttpPost("expenses/create")]
        public async Task<IActionResult> CreateExpense([FromBody] ap_Expenses dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Invalid data", errors = ModelState });
                var result = await _accountService.CreateExpense(dto);
                if (!result.Success)
                {
                    return result.Status switch
                    {
                        400 => BadRequest(result),
                        404 => NotFound(result),
                        _ => StatusCode(500, result)
                    };
                }
                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = new
                    {
                        expenseId = result.Data!.ExpenseId,
                        expenseNo = result.Data.ExpenseNo,
                        expenseDate = result.Data.ExpenseDate,
                        status = result.Data.Status,
                        totalAmount = result.Data.TotalAmount,
                        linesCount = result.Data.ExpenseLines?.Count ?? 0
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        //[HttpPost("expenses/update/{expenseId}")]
        //public async Task<IActionResult> UpdateExpense(Guid expenseId, [FromBody] ap_Expenses dto)
        //{
        //    var result = await _accountService.UpdateExpense(expenseId, dto);
        //    if (!result.Success)
        //    {
        //        return result.Status switch
        //        {
        //            400 => BadRequest(result),
        //            404 => NotFound(result),
        //            _ => StatusCode(500, result)
        //        };
        //    }
        //    return Ok(result);
        //}
        [HttpDelete("expenses/delete/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(Guid expenseId)
        {
            try
            {
                if (expenseId == Guid.Empty)
                    return BadRequest(new { success = false, message = "Expense ID is required" });
                var result = await _accountService.DeleteExpense(expenseId);
                if (!result.Success)
                {
                    return result.Status switch
                    {
                        400 => BadRequest(result),
                        404 => NotFound(result),
                        _ => StatusCode(500, result)
                    };
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("expenses/generateExpenseNumber/{businessId}")]
        public async Task<IActionResult> GenerateExpenseNumber(Guid businessId)
        {
            try
            {
                if (businessId == Guid.Empty)
                    return BadRequest(new { success = false, message = "Invalid business ID" });
                var expenseNumber = await _accountService.GenerateExpenseNumber(businessId);
                return Ok(new
                {
                    success = true,
                    data = expenseNumber,
                    expenseNumber = expenseNumber
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("cheques/create")]
        public async Task<IActionResult> Create_cheque([FromBody] ap_Cheques cheque)
        {
            if (cheque == null) return Ok("No data found");
            var result = await _accountService.Create_cheque(cheque);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("cheques/{businessId}")]
        public async Task<IActionResult> Get_all_cheques(Guid businessId)
        {
            var result = await _accountService.Get_all_cheques(businessId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("uploadChequeAttachments/{chequeId}")]
        public async Task<IActionResult> Upload_cheque_attachments(Guid chequeId, [FromForm] List<IFormFile> files)
        {
            var result = await _accountService.Upload_cheque_attachments(chequeId, files);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("uploadExpenseAttachments/{expenseId}")]
        public async Task<IActionResult> Upload_expense_attachments(Guid expenseId, [FromForm] List<IFormFile> files)
        {
            var result = await _accountService.Upload_expense_attachments(expenseId, files);
            return Ok(result);
        }

        [HttpDelete("cheques/delete/{chequeId}")]
        public async Task<IActionResult> DeleteCheque(Guid chequeId)
        {
            try
            {
                if (chequeId == Guid.Empty)
                    return BadRequest(new { success = false, message = "Cheque ID is required" });
                var result = await _accountService.DeleteCheque(chequeId);
                if (!result.Success)
                {
                    return result.Status switch
                    {
                        400 => BadRequest(result),
                        404 => NotFound(result),
                        _ => StatusCode(500, result)
                    };
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // Replace the existing POST expenses/update with this PUT:
        [HttpPut("expenses/{expenseId}")]
        public async Task<IActionResult> UpdateExpense(Guid expenseId, [FromBody] ap_Expenses dto)
        {
            var result = await _accountService.UpdateExpense(expenseId, dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }
            return Ok(result);
        }

        [HttpPut("cheques/{chequeId}")]
        public async Task<IActionResult> UpdateCheque(Guid chequeId, [FromBody] ap_Cheques dto)
        {
            var result = await _accountService.UpdateCheque(chequeId, dto);
            if (!result.Success)
            {
                return result.Status switch
                {
                    400 => BadRequest(result),
                    404 => NotFound(result),
                    _ => StatusCode(500, result)
                };
            }
            return Ok(result);
        }

        [HttpGet("templates")]
        public async Task<IActionResult> GetTemplates()
        {
            using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var sql = @"
            SELECT
                TemplateId, AccountNumber, AccountName, AccountType, DetailType,
                ParentTemplateId, IsPostable, IsActive, CurrencyCode, OpeningBalance,
                Description, BalanceType, NormalBalance, SortOrder, CreatedAt, UpdatedAt
            FROM dbo.gl_AccountTemplates
            ORDER BY SortOrder, AccountNumber";

            var rows = await con.QueryAsync(sql);
            return Ok(new { status = 200, data = rows });
        }

        //[HttpDelete("templates/{templateId:guid}")]
        //public async Task<IActionResult> DeleteTemplate(Guid templateId)
        //{
        //    using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    // Check if template exists
        //    var exists = await con.ExecuteScalarAsync<int>(
        //        "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
        //        new { templateId });

        //    if (exists == 0)
        //    {
        //        return NotFound(new { status = 404, message = "Template not found." });
        //    }

        //    // Check children (because FK ON DELETE NO ACTION)
        //    var childCount = await con.ExecuteScalarAsync<int>(
        //        "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE ParentTemplateId = @templateId",
        //        new { templateId });

        //    if (childCount > 0)
        //    {
        //        // Soft delete if children exist
        //        await con.ExecuteAsync(
        //            @"UPDATE dbo.gl_AccountTemplates
        //      SET IsActive = 'F', UpdatedAt = GETDATE()
        //      WHERE TemplateId = @templateId",
        //            new { templateId });

        //        return Ok(new { status = 200, message = "Template has children; marked inactive instead of hard delete." });
        //    }

        //    // Hard delete if no children
        //    await con.ExecuteAsync(
        //        "DELETE FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
        //        new { templateId });

        //    return Ok(new { status = 200, message = "Template deleted successfully." });
        //}

        [HttpDelete("templates/{templateId:guid}")]
        public async Task<IActionResult> DeleteTemplate(Guid templateId)
        {
            using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var exists = await con.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
                new { templateId });

            if (exists == 0)
                return NotFound(new { status = 404, message = "Template not found." });

            var childCount = await con.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE ParentTemplateId = @templateId",
                new { templateId });

            if (childCount > 0)
            {
                await con.ExecuteAsync(
                    "UPDATE dbo.gl_AccountTemplates SET IsActive='F', UpdatedAt=GETDATE() WHERE TemplateId=@templateId",
                    new { templateId });

                return Ok(new { status = 200, message = "Template has children; marked inactive." });
            }

            await con.ExecuteAsync(
                "DELETE FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
                new { templateId });

            return Ok(new { status = 200, message = "Template deleted successfully." });
        }

        [HttpPut("templates/{templateId:guid}")]
        public async Task<IActionResult> UpdateTemplate(Guid templateId, [FromBody] JsonElement body)
        {
            if (templateId == Guid.Empty)
                return BadRequest(new { status = 400, message = "Invalid template id." });

            using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var exists = await con.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM dbo.gl_AccountTemplates WHERE TemplateId = @templateId",
                new { templateId });

            if (exists == 0)
                return NotFound(new { status = 404, message = "Template not found." });

            var accountNumber = GetString(body, "AccountNumber") ?? "";
            var accountName = GetString(body, "AccountName") ?? "";
            var accountType = GetString(body, "AccountType") ?? "";
            var isPostable = GetString(body, "IsPostable") ?? "T";
            var isActive = GetString(body, "IsActive") ?? "T";
            var openingBalance = GetDecimal(body, "OpeningBalance") ?? 0m;
            var description = GetString(body, "Description");
            var sortOrder = GetInt(body, "SortOrder") ?? 0;

            Guid? parentTemplateId = null;
            var parentRaw = GetString(body, "ParentTemplateId");
            if (!string.IsNullOrWhiteSpace(parentRaw) && Guid.TryParse(parentRaw, out var parentGuid))
                parentTemplateId = parentGuid;

            if (parentTemplateId.HasValue && parentTemplateId.Value == templateId)
                return BadRequest(new { status = 400, message = "Template cannot be its own parent." });

            var sql = @"
        UPDATE dbo.gl_AccountTemplates
        SET
            AccountNumber = @AccountNumber,
            AccountName = @AccountName,
            AccountType = @AccountType,
            ParentTemplateId = @ParentTemplateId,
            IsPostable = @IsPostable,
            IsActive = @IsActive,
            OpeningBalance = @OpeningBalance,
            Description = @Description,
            SortOrder = @SortOrder,
            UpdatedAt = GETDATE()
        WHERE TemplateId = @TemplateId";

            await con.ExecuteAsync(sql, new
            {
                TemplateId = templateId,
                AccountNumber = accountNumber,
                AccountName = accountName,
                AccountType = accountType,
                ParentTemplateId = parentTemplateId,
                IsPostable = isPostable,
                IsActive = isActive,
                OpeningBalance = openingBalance,
                Description = description,
                SortOrder = sortOrder
            });

            return Ok(new { status = 200, message = "Template updated successfully." });
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
