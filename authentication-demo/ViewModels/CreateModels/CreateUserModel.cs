namespace authentication_demo.ViewModels.CreateModels;
public class CreateUserModel
{
    public string Name {get; set;} = default!;
    public string Email {get; set;} = default!;
    public string Password {get; set;} = default!;
}