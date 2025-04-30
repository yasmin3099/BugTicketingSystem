using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public interface IUnitOfWork
{
   public  IUserRepo UserRepo { get; }
   public  IProjectRepo ProjectRepo { get; }
   public  IBugUserRepo BugUserRepo { get; }
   public  IBugRepo BugRepo { get; }
   public  IAttachmentRepo AttachmentRepo { get; }
    Task<int> SaveChangesAsync();
}
