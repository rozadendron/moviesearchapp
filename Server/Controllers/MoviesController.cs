using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MovieSearchApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private static readonly List<string> _latestQueries = new();
        private readonly IHttpClientFactory _httpClientFactory;
        private const string OmdbApiKey = "5bb25fab";
        private const string OmdbSearchUrlTemplate = "https://www.omdbapi.com/?apikey=";

        public MoviesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title is required.");

            // Save latest queries (max 5)
            if (!_latestQueries.Contains(title)) {
                _latestQueries.Insert(0, title);
                if (_latestQueries.Count > 5)
                    _latestQueries.RemoveAt(5);
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{OmdbSearchUrlTemplate}{OmdbApiKey}&s={title}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet("Latest")]
        public IActionResult Latest()
        {
            return Ok(_latestQueries);
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
                return BadRequest("imdbId is required.");

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{OmdbSearchUrlTemplate}{OmdbApiKey}&i={imdbId}&plot=full");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
