using Example.Core;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
var environmentName = builder.Environment.EnvironmentName;
builder.Configuration
	.SetBasePath(currentDirectory)
	.AddJsonFile($"appsettings.{environmentName}.json", true, true)
	.AddEnvironmentVariables();

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterApplicationServices(configuration);
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.WithOrigins(
                "https://localhost:44348")
                .AllowAnyMethod()
                .WithHeaders("authorization", "accept", "content-type", "origin"));

app.Run();
