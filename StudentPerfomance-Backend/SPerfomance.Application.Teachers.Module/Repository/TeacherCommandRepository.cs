using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.Teachers.Module.Repository;

internal sealed class TeacherCommandRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<Result<Teacher>> Remove(IRepositoryExpression<Teacher> getTeacher)
	{
		Teacher? teacher = await _context.Teachers
		.Include(t => t.Department)
		.FirstOrDefaultAsync(getTeacher.Build());

		if (teacher == null)
			return Result.Failure<Teacher>(new TeacherNotFoundError().ToString());

		await _context.Teachers.Where(t => t.Id == teacher.Id).ExecuteDeleteAsync();
		await Commit();
		return teacher;
	}

	public async Task<Result<Teacher>> Create(TeacherSchema teacher, IRepositoryExpression<Teacher> findDublicate, IRepositoryExpression<TeachersDepartment> getDepartment)
	{
		if (await _context.Teachers.AnyAsync(findDublicate.Build()))
			return Result.Failure<Teacher>(new TeacherDublicateError(teacher.Name, teacher.Surname, teacher.Thirdname, teacher.JobTitle, teacher.WorkingCondition).ToString());

		TeachersDepartment? department = await _context.Departments
		.Include(t => t.Teachers)
		.ThenInclude(t => t.Disciplines)
		.FirstOrDefaultAsync(getDepartment.Build());

		if (department == null)
			return Result.Failure<Teacher>(new DepartmentNotFountError().ToString());

		Teacher entry = teacher.CreateDomainObject(department);
		entry.SetNumber(await GenerateEntityNumber());
		Result add = department.AddTeacher(entry);
		if (add.IsFailure)
			return Result.Failure<Teacher>(add.Error);

		_context.Departments.Attach(entry.Department);
		await _context.Teachers.AddAsync(entry);
		await _context.SaveChangesAsync();
		await Commit();
		return entry;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Teachers.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
