using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseToDoList.EntityFramework
{
    public class DataBaseToDoListDbContext : DbContext
    {
        public DbSet<DataBaseToDoList1> DataBaseToDoLists1 { get; set; }
        public DataBaseToDoListDbContext() : base("DataBaseAllToDoList")
        {
            Database.SetInitializer<DataBaseToDoListDbContext>(new DropCreateDatabaseIfModelChanges<DataBaseToDoListDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        }


    }
}
