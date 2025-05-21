using System.Globalization;

namespace SetmoreSharp
{
    public static class StringExt
    {
        /// <summary>
        /// Parses strings like "05.30", "5:30", "05:30:00" into a TimeSpan.
        /// Throws if the format isn’t recognised.
        /// </summary>
        public static TimeSpan? ToTimeSpan(this string input)
        {
            if (!input.HasValue())
            {
                //throw new ArgumentException("Cannot parse empty string as TimeSpan.", nameof(input));

                return null;
            }

            // normalize “05.30” → “05:30”
            var normalized = input.Trim().Replace('.', ':');

            var ampmFormats = new[] { "h:mm tt", "hh:mm tt", "h:mm:ss tt", "hh:mm:ss tt" };

            if (DateTime.TryParseExact(normalized, ampmFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                return dt.TimeOfDay;
            }

            // try hh:mm[:ss] exact formats first
            var tsFormats = new[] { "h\\:mm", "hh\\:mm", "h\\:mm\\:ss", "hh\\:mm\\:ss" };
            if (TimeSpan.TryParseExact(normalized, tsFormats, CultureInfo.InvariantCulture, out var ts))
            {
                return ts;
            }

            // fallback to general parse (e.g. "1.02:03" etc)
            return TimeSpan.Parse(normalized, CultureInfo.InvariantCulture);
        }
    }
}