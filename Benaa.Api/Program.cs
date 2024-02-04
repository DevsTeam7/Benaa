using Benaa.Api.Extensions;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(optins =>
   optins.UseNpgsql(
   builder.Configuration.GetSection("ConnectionStrings:Defult").Value
));
builder.Services.AddAuthentication(optins =>
{
    optins.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    optins.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    optins.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(optins =>
    optins.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration.GetSection("Jwt:Key").Value!))
    }
); 

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Register Services
builder.Services.RegisterService();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
