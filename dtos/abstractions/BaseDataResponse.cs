namespace Module5HW1.dtos.abstractions;

public class BaseDataResponse<TData>
{
    public TData Data { get; set; } = default!;
    public SupportDto Support { get; set; } = null!;
}