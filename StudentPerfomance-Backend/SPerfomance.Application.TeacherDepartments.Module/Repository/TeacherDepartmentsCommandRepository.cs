using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository;

internal sealed class TeacherDepartmentsCommandRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();

	public async Task<Result<TeachersDepartment>> Create(DepartmentSchema department, IRepositoryExpression<TeachersDepartment> expression)
	{
		if (await _db.Departments.AnyAsync(expression.Build()))
			return Result.Failure<TeachersDepartment>(new DepartmentNameDublicateError(department.Name).ToString());

		Result<TeachersDepartment> create = department.CreateDomainObject();
		create.Value.SetNumber(await GenerateDepartmentNumber());
		if (create.IsFailure)
			return Result.Failure<TeachersDepartment>(create.Error);

		await _db.Departments.AddAsync(create.Value);
		await Commit();
		return create.Value;
	}

	public async Task<Result<TeachersDepartment>> Remove(IRepositoryExpression<TeachersDepartment> getDepartment)
	{
		TeachersDepartment? department = await _db.Departments.FirstOrDefaultAsync(getDepartment.Build());
		if (department == null)
			return Result.Failure<TeachersDepartment>(new DepartmentNotFountError().ToString());

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
		int[] numbers = await _db.Departments.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
