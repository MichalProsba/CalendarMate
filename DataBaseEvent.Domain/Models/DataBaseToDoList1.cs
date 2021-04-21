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
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
    }
}
