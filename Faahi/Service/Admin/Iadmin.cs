using Faahi.Dto;

namespace Faahi.Service.Admin
{
    public interface Iadmin
    {
        Task<AuthResponse> LoginAsyn(string username, string password);
    }
}
