
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepositotory
    {
        Task<List<Region>> GetAllAsync();
        Task <Region?> GetByIdAsync(Guid id);
        Task <Region?> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id,Region region);
        Task<Region?> DeleteRegionAsync(Guid id);
        Task<RegionDto> UpdateRegionAsync(Guid id, RegionDto updatedRegion);
    }

}
