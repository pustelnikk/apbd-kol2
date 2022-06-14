using kol2b.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace kol2b.Services
{
    public class MusicService : IMusicService
    {
        private readonly MusicDbContext _context;
        public MusicService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task DeleteMusician(int IdMusican)
        {
            var toBeDeleted = new Musician
            {
                IdMusician = IdMusican,
            };

            var entry = _context.Entry(toBeDeleted);
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public IQueryable<Musician> GetMusician(int IdMusican)
        {
            return _context.Musician
                .Where(e => e.IdMusician == IdMusican)
                .Include(e => e.MusicianTrack)
                .ThenInclude(e => e.Track);
        }

        public IQueryable<Musician> DoesMusicianExist(int IdMusican)
        {
            return _context.Musician
                .Where(e => e.IdMusician == IdMusican);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
