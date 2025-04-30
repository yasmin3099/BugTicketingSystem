using BUGSystem.BL.DTOs.Attachment;
using BUGSystem.BL.Validation.Attachment;
using BUGSystem.DAL;

namespace BUGSystem.BL;

public class AttachmentManager : IAttachmentManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AttachmentAddValidator _validations;

    public AttachmentManager(IUnitOfWork unitOfWork,
        AttachmentAddValidator validations)
    {
        _unitOfWork = unitOfWork;
        _validations = validations;
    }
    public async Task<GeneralResult> SaveAttachmentAsync(AttachmentUploadDto request, Guid bugId)
    {
        var validationResult = await _validations.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Validation failed",
                Errors = validationResult.Errors
                    .Select(e => new ResultError
                    {
                        Message = e.ErrorMessage,
                        Code = e.ErrorCode
                    }).ToList()
            };
        }
        var bug = await _unitOfWork.BugRepo
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found"
            };
        }
        var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var attachmentsFolder = Path.Combine(webRootPath, "attachments");

        if (!Directory.Exists(attachmentsFolder))
        {
            Directory.CreateDirectory(attachmentsFolder);
        }

        var filePath = Path.Combine(attachmentsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var fileUrl = $"/attachments/{fileName}";

        var attachmentToDb = new Attachment
        {
            FileName = request.File.FileName,
            FileType = request.File.ContentType,
            FilePath = fileUrl,
            BugId = bugId
        };

        await _unitOfWork.AttachmentRepo
            .Add(attachmentToDb);
        await _unitOfWork.SaveChangesAsync();

        return new GeneralResult<AttachmentViewDto>
        {
            IsSuccess = true,
            Message = "Attachment uploaded successfully",
            Data = new AttachmentViewDto
            {
                AttachmentId = attachmentToDb.AttachmentId,
                FileName = attachmentToDb.FileName,
                FilePath = $"http://localhost:5240{fileUrl}"
            }
        };
    }
    public async Task<GeneralResult> GetAttachmentsByBugIdAsync(Guid bugId)
    {
        var attachments = await _unitOfWork.AttachmentRepo
            .GetAttachmentsByBugIdAsync(bugId);
        if (attachments == null || !attachments.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No attachments found for this bug"
            };
        }
        var attachmentDtos = attachments.Select(a => new AttachmentViewDto
        {
            AttachmentId = a.AttachmentId,
            FileName = a.FileName,
            FilePath = $"http://localhost:5240{a.FilePath}",
            
        }).ToList();
        return new GeneralResult<List<AttachmentViewDto>>
        {
            IsSuccess = true,
            Data = attachmentDtos
        };
    }
    public async Task<GeneralResult> DeleteAttachmentByIdAndBugIdAsync(Guid bugId, Guid attachmentId)
    {
        var bug = await _unitOfWork.BugRepo
            .GetByIdAsync(bugId);
        if (bug == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Bug not found"
            };
        }
        var attachment = await _unitOfWork.AttachmentRepo
            .GetAttachmentByIdAndBugIdAsync(attachmentId, bugId);
        if (attachment == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Attachment not found"
            };
        }
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.FilePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        await _unitOfWork.AttachmentRepo
            .Delete(attachmentId);
        await _unitOfWork.SaveChangesAsync();
        return new GeneralResult
        {
            Success = true,
            Message = "Attachment deleted successfully"
        };
    }
    public async Task<GeneralResult> GetAllAttachmentsAsync()
    {
        var attachments = await _unitOfWork.AttachmentRepo
            .GetAllAsync();
        if (attachments == null || !attachments.Any())
        {
            return new GeneralResult
            {
                Success = false,
                Message = "No attachments found"
            };
        }
        var attachmentDtos = attachments.Select(a => new AttachmentViewDto
        {
            AttachmentId = a.AttachmentId,
            FileName = a.FileName,
            FilePath = $"http://localhost:5240{a.FilePath}"
        }).ToList();
        return new GeneralResult<List<AttachmentViewDto>>
        {
            IsSuccess = true,
            Data = attachmentDtos
        };
    }

    public async Task<GeneralResult> GetAttachmentByIdAsync(Guid attachmentId)
    {
        var attachment = await _unitOfWork.AttachmentRepo
            .GetByIdAsync(attachmentId);
        if (attachment == null)
        {
            return new GeneralResult
            {
                Success = false,
                Message = "Attachment not found"
            };
        }
        var attachmentDto = new AttachmentViewDto
        {
            AttachmentId = attachment.AttachmentId,
            FileName = attachment.FileName,
            FilePath = $"http://localhost:5240{attachment.FilePath}",
        };
        return new GeneralResult<AttachmentViewDto>
        {
            IsSuccess = true,
            Data = attachmentDto
        };
    }
}
