
using BUGSystem.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BUGSystem.DAL
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly MyContext _context;


        public UserRepo(MyContext context) : base(context)
        {
            _context = context;
        }
    }
}
