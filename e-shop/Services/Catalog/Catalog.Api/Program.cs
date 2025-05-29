using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogDBConnection")!);
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
