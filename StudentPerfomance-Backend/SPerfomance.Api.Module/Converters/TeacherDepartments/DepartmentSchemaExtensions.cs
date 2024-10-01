using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;

namespace SPerfomance.Api.Module.Converters.TeacherDepartments;

public static class DepartmentSchemaExtensions
{
	public static DepartmentRepositoryObject ToRepositoryObject(this DepartmentSchema schema)
	{
		DepartmentRepositoryObject parameter = new DepartmentRepositoryObject().WithName(schema.FullName);
		return parameter;
	}
}
