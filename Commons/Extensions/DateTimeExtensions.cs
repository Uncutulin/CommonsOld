using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        public static DateTime FirstDayOfYear(this DateTime dt, DayOfWeek dow)
        {
            return dt.FirstDayOfYear().NextDay(dow, true);
        }

        public static DateTime LastDayOfYear(this DateTime dt)
        {
            return dt.FirstDayOfYear().AddYears(1).AddDays(-1);
        }

        public static DateTime LastDayOfYear(this DateTime dt, DayOfWeek dow)
        {
            return dt.LastDayOfYear().PreviousDay(dow, true);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt, DayOfWeek dow)
        {
            return dt.FirstDayOfMonth().NextDay(dow, true);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt, DayOfWeek dow)
        {
            return dt.LastDayOfMonth().PreviousDay(dow, true);
        }

        public static DateTime PreviousDay(this DateTime dt)
        {
            return dt.Date.AddDays(-1);
        }

        public static DateTime PreviousDay(this DateTime dt, DayOfWeek dow)
        {
            return dt.PreviousDay(dow, false);
        }

        public static DateTime PreviousDay(this DateTime dt, DayOfWeek dow, bool includeThis)
        {
            int diff = dt.DayOfWeek - dow;
            if ((includeThis && diff < 0) || (!includeThis && diff <= 0)) diff += 7;
            return dt.Date.AddDays(-diff);
        }

        public static DateTime NextDay(this DateTime dt)
        {
            return dt.Date.AddDays(1);
        }

        public static DateTime NextDay(this DateTime dt, DayOfWeek dow)
        {
            return dt.NextDay(dow, false);
        }

        public static DateTime NextDay(this DateTime dt, DayOfWeek dow, bool includeThis)
        {
            int diff = dow - dt.DayOfWeek;
            if ((includeThis && diff < 0) || (!includeThis && diff <= 0)) diff += 7;
            return dt.Date.AddDays(diff);
        }

        public static int DaysInYear(this DateTime dt)
        {
            return (dt.LastDayOfYear() - dt.FirstDayOfYear()).Days + 1;
        }

        public static int DaysInYear(this DateTime dt, DayOfWeek dow)
        {
            return (dt.LastDayOfYear(dow).DayOfYear - dt.FirstDayOfYear(dow).DayOfYear) / 7 + 1;
        }

        public static int DaysInMonth(this DateTime dt)
        {
            return (dt.LastDayOfMonth() - dt.FirstDayOfMonth()).Days + 1;
        }

        public static int DaysInMonth(this DateTime dt, DayOfWeek dow)
        {
            return (dt.LastDayOfMonth(dow).Day - dt.FirstDayOfMonth(dow).Day) / 7 + 1;
        }

        public static bool IsLeapYear(this DateTime dt)
        {
            return dt.DaysInYear() == 366;
        }

        public static DateTime AddWeeks(this DateTime dt, int weeks)
        {
            return dt.AddDays(7 * weeks);
        }

        public static string DayInSpanish(this DateTime dt)
        {
            var culture = new System.Globalization.CultureInfo("es-ES");
            var text = culture.DateTimeFormat.GetDayName(dt.DayOfWeek);
            return text.First().ToString().ToUpper() + text.Substring(1);
        }
    }
}
