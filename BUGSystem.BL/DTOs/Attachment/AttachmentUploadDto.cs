using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL;

public class AttachmentUploadDto
{
    public IFormFile File { get; set; } = null!;
}
