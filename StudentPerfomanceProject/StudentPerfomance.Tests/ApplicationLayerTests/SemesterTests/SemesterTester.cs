using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.SemesterTests;

public sealed class SemesterTester
{
	public sealed class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScnearios() => new SuccessScenarios();

		public void SemesterValidCreationTest()
		{
			SemesterSchema semesterSchema = new SemesterSchema(1);
			StudentsGroupSchema groupSchema = new StudentsGroupSchema("БИТ 21-11");
			new StudentGroupCreateTest(groupSchema).DoOperation().Wait();
			new SemesterCreateTest(semesterSchema, groupSchema).DoOperation().Wait();
			new StudentGroupDeleteTest(groupSchema).DoOperation().Wait();
		}

		public void FewSemestersValidCreationTest()
		{
			SemesterSchema firstSemester = new SemesterSchema(1);
			SemesterSchema secondSemester = new SemesterSchema(2);
			StudentsGroupSchema groupSchema = new StudentsGroupSchema("БИТ 21-11");
			new StudentGroupCreateTest(groupSchema).DoOperation().Wait();
			new SemesterCreateTest(firstSemester, groupSchema).DoOperation().Wait();
			new SemesterCreateTest(secondSemester, groupSchema).DoOperation().Wait();
			new StudentGroupDeleteTest(groupSchema).DoOperation().Wait();
		}
		public void SemesterFilterTest()
		{
			SemesterSchema[] schemasGroupOne =
			[
				new SemesterSchema(1),
				new SemesterSchema(2),
				new SemesterSchema(3),
				new SemesterSchema(4),
			];
			SemesterSchema[] schemasGroupTwo =
			[
				new SemesterSchema(1),
				new SemesterSchema(2),
				new SemesterSchema(3),
				new SemesterSchema(4),
			];
			StudentsGroupSchema groupOne = new StudentsGroupSchema("Group One");
			StudentsGroupSchema groupTwo = new StudentsGroupSchema("Group Two");
			new StudentGroupCreateTest(groupOne).DoOperation().Wait();
			new StudentGroupCreateTest(groupTwo).DoOperation().Wait();
			foreach (var schema in schemasGroupOne)
			{
				new SemesterCreateTest(schema, groupOne).DoOperation().Wait();
			}
			foreach (var schema in schemasGroupTwo)
			{
				new SemesterCreateTest(schema, groupTwo).DoOperation().Wait();
			}
			StudentsGroupSchema filterSchema = new StudentsGroupSchema("Two");
			SemesterSchema semesterFilter = new SemesterSchema(0);
			new SemesterByFilterTest(1, 14, semesterFilter, filterSchema).DoOperation().Wait();
			new StudentGroupDeleteTest(groupOne).DoOperation().Wait();
			new StudentGroupDeleteTest(groupTwo).DoOperation().Wait();
		}

		public void SemesterPaginationTest()
		{
			SemesterSchema[] schemasGroupOne =
			[
				new SemesterSchema(1),
				new SemesterSchema(2),
				new SemesterSchema(3),
				new SemesterSchema(4),
			];
			StudentsGroupSchema groupSchema = new StudentsGroupSchema("БИТ 21-11");
			new StudentGroupCreateTest(groupSchema).DoOperation().Wait();
			foreach (var schema in schemasGroupOne)
			{
				new SemesterCreateTest(schema, groupSchema).DoOperation().Wait();
			}
			new SemesterPaginationTest(1, 3).DoOperation().Wait();
			new StudentGroupDeleteTest(groupSchema).DoOperation().Wait();
		}
	}

	public sealed class FailureScenarious
	{
		public static FailureScenarious CreateFailureScenarios() => new FailureScenarious();

		public void SemesterInvalidCreationTest()
		{
			SemesterSchema semester = new SemesterSchema(12);
			StudentsGroupSchema group = new StudentsGroupSchema("БИТ 21-11");
			new StudentGroupCreateTest(group).DoOperation().Wait();
			new SemesterCreateTest(semester, group).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void SemesterDublicateTest()
		{
			SemesterSchema firstSemester = new SemesterSchema(1);
			SemesterSchema dublicate = new SemesterSchema(1);
			StudentsGroupSchema groupSchema = new StudentsGroupSchema("БИТ 21-11");
			new StudentGroupCreateTest(groupSchema).DoOperation().Wait();
			new SemesterCreateTest(firstSemester, groupSchema).DoOperation().Wait();
			new SemesterCreateTest(dublicate, groupSchema).DoOperation().Wait();
			new StudentGroupDeleteTest(groupSchema).DoOperation().Wait();
		}
	}
}
