using NZwalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZwalks.API.Repositories
{
    public interface IwalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid Id);
        Task<Walk?> UpdateAsync(Guid Id, Walk walk);
        Task<Walk> DeleteAsync(Guid Id);

    }
}
