using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.CreateStudentGroup;

public sealed class StudentGroupCreationService
(
	StudentsGroupSchema groupSchema,
	EducationPlanSchema planSchema,
	IRepository<StudentGroup> groups,
	IRepository<EducationPlan> plans,
	IRepositoryExpression<StudentGroup> checkDublicate,
	IRepositoryExpression<EducationPlan> findPlan
)
: IService<StudentGroup>
{
	private readonly CreateStudentGroupCommand _command = new CreateStudentGroupCommand(groupSchema, planSchema, checkDublicate, findPlan);
	private readonly CreateStudentGroupCommandHandler _handler = new CreateStudentGroupCommandHandler(groups, plans);
	public async Task<OperationResult<StudentGroup>> DoOperation() => await _handler.Handle(_command);
}
