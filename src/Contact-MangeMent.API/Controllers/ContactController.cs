using Autofac;
using Contact_MangeMent.API.Models;
using Contact_MangeMent.API.Models.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

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

        [Authorize]
        [HttpPost("Contact")]
        public  IActionResult AddContact([FromBody] ContactCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                // Get the currently logged-in user's ID
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                model.Add(Guid.Parse(userId));
                return Ok("Contact Created Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, "Oops! Something went wrong. Please try again later.");
            }
        }

        [Authorize]
        [HttpPut("Contact")]
        public IActionResult EditContact([FromBody] ContactEditModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                // Get the currently logged-in user's ID
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                model.Edit(Guid.Parse(userId));
                return Ok("Contact Edited Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, "Oops! Something went wrong. Please try again later.");
            }
        }

        [Authorize]
        [HttpDelete("Contact")]
        public IActionResult DeleteContact(Guid id)
        {
            var model = _scope.Resolve<ContactListModel>();
            try
            {
                model.ResolveDependency(_scope);
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                model.Delete(id,Guid.Parse(userId));
                return Ok("Contact Deleted Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, "Oops! Something went wrong. Please try again later.");
            }
        }



    }
}
