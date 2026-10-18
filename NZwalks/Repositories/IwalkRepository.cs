using NZwalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZwalks.API.Repositories
{
    public interface IwalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn=null,string?filterQuery=null,string?sortBy=null,bool?isAccending=true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid Id);
        Task<Walk?> UpdateAsync(Guid Id, Walk walk);
        Task<Walk> DeleteAsync(Guid Id);

    }
}
