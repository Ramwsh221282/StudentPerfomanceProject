using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public sealed class TeacherUpdateNameHandler : TeacherUpdateDecorator
{
	public TeacherUpdateNameHandler(ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>> handler) : base(handler)
	{ }

	public override async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
	{
		OperationResult<Teacher> result = await base.Handle(command);
		if (result.Result == null || result.IsFailed) return result;
		if
		(
			result.Result.Name.Name != command.NewSchema.Name ||
		 	result.Result.Name.Surname != command.NewSchema.Surname ||
			result.Result.Name.Thirdname != command.NewSchema.Thirdname
		)
			result.Result.ChangeName(command.NewSchema.CreateName());
		return result;
	}
}
