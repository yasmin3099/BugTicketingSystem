using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class User : IdentityUser<Guid>
{
    //public int Id { get; set; }
   // public string Username { get; set; } = string.Empty;
    //public string Email { get; set; } = string.Empty;
    //public string PasswordHash { get; set; } = string.Empty;
    //public List<string> Roles { get; set; } = new(); 
    public ICollection<BugUser>? BugUsers { get; set; }
}
