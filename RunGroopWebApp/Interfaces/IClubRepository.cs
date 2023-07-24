using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<ClubModel>> GetAll();
        Task<ClubModel> GetByIdAsync(int id);
        Task<ClubModel> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<ClubModel>> GetClubByCity(string city);
        bool Create(ClubModel club);
        bool Delete(ClubModel club);
        bool Update(ClubModel club);
        bool Save();
    }
} 
