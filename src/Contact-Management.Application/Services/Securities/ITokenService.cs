using System.Security.Claims;

namespace Contact_Management.Application.Services.Securities
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}
