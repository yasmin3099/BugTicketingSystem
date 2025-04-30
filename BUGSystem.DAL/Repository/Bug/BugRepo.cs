using BUGSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class BugRepo : GenericRepo<Bug>, IBugRepo
{
    private readonly MyContext _context;
    public BugRepo(Context.MyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Bug>> GetBugsWithProjectInfo()
    {
        return await _context.Set<Bug>()
            .Include(b => b.Project)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Bug?> GetBugWithProjectInfo(Guid bugId)
    {
        return await _context.Set<Bug>()
            .Include(b => b.Project)
            .FirstOrDefaultAsync(b => b.Id == bugId);
    }

    public async Task AssignBugToUserAsync(BugUser bugUser)
    {
        await _context.Set<BugUser>()
           .AddAsync(bugUser);
    }


    public async Task RemoveBugAssignmentAsync(Guid bugId, Guid userId)
    {
        await _context.Set<BugUser>()
            .Where(x => x.BugId == bugId && x.UserId == userId)
            .ExecuteDeleteAsync();
    }



}