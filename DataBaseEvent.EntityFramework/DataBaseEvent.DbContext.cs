using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEvent.EntityFramework
{
    public class DataBaseEventDbContext : DbContext
    {
        public DbSet<DataBaseEvent1> DataBaseEvents1 { get; set; }

        public DataBaseEventDbContext() : base("DataBaseAllEvent")
        {
            Database.SetInitializer<DataBaseEventDbContext>(new DropCreateDatabaseIfModelChanges<DataBaseEventDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        }
    }
}
