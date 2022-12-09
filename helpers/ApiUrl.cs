using System.Web;

namespace Module5HW1.helpers;

public class ApiUrl
{
    private readonly UriBuilder _url;
    private Dictionary<string, string> _queryParams = new ();

    public ApiUrl(string url)
    {
        _url = new UriBuilder(url);
    }

    public ApiUrl WithPath(string path)
    {
        var oldUrl = _url.ToString();
        var newUrl = new UriBuilder(oldUrl);
        newUrl.Path += path;
        return new ApiUrl(newUrl.ToString());
    }

    public ApiUrl SetQueryParam(string key, string? value)
    {
        var queryParams = new Dictionary<string, string>();

        if (value != null)
        {
            queryParams[key] = value;
        }

        return new ApiUrl(Get()) { _queryParams = queryParams };
    }

    public string Get()
    {
        var query = HttpUtility.ParseQueryString(_url.Query);

        foreach (var (key, value) in _queryParams)
        {
            query[key] = value;
        }

        _url.Query = query.ToString();
        return _url.ToString();
    }
}