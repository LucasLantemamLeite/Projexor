using Projexor.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBuild();

var app = builder.Build();

app.AddApp();

app.Run();
