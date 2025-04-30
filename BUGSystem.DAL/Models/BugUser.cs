using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class BugUser
{
    public Guid BugId { get; set; }
    public Bug? Bug { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}
