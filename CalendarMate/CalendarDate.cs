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
        public void AddYears (int arg) 
            {
                date = calendar.AddYears(Date, arg);
            }

        /// <summary>
        /// 
        /// </summary>
        public void AddMonths(int arg)
        {
            date = calendar.AddMonths(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddWeeks(int arg)
        {
            date = calendar.AddWeeks(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddDays(int arg)
        {
            date = calendar.AddDays(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddHours(int arg)
        {
            date = calendar.AddHours(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddMinutes(int arg)
        {
            date = calendar.AddMinutes(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddSeconds(int arg)
        {
            date = calendar.AddSeconds(Date, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddMilliseconds(int arg)
        {
            date = calendar.AddMilliseconds(Date, arg);
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
    }
}
