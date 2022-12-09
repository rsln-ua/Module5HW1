using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module5HW1.config;
using Module5HW1.dtos.requests;
using Module5HW1.dtos.responses;
using Module5HW1.services.abstractions;

namespace Module5HW1.services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IInnerHttpClientService _httpClientService;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly string _apiUrl;

    public AuthenticationService(IInnerHttpClientService httpClientService, ILogger<AuthenticationService> logger, IOptions<ApiOptions> apiOptions)
    {
        _httpClientService = httpClientService;
        _logger = logger;
        _apiUrl = $"{apiOptions.Value.Host}{apiOptions.Value.AuthenticationApi}";
    }

    public async Task<string> Login(string email, string password)
    {
        var result = await _httpClientService.SendAsync<LoginResponse, LoginRequest>(
            _apiUrl,
            HttpMethod.Post,
            new LoginRequest() { Email = email, Password = password });

        if (result is not { Error: null })
        {
            _logger.LogInformation("Login failed - error message: {ResultError}", result?.Error);
            return default!;
        }

        _logger.LogInformation("User was authorized");
        return result.Token;
    }
}