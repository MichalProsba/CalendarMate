using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        [Key] public int Id { get; set; }

        // The Name
        /// <value>Gets and sets the Name value.</value>
        [Required] [MinLength(1)] [MaxLength(100)] public string Name { get; set; }

        // The Localization
        /// <value>Gets and sets the Localization value.</value>
        [Required] [MinLength(1)] [MaxLength(50)] public string Localization { get; set; }

        // The Year
        /// <value>Gets and sets the Year value.</value>
        [Required] public int Year { get; set; }

        // The Month
        /// <value>Gets and sets the Month value.</value>
        [Required] public int Month { get; set; }

        // The Day
        /// <value>Gets and sets the Day value.</value>
        [Required] public int Day { get; set; }

        // The Start Time
        /// <value>Gets and sets the Start Time value.</value>
        [Required] public string StartTime { get; set; }

        // The Stop Time
        /// <value>Gets and sets the Stop Time value.</value>
        [Required] public string StopTime { get; set; }

        // The Remind Time
        /// <value>Gets and sets the Remind Time value.</value>
        [Required] public DateTime RemindTime { get; set; }
    }
}
