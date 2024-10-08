using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.AttachTeacher;

internal sealed class AttachTeacherCommand : ICommand
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly IRepositoryExpression<SemesterPlan> _getPlan;
	private readonly IRepositoryExpression<Teacher> _getTeacher;
	public ICommandHandler<AttachTeacherCommand, SemesterPlan> Handler;
	public AttachTeacherCommand(SemesterPlanSchema plan, TeacherSchema teacher)
	{
		_repository = new SemesterPlansQueryRepository();
		_getPlan = ExpressionsFactory.GetPlan(plan.ToRepositoryObject());
		_getTeacher = ExpressionsFactory.GetTeacher(teacher.ToRepositoryObject());
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(SemesterPlansQueryRepository repository) : ICommandHandler<AttachTeacherCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<SemesterPlan>> Handle(AttachTeacherCommand command)
		{
			SemesterPlan? plan = await _repository.GetByParameter(command._getPlan);
			if (plan == null) return new OperationResult<SemesterPlan>(new SemesterPlanNotFoundError().ToString());
			Teacher? teacher = await _repository.GetByParameter(command._getTeacher);
			if (teacher == null) return new OperationResult<SemesterPlan>(new TeacherNotFoundError().ToString());
			Result attachment = plan.AttachTeacher(teacher);
			if (attachment.IsFailure) return new OperationResult<SemesterPlan>(attachment.Error);
			await _repository.Commit();
			return new OperationResult<SemesterPlan>(plan);
		}
	}
}
