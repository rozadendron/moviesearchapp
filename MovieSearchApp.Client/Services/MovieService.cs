using MovieSearchApp.Shared;
using System.Net.Http.Json;
namespace MovieSearchApp.Client.Services
{
    public class MovieService
    {
        private readonly HttpClient _http;

        public MovieService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetLatestQueriesAsync()
            => await _http.GetFromJsonAsync<List<string>>("api/movies/Latest");

        public async Task<OmdbSearchResult> SearchAsync(string title)
            => await _http.GetFromJsonAsync<OmdbSearchResult>($"api/movies/search?title={title}");

        public async Task<OmdbMovieDetails> GetDetailsAsync(string imdbId)
            => await _http.GetFromJsonAsync<OmdbMovieDetails>($"api/movies/details?imdbId={imdbId}");
    }    
}
