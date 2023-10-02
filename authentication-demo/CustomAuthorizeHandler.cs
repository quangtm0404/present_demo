using System.Net.Cache;
using Microsoft.AspNetCore.Authorization;

namespace authentication_demo;
public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public MinimumAgeRequirement(int minAge)
    {
        MinimumAge = minAge;
    }
    public int MinimumAge {get;}
}

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var ageClaim = context.User.FindFirst(
            c => c.Type == "Age");

        if (ageClaim is null)
        {
            return Task.CompletedTask;
        }
        
        var age = Convert.ToInt32(ageClaim.Value);
        if(age >= requirement.MinimumAge) 
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}