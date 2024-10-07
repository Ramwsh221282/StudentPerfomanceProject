using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository;

internal sealed class TeacherDepartmentsCommandRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();

	public async Task<Result<TeachersDepartment>> Create(DepartmentSchema department, IRepositoryExpression<TeachersDepartment> expression)
	{
		if (await _db.Departments.AnyAsync(expression.Build()))
			return Result.Failure<TeachersDepartment>(new DepartmentNameDublicateError(department.FullName).ToString());
		Result<TeachersDepartment> create = department.CreateDomainObject();
		create.Value.SetNumber(await GenerateDepartmentNumber());
		if (create.IsFailure) return Result.Failure<TeachersDepartment>(create.Error);
		await _db.Departments.AddAsync(create.Value);
		await Commit();
		return create.Value;
	}

	public async Task<Result<Teacher>> AddTeacher(IRepositoryExpression<TeachersDepartment> expression, TeacherSchema teacher)
	{
		TeachersDepartment? department = await _db.Departments.FirstOrDefaultAsync(expression.Build());
		if (department == null) return Result.Failure<Teacher>(new DepartmentNotFountError().ToString());
		Result<Teacher> create = teacher.CreateDomainObject(department);
		if (create.IsFailure) return Result.Failure<Teacher>(create.Error);
		Result append = department.AddTeacher(create.Value);
		if (append.IsFailure) return Result.Failure<Teacher>(append.Error);
		create.Value.SetNumber(await GenerateTeacherNumber());
		await _db.Teachers.AddAsync(create.Value);
		await Commit();
		return create.Value;
	}

	public async Task<Result<Teacher>> RemoveTeacher(IRepositoryExpression<TeachersDepartment> getDepartment, IRepositoryExpression<Teacher> getTeacher)
	{
		TeachersDepartment? department = await _db.Departments.FirstOrDefaultAsync(getDepartment.Build());
		if (department == null) return Result.Failure<Teacher>(new DepartmentNotFountError().ToString());
		Teacher? teacher = await _db.Teachers.FirstOrDefaultAsync(getTeacher.Build());
		if (teacher == null) return Result.Failure<Teacher>(new TeacherNotFoundError().ToString());
		Result operation = department.RemoveTeacher(teacher);
		if (operation.IsFailure) return Result.Failure<Teacher>(operation.Error);
		await _db.Teachers.Where(t => t.Id == teacher.Id).ExecuteDeleteAsync();
		await Commit();
		return teacher;
	}

	public async Task<Result<TeachersDepartment>> Remove(IRepositoryExpression<TeachersDepartment> getDepartment)
	{
		TeachersDepartment? department = await _db.Departments.FirstOrDefaultAsync(getDepartment.Build());
		if (department == null) return Result.Failure<TeachersDepartment>(new DepartmentNotFountError().ToString());
		await _db.Teachers.Where(t => t.Department.Id == department.Id).ExecuteDeleteAsync();
		await _db.Departments.Where(d => d.Id == department.Id).ExecuteDeleteAsync();
		return department;
	}

	public async Task Commit() => await _db.SaveChangesAsync();

	public async Task<int> GenerateDepartmentNumber()
	{
		int count = await _db.Departments.CountAsync();
		return count + 1;
	}

	public async Task<int> GenerateTeacherNumber()
	{
		int count = await _db.Teachers.CountAsync();
		return count + 1;
	}
}
