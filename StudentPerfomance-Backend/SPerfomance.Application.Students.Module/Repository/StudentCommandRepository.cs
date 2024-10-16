using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.Students.Module.Repository;

internal sealed class StudentCommandRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<Result<Student>> Create(StudentSchema student, IRepositoryExpression<StudentGroup> getGroup, IRepositoryExpression<Student> findDulicate)
	{
		if (await _context.Students.AnyAsync(findDulicate.Build()))
			return Result.Failure<Student>(new StudentDublicateRecordBookError(student.Recordbook).ToString());

		StudentGroup? group = await _context.Groups
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.FirstOrDefaultAsync(getGroup.Build());

		if (group == null)
			return Result.Failure<Student>(new GroupNotFoundError().ToString());

		Student entry = student.CreateDomainObject(group);
		Result create = group.AddStudent(entry);

		if (create.IsFailure)
			return Result.Failure<Student>(create.Error);

		entry.SetNumber(await GenerateEntityNumber());
		_context.Groups.Attach(entry.Group);

		await _context.Students.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<Student>> Remove(IRepositoryExpression<Student> getStudent, IRepositoryExpression<StudentGroup> getGroup)
	{
		StudentGroup? group = await _context.Groups
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.Include(g => g.Students)
		.FirstOrDefaultAsync();

		if (group == null)
			return Result.Failure<Student>(new GroupNotFoundError().ToString());

		Student? student = await _context.Students.FirstOrDefaultAsync(getStudent.Build());
		if (student == null)
			return Result.Failure<Student>(new StudentNotFoundError().ToString());

		Result remove = group.RemoveStudent(student);
		if (remove.IsFailure)
			return Result.Failure<Student>(remove.Error);

		await _context.Students.Where(s => s.Id == student.Id).ExecuteDeleteAsync();
		return student;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Students.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
