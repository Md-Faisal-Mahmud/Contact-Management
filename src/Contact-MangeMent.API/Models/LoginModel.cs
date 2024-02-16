using Autofac;
using Contact_Management.Application.Services.Auth;
using System.ComponentModel.DataAnnotations;

namespace Contact_MangeMent.API.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private IAuthenticationService _authenticationService;   
        public LoginModel()
        {
            
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _authenticationService = scope.Resolve<IAuthenticationService>();
        }

        public LoginModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<(bool, object)> LoginAsync()
        {
            return await _authenticationService.LoginAsync(Email, Password);
        }

    }
}
