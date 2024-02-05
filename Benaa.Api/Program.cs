using Benaa.Api.Extensions;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Services;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using Benaa.Core.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(optins =>
   optins.UseNpgsql(
   builder.Configuration.GetSection("ConnectionStrings:Defult").Value
));

builder.Services.AddAutoMapper(typeof(BaseMapper));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Email settings
    options.User.RequireUniqueEmail = true;
   // options.SignIn.RequireConfirmedEmail = true;

    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(optins =>
//{
//    optins.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    optins.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(optins =>
//{
//    optins.RequireHttpsMetadata = false;
//    optins.SaveToken = true;
//    optins.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateIssuerSigningKey = true,

//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
//       (builder.Configuration["Jwt:Key"]))
//    };
//});

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateActor = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
    };
});


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
