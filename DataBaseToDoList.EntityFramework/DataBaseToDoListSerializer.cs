using DataBaseEvent.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseToDoList.EntityFramework
{
    public class DataBaseToDoListSerializer: DataBaseToDoListDbContext
    {

        // The method that save simple todolist
        /// <summary>
        /// The method that save simple todolist
        /// </summary>
        /// <param name="todolist"> the object we are adding to database </param>
        public void SaveToDoList(DataBaseToDoList1 todolist)
        {
            this.DataBaseToDoLists1.Add(todolist);
            this.SaveChanges();
        }

        // The method that save changes simple todolist
        /// <summary>
        /// The method that save changes simple todolist
        /// </summary>
        /// <param name="obj"> the object we are changing to </param>
        /// <param name="id"> the id of the object being changed</param>
        public void SaveChangesToDoList(DataBaseToDoList1 todolist, int id)
        {
            var r = from d in this.DataBaseToDoLists1
                    where d.Id == id
                    select d;
            DataBaseToDoList1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Name = todolist.Name;
                obj.Done = todolist.Done;
            }
            this.SaveChanges();
        }

        // The method that delete simple todolist
        /// <summary>
        /// The method that delete simple todolist
        /// </summary>
        /// <param name="id"> the id of the object being deleted </param>
        public void DeleteToDoList(int id)
        {
            var r = from d in this.DataBaseToDoLists1
                    where d.Id == id
                    select d;
                DataBaseToDoList1 obj = r.SingleOrDefault();
                if (obj != null)
                {
                    this.DataBaseToDoLists1.Remove(obj);
                    this.SaveChanges();
                }
        }

    }
}
