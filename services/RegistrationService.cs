using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module5HW1.config;
using Module5HW1.dtos.requests;
using Module5HW1.dtos.responses;
using Module5HW1.services.abstractions;

namespace Module5HW1.services;

public class RegistrationService : IRegistrationService
{
    private readonly IInnerHttpClientService _httpClientService;
    private readonly ILogger<RegistrationService> _logger;
    private readonly string _apiUrl;

    public RegistrationService(IInnerHttpClientService httpClientService, IOptions<ApiOptions> apiOptions, ILogger<RegistrationService> logger)
    {
        _httpClientService = httpClientService;
        _logger = logger;
        _apiUrl = $"{apiOptions.Value.Host}{apiOptions.Value.RegistrationApi}";
    }

    public async Task<string> Register(string email, string password)
    {
        var result = await _httpClientService.SendAsync<RegisterResponse, RegisterRequest>(
            _apiUrl,
            HttpMethod.Post,
            new RegisterRequest() { Email = email, Password = password });

        if (result is not { Error: null })
        {
            _logger.LogInformation("Registration failed - error message: {ResultError}", result?.Error);
            return default!;
        }

        _logger.LogInformation("User with id: {ResultId} was registered", result.Id);
        return result.Token;
    }
}