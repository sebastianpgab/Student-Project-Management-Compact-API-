using Microsoft.AspNetCore.Mvc;
using System_do_zarządzania_projektami.Entites;
using System_do_zarządzania_projektami.Services;

namespace System_do_zarządzania_projektami.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var project = _projectService.Get(id);
            return Ok(project);
        }

        [HttpPost]
        public ActionResult Get([FromBody] Project project)
        {
            _projectService.Create(project);
            return Created("Poprawnie utworzono projekt", project);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _projectService.Delete(id);
            return NoContent();

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Project project, [FromRoute] int id)
        {
            _projectService.Update(project, id);
            return Ok();
        }
    }

}
