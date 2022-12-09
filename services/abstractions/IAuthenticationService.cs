namespace Module5HW1.services.abstractions;

public interface IAuthenticationService
{
    Task<string> Login(string email, string password);
}