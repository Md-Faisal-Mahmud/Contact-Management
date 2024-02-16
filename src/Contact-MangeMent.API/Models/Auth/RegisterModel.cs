using Autofac;
using Contact_Management.Application.Services.Auth;
using System.ComponentModel.DataAnnotations;

namespace Contact_MangeMent.API.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        private IAuthenticationService _authenticationService;
        public RegisterModel()
        {

        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _authenticationService = scope.Resolve<IAuthenticationService>();
        }

        public RegisterModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<bool> RegisterUserAsync()
        {
            return await _authenticationService.RegisterUserAsync(UserName, Password, Email);
        }

    }
}