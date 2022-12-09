using Module5HW1.dtos.abstractions;

namespace Module5HW1.dtos.responses;

public class RegisterResponse : BaseErrorPossibleResponse
{
    public int Id { get; set; }
    public string Token { get; set; } = null!;
}