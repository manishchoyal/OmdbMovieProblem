using Movies.Service.Models;
using System.Threading.Tasks;

namespace Movies.Service
{
    public interface IMovieService
    {
        Movie GetItemByTitle(string title, int? year);
        Task<MovieList> GetSearchListAsync(string query, int page = 1);
    }
}