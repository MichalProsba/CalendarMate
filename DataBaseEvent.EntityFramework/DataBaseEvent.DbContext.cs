using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEvent.EntityFramework
{
    // The DataBaseEventDbContext containes mechanics create a database contain event table
    /// <summary>
    /// The <c> DataBaseEventDbContext </c> class
    /// </summary>
    public class DataBaseEventDbContext : DbContext
    {
        // The DataBaseEvents1
        /// <value>Gets and sets the DataBaseEvents1 value.</value>
        public DbSet<DataBaseEvent1> DataBaseEvents1 { get; set; }

        // Initializes the data base
        /// <summary>
        /// Initializes the data base. 
        /// </summary>
        public DataBaseEventDbContext() : base("DataBaseAllEvent")
        {
            Database.SetInitializer<DataBaseEventDbContext>(new DropCreateDatabaseIfModelChanges<DataBaseEventDbContext>());
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
