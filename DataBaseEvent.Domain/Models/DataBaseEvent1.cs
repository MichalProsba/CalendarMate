using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEvent.Domain.Models
{
    // The DataBaseEvent1 containes structure of the event table
    /// <summary>
    /// The <c> DataBaseEvent1 </c> class
    /// </summary>
    public class DataBaseEvent1
    {
        // The Id
        /// <value>Gets and sets the Id value.</value>
        public int Id { get; set; }

        // The Name
        /// <value>Gets and sets the Name value.</value>
        public string Name { get; set; }

        // The Localization
        /// <value>Gets and sets the Localization value.</value>
        public string Localization { get; set; }

        // The Year
        /// <value>Gets and sets the Year value.</value>
        public int Year { get; set; }

        // The Month
        /// <value>Gets and sets the Month value.</value>
        public int Month { get; set; }

        // The Day
        /// <value>Gets and sets the Day value.</value>
        public int Day { get; set; }

        // The Start Time
        /// <value>Gets and sets the Start Time value.</value>
        public string StartTime { get; set; }

        // The Stop Time
        /// <value>Gets and sets the Stop Time value.</value>
        public string StopTime { get; set; }

        // The Remind Time
        /// <value>Gets and sets the Remind Time value.</value>
        public DateTime RemindTime { get; set; }
    }
}
