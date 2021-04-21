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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Localization { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public DateTime RemindTime { get; set; }
    }
}
