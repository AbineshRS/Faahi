using Faahi.Dto;
using Faahi.Dto.mk_blacklisted;
using Faahi.Model.site_settings;
using Faahi.Model.tax_class_table;

namespace Faahi.Service.site_settings_service
{
    public interface Isite_settings
    {
        Task<ServiceResult<tx_TaxClasses>> Add_tax_class(tx_TaxClasses tax_class);

        Task<ServiceResult<tx_TaxClasses>> Update_tax(Guid tax_class_id, tx_TaxClasses tax_class);

        Task<ServiceResult<List<tx_TaxClasses>>> Get_tax(Guid company_id);

        Task<ServiceResult<tx_TaxClasses>> get_tax_class(Guid tax_class_id);

        Task<ServiceResult<mk_blacklisted_numbers>> Add_blacklist(mk_blacklisted_numbers mk_Blacklisted_Numbers);

        Task<ServiceResult<List<mk_blacklisted_numbers>>> blacklist(Guid company_id);

        Task<ServiceResult<mk_blacklisted_numbers_dto>> Change_status(Guid blacklist_id, mk_blacklisted_numbers_dto mk_Blacklisted_Numbers);
    }
}
