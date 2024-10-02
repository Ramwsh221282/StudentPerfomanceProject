using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public sealed class TeacherUpdateJobTitleHandler : TeacherUpdateDecorator
{
	public TeacherUpdateJobTitleHandler(ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> handler) : base(handler) { }

	public override async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
	{
		OperationResult<Teacher> result = await base.Handle(command);
		if (result.Result == null || result.IsFailed) return new OperationResult<Teacher>(result.Error);
		if (result.Result.JobTitle.Value != command.NewSchema.Job)
			result.Result.ChangeJobTitle(command.NewSchema.CreateJobTitle());
		return result;
	}
}
