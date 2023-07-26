using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(AppUserModel user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUserModel> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUserModel user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
