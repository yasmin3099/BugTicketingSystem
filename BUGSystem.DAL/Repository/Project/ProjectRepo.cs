using BUGSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class ProjectRepo : GenericRepo<Project>, IProjectRepo
{
    private readonly MyContext _context;
    public ProjectRepo(Context.MyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetProjectsWithAllInfoAsync()
    {
        return await _context.Set<Project>()
            .Include(p => p.Bugs)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project> GetProjectWithAllInfoAsync(Guid projectId)
    {
        return await _context.Set<Project>()
            .Include(p => p.Bugs)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == projectId) ?? throw new Exception("Project not found");
    }
}
