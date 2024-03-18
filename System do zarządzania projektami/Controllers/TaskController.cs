using Microsoft.AspNetCore.Mvc;
using System_do_zarządzania_projektami.Entites;
using System_do_zarządzania_projektami.Services;

namespace System_do_zarządzania_projektami.Controllers
{
    [Route("api/project/{projectId}/task")]
    [ApiController]
    public class TaskController: ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(int projectId, [FromBody] TaskItem task)
        {
            _taskService.Create(projectId, task);
            return Created("Poprawnie utworzono zadanie", task);
        }

        [HttpGet("{taskId}")]
        public ActionResult<TaskItem> GetTask(int projectId, int taskId)
        {
            var task = _taskService.Get(projectId, taskId);
            return Ok(task);
        }

        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int projectId, int taskId, [FromBody] TaskItem taskUpdate)
        {
            _taskService.Update(projectId, taskId, taskUpdate); 
            return NoContent();
        }
         
        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int projectId, int taskId)
        {
            _taskService.Delete(projectId, taskId); 
            return NoContent();
        }
    }
}
