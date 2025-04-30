
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public interface IBugRepo : IGenericRepo<Bug>
{
    Task<List<Bug>> GetBugsWithProjectInfo();
    Task<Bug?> GetBugWithProjectInfo(Guid bugId);
    Task AssignBugToUserAsync(BugUser bugUser);
    Task RemoveBugAssignmentAsync(Guid bugId, Guid userId);

}
