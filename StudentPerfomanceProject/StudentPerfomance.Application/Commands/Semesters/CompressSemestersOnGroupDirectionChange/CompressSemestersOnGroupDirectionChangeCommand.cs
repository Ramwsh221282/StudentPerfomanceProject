using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.CompressSemestersOnGroupDirectionChange;

internal sealed class CompressSemestersOnGroupDirectionChangeCommand
(
	StudentGroup group,
	StudentsGroupSchema newGroupSchema,
	IRepositoryExpression<Semester> byGroup
) : ICommand
{
	public StudentGroup Group { get; init; } = group;
	public StudentsGroupSchema NewSchema { get; init; } = newGroupSchema;
	public IRepositoryExpression<Semester> ByGroup { get; init; } = byGroup;
}
