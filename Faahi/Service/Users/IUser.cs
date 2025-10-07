using Faahi.Dto;
using Faahi.Model.am_vcos;

namespace Faahi.Service.Users
{
    public interface IUser
    {
        Task<ServiceResult<ap_Vendors>> Create_vendors(ap_Vendors ap_Vendors);

        Task<ServiceResult<ar_Customers>> Create_customer(ar_Customers ar_Customers);
    }
}
