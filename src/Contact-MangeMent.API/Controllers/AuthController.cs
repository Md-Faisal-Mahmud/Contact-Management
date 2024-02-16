using Autofac;
using Contact_Management.Application.Services.Auth;
using Contact_Management.Application.Services.Securities;
using Contact_Management.Persistence.Membership;
using Contact_MangeMent.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contact_MangeMent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILifetimeScope _scope;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(ILogger<AuthController> logger,
            IAuthenticationService authenticationService,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IConfiguration configuration,
            ILifetimeScope scope)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _tokenService = tokenService;
            _configuration = configuration;
            _scope = scope;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.ResolveDependency(_scope);
            var registrationResult = await model.RegisterUserAsync();

            if (registrationResult)
            {
                return Ok(new { message = "User registered successfully." });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User registration failed. Please try again later.");
                return BadRequest(ModelState);
            }
        }






    }
}
