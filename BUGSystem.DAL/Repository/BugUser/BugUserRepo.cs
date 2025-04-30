using BUGSystem.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL;

public class BugUserRepo : GenericRepo<BugUser>, IBugUserRepo
{
    private readonly MyContext _context;
    public BugUserRepo(Context.MyContext context) : base(context)
    {
        _context = context;
    }
}
