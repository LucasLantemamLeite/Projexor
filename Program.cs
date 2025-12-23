using Stokify.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBuild();

var app = builder.Build();

app.Run();
