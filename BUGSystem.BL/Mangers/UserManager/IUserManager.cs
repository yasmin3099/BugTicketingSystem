using BUGSystem.BL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL.Mangers.UserManager
{
    public interface IUserManager
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        //Task<string?> LoginAsync(UserLoginDto dto);
        Task<GeneralResult<string>> LoginAsync(UserLoginDto dto);
    }
}
