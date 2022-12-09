using Module5HW1.dtos;
using Module5HW1.services.abstractions;

namespace Module5HW1;

public class App
{
    private readonly IUserService _userService;
    private IRegistrationService _registrationService;
    private IAuthenticationService _authenticationService;
    private IResourceService _resourceService;

    public App(IUserService userService, IRegistrationService registrationService, IAuthenticationService authenticationService, IResourceService resourceService)
    {
        _userService = userService;
        _registrationService = registrationService;
        _authenticationService = authenticationService;
        _resourceService = resourceService;
    }

    public async Task Start()
    {
        var test1 = await _userService.GetUsers(2);

        test1.ForEach(el =>
        {
            Console.WriteLine(el.Id);
            Console.WriteLine(el.Email);
        });
        Console.WriteLine("=====================");
        await _userService.UpdateUser(
            new UserDto() { Id = 2, Avatar = "https://avatar.com/100500", Email = "example.com", FirstName = "Vasya", LastName = "Pupkin" });

        var test3 = await _resourceService.GetResource(2);
        Console.WriteLine(test3.Name);
    }
}