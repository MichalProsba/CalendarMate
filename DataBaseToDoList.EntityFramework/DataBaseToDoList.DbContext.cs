using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseToDoList.EntityFramework
{
    public class DataBaseToDoList : DbContext
    {
        public DbSet<DataBaseToDoList1> DataBaseToDoLists1 { get; set; }
        public DataBaseToDoList() : base("DataBaseAllToDoList")
        {
            Database.SetInitializer<DataBaseToDoList>(new DropCreateDatabaseIfModelChanges<DataBaseToDoList>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        }
    }
}
