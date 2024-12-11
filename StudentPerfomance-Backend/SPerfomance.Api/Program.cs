global using SPerfomance.Api.Features.Users;
global using SPerfomance.Application.Services.Authentication;
global using SPerfomance.Application.Services.Authentication.Models;
global using SPerfomance.Domain.Models.Users.Abstractions;
global using SPerfomance.Domain.Models.Users.ValueObjects;
using Microsoft.AspNetCore.HttpOverrides;
using NReco.Logging.File;
using Scalar.AspNetCore;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Configuration;
using SPerfomance.Api.Features.EducationDirections.Configuration;
using SPerfomance.Api.Features.EducationPlans.Configuration;
using SPerfomance.Api.Features.PerfomanceContext.Configuration;
using SPerfomance.Api.Features.Semesters.Configuration;
using SPerfomance.Api.Features.StudentGroups.Configurations;
using SPerfomance.Api.Features.TeacherDepartments.Configuration;
using SPerfomance.Api.Features.Users.Configurations;
using SPerfomance.Api.MiddleWare;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCqrsDispatchers();
builder.Services.ConfigureEducationDirectionsComponents();
builder.Services.ConfigureEducationPlans();
builder.Services.ConfigureTeacherDepartments();
builder.Services.ConfigureSemesters();
builder.Services.ConfigureStudentGroups();
builder.Services.ConfigureMailru();
builder.Services.ConfigureAuth();
builder.Services.ConfigureUsers();
builder.Services.ConfigurePerfomanceContext();
builder.Services.ConfigureMiddleWare();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpoints();

builder.Services.AddCors(options =>
{
    var origins = builder.Configuration.GetSection("Cors").GetSection("Origins").Get<string[]>()!;
    Console.WriteLine("Cors configuration");
    foreach (var origin in origins)
    {
        Console.WriteLine(origin);
    }
    options.AddPolicy(
        "Frontend",
        builder => builder.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader()
    );
});

Console.WriteLine($"Database file found: {File.Exists("Database.db")}");

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddFile("app.log", append: true);
    loggingBuilder.AddConsole();
});

var app = builder.Build();

app.UseForwardedHeaders(
    new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    }
);

var logger = app.Services.GetRequiredService<ILogger<Program>>()!;
logger.LogInformation("Application starting...");
app.UseHttpsRedirection();
app.UseMiddleware<TaskCancellationTokenExtensions>();
app.UseRateLimiter();
app.UseResponseCompression();
app.UseCors("Frontend");
app.MapEndpoints();
logger.LogInformation("Application started");
app.Run();
