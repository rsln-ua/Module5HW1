using System.Text;
using System.Text.Json.Serialization;
using Module5HW1.services.abstractions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Module5HW1.services;

public class InnerHttpClientService : IInnerHttpClientService
{
    private readonly IHttpClientFactory _clientFactory;

    public InnerHttpClientService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<TResponse?> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content = default)
        where TRequest : class
    {
        var client = _clientFactory.CreateClient();

        var httpMessage = new HttpRequestMessage();
        httpMessage.RequestUri = new Uri(url);
        httpMessage.Method = method;

        if (content != null)
        {
            httpMessage.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        var result = await client.SendAsync(httpMessage);

        if (!result.IsSuccessStatusCode)
        {
            return default;
        }

        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<TResponse>(resultContent);

        return response;
    }
}