using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Api.Module.Responses.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Commands.Create;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupCreationFacade(StudentsGroupSchema schema) : IFacade<StudentGroupResponse>
{
	private readonly StudentsGroupSchema _schema = schema;
	public async Task<ActionResult<StudentGroupResponse>> Process()
	{
		StudentGroupsRepositoryObject checkDublicate = StudentGroupSchemaConverter.ToRepositoryObject(_schema);
		IService<StudentGroup> service = new StudentGroupCreationService
		(
			_schema,
			StudentGroupsRepositoryExpressionFactory.CreateHasGroupExpression(checkDublicate),
			EducationPlanExpressionsFactory.CreateFindPlan(checkDublicate.EducationPlan),
			RepositoryProvider.CreateStudentGroupsRepository(),
			RepositoryProvider.CreateEducationPlansRepository()
		);
		return StudentGroupResponse.FromResult(await service.DoOperation());
	}
}
