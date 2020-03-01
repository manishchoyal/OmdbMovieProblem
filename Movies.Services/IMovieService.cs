using Movies.Services.Model;

namespace Movies.Services
{
    public interface IMovieService
    {
        Movie GetItemByTitle(string title, int? year);
    }
}