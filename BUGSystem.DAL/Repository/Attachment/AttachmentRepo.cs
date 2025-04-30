using BUGSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class AttachmentRepo : GenericRepo<Attachment>, IAttachmentRepo
{
    private readonly MyContext _context;
    public AttachmentRepo(Context.MyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Attachment>> GetAttachmentsByBugIdAsync(Guid bugId)
    {
        return await _context.Set<Attachment>()
           .Include(a => a.Bugs)
           .Where(a => a.BugId == bugId)
           .AsNoTracking()
           .ToListAsync();
    }
    public async Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId)
    {
        return await _context.Set<Attachment>()
            .Include(a => a.Bugs)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId && a.BugId == bugId)
            ?? null;
    }


}
