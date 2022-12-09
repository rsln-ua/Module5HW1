using Module5HW1.dtos;

namespace Module5HW1.services.abstractions;

public interface IResourceService
{
    Task<List<ResourceDto>> GetListResources(int page, int? delay = null);
    Task<ResourceDto> GetResource(int id);
}