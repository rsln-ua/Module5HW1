using Module5HW1.dtos.abstractions;

namespace Module5HW1.dtos.responses;

public class LoginResponse : BaseErrorPossibleResponse
{
    public string Token { get; set; } = null!;
}