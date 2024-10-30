using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;

public class GetStudentGroupQueryHandler
(
	IStudentGroupsRepository repository
) : IQueryHandler<GetStudentGroupQuery, StudentGroup>
{
	private readonly IStudentGroupsRepository _repository = repository;

	public async Task<Result<StudentGroup>> Handle(GetStudentGroupQuery command)
	{
		if (string.IsNullOrWhiteSpace(command.Name))
			return Result<StudentGroup>.Failure(StudentGroupErrors.NameEmpty());

		StudentGroup? requested = await _repository.Get(command.Name);
		return requested == null ?
			Result<StudentGroup>.Failure(StudentGroupErrors.NotFound()) :
			Result<StudentGroup>.Success(requested);
	}
}
