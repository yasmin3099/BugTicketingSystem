
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public interface IAttachmentRepo : IGenericRepo<Attachment>
{
    Task<List<Attachment?>> GetAttachmentsByBugIdAsync(Guid bugId);
    Task<Attachment?> GetAttachmentByIdAndBugIdAsync(Guid attachmentId, Guid bugId);
    
}
