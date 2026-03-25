using Faahi.Model;
using Faahi.Model.OneMall;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Faahi.Service.OneMall
{
    public class OneMallService : IOneMallService
    {
        private readonly DbContext _context;

        public OneMallService(DbContext context)
        {
            _context = context;
        }

        public async Task<object> RegisterCustomerAsync(am_users user)
        {
            if (user == null) return new { ok = false, message = "Invalid payload." };

            if (string.IsNullOrWhiteSpace(user.userName) ||
                string.IsNullOrWhiteSpace(user.password) ||
                string.IsNullOrWhiteSpace(user.fullName) ||
                string.IsNullOrWhiteSpace(user.email))
            {
                return new { ok = false, message = "Username, password, fullName, email are required." };
            }

            // duplicate checks
            bool usernameExists = await _context.Set<am_users>().AnyAsync(x => x.userName == user.userName);
            if (usernameExists) return new { ok = false, message = "Username already exists." };

            bool emailExists = await _context.Set<am_users>().AnyAsync(x => x.email == user.email);
            if (emailExists) return new { ok = false, message = "Email already exists." };

            // keep simple; in production hash password
            user.userId = Guid.NewGuid();
            user.created_at = DateTime.Now;
            user.edit_date_time = DateTime.Now;
            user.status = string.IsNullOrWhiteSpace(user.status) ? "T" : user.status;
            user.emailVerified = string.IsNullOrWhiteSpace(user.emailVerified) ? "F" : user.emailVerified;
            user.isGoogleSignUp = string.IsNullOrWhiteSpace(user.isGoogleSignUp) ? "F" : user.isGoogleSignUp;

            // if firstName/lastName not sent, derive from fullName
            if (string.IsNullOrWhiteSpace(user.firstName))
            {
                var parts = user.fullName.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                user.firstName = parts.Length > 0 ? parts[0] : "";
                user.lastName = parts.Length > 1 ? parts[1] : user.lastName;
            }

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Set<am_users>().Add(user);
                await _context.SaveChangesAsync();

                var profile = new mk_customer_profiles
                {
                    user_id = user.userId!.Value,
                    status = "A",
                    created_at = DateTime.Now
                };

                _context.Set<mk_customer_profiles>().Add(profile);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();

                return new
                {
                    ok = true,
                    message = "Signup successful.",
                    userId = user.userId,
                    customerProfileId = profile.customer_profile_id
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new { ok = false, message = ex.Message };
            }
        }
    }
}