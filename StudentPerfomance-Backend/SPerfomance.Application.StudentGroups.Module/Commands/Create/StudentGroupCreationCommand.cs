using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans.Validators;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.CompositeEntityValidator;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Create;

public sealed class StudentGroupCreationCommand : ICommand
{
	private readonly StudentsGroupSchema _group;
	private readonly IRepositoryExpression<StudentGroup> _checkGroupDublicate;
	private readonly IRepositoryExpression<EducationPlan> _findEducationPlan;
	private readonly CompositeValidator _validator;
	public readonly ICommandHandler<StudentGroupCreationCommand, StudentGroup> Handler;
	public StudentGroupCreationCommand
	(
		StudentsGroupSchema group,
		IRepositoryExpression<StudentGroup> checkGroupDublicate,
		IRepositoryExpression<EducationPlan> findEducationPlan,
		IRepository<StudentGroup> groups,
		IRepository<EducationPlan> plans
	)
	{
		_group = group;
		_checkGroupDublicate = checkGroupDublicate;
		_findEducationPlan = findEducationPlan;
		_validator = new CompositeValidator
		(
			new StudentGroupSchemaValidator().WithNameValidation(group),
			new EducationPlanValidator().WithYearValidation(group.PlanInfo),
			new EducationDirectionValidator()
			.WithNameValidation(group.PlanInfo.Direction)
			.WithTypeValidation(group.PlanInfo.Direction)
			.WithCodeValidator(group.PlanInfo.Direction)
		);
		_validator.ProcessValidation();
		Handler = new CommandHandler(groups, plans);
	}

	internal sealed class CommandHandler(IRepository<StudentGroup> groups, IRepository<EducationPlan> plans) : ICommandHandler<StudentGroupCreationCommand, StudentGroup>
	{
		private readonly IRepository<StudentGroup> _groups = groups;
		private readonly IRepository<EducationPlan> _plans = plans;
		public async Task<OperationResult<StudentGroup>> Handle(StudentGroupCreationCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<StudentGroup>(command._validator.Error);
			if (await _groups.HasEqualRecord(command._checkGroupDublicate))
				return new OperationResult<StudentGroup>(new GroupDublicateNameError(command._group.NameInfo).ToString());
			EducationPlan? plan = await _plans.GetByParameter(command._findEducationPlan);
			if (plan == null)
				return new OperationResult<StudentGroup>(new EducationPlanNotFoundError().ToString());
			GroupName name = command._group.CreateGroupName();
			StudentGroup group = StudentGroup.Create(name, plan).Value;
			await _groups.Create(group);
			return new OperationResult<StudentGroup>(group);
		}
	}
}
