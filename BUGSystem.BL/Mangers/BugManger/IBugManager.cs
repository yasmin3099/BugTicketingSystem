using BUGSystem.BL.DTOs.UserBug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL;

public interface IBugManager
{
    Task AddBugAsync(BugAddDto bugAddDto);
    Task<List<BugViewDto>> GetAllBugsAsync();
    Task<BugViewDto?> GetBugByIdAsync(Guid bugId);
    Task<GeneralResult> AssignBugToUserAsync(Guid bugId, AssignUserDto assignUserDto);
    Task<GeneralResult> RemoveBugAssignmentAsync(Guid bugId, Guid userId);
}
