using Infrastructure;
using Web.Api;
using Web.Api.Infrastructure;
using Web.Api.Endpoints;
using System.Reflection;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.MapEndpoints(Assembly.GetExecutingAssembly());
app.UseExceptionHandler();

app.Run();
