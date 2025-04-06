using BugTicketingSystem.BLL.DTOs;
using BugTicketingSystem.BLL.Services.ProjectService;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        //private readonly IProjectRepository _projectRepository;

        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAll()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }


        [HttpPost]
        public async Task<ActionResult> Add(ProjectAddDto projectAddDto)
        {
            await _projectService.Add(projectAddDto);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Project>>> GetProjectWithBug(int id)
        {
            var projects = await _projectService.GetProjectWithBugs(id);
            if (projects == null) { return NotFound(); }
            return Ok(projects);
        }

    }
}
