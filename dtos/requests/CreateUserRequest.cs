namespace Module5HW1.dtos.requests;

public class CreateUserRequest
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Avatar { get; set; } = null!;
}