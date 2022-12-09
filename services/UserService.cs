using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module5HW1.config;
using Module5HW1.dtos;
using Module5HW1.dtos.requests;
using Module5HW1.dtos.responses;
using Module5HW1.helpers;
using Module5HW1.services.abstractions;

namespace Module5HW1.services;

public class UserService : BaseDataService, IUserService
{
    private readonly IInnerHttpClientService _httpClientService;
    private readonly ILogger<UserService> _logger;
    private readonly ApiUrl _apiUrl;

    public UserService(IInnerHttpClientService httpClientService, ILogger<UserService> logger, IOptions<ApiOptions> apiOptions)
    {
        _httpClientService = httpClientService;
        _logger = logger;
        _apiUrl = new ApiUrl(apiOptions.Value.Host + apiOptions.Value.UsersApi);
    }

    public async Task<bool> CreateUser(string firsName, string lastName, string email, string avatar)
    {
        var result = await _httpClientService.SendAsync<CreateUserResponse, CreateUserRequest>(
            _apiUrl.Get(),
            HttpMethod.Post,
            new CreateUserRequest() { FirstName = firsName, LastName = lastName, Email = email, Avatar = avatar });

        if (result?.Id == null)
        {
            _logger.LogInformation("User hasn't created");
            return false;
        }

        _logger.LogInformation("User with id: {Id} has created", result.Id);
        return true;
    }

    public async Task<bool> UpdateUser(UserDto user)
    {
        var result = await _httpClientService.SendAsync<UpdateUserResponse, UpdateUserRequest>(
            _apiUrl.WithPath(user.Id.ToString()).Get(),
            HttpMethod.Put,
            new UpdateUserRequest() { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Avatar = user.Avatar });

        if (result?.UpdatedAt == null)
        {
            _logger.LogInformation("User with id: {Id} hasn't updated", user.Id);
            return false;
        }

        _logger.LogInformation("User with id: {Id} has updated", user.Id);
        return true;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var result = await _httpClientService.SendAsync<DeleteUserResponse, DeleteUserRequest>(
            _apiUrl.WithPath(id.ToString()).Get(),
            HttpMethod.Delete,
            new DeleteUserRequest());

        if (result == null)
        {
            _logger.LogInformation("User with id: {Id} hasn't deleted", id);
            return false;
        }

        _logger.LogInformation("User with id: {Id} has deleted", id);
        return true;
    }

    public async Task<UserDto> GetUser(int id)
    {
        var result = await _httpClientService.SendAsync<GetUserByIdResponse, GetUserByIdRequest>(
            _apiUrl.WithPath(id.ToString()).Get(),
            HttpMethod.Get,
            new GetUserByIdRequest());

        if (result?.Data.Id == null)
        {
            _logger.LogInformation("User with id: {Id} hasn't loaded", id);
            return default!;
        }

        _logger.LogInformation("User with id: {Id} has loaded", id);
        return result.Data;
    }

    public async Task<List<UserDto>> GetUsers(int page, int? delay = null)
    {
        var result = await _httpClientService.SendAsync<GetListUsersResponse, GetListUsersRequest>(
            _apiUrl.SetQueryParam(DelayParamKey, delay?.ToString()).SetQueryParam(PageParamKey, page.ToString()).Get(),
            HttpMethod.Get,
            new GetListUsersRequest());

        if (result?.Data == null)
        {
            _logger.LogInformation("List of users hasn't loaded");
            return default!;
        }

        _logger.LogInformation("List of users has loaded");
        return result.Data;
    }
}