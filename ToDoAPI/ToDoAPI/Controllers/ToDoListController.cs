using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoCore;
using ToDoInfrastructure.Interfaces;
using Serilog;
using ILogger = Serilog.ILogger;
using Microsoft.AspNetCore.Authorization;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        public readonly IToDoListOperations toDoListOps;
        ILogger _logger;
        public ToDoListController(IToDoListOperations ops, ILogger logger)
        {
            this.toDoListOps = ops;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ToDoList>>> Get(string? title = "")
        {
            string accessToken = User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;

            try
            {
                var items = toDoListOps.Get(title);
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return Problem(e.Message);
            }

        }

        [HttpGet("/GetById")]
        [Authorize]

        public async Task<ActionResult<ToDoList>> GetById(int id)
        {
            try
            {
                var items = toDoListOps.GetById(id);
                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return Problem(e.Message);
            }

        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult> Post([FromBody] ToDoList toDoList)
        {
            try
            {
                int id = toDoListOps.Post(toDoList);
                return Ok(id);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return Problem(e.Message);
            }

            //createdatroute
        }

        [HttpPost("/share")]
        [Authorize]
        public async Task<ActionResult> Share([FromBody] ToDoList toDoList)
        {
            try
            {
                int id = toDoListOps.Share(toDoList);
                return Ok(id);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return Problem(e.Message);
            }

            //createdatroute
        }


        [HttpGet("/GetShared")]
        public async Task<ActionResult<ToDoList>> GetShared(int shareId)
        {
            try
            {

               ToDoList ret= toDoListOps.GetShared(shareId);
                return Ok(ret);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return Problem(e.Message);
            }

            //createdatroute
        }

        [HttpDelete]
        [Authorize]

        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                toDoListOps.Delete(id);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();

        }

        [HttpPost("/AddListItemToList")]
        [Authorize]

        public async Task<ActionResult> AddListItemToList(int id, [FromBody] ToDoListItem toDoListItem)
        {
            try
            {
                toDoListOps.AddListItemToList(id, toDoListItem);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();
        }

        [HttpPatch]
        [Authorize]

        public async Task<ActionResult> UpdatePosition(int id, int position)
        {
            try
            {
                toDoListOps.UpdatePosition(id, position);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();
        }

        [HttpPut("/UpdateReminder")]
        [Authorize]

        public async Task<ActionResult> UpdateReminder(int id,bool reminder)
        {
            try
            {
                toDoListOps.UpdateRemindCriteria(id,reminder);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();
        }


        [HttpPut]
        [Authorize]

        public async Task<ActionResult> Put([FromBody]ToDoList toDoList) 
        {
            try
            {
                toDoListOps.Put(toDoList);
            }
            catch (Exception e)
            {
                _logger.Error(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();
        }

    }
}
