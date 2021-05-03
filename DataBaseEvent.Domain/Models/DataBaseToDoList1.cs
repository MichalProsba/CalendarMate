using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataBaseEvent.Domain.Models
{
    // The DataBaseToDoList1 containes structure of the to do list table
    /// <summary>
    /// The <c> DataBaseToDoList1 </c> class
    /// </summary>
    public class DataBaseToDoList1
    {
        // The Id
        /// <value>Gets and sets the Id value.</value>
        [Key] public int Id { get; set; }

        // The Name
        /// <value>Gets and sets the Name value.</value>
        [Required] [MaxLength(100)] public string Name { get; set; }

        // The Done status
        /// <value>Gets and sets the Done status value.</value>
        [Required] public bool Done { get; set; }
    }
}
