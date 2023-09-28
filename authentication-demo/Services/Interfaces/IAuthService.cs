using authentication_demo.Models;
using authentication_demo.ViewModels;
using authentication_demo.ViewModels.CreateModels;

namespace authentication_demo.Services.Interfaces;
public interface IAuthService 
{
    Task<LoginResponseModel>  LoginAsync(LoginRequestModel loginRequestModel);
    Task<bool> RegisterAsync(CreateUserModel model);
    Task<bool> AssignRoleAsync(string email, string role);

}