using SPerfomance.Api.Module.Converters.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;

namespace SPerfomance.Api.Module.Converters.Students;

public static class StudentSchemaConverter
{
	public static StudentsRepositoryObject ToRepositoryObject(this StudentSchema schema)
	{
		StudentGroupsRepositoryObject group = schema.Group.ToRepositoryObject();
		StudentsRepositoryObject student = new StudentsRepositoryObject()
		.WithName(schema.Name)
		.WithSurname(schema.Surname)
		.WithThirdname(schema.Thirdname)
		.WithRecordbook(schema.Recordbook)
		.WithState(schema.State)
		.WithGroup(group);
		return student;
	}
}
