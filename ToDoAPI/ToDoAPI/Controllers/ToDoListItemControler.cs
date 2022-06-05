using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoCore;
using ToDoInfrastructure.Interfaces;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListItemControler : ControllerBase
    {
        public readonly IToDoListItemOperations toDoListItemOps;
        private ILogger _logger;

        public ToDoListItemControler(IToDoListItemOperations ops, ILogger<ToDoListItemControler> logger)
        {
            this.toDoListItemOps = ops;
            _logger = logger;
        }

        [HttpDelete]
        [Authorize]

        public async Task<ActionResult> Delete(int id)
        {
            //Exception filters
            try
            {
                toDoListItemOps.Delete(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
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
                toDoListItemOps.UpdatePosition(id, position);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ToDoListItem>> GetById(int id)
        {
            
            try
            {
                var item=toDoListItemOps.Get(id);
                return Ok(item);

            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return NotFound(e.Message);
            }
        }
            
        [HttpPut]
        [Authorize]

        public async Task<ActionResult> Put([FromBody]ToDoListItem toDoListItem)
        {
            try
            {
                toDoListItemOps.Put(toDoListItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return NotFound(e.Message);
            }
            return Ok();
        }

    }
}
