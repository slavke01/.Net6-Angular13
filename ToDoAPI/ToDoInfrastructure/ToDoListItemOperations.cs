using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;
using ToDoInfrastructure.Interfaces;
using Serilog;

namespace ToDoInfrastructure
{
    public class ToDoListItemOperations : IToDoListItemOperations
    {

        private readonly ILogger _logger;

        public readonly ToDoContext toDoContext;

        public ToDoListItemOperations(ToDoContext context, ILogger logger)
        {
            this.toDoContext = context;
            _logger = logger;
        }


        public void Delete(int id)
        {
            ToDoListItem toDoListItem = toDoContext.ToDoListItems.Find(id);

            if (toDoListItem == null)
            {
                _logger.Information("Id not found");
                throw new Exception("There is no ListItem with the given ID");
            }

            this.toDoContext.ToDoListItems.Remove(toDoListItem);
            this.toDoContext.SaveChanges();
            _logger.Information("ToDoListItems.Delete() executed");
        }

        public void Put(ToDoListItem toDoListItem)
        {
            ToDoListItem toUpdate = toDoContext.ToDoListItems.Find(toDoListItem.Id);

            if (toUpdate == null)
            {
                _logger.Warning("Tried to update null object");
                throw new Exception("toDoListItem is null");
            }
            toDoContext.Attach(toUpdate);
            toUpdate.Name = toDoListItem.Name;
            toUpdate.Description = toDoListItem.Description;
            toDoContext.SaveChanges();
            _logger.Information("ToDoListItems.Put() executed");
        }

        public void UpdatePosition(int id, int newPosition)
        {
            ToDoListItem toUpdate = toDoContext.ToDoListItems.Find(id);
            List<ToDoListItem> all = toDoContext.ToDoListItems.Where(p => p.ToDoListId == toUpdate.ToDoListId).ToList();
            if (toUpdate == null)
            {
                _logger.Warning("Tried to update null object");
                throw new Exception("toDoListItem is null");
            }

            int currentPosition = toUpdate.Position;
            if (newPosition < currentPosition)
            {
                List<ToDoListItem> allToUpdate = all.Where(p => (p.Position < currentPosition) && (p.Position >= newPosition))
                                                .ToList();
                //toDoContext.AttachRange(allToUpdate);
                allToUpdate.ForEach(p => p.Position++);
            }
            else
            {
                List<ToDoListItem> allToUpdate = all.Where(p => (p.Position > currentPosition) && (p.Position <= newPosition))
                                                   .ToList();
               // toDoContext.AttachRange(allToUpdate);
                allToUpdate.ForEach(p => p.Position--);
            }
            toUpdate.Position = newPosition;
            _logger.Information("ToDoListItem position updated");
            toDoContext.SaveChanges();
        }

        public ToDoListItem Get(int id)
        {
            ToDoListItem todoListItem = toDoContext.ToDoListItems.Find(id);
            if (todoListItem==null) 
            {
                _logger.Warning("Tried to update null object");
                throw new Exception("ListItem with the given id not found");
            }

            _logger.Information("ToDoListItem.Get() executed");
            return todoListItem;    

        }

    }
}
