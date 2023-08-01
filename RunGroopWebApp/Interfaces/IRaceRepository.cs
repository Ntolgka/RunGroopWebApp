using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<RaceModel>> GetAll();
        Task<RaceModel> GetByIdAsync(int id);
        Task<RaceModel> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<RaceModel>> GetAllRacesByCity(string city);
        bool Exists(string title, string description);
        bool Create(RaceModel race);
        bool Delete(RaceModel race);
        bool Update(RaceModel race);
        bool Save();
    }
}

