﻿using Restaurants.API.Middleware;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
//❌ Non recommended syntax - may cause issues depending on OS path format
//builder.Configuration.AddJsonFile(String.Concat(Directory.GetCurrentDirectory(),"/serilog.json"));
builder.Configuration.AddJsonFile("serilog.json");

builder.Host.UseSerilog((context, configuration) =>
{
    //To configure minimally serilog, we need to provide these two parameters
    //1.  Logging level 
    //2. Output Sinks (where to write the logs)
    configuration
    .ReadFrom.Configuration(context.Configuration);
    //Now serilog configuration is delegeted by serilog.json
    //Below is a manuel setting
    //.MinimumLevel.Override("Microsoft", new LoggingLevelSwitch(LogEventLevel.Warning))
    //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
    //.WriteTo.File(path:"Logs/Restaurant-API-.log", rollingInterval: RollingInterval.Day,rollOnFileSizeLimit:true)//default size:1GB
    //.WriteTo.Console(outputTemplate :"[{Timestamp:dd:HH:mm:ss} {Level:u3}] {Message:lj} |{SourceContext} | {NewLine}{Exception}");
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI Container - Extensions Methods calls
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

//Seeding Restaurant Data
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantConfig>();
await seeder.SeedAsync();
// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
