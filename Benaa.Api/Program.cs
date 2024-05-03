using Benaa.Api.Extensions;
using Benaa.Core.Entities.General;
using Benaa.Core.Mapper;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Benaa.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
//long maxFileSize = 1L * 1024L * 1024L * 1024L; // 1 GB
//builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = maxFileSize) ;

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(optins =>
   optins.UseNpgsql(
   builder.Configuration.GetSection("ConnectionStrings:Defult").Value
));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowAll",
              policy =>
              {
                  policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .SetIsOriginAllowed((host) => true)
                      .AllowCredentials();
              });
});

builder.Services.AddInfrastructure();

//adding the mapper
builder.Services.AddAutoMapper(typeof(BaseMapper));
//adding th identuty
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

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

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = context.Request.Query["access_token"];
            }
            return Task.CompletedTask;
        }
    };

});

//adding SignalR
builder.Services.AddSignalR().AddJsonProtocol();

//Register Services
builder.Services.RegisterService();

builder.Services.AddEndpointsApiExplorer();
//adding text filed to enter the bearer token to swagger for authentcating the user identity
builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("allowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
//add Hubs
app.AddHubs();

app.Run();
