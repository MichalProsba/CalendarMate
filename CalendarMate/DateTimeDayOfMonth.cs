using System;

namespace CalendarMate
{
    // The DateTimeDayOfMonth class containes and modifies given date
    /// <summary>
    /// The <c>DateTimeDayOfMonth</c> class.
    /// Containes and modifies given date.
    /// </summary>
    public static class DateTimeDayOfMonth
    {
        // Returns the date of the first day of the month.
        /// <summary>
        /// Returns the date of the first day of the month.
        /// </summary>
        /// <param arg1="value">DateTime object containing any date of the current month.</param>
        /// <returns> The date of the first day of the month.</returns>
        public static DateTime FirstDayOfMonth(this DateTime arg1)
        {
            return new DateTime(arg1.Year, arg1.Month, 1);
        }

        // Returns the date of the last day of the month
        /// <summary>
        /// Returns the date of the last day of the month.
        /// </summary>
        /// <param arg1="value">DateTime object containing any date of the current month.</param>
        /// <returns>The date of the last day of the month.</returns>
        public static DateTime LastDayOfMonth(this DateTime arg1)
        {
            return new DateTime(arg1.Year, arg1.Month, arg1.DaysInMonth());
        }

        // Returns the number of days in a month.
        /// <summary>
        /// Returns the number of days in a month.
        /// </summary>
        /// <param arg1="value">DateTime object containing any date of the current month.</param>
        /// <returns>The number of days in a month.</returns>
        public static int DaysInMonth(this DateTime arg1)
        {
            return DateTime.DaysInMonth(arg1.Year, arg1.Month);
        }

        // Returns the day of week of given date
        /// <summary>
        /// Returns the day of week of given date.
        /// </summary>
        /// <param arg1="value">DateTime object containing any date.</param>
        /// <returns>The day of week of given date.</returns>
        public static int DayOfWeekOfGivenDate(this DateTime arg1)
        {
            DateTime dt = new DateTime(arg1.Year, arg1.Month, arg1.Day);
            return (int)dt.DayOfWeek;
        }

        // Returns the next month of given date
        /// <summary>
        /// Returns the next month of given date.
        /// </summary>
        /// <param name="current_date">DateTime object containing any date.</param>
        /// <returns>The next month of given date.</returns>
        public static DateTime NextMonth(DateTime current_date)
        {
            int year = current_date.Year;
            int month = current_date.Month;
            int day = current_date.Day;
            if(current_date.Month == 12)
            {
                year += 1;
                month = 1;
            }
            else
            {
                month += 1;
            }

            return new DateTime(year, month, day);
        }

        // Returns the previous month of given date
        /// <summary>
        /// Returns the previous month of given date.
        /// </summary>
        /// <param name="current_date">DateTime object containing any date.</param>
        /// <returns>The previous month of given date.</returns>
        public static DateTime PreviousMonth(DateTime current_date)
        {
            int year = current_date.Year;
            int month = current_date.Month;
            int day = current_date.Day;
            if (current_date.Month == 1)
            {
                year -= 1;
                month = 12;
            }
            else
            {
                month -= 1;
            }

            return new DateTime(year, month, day);
        }
    }
}