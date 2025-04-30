
using BUGSystem.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BUGSystem.BL;

public class ProjectManager : IProjectManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProjectAddDtoValidator _projectAddDtoValidator;
    public ProjectManager(IUnitOfWork unitOfWork,
        ProjectAddDtoValidator projectAddDtoValidator)

    {

        _unitOfWork = unitOfWork;
        _projectAddDtoValidator = projectAddDtoValidator;
    }

    public async Task<GeneralResult> AddProjectAsync(ProjectAddDto projectAddDto)
    {
        var validationResult = await _projectAddDtoValidator
            .ValidateAsync(projectAddDto);
        if (!validationResult.IsValid)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Validation failed",
                Errors = validationResult.Errors
                    .Select(e => new ResultError
                    {
                        Message = e.ErrorMessage,
                        Code = e.ErrorCode
                    }).ToList()
            };
        }
        var projectAddDb = new Project
        {
            Name = projectAddDto.Name,
            Description = projectAddDto.Description,

        };
        await _unitOfWork.ProjectRepo
            .Add(projectAddDb);
        await _unitOfWork.SaveChangesAsync();
        return new GeneralResult<ProjectViewDto>
        {
            IsSuccess = true,
            Message = "Project added successfully",
            Data = new ProjectViewDto { ProjectId = projectAddDb.Id, Name = projectAddDb.Name, Description = projectAddDb.Description } 
        };
    }


    public async Task<List<ProjectViewDto>> GetAllProjectsAsync()
    {
        var projectsFromDb = await _unitOfWork.ProjectRepo
            .GetProjectsWithAllInfoAsync();

        return projectsFromDb.Select(
            p => new ProjectViewDto { ProjectId = p.Id, Name = p.Name, Description = p.Description }    
            ).ToList();
    }

    public async Task<GeneralResult> GetProjectByIdAsync(Guid id)
    {
        var projectFromDb = await _unitOfWork.ProjectRepo
            .GetProjectWithAllInfoAsync(id);
        if (projectFromDb == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Project not found",
                Errors = new List<ResultError>
            {
                new ResultError
                {
                    Message = "Project not found",
                    Code = "404"
                }
            }
            };
        }
        return new GeneralResult<ProjectViewDto>
        {
            IsSuccess = true,
            Message = "Projects found",
            Data = new ProjectViewDto
            {
                ProjectId = projectFromDb.Id,
                Name = projectFromDb.Name,
                Description = projectFromDb.Description,
            }
        };
    }

}
