using Contact_Management.Application.Services.Auth;
using Contact_Management.Application.Services.Securities;
using Contact_ManageMent.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Contact_Management.Infrastructure.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

        public async Task<(bool, object)> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return (false, new { status = "error", message = "Please provide both email and password" });
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, new { status = "error", message = "Invalid email address" });
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, true, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);

                var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

                foreach (var claim in roleClaims)
                {
                    claims.Add(claim);
                }

                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                var token = await _tokenService.GetJwtToken(claims);

                var userData = new
                {
                    id = user.Id,
                    username = user.UserName,
                    email = user.Email,
                    claims = claims.Select(c => new { value = c.Value }),
                };

                return (true, new
                {
                    status = "Success",
                    message = "Login successful",
                    data = new { user = userData, token }
                });
            }
            else
            {
                _logger.LogError("Failed login attempt for email {Email}. Reason: {Reason}", email, result.ToString());
                return (false, new { status = "error", message = "Invalid credentials" });
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
