using Newtonsoft.Json;

namespace Module5HW1.dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;

    [JsonProperty(PropertyName = "first_name")]
    public string FirstName { get; set; } = null!;

    [JsonProperty(PropertyName = "last_name")]
    public string LastName { get; set; } = null!;

    public string Avatar { get; set; } = null!;
}