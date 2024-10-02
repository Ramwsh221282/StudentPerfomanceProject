using SPerfomance.Api.Module.Converters.TeacherDepartments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;

namespace SPerfomance.Api.Module.Converters.Teachers;

public static class TeacherSchemaConverter
{
	public static TeacherRepositoryObject ToRepositoryObject(this TeacherSchema teacher)
	{
		DepartmentRepositoryObject department = teacher.Department.ToRepositoryObject();
		TeacherRepositoryObject parameter = new TeacherRepositoryObject()
		.WithName(teacher.Name)
		.WithSurname(teacher.Surname)
		.WithThirdname(teacher.Thirdname)
		.WithJobTitle(teacher.Job)
		.WithWorkingCondition(teacher.Condition)
		.WithDepartment(department);
		return parameter;
	}
}
