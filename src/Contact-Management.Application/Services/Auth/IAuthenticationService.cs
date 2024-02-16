namespace Contact_Management.Application.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterUserAsync(string username, string password, string email);
    }
}
