using Ordering_API;
using Ordering_Application;
using Ordering_Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices().AddApplicationServices().AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
