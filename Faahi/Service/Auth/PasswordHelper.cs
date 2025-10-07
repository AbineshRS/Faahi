namespace Faahi.Service.Auth
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Generate a salt and hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        // Verify if the entered password matches the hashed password
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }
    }
}
