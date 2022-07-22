using Movies.Client.Models;

namespace Movies.Client.Services
{
    public class MovieService : IMovieService
    {
        public MovieService()
        {

        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> CreateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
