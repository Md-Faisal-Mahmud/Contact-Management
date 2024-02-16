using Contact_Management.Application.Services.Auth;
using Contact_Management.Persistence.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

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

                // Assign a default claim to the new user
                var claimResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                if (!claimResult.Succeeded)
                {
                    // If adding the claim fails, log errors and rollback user creation
                    _logger.LogError($"Failed to assign default claim to user: {string.Join(", ", claimResult.Errors.Select(e => e.Description))}");
                    await _userManager.DeleteAsync(user);
                    return false;
                }

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
