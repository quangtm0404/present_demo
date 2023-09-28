using authentication_demo.Data;
using authentication_demo.Models;
using authentication_demo.Services.Interfaces;
using authentication_demo.ViewModels;
using authentication_demo.ViewModels.CreateModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace authentication_demo.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOptions;
    public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, AppDbContext dbContext, IOptions<JwtOptions> options)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _mapper = mapper;
        _dbContext = dbContext;
        _jwtOptions = options.Value;
    }
    public async Task<bool> AssignRoleAsync(string email, string role)
    {
        role = role.ToUpper();
        var user = _dbContext.User.Where(x => x.Email == email).First();
        if (user is not null)
        {
            if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new AppRole { Name = role }).GetAwaiter().GetResult();

            }
            await _userManager.AddToRoleAsync(user, role);
            return true;
        }
        else throw new Exception("User not found!");
    }

    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequestModel)
    {
        var user = await _dbContext.User.Where(x => x.Email == loginRequestModel.Email).FirstOrDefaultAsync();
        if (user is not null)
        {
            return await _userManager.CheckPasswordAsync(user, loginRequestModel.Password) ?
                new LoginResponseModel
                {
                    Token = user.GenerateToken(jwtOptions: _jwtOptions, role: (await _userManager.GetRolesAsync(user)).First()),
                    User = _mapper.Map<UserModel>(user)
                }
                : new LoginResponseModel
                {
                    Token = "",
                    User = null
                };

        }
        else throw new Exception("Not found");
    }

    public async Task<bool> RegisterAsync(CreateUserModel model)
    {
        var user = _mapper.Map<AppUser>(model);
        try
        {
            user.Id = Guid.NewGuid();
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddPasswordAsync(user, model.Password);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Error " + ex.Message);
            return false;
        }

    }
}
