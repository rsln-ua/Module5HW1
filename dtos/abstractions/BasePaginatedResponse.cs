using Newtonsoft.Json;

namespace Module5HW1.dtos.abstractions;

public class BasePaginatedResponse<TData> : BaseDataResponse<TData>
{
    public int Page { get; set; }

    [JsonProperty(PropertyName = "per_page")]
    public int PerPage { get; set; }

    public int Total { get; set; }

    [JsonProperty(PropertyName = "total_pages")]
    public int TotalPages { get; set; }
}