namespace Module5HW1.dtos.responses;

public class CreateUserResponse
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}