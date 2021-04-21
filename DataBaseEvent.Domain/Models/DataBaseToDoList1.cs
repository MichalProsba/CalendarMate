using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Id { get; set; }

        // The Name
        /// <value>Gets and sets the Name value.</value>
        public string Name { get; set; }

        // The Done status
        /// <value>Gets and sets the Done status value.</value>
        public bool Done { get; set; }
    }
}
