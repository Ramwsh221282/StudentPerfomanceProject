using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.DeleteStudentGroup;

internal sealed class DeleteStudentGroupCommand(StudentsGroupSchema group, IRepositoryExpression<StudentGroup> expression) : ICommand
{
	public StudentsGroupSchema Group { get; init; } = group;
	public IRepositoryExpression<StudentGroup> Expression { get; init; } = expression;
}
