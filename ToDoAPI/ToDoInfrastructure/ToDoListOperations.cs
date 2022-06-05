using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using ToDoCore;
using ToDoInfrastructure.Interfaces;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace ToDoInfrastructure
{
    public class ToDoListOperations : IToDoListOperations
    {
        private readonly ILogger _logger;
        public readonly ToDoContext toDoContext;
        private readonly IHttpContextAccessor _accesor;
        public ToDoListOperations( ToDoContext context,ILogger logger, IHttpContextAccessor httpContextAccessor) 
        {
            this.toDoContext = context;
            _logger=logger;
            _accesor = httpContextAccessor;
        }
        public void Delete(int id)
        {
            ToDoList toDoList = toDoContext.ToDoLists.Find(id);
            var owner = _accesor.HttpContext.User.Claims
              .FirstOrDefault(p => p.Type == "http://www.novalite.com/korisnik")?.Value;
            if (toDoList == null) 
            {
                _logger.Warning("Id not found. Current user: "+ owner);
                throw new Exception("There is no List with the given ID");       
            }
            this.UpdatePosition(id, this.toDoContext.ToDoLists.ToList().Count-1);
            this.toDoContext.ToDoLists.Remove(toDoList);
            this.toDoContext.SaveChanges();
            _logger.Information("ToDoList.Delete() executed. Current user: " + owner);

        }

        public List<ToDoList> Get(string? title)
        {
            List<ToDoList> retVal;

            var eagerLoading = toDoContext.ToDoLists.Include(p => p.ListItems).ToList();
            if (String.IsNullOrEmpty(title)) 
            {
                _logger.Information("ToDoList.Get() without title executed");
                retVal = toDoContext.ToDoLists.ToList();
                retVal=retVal.OrderBy(x => x.Position).ToList();
                retVal.ForEach(x => x.ListItems = x.ListItems.OrderBy(x => x.Position).ToList());
                return retVal;
            }
            retVal= toDoContext.ToDoLists.Where(item => item.Title.ToLower().Contains(title.ToLower())).ToList();
            retVal = retVal.OrderBy(x => x.Position).ToList();
            retVal.ForEach(x=>x.ListItems=x.ListItems.OrderBy(x=>x.Position).ToList());
            _logger.Information("ToDoList.Get() with title executed");
            return retVal;
        }
        public ToDoList GetById(int id)
        {
            var eagerLoading = toDoContext.ToDoLists.Include(p => p.ListItems).ToList();
           
            ToDoList toDoList = toDoContext.ToDoLists.Find(id);
            toDoList.ListItems = toDoList.ListItems.OrderBy(x => x.Position).ToList();
            if (toDoList == null)
            {
                _logger.Warning("Id not found.");
                throw new Exception("There is no List with the given ID");
            }
            return toDoList;
        }

        public int Post(ToDoList toDoList)
        {
            var owner = _accesor.HttpContext.User.Claims
               .FirstOrDefault(p => p.Type == "http://www.novalite.com/korisnik")?.Value;
            if (toDoList ==null) 
            {
                _logger.Warning("Tried to add null object. Current user: " + owner);
                throw new Exception("ToDoList cant be null.");
            }

           
            var email = _accesor.HttpContext.User.Claims
               .FirstOrDefault(p => p.Type == "http://www.novalite.com/email")?.Value;

            toDoList.Owner = owner;
            toDoList.RemindEmail = email;
            toDoList.Position = 0;
            int pos = 0;
            toDoList.ListItems.ToList().ForEach(p=>p.Position=pos++);
            List<ToDoList> toDoPosition = toDoContext.ToDoLists.ToList();
            toDoContext.AttachRange(toDoPosition);

            toDoPosition.ForEach(p=> p.Position++);
            toDoContext.ToDoLists.Add(toDoList);
            toDoContext.SaveChanges();

            List<ToDoList> toDos = toDoContext.ToDoLists.OrderByDescending(x=>x.Id).ToList();  
            _logger.Information("ToDoList.Post() executed. Current User: "+owner);
            return toDos[0].Id;
        }

        public void Put(ToDoList toDoList)
        {
            var owner = _accesor.HttpContext.User.Claims
              .FirstOrDefault(p => p.Type == "http://www.novalite.com/korisnik")?.Value;
            ToDoList toUpdate = toDoContext.ToDoLists.Find(toDoList.Id);
            if (toUpdate == null)
            {
                _logger.Warning("Tried to update null object. Current user: " +owner);
                throw new Exception("toDoList is null");
            }
            toUpdate.Title = toDoList.Title;
            toUpdate.Remind = toDoList.Remind;
            toUpdate.Opened = toDoList.Opened;
            toUpdate.RemindAfter = toDoList.RemindAfter;

            _logger.Information("ToDoList.Put() executed. Current user: "+owner);
            toDoContext.SaveChanges();

        }

        public void UpdateOpened(ToDoList toDoList, bool opened)
        {
            toDoContext.Attach(toDoList);
            toDoList.Opened = opened;
            toDoContext.SaveChangesAsync();
            _logger.Information("ToDoList.UpdateOpened() executed");

        }

        public void UpdateRemindCriteria(int id,bool remind)
        {
            var toDoList = toDoContext.ToDoLists.Find(id);
            toDoList.Remind = remind;
            toDoContext.SaveChanges();
            _logger.Information("ToDoList.UpdateReminder() executed");

        }
        public void UpdatePosition(int id,int newPosition) 
        {
            var owner = _accesor.HttpContext.User.Claims
              .FirstOrDefault(p => p.Type == "http://www.novalite.com/korisnik")?.Value;
            ToDoList toUpdate = toDoContext.ToDoLists.Find(id);
            List<ToDoList> all = toDoContext.ToDoLists.ToList();
            if (toUpdate == null)
            {
                _logger.Warning("Tried to update null object. Current user: "+owner);
                throw new Exception("toDoList is null");
            }

            int currentPosition = toUpdate.Position;
            toDoContext.ToDoLists.Attach(toUpdate);
            if (newPosition < currentPosition)
            {
                List<ToDoList> allToUpdate = all.Where(p => (p.Position < currentPosition) && (p.Position >= newPosition))
                                                .ToList();
                toDoContext.AttachRange(allToUpdate);
                allToUpdate.ForEach(p => p.Position++);
            }
            else 
            {
                List<ToDoList> allToUpdate = all.Where(p => (p.Position > currentPosition) && (p.Position <= newPosition))
                                                   .ToList();
                toDoContext.AttachRange(allToUpdate);
                allToUpdate.ForEach(p => p.Position--);
            }
            toUpdate.Position = newPosition;
            _logger.Information("Todolist position updated. Current user: "+owner);
            toDoContext.SaveChanges();
        }


        public void AddListItemToList(int id, ToDoListItem toDoListItem)
        {
            var owner = _accesor.HttpContext.User.Claims
              .FirstOrDefault(p => p.Type == "http://www.novalite.com/korisnik")?.Value;
            if (toDoListItem == null)
            {
                _logger.Warning("Tried to add null object to list. Current user: "+owner);
                throw new Exception("toDoListItem is null");
            }

            ToDoList toDoList = toDoContext.ToDoLists.Find(id);

            if (toDoList == null)
            {
                _logger.Warning("Id not found. Current user: "+owner);
                throw new Exception("There is no List with the given ID");
            }

            int position = toDoContext.ToDoListItems.Where(p => p.ToDoListId == id).ToList().Count();
            toDoListItem.Position = position;
            toDoListItem.ToDoListId = toDoList.Id;
            
            toDoContext.ToDoListItems.Add(toDoListItem);
            toDoContext.SaveChanges();
        }

        public int Share(ToDoList toShare)
        {
            SharedList shared  =  new SharedList { toDoListId = toShare.Id,ExpirationDate= DateTime.Now.AddHours(2)};

            this.toDoContext.SharedLists.Add(shared);
            toDoContext.SaveChanges();

            int id = shared.Id;

            return id;
        }

        public ToDoList GetShared(int shareId)
        {
            SharedList shared = toDoContext.SharedLists.Find(shareId);
            if (shared.ExpirationDate > DateTime.Now)
            {
                var eagerLoading = toDoContext.ToDoLists.Include(p => p.ListItems).ToList();

                ToDoList toShare = toDoContext.ToDoLists.Find(shared.toDoListId);
                return toShare;
            }
            else 
            {
                toDoContext.SharedLists.Remove(shared);
                toDoContext.SaveChanges();
                throw new Exception("Expired");
            }
            throw new Exception("Nema");
        }
    }
}
