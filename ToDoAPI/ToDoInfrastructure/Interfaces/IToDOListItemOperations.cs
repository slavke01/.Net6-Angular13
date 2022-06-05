using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;

namespace ToDoInfrastructure.Interfaces
{
    public interface IToDoListItemOperations
    {

        void Delete(int id);
        void Put(ToDoListItem toDoListItem);
        void UpdatePosition(int id, int newPosition);
        ToDoListItem Get(int id);
    }
}
