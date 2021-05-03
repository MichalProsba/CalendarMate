using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        private bool Debug = false;
        // Initializes the data base
        /// <summary>
        /// The constructor of the class. 
        /// </summary>
        public DataBaseEventDbContext(bool debug = false)
        {
            Debug = debug;
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Overwritten method delating database.
        /// </summary>
        public override void Dispose()
        {
            Database.CloseConnection();
            base.Dispose();
        }

        /// <summary>
        /// Database configuration method overwritten.
        /// </summary>
        /// <param name="options">Parametry konfiguracyjne bazy danych</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (Debug)
            {
                try
                {
                    options.UseSqlite(@"Data Source=file::memory:?cache=shared").EnableSensitiveDataLogging();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: cannot connect to database");
                }
            }
            else
            {
                try
                {
                    options.UseSqlite(@"Data Source=.\database1.db").EnableSensitiveDataLogging();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: cannot connect to database");
                }
            }
        }
    }
}
