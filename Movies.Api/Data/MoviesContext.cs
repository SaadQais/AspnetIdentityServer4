using Microsoft.EntityFrameworkCore;
using Movies.Api.Models;

namespace Movies.Api.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
