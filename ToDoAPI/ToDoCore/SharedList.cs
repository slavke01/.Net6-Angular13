using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCore
{
    public class SharedList
    {

        public int Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int toDoListId { get; set; }

        public SharedList() { }
    }
}
