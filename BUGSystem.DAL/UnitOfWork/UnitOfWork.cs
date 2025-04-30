using BUGSystem.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyContext _context;

    public IUserRepo UserRepo { get; }
    public IProjectRepo ProjectRepo { get; }
    public IBugUserRepo BugUserRepo { get; }
    public IBugRepo BugRepo { get; }
    public IAttachmentRepo AttachmentRepo { get; }

    public UnitOfWork(
        IUserRepo userRepo,
        IProjectRepo projectRepo,
        IBugUserRepo bugUserRepo,
        IBugRepo bugRepo,
        IAttachmentRepo attachmentRepo,
        MyContext context)
    {
        UserRepo = userRepo;
        ProjectRepo = projectRepo;
        BugUserRepo = bugUserRepo;
        BugRepo = bugRepo;
        AttachmentRepo = attachmentRepo;
        _context = context;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
