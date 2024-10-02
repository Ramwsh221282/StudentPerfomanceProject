using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public sealed class TeacherUpdateConditionHandler : TeacherUpdateDecorator
{
	public TeacherUpdateConditionHandler(ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> handler) : base(handler) { }

	public override async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
	{
		OperationResult<Teacher> result = await base.Handle(command);
		if (result.Result == null || result.IsFailed)
			return result;
		if (result.Result.Condition.Value != command.NewSchema.Condition)
			result.Result.ChangeCondition(command.NewSchema.CreateWorkingCondition());
		return result;
	}
}
