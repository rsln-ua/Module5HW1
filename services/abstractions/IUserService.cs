using Module5HW1.dtos;

namespace Module5HW1.services.abstractions;

public interface IUserService
{
    Task<bool> CreateUser(string firsName, string lastName, string email, string avatar);
    Task<bool> UpdateUser(UserDto user);
    Task<bool> DeleteUser(int id);
    Task<UserDto> GetUser(int id);
    Task<List<UserDto>> GetUsers(int page, int? delay = null);
}