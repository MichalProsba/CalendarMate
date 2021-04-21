using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseToDoList.EntityFramework
{
    // The DataBaseEventDbContext containes mechanics create a database contain to do list table
    /// <summary>
    /// The <c> DataBaseEventDbContext </c> class
    /// </summary>
    public class DataBaseToDoListDbContext : DbContext
    {
        // The DataBaseToDoLists1
        /// <value>Gets and sets the DataBaseToDoLists1 value.</value>
        public DbSet<DataBaseToDoList1> DataBaseToDoLists1 { get; set; }

        // Initializes the data base
        /// <summary>
        /// Initializes the data base. 
        /// </summary>
        public DataBaseToDoListDbContext() : base("DataBaseAllToDoList")
        {
            Database.SetInitializer<DataBaseToDoListDbContext>(new DropCreateDatabaseIfModelChanges<DataBaseToDoListDbContext>());
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
