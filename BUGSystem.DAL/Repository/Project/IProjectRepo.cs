
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public interface IProjectRepo : IGenericRepo<Project>
{
    Task<List<Project>> GetProjectsWithAllInfoAsync();
    Task<Project> GetProjectWithAllInfoAsync(Guid projectId);
}
