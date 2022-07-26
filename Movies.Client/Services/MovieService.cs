using Movies.Client.Models;
using Newtonsoft.Json;
using System.Text;

namespace Movies.Client.Services
{
    public class MovieService : IMovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public MovieService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesApiClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "api/movies");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movies;

        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesApiClient");

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/movies/{id}");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Movie movie = JsonConvert.DeserializeObject<Movie>(content);

            return movie;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesApiClient");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/movies/")
            {
                Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return movie;
        }

        public async Task<Movie> UpdateAsync(Movie movie)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesApiClient");

            var request = new HttpRequestMessage(HttpMethod.Put, $"api/movies/{movie.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return movie;
        }

        public async Task DeleteAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesApiClient");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/movies/{id}");

            var response = await httpClient.SendAsync(request)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}
