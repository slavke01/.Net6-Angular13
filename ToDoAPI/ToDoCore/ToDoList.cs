using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCore
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Position { get; set; }

        public bool Remind { get; set; }
        public DateTime RemindAfter { get; set; }
        public string RemindEmail { get; set; }
        public bool Opened { get; set; }
        public string Owner { get; set; }
        //Navigacioni
        public ICollection<ToDoListItem> ListItems { get; set; }

        public ToDoList() { }
    }
}
