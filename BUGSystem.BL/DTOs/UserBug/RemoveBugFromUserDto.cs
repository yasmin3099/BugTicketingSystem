using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL.DTOs.UserBug
{
    public class RemoveBugFromUserDto
    {
        public Guid UserId { get; set; }
        public Guid BugId { get; set; }
    }

}
