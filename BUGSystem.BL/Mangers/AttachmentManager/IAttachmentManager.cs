using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL;

public interface IAttachmentManager
{
    Task<GeneralResult> SaveAttachmentAsync(AttachmentUploadDto request, Guid bugId);
    Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId);
    Task<GeneralResult> DeleteAttachmentByIdAndBugIdAsync(Guid bugId, Guid attachmentId);
    Task<GeneralResult> GetAllAttachmentsAsync();
    Task<GeneralResult> GetAttachmentByIdAsync(Guid attachmentId);
}
