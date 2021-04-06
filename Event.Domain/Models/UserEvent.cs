using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Domain.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Localization { get; set; }
        public DateTime Date { get; set; }
    }
}
