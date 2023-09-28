using authentication_demo.Models;

namespace authentication_demo.ViewModels;
public class LoginResponseModel
{
    public UserModel? User { get; set; } = default!;
    public string Token { get; set; } = default!;
}