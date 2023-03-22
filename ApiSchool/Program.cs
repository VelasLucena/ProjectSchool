using ApiSchool.Data;
using ApiSchool.Models;
using ApiSchool.Services;
using ApiSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Data_ProfileDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("Data_Profile"), ServerVersion.Parse("8.0.32")));
builder.Services.AddDbContext<Data_SystemDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("Data_System"), ServerVersion.Parse("8.0.32")));
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISystemService, SystemService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.Configure<ConfigurationModel>(builder.Configuration.GetSection("Settings"));
builder.Services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<Data_ProfileDbContext>().AddDefaultTokenProviders();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Alunos.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Jwt Authorization header using the bearer scheme"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
