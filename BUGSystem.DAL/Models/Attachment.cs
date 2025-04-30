using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class Attachment
{
    public Guid AttachmentId { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public Guid BugId { get; set; }
    public Bug? Bugs { get; set; }
}
