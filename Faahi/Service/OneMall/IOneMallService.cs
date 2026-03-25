using Faahi.Model;
using System.Threading.Tasks;

namespace Faahi.Service.OneMall
{
    public interface IOneMallService
    {
        Task<object> RegisterCustomerAsync(am_users user);
    }
}