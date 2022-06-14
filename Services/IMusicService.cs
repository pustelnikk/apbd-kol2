using kol2b.Models;
using System.Linq;
using System.Threading.Tasks;

namespace kol2b.Services
{
    public interface IMusicService
    {
        IQueryable<Musician> GetMusician(int IdMusican);
        IQueryable<Musician> DoesMusicianExist(int IdMusican);

        Task SaveChangesAsync();

        Task DeleteMusician(int IdMusican);
    }
}
