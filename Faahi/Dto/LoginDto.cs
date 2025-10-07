namespace Faahi.Dto
{
    public class LoginDto
    {
        public string Usernmae { get; set; }
        public string Password { get; set; }
    }
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class TokenRequest
    {
        public string RefreshToken { get; set; }
    }

}
