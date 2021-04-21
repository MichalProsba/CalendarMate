using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLocalization.EntityFramework
{
    // The DataBaseEventDbContext containes mechanics create a database contain localization table
    /// <summary>
    /// The <c> DataBaseEventDbContext </c> class
    /// </summary>
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
