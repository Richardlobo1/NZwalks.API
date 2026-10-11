using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Repositories
{
    public class SQLRegionRepository:IRegionRepositotory
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext) { this.dbContext = dbContext; }

        public async Task<Region?> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
           // var region = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RegionDto?> UpdateRegionAsync(Guid id, RegionDto region)
        {
            var existingregion= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingregion==null)
                return null;

            existingregion.Code=region.Code;
            existingregion.Name=region.Name;
            existingregion.RegionImageUrl=region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return new RegionDto
            {
                Id = existingregion.Id,
                Code = existingregion.Code,
                Name = existingregion.Name,
                RegionImageUrl = existingregion.RegionImageUrl
            };

        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingregion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingregion == null)
                return null;

            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingregion;
        }
    }
}
