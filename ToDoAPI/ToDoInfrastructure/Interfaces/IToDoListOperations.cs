using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;

namespace ToDoInfrastructure.Interfaces
{
   public interface IToDoListOperations
    {
        List<ToDoList> Get(string? title);
        ToDoList GetById(int id);
        void Put(ToDoList toDoList);
        void Delete(int id);
        int Post(ToDoList toDoList);
        void AddListItemToList(int id,ToDoListItem toDoListItem);
        void UpdateRemindCriteria(int id,bool remind);
        void UpdateOpened(ToDoList toDoList, bool opened);
        void UpdatePosition(int id, int newPosition);

        int Share(ToDoList toShare);
        ToDoList GetShared(int shareId);
    }
}
