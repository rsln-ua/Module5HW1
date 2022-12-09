using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module5HW1.config;
using Module5HW1.dtos;
using Module5HW1.dtos.requests;
using Module5HW1.dtos.responses;
using Module5HW1.helpers;
using Module5HW1.services.abstractions;

namespace Module5HW1.services;

public class ResourceService : BaseDataService, IResourceService
{
    private readonly IInnerHttpClientService _httpClientService;
    private readonly ILogger<ResourceService> _logger;
    private readonly ApiUrl _apiUrl;

    public ResourceService(IInnerHttpClientService httpClientService, ILogger<ResourceService> logger, IOptions<ApiOptions> apiOptions)
    {
        _httpClientService = httpClientService;
        _logger = logger;
        _apiUrl = new ApiUrl(apiOptions.Value.Host + apiOptions.Value.ResourceApi);
    }

    public async Task<List<ResourceDto>> GetListResources(int page, int? delay = null)
    {
        var result = await _httpClientService.SendAsync<GetListResourcesResponse, GetListResourcesRequest>(
            _apiUrl.SetQueryParam(DelayParamKey, delay?.ToString()).SetQueryParam(PageParamKey, page.ToString()).Get(),
            HttpMethod.Get,
            new GetListResourcesRequest());

        if (result?.Data == null)
        {
            _logger.LogInformation("List of resources hasn't loaded");
            return default!;
        }

        _logger.LogInformation("List of resources has loaded");
        return result.Data;
    }

    public async Task<ResourceDto> GetResource(int id)
    {
        var result = await _httpClientService.SendAsync<GetResourceByIdResponse, GetResourceByIdRequest>(
            _apiUrl.WithPath(id.ToString()).Get(),
            HttpMethod.Get,
            new GetResourceByIdRequest());

        if (result?.Data.Id == null)
        {
            _logger.LogInformation("Resource with id: {Id} hasn't loaded", id);
            return default!;
        }

        _logger.LogInformation("Resource with id: {Id} has loaded", id);
        return result.Data;
    }
}