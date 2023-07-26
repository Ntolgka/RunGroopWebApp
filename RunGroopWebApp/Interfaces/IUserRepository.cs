using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUserModel>> GetAllUsers();
        Task<AppUserModel> GetUserById(string id);
        bool Add(AppUserModel user);
        bool Update(AppUserModel user);
        bool Delete(AppUserModel user);
        bool Save();
    }
}
