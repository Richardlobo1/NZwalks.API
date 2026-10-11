using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepositotory
    {
        public Task<Region?> CreateRegionAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> DeleteRegionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Region>> GetAllAsync()
        {
            return Task.FromResult(new List<Region>()
    {
        new Region { Id = Guid.NewGuid(), Code = "MH", Name = "Maharashtra", RegionImageUrl = "https://example.com/mh.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "GJ", Name = "Gujarat", RegionImageUrl = "https://example.com/gj.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "RJ", Name = "Rajasthan", RegionImageUrl = "https://example.com/rj.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "KA", Name = "Karnataka", RegionImageUrl = "https://example.com/ka.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "TN", Name = "Tamil Nadu", RegionImageUrl = "https://example.com/tn.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "KL", Name = "Kerala", RegionImageUrl = "https://example.com/kl.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "UP", Name = "Uttar Pradesh", RegionImageUrl = "https://example.com/up.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "PB", Name = "Punjab", RegionImageUrl = "https://example.com/pb.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "WB", Name = "West Bengal", RegionImageUrl = "https://example.com/wb.jpg" },
        new Region { Id = Guid.NewGuid(), Code = "DL", Name = "Delhi", RegionImageUrl = "https://example.com/dl.jpg" }
    });
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }

        public Task<RegionDto> UpdateRegionAsync(Guid id, RegionDto updatedRegion)
        {
            throw new NotImplementedException();
        }
    }
}
