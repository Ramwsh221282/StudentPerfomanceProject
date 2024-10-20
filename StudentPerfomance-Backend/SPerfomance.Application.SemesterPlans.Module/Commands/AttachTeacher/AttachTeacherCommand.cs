using CSharpFunctionalExtensions;

using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Commands.AttachTeacher;

internal sealed class AttachTeacherCommand : ICommand
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly SemesterPlanSchema _plan;
	private readonly IRepositoryExpression<Semester> _getSemester;
	private readonly IRepositoryExpression<Teacher> _getTeacher;

	public ICommandHandler<AttachTeacherCommand, SemesterPlan> Handler;

	public AttachTeacherCommand(SemesterPlanSchema plan, TeacherSchema teacher, string token)
	{
		_plan = plan;
		_repository = new SemesterPlansQueryRepository();
		_getSemester = ExpressionsFactory.GetSemester(plan.Semester.ToRepositoryObject());
		_getTeacher = ExpressionsFactory.GetTeacher(teacher.ToRepositoryObject());
		Handler = new VerificationHandler<AttachTeacherCommand, SemesterPlan>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<AttachTeacherCommand, SemesterPlan>
	{
		private readonly SemesterPlansQueryRepository _repository;

		public CommandHandler(
			ICommandHandler<AttachTeacherCommand, SemesterPlan> handler,
			SemesterPlansQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<SemesterPlan>> Handle(AttachTeacherCommand command)
		{
			OperationResult<SemesterPlan> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Semester? semester = await _repository.GetByParameter(command._getSemester);
			if (semester == null)
				return new OperationResult<SemesterPlan>(new SemesterNotFoundError().ToString());

			Teacher? teacher = await _repository.GetByParameter(command._getTeacher);
			if (teacher == null)
				return new OperationResult<SemesterPlan>(new TeacherNotFoundError().ToString());

			Result<SemesterPlan> getPlan = semester.GetContract(command._plan.Discipline);
			if (getPlan.IsFailure)
				return new OperationResult<SemesterPlan>(new SemesterPlanNotFoundError().ToString());

			Result attachment = getPlan.Value.AttachTeacher(teacher);
			if (attachment.IsFailure)
				return new OperationResult<SemesterPlan>(attachment.Error);

			await _repository.Commit();
			return new OperationResult<SemesterPlan>(getPlan.Value);
		}
	}
}
