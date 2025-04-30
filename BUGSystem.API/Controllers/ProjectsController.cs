using BUGSystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BUGSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectManager _projectManager;

        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet]
        public async Task<Ok<List<ProjectViewDto>>> GetAllProjects()
        {
            var projects = await _projectManager
                .GetAllProjectsAsync();
            return TypedResults.Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> GetProjectById(Guid id)
        {
            var project = await _projectManager
                .GetProjectByIdAsync(id);
            if (project.Success)
            {
                return TypedResults.Ok(project);
            }
            else
            {
                return TypedResults.BadRequest(project);
            }
        }
        //[Authorize(Policy = Constants.Policies.Manager)]
        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddProject([FromBody] ProjectAddDto projectAddDto)
        {
            var response = await _projectManager
                .AddProjectAsync(projectAddDto);
            if (response.Success)
            {
                return TypedResults.Ok(response);
            }
            else
            {
                return TypedResults.BadRequest(response);
            }
        }
    }
}
