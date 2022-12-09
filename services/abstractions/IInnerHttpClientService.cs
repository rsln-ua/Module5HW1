namespace Module5HW1.services.abstractions;

public interface IInnerHttpClientService
{
    Task<TResponse?> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content = null)
        where TRequest : class;
}