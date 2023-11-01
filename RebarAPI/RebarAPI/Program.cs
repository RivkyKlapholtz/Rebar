using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RebarAPI.Data;
using Microsoft.OpenApi.Models;  // <-- For Swagger

var builder = WebApplication.CreateBuilder(args);

// Add your configuration, such as appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(ServiceProvider => new MongoClient(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<MongoService>();
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RebarAPI", Version = "v1" });
});

var app = builder.Build();

// Use exception handling middleware (good for development)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RebarAPI V1");
});

// Use the routing middleware
app.UseRouting();

// Configure the routes for your controllers
app.MapControllers();

var databaseName = builder.Configuration["ConnectionStrings:DatabaseName"];
Console.WriteLine($"Using database: {databaseName}");

app.Run();
