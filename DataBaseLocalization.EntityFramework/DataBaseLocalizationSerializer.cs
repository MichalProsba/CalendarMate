using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLocalization.EntityFramework
{
    //Serializer data base localization 
    /// <summary>
    /// Serializer data base localization 
    /// </summary>
    public class DataBaseLocalizationSerializer : DataBaseLocalizationDbContext
    {

        // The method that save simple localization
        /// <summary>
        /// The method that save simple localization
        /// </summary>
        /// <param name="localization"> the object we are adding to database </param>
        public void SaveLocalization(string localization)
        {
            var r = from d in this.DataBaseLocalizations1
                    where d.Id == 1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Localization = localization;
            }
            this.SaveChanges();
        }

        // Get data from database
        /// <summary>
        /// Get data from database
        /// </summary>
        /// <returns> return localization </returns>
        public string GetLocalization()
        {
            var r = from d in this.DataBaseLocalizations1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                return obj.Localization.ToString();
            }
            return "Empty database";
        }
    }
}


