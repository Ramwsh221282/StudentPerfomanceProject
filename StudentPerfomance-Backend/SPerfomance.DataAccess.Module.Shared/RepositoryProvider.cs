using SPerfomance.DataAccess.Module.Shared.Repositories.Disciplines;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.DataAccess.Module.Shared.Repositories.Students;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared;

public static class RepositoryProvider
{
	public static IRepository<EducationDirection> CreateDirectionsRepository() => new EducationDirectionsRepository();
	public static IRepository<EducationPlan> CreateEducationPlansRepository() => new EducationPlansRepository();
	public static IRepository<Semester> CreateSemestersRepository() => new SemestersRepository();
	public static IRepository<SemesterPlan> CreateSemesterPlansRepository() => new SemesterPlansRepository();
	public static IRepository<Discipline> CreateDisciplinesRepository() => new DisciplineRepository();
	public static IRepository<Teacher> CreateTeachersRepository() => new TeachersRepository();
	public static IRepository<TeachersDepartment> CreateDepartmentsRepository() => new TeachersDepartmentsRepository();
	public static IRepository<Student> CreateStudentsRepository() => new StudentsRepository();
	public static IRepository<StudentGroup> CreateStudentGroupsRepository() => new StudentGroupsRepository();
}
