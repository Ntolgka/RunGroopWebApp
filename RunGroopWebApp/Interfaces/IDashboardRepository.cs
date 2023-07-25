using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<RaceModel>> GetAllUserRaces();
        Task<List<ClubModel>> GetAllUserClubs();
    }
}
