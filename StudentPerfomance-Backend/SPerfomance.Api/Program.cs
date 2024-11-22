global using SPerfomance.Api.Features.Users;
global using SPerfomance.Application.Services.Authentication;
global using SPerfomance.Application.Services.Authentication.Models;
global using SPerfomance.Domain.Models.Users;
global using SPerfomance.Domain.Models.Users.Abstractions;
global using SPerfomance.Domain.Models.Users.ValueObjects;
using System.IO.Compression;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.MiddleWare;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Statistics.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    rateLimiterOptions.AddFixedWindowLimiter(
        "fixed",
        options =>
        {
            options.Window = TimeSpan.FromSeconds(5);
            options.PermitLimit = 3;
            options.QueueLimit = 3;
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        }
    );
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IEducationDirectionRepository, EducationDirectionRepository>();
builder.Services.AddScoped<IEducationPlansRepository, EducationPlansRepository>();
builder.Services.AddScoped<ISemesterPlansRepository, SemesterPlansRepository>();
builder.Services.AddScoped<ITeacherDepartmentsRepository, TeacherDepartmentsRepository>();
builder.Services.AddScoped<ITeachersRepository, TeachersRepository>();
builder.Services.AddScoped<IStudentGroupsRepository, StudentGroupsRepository>();
builder.Services.AddSingleton<IStudentsRepository, StudentsRepository>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
builder.Services.AddSingleton<IMailingService, MailingService>();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IAssignmentSessionsRepository, AssignmentSessionsRepository>();
builder.Services.AddScoped<IStudentAssignmentsRepository, StudentAssignmentsRepository>();
builder.Services.AddScoped<IControlWeekReportRepository, ControlWeekRepository>();

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
