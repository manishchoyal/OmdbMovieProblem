using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Movies.Service.Utilities
{
    public class QueryBuilder
    {
		public static string GetMoviesByTitleQuery(string title, int? year)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
			}

			var editedTitle = Regex.Replace(title, @"\s+", "+");

			var query = $"&s={editedTitle}";

			if (year != null)
			{
				if (year > 1800)
				{
					query += $"&y={year}";
				}
				else
				{
					throw new ArgumentOutOfRangeException("Year can not be less than 1800.", nameof(year));
				}
			}

			return query;
		}
		public static string GetMovieListQuery(int? year, string query, int page)
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				throw new ArgumentException("Query can not be null or empty.", nameof(query));
			}

			if (page <= 0)
			{
				throw new ArgumentOutOfRangeException("Page can not be zero.", nameof(page));
			}

			var newQuery = $"&s={Regex.Replace(query, @"\s+", "+")}&page={page}";


			if (year != null)
			{
				if (year > 1800)
				{
					newQuery += $"&y={year}";
				}
				else
				{
					throw new ArgumentOutOfRangeException("No movies before 1800.", nameof(year));
				}
			}

			return newQuery;
		}
	}
}
