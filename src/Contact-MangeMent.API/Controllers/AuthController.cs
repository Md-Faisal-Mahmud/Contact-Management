using Autofac;
using Contact_ManageMent.Domain.Entities;
using Contact_MangeMent.API.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contact_MangeMent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILifetimeScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(ILogger<AuthController> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILifetimeScope scope)
        {
            _logger = logger;
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.ResolveDependency(_scope);
            var (success, result) = await model.LoginAsync();

            if (success)
            {
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                return BadRequest(ModelState);
            }
        }

    }
}