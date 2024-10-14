using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Application.Teachers.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.GetDepartmentTeachers;

internal sealed class GetDepartmentTeachersQuery : IQuery
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression;
	private readonly TeacherQueryRepository _repository;

	public readonly IQueryHandler<GetDepartmentTeachersQuery, IReadOnlyCollection<Teacher>> Handler;

	public GetDepartmentTeachersQuery(DepartmentSchema department)
	{
		_expression = ExpressionsFactory.GetDepartment(department.ToRepositoryObject());
		_repository = new TeacherQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler : IQueryHandler<GetDepartmentTeachersQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository;

		public QueryHandler(TeacherQueryRepository repository) => _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetDepartmentTeachersQuery query)
		{
			TeachersDepartment? department = await _repository.GetByParameter(query._expression);
			if (department == null)
				return new OperationResult<IReadOnlyCollection<Teacher>>(new DepartmentNotFountError().ToString());

			return new OperationResult<IReadOnlyCollection<Teacher>>(department.Teachers);
		}
	}
}
