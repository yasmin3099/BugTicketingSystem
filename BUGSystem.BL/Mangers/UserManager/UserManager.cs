using BUGSystem.BL.DTOs.User;
using BUGSystem.BL.Mangers.UserManager;
using BUGSystem.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BUGSystem.BL;

public class UserManager : IUserManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _identityUserManager;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserManager(IUnitOfWork unitOfWork, UserManager<User> identityUserManager, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _identityUserManager = identityUserManager;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();

    }

    public async Task<string> RegisterAsync(UserRegisterDto dto)
    {

        var existingUser = await _identityUserManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return "Email already exists.";
        }
        var newUser = new User
        {
            UserName = dto.Username,
            Email = dto.Email
        };

        var result = await _identityUserManager.CreateAsync(newUser, dto.Password);

        if (!result.Succeeded)
        {
            return string.Join(", ", result.Errors.Select(e => e.Description));
        }

        //var roleExist = await _identityUserManager.RoleExistsAsync(dto.Role);
        //if (!roleExist)
        //{
        //    return "Role does not exist.";
        //}

        await _identityUserManager.AddToRoleAsync(newUser, dto.Role);
        return "User registered successfully.";
    }

    public async Task<GeneralResult<string>> LoginAsync(UserLoginDto dto)
    {
        var user = await _unitOfWork.UserRepo.SingleOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
            return GeneralResult<string>.Failure("Invalid Username or password");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            return GeneralResult<string>.Failure("Invalid Username or password");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)

        }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpirationInMinutes"]!)),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return GeneralResult<string>.Success(tokenString);
    }


    //public async Task<string?> LoginAsync(UserLoginDto dto)
    //{
    //    var user = await _identityUserManager.FindByEmailAsync(dto.Email);
    //    if (user == null)
    //        return "Invalid email or password.";

    //    var isPasswordValid = await _identityUserManager.CheckPasswordAsync(user, dto.Password);
    //    if (!isPasswordValid)
    //        return "Invalid email or password.";

    //    var token = await GenerateJwtToken(user);
    //    return token;
    //}

    //private async Task<string> GenerateJwtToken(User user)
    //{
    //    var userRoles = await _identityUserManager.GetRolesAsync(user);

    //    var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
    //        new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
    //    };

    //    claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"]));

    //    var token = new JwtSecurityToken(
    //        issuer: _configuration["Jwt:Issuer"],
    //        audience: _configuration["Jwt:Audience"],
    //        claims: claims,
    //        expires: expires,
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}
}
