using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL;

public interface IProjectManager
{
    Task<GeneralResult> AddProjectAsync(ProjectAddDto projectAddDto);
    Task<List<ProjectViewDto>> GetAllProjectsAsync();
    Task<GeneralResult> GetProjectByIdAsync(Guid id);
}
