using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class StringToDateExtention
    {
        public static DateTime ToDateTime(this string str)
        {
            var dt = new DateTime();
            if (!DateTime.TryParse(str, out dt))
            {
                dt = new DateTime(0);
            }
            return dt;
        }

        public static DateTime? ToDateTimeNullable(this string str)
        {
            DateTime dt;
            if (!DateTime.TryParse(str, out dt))
            {
                return null;
            }
            return dt;
        }
    }
}
