using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RebarAPI.Data;
using Microsoft.OpenApi.Models;  // <-- Add this import for Swagger

var builder = WebApplication.CreateBuilder(args);

// Add your configuration, such as appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(ServiceProvider => new MongoClient(builder.Configuration.GetConnectionString("mongodb://localhost:27017")));
builder.Services.AddTransient<MongoService>();
builder.Services.AddControllers();


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RebarAPI", Version = "v1" });
});

var app = builder.Build();

// Use Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});


// Configure middleware, routes, etc.
app.MapControllers();

app.Run();
