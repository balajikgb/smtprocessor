using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArasPLMWebAp.Extentions
{
    public static class DateTimeFormatExtention
    {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double? ToJavascriptMs(this DateTime? date)
        {
            if (date == null) { return 0; }
            TimeSpan duration = date.Value.Subtract(UnixEpoch);
            return duration.TotalMilliseconds;
        }
        public static string InterpretateDateTime(this DateTime? date)
        {
            return date.HasValue ? date.Value.InterpretateDateTime() : "";
        }

        public static string InterpretateDateTime(this DateTime date)
        {
            if (date < new DateTime(1970, 12, 31))
            {
                return "";
            }
            return date.ToString("MMM d, yyyy");
        }
    }
}
