using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarMate
{
    class CalendarDate
    {
        private DateTime date;
       
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

        private Calendar calendar;

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

        /// <summary>
        /// Parameterless Constructor - the date is set at today date
        /// </summary>
        public CalendarDate()
        {
            this.date = DateTime.Today;
            this.calendar = CultureInfo.InvariantCulture.Calendar;
        }
        /// <summary>
        /// Parameter Constructor - the date is set at date given in argument
        /// </summary>
        /// <param name="arg1"></param>
        public CalendarDate(DateTime arg1)
        {
            this.date = arg1;
            this.calendar = CultureInfo.InvariantCulture.Calendar;
        }
        /// <summary>
        /// Parameter Constructor - the date is set at date given in argument
        /// </summary>
        /// <param name="arg1"></param>
        public CalendarDate(int year, int month, int day_of_month)
        {
            this.date = new DateTime(year, month, day_of_month, new GregorianCalendar());
            this.calendar = CultureInfo.InvariantCulture.Calendar;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetCurrentDate()
        {
            this.date = DateTime.Today;
        }

        public void SetClickDate(int day_of_click_month)
        {
            this.date = new DateTime(date.Year, date.Month, day_of_click_month, new GregorianCalendar());
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddYears (int arg) 
            {
                date = calendar.AddYears(Date, arg);
            }

        /// <summary>
        /// 
        /// </summary>
        public void AddMonths(int arg)
        {
            this.date = this.calendar.AddMonths(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddWeeks(int arg)
        {
            this.date = this.calendar.AddWeeks(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddDays(int arg)
        {
            this.date = this.calendar.AddDays(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddHours(int arg)
        {
            this.date = this.calendar.AddHours(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddMinutes(int arg)
        {
            this.date = this.calendar.AddMinutes(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddSeconds(int arg)
        {
            this.date = this.calendar.AddSeconds(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddMilliseconds(int arg)
        {
            this.date = this.calendar.AddMilliseconds(Date, arg);
        }

        /// <summary>
        /// 
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

        /// <summary>
        /// The metod returns the date of the first day of the month
        /// </summary>
        /// <param arg1="value"> any date of the current month </param>
        /// <returns> returns the date of the first day of the month </returns>
        public DateTime FirstDayOfCalendarMonth()
        {
            return new DateTime(this.date.Year, this.date.Month, 1);
        }

        /// <summary>
        /// The metod returns the date of the last day of the month
        /// </summary>
        /// <param arg1="value"> any date of the current month </param>
        /// <returns> returns the date of the last day of the month </returns>
        public DateTime LastDayOfCalendarMonth()
        {
            return new DateTime(this.date.Year, this.date.Month, this.date.DaysInMonth());
        }

        /// <summary>
        /// The metod returns the number of days in a month
        /// </summary>
        /// <param arg1="value"> any date of the current month </param>
        /// <returns> returns the number of days in a month </returns>
        public int DaysInCalendarMonth()
        {
            return DateTime.DaysInMonth(this.date.Year, this.date.Month);
        }

        /// <summary>
        /// The metod returns the day of week of given date
        /// </summary>
        /// <param arg1="value">any date</param>
        /// <returns>returns the day of week of given date</returns>
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
