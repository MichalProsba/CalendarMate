using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseEvent.EntityFramework
{
    // Serializer class
    /// <summary>
    /// Serializer class
    /// </summary>
    public class DataBaseEventSerializer: DataBaseEventDbContext
    {
        // The method that save simple event
        /// <summary>
        /// The method that save simple event
        /// </summary>
        /// <param name="obj"> the object we are adding to database </param>
        public void SaveEvent(DataBaseEvent1 obj)
        {
            this.DataBaseEvents1.Add(obj);
            this.SaveChanges();
        }

        // The method that save changes simple event
        /// <summary>
        /// The method that save changes simple event
        /// </summary>
        /// <param name="obj"> the object we are changing to </param>
        /// <param name="id"> the id of the object being changed</param>
        public void SaveChangesEvent(DataBaseEvent1 obj, int id)
        { 
            var r = from d in this.DataBaseEvents1
                    where d.Id == id
                    select d;
            DataBaseEvent1 db_obj = r.SingleOrDefault();
            if (obj != null)
            {
                db_obj.Name = obj.Name;
                db_obj.Localization = obj.Localization;
                db_obj.Year = obj.Year;
                db_obj.Month = obj.Month;
                db_obj.Day = obj.Day;
                db_obj.StartTime = obj.StartTime;
                db_obj.StopTime = obj.StopTime;
                db_obj.RemindTime = obj.RemindTime;
            }
            this.SaveChanges();
        }

        // The method that delete simple event
        /// <summary>
        /// The method that delete simple event
        /// </summary>
        /// <param name="id"> the id of the object being deleted </param>
        public void DeleteEvent(int id)
        {
            var r = from d in this.DataBaseEvents1
                    where d.Id == id
                    select d;
            DataBaseEvent1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                this.DataBaseEvents1.Remove(obj);
                this.SaveChanges();
            }
        }
    }
}
