using BUGSystem.BL.DTOs.Attachment;
using BUGSystem.BL.DTOs.UserBug;
using BUGSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL;

public class BugManager : IBugManager
{
    private readonly IUnitOfWork _unitOfWork;
    public BugManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task AddBugAsync(BugAddDto bugAddDto)
    {
        await _unitOfWork.BugRepo.Add(new Bug
        {
            Title = bugAddDto.Title,
            Description = bugAddDto.Description,
            ProjectId = bugAddDto.ProjectId
        });
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<BugViewDto>> GetAllBugsAsync()
    {
        var bugs = await _unitOfWork.BugRepo.GetBugsWithProjectInfo();
        var bugViewDtos = bugs.Select(b => new BugViewDto
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            Project = new ProjectViewDto
            {
                ProjectId = b.Project.Id,
                Name = b.Project.Name,
                Description = b.Project.Description,
            }
        }).ToList();
        return bugViewDtos;
    }

    public async Task<BugViewDto?> GetBugByIdAsync(Guid bugId)
    {
        var bug = await _unitOfWork.BugRepo
            .GetBugWithProjectInfo(bugId);
        if (bug == null)
            return null;
        var bugViewDto = new BugViewDto
        {
            Id = bug.Id,
            Title = bug.Title,
            Description = bug.Description,
            Project = new ProjectViewDto
            {
                ProjectId = bug.Project.Id,
                Name = bug.Project.Name,
                Description = bug.Project.Description,
            }
        };
        return bugViewDto;
    }

    public async Task<GeneralResult> AssignBugToUserAsync(Guid bugId, AssignUserDto assignUserRequestDto)
    {
        if (bugId == Guid.Empty)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Invalid Bug ID."
            };
        }

        if (assignUserRequestDto.UserId == Guid.Empty)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Invalid User ID."
            };
        }

        var addBugToUserDto = new AddBugToUserDto
        {
            BugId = bugId,
            UserId = assignUserRequestDto.UserId
        };

        var bugAssignment = new BugUser
        {
            BugId = addBugToUserDto.BugId,
            UserId = addBugToUserDto.UserId,
        };

        await _unitOfWork.BugRepo.AssignBugToUserAsync(bugAssignment);
        await _unitOfWork.SaveChangesAsync();

        return new GeneralResult
        {
            Success = true,
            Message = "Bug assigned successfully."
        };
    }


    public async Task<GeneralResult> RemoveBugAssignmentAsync(Guid bugId, Guid userId)
    {
        if (bugId == Guid.Empty)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Invalid Bug ID."
            };
        }

        if (userId == Guid.Empty)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Invalid User ID."
            };
        }

        await _unitOfWork.BugRepo.RemoveBugAssignmentAsync(bugId, userId);
        await _unitOfWork.SaveChangesAsync();

        return new GeneralResult
        {
            Success = true,
            Message = "Bug assignment is removed successfully."
        };
    }






}
