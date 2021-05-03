using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        private bool Debug = false;
        // The DataBaseToDoLists1
        /// <value>Gets and sets the DataBaseToDoLists1 value.</value>
        public DbSet<DataBaseToDoList1> DataBaseToDoLists1 { get; set; }

        // Initializes the data base
        /// <summary>
        /// Initializes the data base. 
        /// </summary>

        public DataBaseToDoListDbContext(bool debug = false)
        {
            Debug = debug;
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        public override void Dispose()
        {
            Database.CloseConnection();
            base.Dispose();
        }

        /// <summary>
        /// Nadpisana metoda konfiguracji bazy danych. 
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
                catch (Exception e)
                {
                    Console.WriteLine("Error: cannot connect to database");
                }
            }
            else
            {
                try
                {
                    options.UseSqlite(@"Data Source=.\database3.db").EnableSensitiveDataLogging();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: cannot connect to database");
                }
            }
        }
    }
}
