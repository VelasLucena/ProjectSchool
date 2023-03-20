using ApiSchool.Data;
using ApiSchool.Models;
using ApiSchool.Services;
using ApiSchool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProfileDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("Profile"), ServerVersion.Parse("8.0.32")));
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.Configure<ConfigurationModel>(builder.Configuration.GetSection("Settings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
