using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Movies.Services.Utilities
{
    public static class QueryBuilder
    {
		public static string GetMoviesByTitleQuery(string title, int? year)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
			}

			var editedTitle = Regex.Replace(title, @"\s+", "+");
			
			var query = $"&t={editedTitle}";

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
	}
}
