using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authentication_demo.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace authentication_demo;
public static class TokenGenerator
{
    public static string GenerateToken(this AppUser user, string role, JwtOptions jwtOptions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);
        var claimList = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(ClaimTypes.Role, role),
            new Claim("Age", user.Age.ToString())

        };
        if(user.StudentId is not null)
            claimList.Add(new Claim("StudentId", user.StudentId.Value.ToString()));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtOptions.Issuer,
            Subject = new ClaimsIdentity(claimList),
            Audience = jwtOptions.Audience,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
    public static WebApplicationBuilder AddAuthenticationValidator(this WebApplicationBuilder builder)
    {
        var settingsSection = builder.Configuration.GetSection("JwtOptions");
        var secret = settingsSection.GetValue<string>("Secret");
        var issuer = settingsSection.GetValue<string>("Issuer");
        var audience = settingsSection.GetValue<string>("Audience");
        var key = Encoding.ASCII.GetBytes(secret!);
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateAudience = true
            };
        });

        return builder;
    }


    // Extension Methods Add Claim Check
    public static WebApplicationBuilder AddDemoAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        builder.Services.AddAuthorization(opt => 
        {
            opt.AddPolicy("StudentOnly", policy => policy.RequireClaim("StudentId"));
            opt.AddPolicy("AtLeast21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
        });
        
        return builder;
    }
}