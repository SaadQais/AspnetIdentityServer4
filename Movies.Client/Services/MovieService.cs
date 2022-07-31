using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using Newtonsoft.Json;
using System.Text;

namespace Movies.Client.Services
{
    public class MovieService : IMovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesApiClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/movies");

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

            var request = new HttpRequestMessage(HttpMethod.Get, $"/movies/{id}");

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

            var request = new HttpRequestMessage(HttpMethod.Post, "/movies/")
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

            var request = new HttpRequestMessage(HttpMethod.Put, $"/movies/{movie.Id}")
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

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/movies/{id}");

            var response = await httpClient.SendAsync(request)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task<UserInfoViewModel> GetUserInfoAsync()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }

            var accessToken = await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while getting user info");
            }

            var userInfoDictionary = new Dictionary<string, string>();

            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type, claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }
    }
}
