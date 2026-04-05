using Faahi.Dto;
using Faahi.Model.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Faahi.Service.Accounts
{
    public interface IAccountService
    {
        //List<AccountType> GetAccountTypes();
        Task<ServiceResult<List<AccountType>>> GetAccountTypes();
        AccountType CreateAccountType(AccountType dto);
        //List<gl_Accounts> GetGl_Accounts(Guid company_id);
        Task<ServiceResult<List<gl_Accounts>>> GetGl_Accounts(Guid company_id);
        //gl_Accounts CreateGlAccounts(gl_Accounts dto);
        Task<ServiceResult<gl_Accounts>> CreateGl_Accounts(gl_Accounts dto);
        Task<ServiceResult<gl_Accounts>> UpdateGl_Accounts(Guid glAccountId, gl_Accounts dto);

        // gl_AccountMapping Methods
        Task<ServiceResult<List<gl_AccountMapping>>> GetAccountMappings(Guid companyId);
        Task<ServiceResult<gl_AccountMapping>> CreateAccountMapping(gl_AccountMapping dto);
        Task<ServiceResult<gl_AccountMapping>> UpdateAccountMapping(Guid defaultId, gl_AccountMapping dto);

        // gl_Ledger Methods
        Task<ServiceResult<List<gl_Ledger>>> GetLedgerEntries(Guid companyId);
        Task<ServiceResult<gl_Ledger>> CreateLedgerEntry(gl_Ledger dto);
        Task<ServiceResult<gl_Ledger>> UpdateLedgerEntry(Guid ledgerId, gl_Ledger dto);

        // gl_JournalHeaders Methods
        Task<ServiceResult<List<gl_JournalHeaders>>> GetJournalEntries(Guid company_id);
        Task<ServiceResult<gl_JournalHeaders>> GetJournalEntryById(Guid journalId);
        Task<ServiceResult<gl_JournalHeaders>> CreateJournalEntry(gl_JournalHeaders dto);
        Task<ServiceResult<gl_JournalHeaders>> UpdateJournalEntry(Guid journalId, gl_JournalHeaders dto);
        Task<string> GenerateJournalNumber(Guid company_id);

        // gl_JournalAttachments Methods
        Task<ActionResult<ServiceResult<List<gl_JournalAttachments>>>> UploadJournalAttachments(IFormFile[] formFiles, Guid journalId);
        Task<ServiceResult<bool>> DeleteJournalAttachment(Guid attachmentId);


        //gl_AccountCurrentBalances
        Task<ServiceResult<List<gl_AccountCurrentBalances>>> GetAccountCurrentBalances(Guid businessId);
        Task<ServiceResult<gl_AccountCurrentBalances>> CreateAccountCurrentBalances(gl_AccountCurrentBalances dto);
        Task<ServiceResult<bool>> SeedAccountCurrentBalances(Guid businessId);

        //ap_Expenses
        Task<ServiceResult<List<ap_Expenses>>> GetExpenses(Guid businessId);
        Task<ServiceResult<ap_Expenses>> GetExpenseById(Guid expenseId);
        Task<ServiceResult<ap_Expenses>> CreateExpense(ap_Expenses dto);
        Task<ServiceResult<ap_Expenses>> UpdateExpense(Guid expenseId, ap_Expenses dto);
        Task<ServiceResult<bool>> DeleteExpense(Guid expenseId);
        Task<string> GenerateExpenseNumber(Guid businessId);
        Task<ServiceResult<string>> Upload_expense_attachments(Guid expenseId, List<IFormFile> files);

        //ap_Cheques
        Task<ServiceResult<ap_Cheques>> Create_cheque(ap_Cheques cheque);
        Task<ServiceResult<List<ap_Cheques>>> Get_all_cheques(Guid businessId);
        Task<ServiceResult<string>> Upload_cheque_attachments(Guid chequeId, List<IFormFile> files);
        Task<ServiceResult<object>> DeleteCheque(Guid chequeId);
        Task<ServiceResult<ap_Cheques>> UpdateCheque(Guid chequeId, ap_Cheques dto);

        // Template and MappingTemplate Methods
        Task<ServiceResult<object>> GetTemplates();
        Task<ServiceResult<object>> CreateTemplate(JsonElement body);
        Task<ServiceResult<object>> UpdateTemplate(Guid templateId, JsonElement body);
        Task<ServiceResult<object>> DeleteTemplate(Guid templateId);
        Task<ServiceResult<object>> GetMappingTemplates();
        Task<ServiceResult<object>> CreateMappingTemplate(JsonElement body);
        Task<ServiceResult<object>> UpdateMappingTemplate(Guid templateId, JsonElement body);
        Task<ServiceResult<object>> DeleteMappingTemplate(Guid templateId);

    }
}
