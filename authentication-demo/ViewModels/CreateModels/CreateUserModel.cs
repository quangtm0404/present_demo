namespace authentication_demo.ViewModels.CreateModels;
public class CreateUserModel
{
    public string Name {get; set;} = default!;
    public string Email {get; set;} = default!;
    public Guid? StudentId {get; set;}
    public int Age {get; set;} = 18;
    public string Password {get; set;} = default!;
}