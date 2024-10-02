using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class TeacherDublicateError : Error
{
	public TeacherDublicateError(string name, string surname, string thirdname, string jobtitle, string condition)
	{
		error = $@"Преподаватель с такими данными уже существует
		{name}
		{surname}
		{thirdname}
		{jobtitle}
		{condition}";
	}
	public override string ToString() => error;
}
