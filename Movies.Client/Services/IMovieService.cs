using Movies.Client.Models;

namespace Movies.Client.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetAllAsync();
        public Task<Movie> GetByIdAsync(int id);
        public Task<Movie> CreateAsync(Movie movie);
        public Task<Movie> UpdateAsync(Movie movie);
        public Task DeleteAsync(int id);
    }
}
