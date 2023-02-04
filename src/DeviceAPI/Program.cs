using DeviceAPI.Extensions;
using DeviceAPI.Middlewares;
using DeviceAPI.Validations;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddDatabase(configuration);

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<DeviceDtoValidation>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices();

var app = builder.Build();
var currentEnvironment = app.Environment;

app.UseMiddleware<ConfigureExceptionMiddleware>();

app.UseSwaggerGen();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UpdateDatabase(currentEnvironment);

app.Run();
