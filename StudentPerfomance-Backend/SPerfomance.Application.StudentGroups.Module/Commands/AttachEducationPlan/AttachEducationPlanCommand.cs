using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Application.StudentGroups.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Commands.AttachEducationPlan;

internal sealed class AttachEducationPlanCommand : ICommand
{
	private readonly IRepositoryExpression<EducationPlan> _getPlan;
	private readonly IRepositoryExpression<StudentGroup> _getGroup;
	private readonly StudentGroupQueryRepository _repository;

	public readonly ICommandHandler<AttachEducationPlanCommand, StudentGroup> Handler;

	public AttachEducationPlanCommand(StudentsGroupSchema group, EducationPlanSchema plan)
	{
		_getGroup = ExpressionsFactory.GetByName(group.ToRepositoryObject());
		_getPlan = ExpressionsFactory.GetPlan(plan.ToRepositoryObject());
		_repository = new StudentGroupQueryRepository();
		Handler = new DefaultCommandHandler(_repository);
		Handler = new AttachEducationPlanHandler(Handler, _repository);
		Handler = new CommitHandler(Handler, _repository);
	}

	internal abstract class DecoratedHandler(ICommandHandler<AttachEducationPlanCommand, StudentGroup> handler)
	: ICommandHandler<AttachEducationPlanCommand, StudentGroup>
	{
		private readonly ICommandHandler<AttachEducationPlanCommand, StudentGroup> _handler = handler;

		public virtual async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command) => await _handler.Handle(command);
	}

	internal sealed class DefaultCommandHandler(StudentGroupQueryRepository repository) : ICommandHandler<AttachEducationPlanCommand, StudentGroup>
	{
		private readonly StudentGroupQueryRepository _repository = repository;

		public async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command)
		{
			StudentGroup? group = await _repository.GetByParameter(command._getGroup);
			return group == null ?
				new OperationResult<StudentGroup>(new GroupNotFoundError().ToString()) :
				new OperationResult<StudentGroup>(group);
		}
	}

	internal sealed class AttachEducationPlanHandler : DecoratedHandler
	{
		private readonly StudentGroupQueryRepository _repository;

		public AttachEducationPlanHandler(ICommandHandler<AttachEducationPlanCommand, StudentGroup> handler, StudentGroupQueryRepository repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command)
		{
			OperationResult<StudentGroup> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			EducationPlan? plan = await _repository.GetByParameter(command._getPlan);
			if (plan == null)
				return new OperationResult<StudentGroup>(new EducationPlanNotFoundError().ToString());

			Result attach = result.Result.AttachEducationPlan(plan);
			return attach.IsFailure ?
				new OperationResult<StudentGroup>(attach.Error) :
				result;
		}
	}

	internal sealed class CommitHandler : DecoratedHandler
	{
		private readonly StudentGroupQueryRepository _repository;

		public CommitHandler(ICommandHandler<AttachEducationPlanCommand, StudentGroup> handler, StudentGroupQueryRepository repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<StudentGroup>> Handle(AttachEducationPlanCommand command)
		{
			OperationResult<StudentGroup> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			await _repository.Commit();
			return result;
		}
	}
}
