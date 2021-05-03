using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataBaseEvent.Domain.Models
{
    // The DataBaseLocalization1 containes structure of the localization table
    /// <summary>
    /// The <c> DataBaseLocalization1 </c> class
    /// </summary>
    public class DataBaseLocalization1
    {
        // The Id
        /// <value>Gets and sets the Id value.</value>
        [Key] public int Id { get; set; }

        // The Localization
        /// <value>Gets and sets the Localization value.</value>
        [Required] [MaxLength(50)] public string Localization { get; set; }
    }
}
