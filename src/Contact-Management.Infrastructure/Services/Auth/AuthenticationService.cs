using Contact_Management.Application.Services.Auth;
using Contact_Management.Persistence.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Contact_Management.Infrastructure.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(UserManager<ApplicationUser> userManager, ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> RegisterUserAsync(string username, string password, string email)
        {
            // Check if the email is already registered
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                _logger.LogWarning($"Registration failed: Email address '{email}' is already registered.");
                return false;
            }

            // Create a new user
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                return true;
            }

            // If creation fails, log errors
            foreach (var error in result.Errors)
            {
                _logger.LogError($"User creation failed: {error.Description}");
            }

            return false;
        }
    }

}
