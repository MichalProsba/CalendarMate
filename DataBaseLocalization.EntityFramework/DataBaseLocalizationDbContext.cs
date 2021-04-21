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
        // The DataBaseLocalizations1
        /// <value>Gets and sets the DataBaseLocalizations1 value.</value>
        public DbSet<DataBaseLocalization1> DataBaseLocalizations1 { get; set; }

        // Initializes the data base
        /// <summary>
        /// Initializes the data base. 
        /// </summary>
        public DataBaseLocalizationDbContext() : base("DataBaseLocalization")
        {
            Database.SetInitializer<DataBaseLocalizationDbContext>(new DropCreateDatabaseIfModelChanges<DataBaseLocalizationDbContext>());
        }

        // Configures the data base
        /// <summary>
        /// Configures the data base
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder object containes the database configuration.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
        }
    }
}
