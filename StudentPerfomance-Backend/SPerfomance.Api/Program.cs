global using SPerfomance.Api.Features.Users;
global using SPerfomance.Application.Services.Authentication;
global using SPerfomance.Application.Services.Authentication.Models;
global using SPerfomance.Domain.Models.Users.Abstractions;
global using SPerfomance.Domain.Models.Users.ValueObjects;
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

builder.Services.AddSwaggerGen(options =>
    options.CustomSchemaIds(t => t.FullName?.Replace('+', '.'))
);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseMiddleware<TaskCancellationTokenExtensions>();
app.UseRateLimiter();
app.UseResponseCompression();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapEndpoints();
app.Run();
