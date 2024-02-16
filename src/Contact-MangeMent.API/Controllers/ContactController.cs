using Autofac;
using Contact_MangeMent.API.Models;
using Contact_MangeMent.API.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace Contact_MangeMent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly ILifetimeScope _scope;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        [HttpPost("Contact")]
        public  IActionResult AddContact([FromBody] ContactCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);

                model.Add();

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, "Oops! Something went wrong. Please try again later.");
            }
        }
    }
}
