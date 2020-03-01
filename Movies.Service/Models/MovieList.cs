using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Service.Models
{
    public class MovieList
    {
        [JsonProperty("Search")]
        public List<MovieItem> MovieResults { get; set; }

        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }

        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("Error")]
        public string Error { get; set; }
    }

    public class MovieItem
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Poster")]
        public string Poster { get; set; }
    }
}
