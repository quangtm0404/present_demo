namespace authentication_demo.ViewModels;
public class UserModel 
{
    public Guid Id {get; set;}
    public string Name {get; set;} = default!;
    public string Email {get; set;} = default!;

}