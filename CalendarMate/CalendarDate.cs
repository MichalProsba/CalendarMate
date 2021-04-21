using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarMate
{
    // The CalendarDate class containes and modifies given date
    /// <summary>
    /// The <c>CalendarDate</c> class.
    /// Containes and modifies given date.
    /// </summary>
    class CalendarDate
    {
        // The date
        /// <value>Gets and sets the date value.</value>
        private DateTime date;

        // The Date
        /// <value>Gets and sets the Date value.</value>
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        // The calendar
        /// <value>Gets and sets the calendar value.</value>
        private Calendar calendar;

        // The Calendar
        /// <value>Gets and sets the Calendar value.</value>
        public Calendar Calendar
        {
            get
            {
                return calendar;
            }
            set
            {
                calendar = value;
            }
        }

        // Parameterless Constructor - the date is set as today date
        /// <summary>
        /// Parameterless Constructor - the date is set as today date.
        /// </summary>
        public CalendarDate()
        {
            this.date = DateTime.Today;
            this.calendar = CultureInfo.InvariantCulture.Calendar;
        }

        // Parameter Constructor - the date is set as date given in argument
        /// <summary>
        /// Parameter Constructor - the date is set as date given in argument.
        /// </summary>
        /// <param name="arg1">DateTime object containing the date.</param>
        public CalendarDate(DateTime arg1)
        {
            this.date = arg1;
            this.calendar = CultureInfo.InvariantCulture.Calendar;
        }

        // Parameter Constructor - the date is set as date given in argument
        /// <summary>
        /// Parameter Constructor - the date is set as date given in argument.
        /// </summary>
        /// <param name="year">Int variable containing the year.</param>
        /// <param name="month">Int variable containing the month.</param>
        /// <param name="day_of_month">Int variable containing the day of month.</param>
        public CalendarDate(int year, int month, int day_of_month)
        {
            this.date = new DateTime(year, month, day_of_month, new GregorianCalendar());
            this.calendar = CultureInfo.InvariantCulture.Calendar;
        }

        // Sets the current date
        /// <summary>
        /// Sets the current date.
        /// </summary>
        public void SetCurrentDate()
        {
            this.date = DateTime.Today;
        }

        // Sets the clicked date to given argument
        /// <summary>
        /// Sets the clicked date to given argument.
        /// </summary>
        /// <param name="day_of_click_month">Int variable containing day of month.</param>
        public void SetClickDate(int day_of_click_month)
        {
            this.date = new DateTime(date.Year, date.Month, day_of_click_month, new GregorianCalendar());
        }

        // Adds years to date
        /// <summary>
        /// Adds years to date.
        /// </summary>
        /// <param name="arg">Int variable containing years.</param>
        public void AddYears (int arg) 
            {
                date = calendar.AddYears(Date, arg);
            }

        // Adds months to date
        /// <summary>
        /// Adds months to date.
        /// </summary>
        /// <param name="arg">Int variable containing months.</param>
        public void AddMonths(int arg)
        {
            this.date = this.calendar.AddMonths(Date, arg);
        }

        // Adds weeks to date
        /// <summary>
        /// Adds weeks to date.
        /// </summary>
        /// <param name="arg">Int variable containing weeks.</param>
        public void AddWeeks(int arg)
        {
            this.date = this.calendar.AddWeeks(Date, arg);
        }

        // Adds days to date
        /// <summary>
        /// Adds days to date.
        /// </summary>
        /// <param name="arg">Int variable containing days.</param>
        public void AddDays(int arg)
        {
            this.date = this.calendar.AddDays(Date, arg);
        }

        // Adds hours to date
        /// <summary>
        /// Adds hours to date.
        /// </summary>
        /// <param name="arg">Int variable containing hours.</param>
        public void AddHours(int arg)
        {
            this.date = this.calendar.AddHours(Date, arg);
        }

        // Adds minutes to date
        /// <summary>
        /// Adds minutes to date.
        /// </summary>
        /// <param name="arg">Int variable containing minutes.</param>
        public void AddMinutes(int arg)
        {
            this.date = this.calendar.AddMinutes(Date, arg);
        }

        // Adds seconds to date
        /// <summary>
        /// Adds seconds to date.
        /// </summary>
        /// <param name="arg">Int variable containing seconds.</param>
        public void AddSeconds(int arg)
        {
            this.date = this.calendar.AddSeconds(Date, arg);
        }

        // Adds milliseconds to date
        /// <summary>
        /// Adds milliseconds to date.
        /// </summary>
        /// <param name="arg">Int variable containing milliseconds.</param>
        public void AddMilliseconds(int arg)
        {
            this.date = this.calendar.AddMilliseconds(Date, arg);
        }

        // Displays the date
        /// <summary>
        /// Displays the date.
        /// </summary>
        public void DisplayDate()
        {
            Console.WriteLine("   Era:          {0}", this.Calendar.GetEra(this.Date));
            Console.WriteLine("   Year:         {0}", this.Calendar.GetYear(this.Date));
            Console.WriteLine("   Month:        {0}", this.Calendar.GetMonth(this.Date));
            Console.WriteLine("   DayOfYear:    {0}", this.Calendar.GetDayOfYear(this.Date));
            Console.WriteLine("   DayOfMonth:   {0}", this.Calendar.GetDayOfMonth(this.Date));
            Console.WriteLine("   DayOfWeek:    {0}", this.Calendar.GetDayOfWeek(this.Date));
            Console.WriteLine("   Hour:         {0}", this.Calendar.GetHour(this.Date));
            Console.WriteLine("   Minute:       {0}", this.Calendar.GetMinute(this.Date));
            Console.WriteLine("   Second:       {0}", this.Calendar.GetSecond(this.Date));
            Console.WriteLine("   Milliseconds: {0}", this.Calendar.GetMilliseconds(this.Date));
            Console.WriteLine();
        }

        // Returns the date of the first day of the month
        /// <summary>
        /// Returns the date of the first day of the month.
        /// </summary>
        /// <returns> The date of the first day of month.</returns>
        public DateTime FirstDayOfCalendarMonth()
        {
            return new DateTime(this.date.Year, this.date.Month, 1);
        }

        // Returns the date of the last day of the month
        /// <summary>
        /// Returns the date of the last day of the month.
        /// </summary>
        /// <returns> The date of the last day of month.</returns>
        public DateTime LastDayOfCalendarMonth()
        {
            return new DateTime(this.date.Year, this.date.Month, this.date.DaysInMonth());
        }

        // Returns the number of days in a month
        /// <summary>
        /// Returns the number of days in a month.
        /// </summary>
        /// <returns> The number of days in a month.</returns>
        public int DaysInCalendarMonth()
        {
            return DateTime.DaysInMonth(this.date.Year, this.date.Month);
        }

        // Returns the day of week of date
        /// <summary>
        /// Returns the day of week of date.
        /// </summary>
        /// <returns> The day of week of date.</returns>
        public int FirstDayOfWeekCalendarMonth()
        {
            DateTime date = FirstDayOfCalendarMonth();
            if ((int)date.DayOfWeek != 0)
                return (int)date.DayOfWeek;
            else
                return 7;
        }
    }
}
