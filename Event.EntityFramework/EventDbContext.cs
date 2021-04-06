using Microsoft.EntityFrameworkCore;
using Event.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.EntityFramework
{
    public class EventDbContext : DbContext
    {
        public DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EventDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }


    }
}
