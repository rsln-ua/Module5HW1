namespace Module5HW1.services.abstractions;

public interface IRegistrationService
{
    Task<string> Register(string email, string password);
}