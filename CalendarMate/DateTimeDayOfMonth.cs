using System;

namespace CalendarMate
{
    /// <summary>
    /// Static class 
    /// </summary>
    public static class DateTimeDayOfMonth
    {
        /// <summary>
        /// The metod returns the date of the first day of the month
        /// </summary>
        /// <param arg1="value"> any data of the current month </param>
        /// <returns> returns the date of the first day of the month </returns>
        public static DateTime FirstDayOfMonth(this DateTime arg1)
        {
            return new DateTime(arg1.Year, arg1.Month, 1);
        }
        /// <summary>
        /// The metod returns the date of the last day of the month
        /// </summary>
        /// <param arg1="value"> any data of the current month </param>
        /// <returns> returns the date of the last day of the month </returns>
        public static DateTime LastDayOfMonth(this DateTime arg1)
        {
            return new DateTime(arg1.Year, arg1.Month, arg1.DaysInMonth());
        }
        /// <summary>
        /// The metod returns the number of days in a month
        /// </summary>
        /// <param arg1="value"> any data of the current month </param>
        /// <returns> returns the number of days in a month </returns>
        public static int DaysInMonth(this DateTime arg1)
        {
            return DateTime.DaysInMonth(arg1.Year, arg1.Month);
        }
    }
}