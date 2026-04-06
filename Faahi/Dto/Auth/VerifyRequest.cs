namespace Faahi.Dto.Auth
{
    public class VerifyRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
    }
}
