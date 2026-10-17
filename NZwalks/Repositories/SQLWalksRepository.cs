using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class SQLWalksRepository : IwalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalksRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
         
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include(w => w.Region).Include(w => w.Difficulty).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                 .Include(w => w.Region)
                 .Include(w => w.Difficulty)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingwalk == null)
            {
                return null;
            }
            existingwalk.Name = walk.Name;
            existingwalk.Description = walk.Description;
            existingwalk.DifficultyID = walk.DifficultyID;
            existingwalk.RegionID = walk.RegionID;
            existingwalk.LengthInKm = walk.LengthInKm;
            existingwalk.WalkImageUrl=walk.WalkImageUrl;

            await dbContext.SaveChangesAsync();
            return existingwalk;

        }


        public async Task<Walk> DeleteAsync(Guid Id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (walk == null)
            {
                return null;
            }
        dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }
    }
}
