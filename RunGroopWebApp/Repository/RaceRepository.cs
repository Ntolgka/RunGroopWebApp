using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class RaceRepository: IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public bool Create(RaceModel race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(RaceModel race)
        {
            _context.Remove(race);
            return Save();
        }

        public bool Update(RaceModel race)
        {
            _context.Update(race);
            return Save();
        }

        public async Task<IEnumerable<RaceModel>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<RaceModel>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<RaceModel> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<RaceModel> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Exists(string title, string description)
        {
            return _context.Races.Any(r => r.Title == title && r.Description == description);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
