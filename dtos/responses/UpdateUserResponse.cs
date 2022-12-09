namespace Module5HW1.dtos.responses;

public class UpdateUserResponse
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}