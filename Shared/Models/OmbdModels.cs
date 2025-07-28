namespace MovieSearchApp.Client.Models
{
    public class OmdbSearchResult
    {
        public List<OmdbMovie> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

    public class OmdbMovie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }

    public class OmdbMovieDetails : OmdbMovie
    {
        public string Plot { get; set; }
        public string imdbRating { get; set; }
        // Add other fields as needed
    }       
}
