using Autofac;
using Contact_Management.Application.Services.Securities;
using Contact_Management.Persistence.Membership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contact_MangeMent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILifetimeScope _scope;
        public AuthController(ILogger<AuthController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IConfiguration configuration,
            ILifetimeScope scope)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _scope = scope;
        }


    }
}
