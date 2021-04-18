using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLocalization.EntityFramework
{
    public class DataBaseLocalizationDbContext : DbContext
    {
        public DbSet<DataBaseLocalization1> DataBaseLocalizations1 { get; set; }
        public DataBaseLocalizationDbContext() : base("DataBaseLocalization")
        {
            Database.SetInitializer<DataBaseLocalizationDbContext>(new DropCreateDatabaseIfModelChanges<DataBaseLocalizationDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        }
    }
}
