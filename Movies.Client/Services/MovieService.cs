using Movies.Client.Models;
using Newtonsoft.Json;

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

            //var apiCredentials = new ClientCredentialsTokenRequest
            //{
            //    Address = "https://localhost:5005/connect/token",
            //    ClientId = "MoviesClient",
            //    ClientSecret = "secret",
            //    Scope = "MoviesApi"
            //};

            //var client = new HttpClient();

            //var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:5005");

            //if (discovery.IsError)
            //    return null;

            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiCredentials);

            //if (tokenResponse.IsError)
            //    return null;

            //var apiClient = new HttpClient();

            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var response = await apiClient.GetAsync("https://localhost:5001/api/Movies");
            //response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();

            //List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);

            //return movies;
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
