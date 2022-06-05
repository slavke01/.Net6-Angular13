using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoCore
{
    public class ToDoListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Position { get; set; }

        //Navigacioni
        public int ToDoListId { get; set; }
        //public ToDoList ToDoList { get; set; }

        public ToDoListItem() { }



    }
}
