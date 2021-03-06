﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Movies.Services.Model;
using Movies.Services.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private const string BaseUrl = "http://www.omdbapi.com/?";
        private readonly string _apikey;
        private readonly bool _rottenTomatoesRatings;

        public Movie GetItemByTitle(string title, int? year)
        {
            var query = QueryBuilder.GetMoviesByTitleQuery(title, year);

            var movie = GetMoviesAsync<Movie>(query).Result;

            if (movie.Response.Equals("False"))
            {
                throw new HttpRequestException(movie.Error);
            }

            return movie;
        }

        private async Task<T> GetMoviesAsync<T>(string query)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client
                    .GetAsync($"{BaseUrl}apikey={_apikey}{query}&tomatoes={_rottenTomatoesRatings}")
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Error = delegate (object sender, ErrorEventArgs args)
                    {
                        var currentError = args.ErrorContext.Error.Message;
                        args.ErrorContext.Handled = true;
                    }
                });
            }
        }
    }
}
