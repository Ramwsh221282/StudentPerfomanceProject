global using SPerfomance.Application.Services.Authentication;
global using SPerfomance.Application.Services.Authentication.Models;
global using SPerfomance.Domain.Models.Users;
global using SPerfomance.Domain.Models.Users.Abstractions;
global using SPerfomance.Domain.Models.Users.ValueObjects;
global using SPerfomance.Api.Features.Users;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers.Abstractions;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddEndpoints();

builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(t => t.FullName?.Replace('+', '.')));
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
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
//app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();
