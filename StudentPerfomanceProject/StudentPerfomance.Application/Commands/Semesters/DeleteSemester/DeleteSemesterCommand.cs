using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.DeleteSemester;

internal sealed class DeleteSemesterCommand(IRepositoryExpression<Semester> existance) : ICommand
{
	public IRepositoryExpression<Semester> Existance { get; init; } = existance;
}
