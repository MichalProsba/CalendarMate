using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
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

        private bool Debug = false;
        // Initializes the data base
        /// <summary>
        /// Initializes the data base. 
        /// </summary>

        public DataBaseLocalizationDbContext(bool debug = false)
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
                catch(Exception e)
                {
                    Console.WriteLine("Error: cannot connect to database");
                }
                
            }
            else
            {
                try
                {
                    options.UseSqlite(@"Data Source=.\database2.db").EnableSensitiveDataLogging();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: cannot connect to database");
                }
            }
        }
    }
}
