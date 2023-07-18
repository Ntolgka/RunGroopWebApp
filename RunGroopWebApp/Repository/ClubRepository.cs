using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(ClubModel club)
        {
            _context.Add(club);
            return Save();
        }

        public bool Delete(ClubModel club)
        {
            _context.Remove(club);
            return Save();
        }

        public bool Update(ClubModel club)
        {
            _context.Update(club);
            return Save();
        }

        public async Task<IEnumerable<ClubModel>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<ClubModel> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<ClubModel>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
