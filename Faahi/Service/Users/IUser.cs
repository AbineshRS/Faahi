using Faahi.Dto;
using Faahi.Model.am_vcos;

namespace Faahi.Service.Users
{
    public interface IUser
    {
        Task<ServiceResult<ap_Vendors>> Create_vendors(ap_Vendors ap_Vendors);

        Task<ServiceResult<ar_Customers>> Create_customer(ar_Customers ar_Customers);

        Task<ServiceResult<List<ar_Customers>>> Get_all_customer(Guid company_id);

        Task<ServiceResult<List<ap_Vendors>>> Get_all_vendors(Guid company_id);
    }
}
