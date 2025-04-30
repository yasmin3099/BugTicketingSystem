using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class Bug
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public ICollection<BugUser>? BugUsers { get; set; }
    public ICollection<Attachment>? Attachments { get; set; }
}
