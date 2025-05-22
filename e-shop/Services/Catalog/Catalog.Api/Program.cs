using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR((config) =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
});
 
var app = builder.Build();

app.MapCarter();

app.Run();
