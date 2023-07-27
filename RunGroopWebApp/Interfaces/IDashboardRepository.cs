using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<RaceModel>> GetAllUserRaces();
        Task<List<ClubModel>> GetAllUserClubs();
        Task<AppUserModel> GetUserById(string id);
        Task<AppUserModel> GetByIdNoTracking(string id);
        bool Update(AppUserModel user);
        bool Save();
    }
}
