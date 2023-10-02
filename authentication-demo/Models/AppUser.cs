using Microsoft.AspNetCore.Identity;

namespace authentication_demo.Models;
public class AppUser : IdentityUser<Guid> 
{
    public string Name {get ;set;} = default!;
    public Guid? StudentId {get; set;}
    public int Age {get; set;} 
}