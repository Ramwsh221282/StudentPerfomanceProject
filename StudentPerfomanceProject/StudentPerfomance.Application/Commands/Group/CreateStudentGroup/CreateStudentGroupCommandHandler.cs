using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.CreateStudentGroup;

internal sealed class CreateStudentGroupCommandHandler
(
	IRepository<StudentGroup> groups, IRepository<EducationPlan> educationPlans
)
: CommandWithErrorBuilder, ICommandHandler<CreateStudentGroupCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _groups = groups;
	private readonly IRepository<EducationPlan> _educationPlans = educationPlans;
	public async Task<OperationResult<StudentGroup>> Handle(CreateStudentGroupCommand command)
	{
		command.GroupValidator.ValidateSchema(this);
		command.EducationPlanValidator.ValidateSchema(this);
		await _groups.ValidateExistance(command.CheckForDublicate, "Такая группа уже существует", this);
		EducationPlan? educationPlan = await _educationPlans.GetByParameter(command.CheckForExistance);
		educationPlan.ValidateNullability("Учебный план не найден. Невозможно создать группу.", this);
		return await this.ProcessAsync(async () =>
		{
			StudentGroup group = StudentGroup.Create(command.Group.Name.Value, educationPlan).Value;
			await _groups.Create(group);
			return new OperationResult<StudentGroup>(group);
		});
	}
}
