using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("http://localhost:4200");
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
	});
});

builder.Services.AddScoped<IRepository<EducationDirection>, EducationDirectionsRepository>();
builder.Services.AddScoped<IRepository<EducationPlan>, EducationPlansRepository>();
builder.Services.AddScoped<IRepository<Semester>, SemestersRepository>();
builder.Services.AddScoped<IRepository<StudentGroup>, StudentGroupsRepository>();
builder.Services.AddScoped<IRepository<Student>, StudentsRepository>();
builder.Services.AddScoped<IRepository<TeachersDepartment>, TeachersDepartmentsRepository>();
builder.Services.AddScoped<IRepository<Teacher>, TeachersRepository>();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors();

app.Run();
