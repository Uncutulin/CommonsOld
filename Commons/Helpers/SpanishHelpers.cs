using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Commons.Helpers
{
    public static class SpanishHelpers
    {
        public static string Day(DayOfWeek day)
        {
            var culture = new System.Globalization.CultureInfo("es-ES");
            var text = culture.DateTimeFormat.GetDayName(day);
            return text.First().ToString().ToUpper() + text.Substring(1);
        }
        public static string LongDateString(DateTime date)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            string text = date.ToLongDateString();
            return text.First().ToString().ToUpper() + text.Substring(1);
        }
    }
}
