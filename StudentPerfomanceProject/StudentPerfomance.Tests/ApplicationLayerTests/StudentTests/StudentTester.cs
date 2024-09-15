using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentTests;

public class StudentTester
{
	public class SuccessScenarios
	{
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();

		public void TestStudentCreation()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			new StudentDeleteTest(student).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void TestStudentCreationWithoutThirdname()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, null, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			new StudentDeleteTest(student).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void TestStudentDeletion()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			new StudentDeleteTest(student).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void TestStudentDataChangeOfName()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			string newStudenName = "John";
			StudentSchema newStudent = new StudentSchema(newStudenName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentUpdateTest(student, newStudent, group).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void TestStudentDataChangeOfSurname()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			string newStudenSurname = "Armstrong";
			StudentSchema newStudent = new StudentSchema(studentName, newStudenSurname, studentThirdname, studentState, recordBook);
			new StudentUpdateTest(student, newStudent, group).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void TestStudentDataChangeOfThirdname()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			string newStudenThirdname = "Armstrongovich";
			StudentSchema newStudent = new StudentSchema(studentName, studentSurname, newStudenThirdname, studentState, recordBook);
			new StudentUpdateTest(student, newStudent, group).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void TestStudentDataChangeOfState()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			string newStudentState = "Неактивен";
			StudentSchema newStudent = new StudentSchema(studentName, studentSurname, studentThirdname, newStudentState, recordBook);
			new StudentUpdateTest(student, newStudent, group).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void TestStudentDataChangeRecordbook()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			ulong newRecordBook = 321;
			StudentSchema newStudent = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, newRecordBook);
			new StudentUpdateTest(student, newStudent, group).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void TestStudentDataChangeGroup()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			string newGroupName = "Group 2";
			StudentsGroupSchema newGroup = new StudentsGroupSchema(newGroupName);
			new StudentGroupCreateTest(newGroup).DoOperation().Wait();
			StudentSchema newStudent = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentUpdateTest(student, newStudent, newGroup).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
			new StudentGroupDeleteTest(newGroup).DoOperation().Wait();
		}

		public void TestStudentDataChangeEntireStudent()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Активен";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			string newGroupName = "Group 2";
			StudentsGroupSchema newGroup = new StudentsGroupSchema(newGroupName);
			new StudentGroupCreateTest(newGroup).DoOperation().Wait();
			string newStudentName = "John";
			string newStudentSurname = "Armstrong";
			string newStudentThirdName = "Armstrongovich";
			string newStudentState = "Неактивен";
			ulong newRecordBook = 321;
			StudentSchema newStudent = new StudentSchema(newStudentName, newStudentSurname, newStudentThirdName, newStudentState, newRecordBook);
			new StudentUpdateTest(student, newStudent, newGroup).DoOperation().Wait();
			new StudentDeleteTest(newStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
			new StudentGroupDeleteTest(newGroup).DoOperation().Wait();
		}

		public void TestStudentPagination()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			StudentSchema[] students =
			[
				new StudentSchema("Name1", "Surname1", "Thirdname1", "Активен", 1),
				new StudentSchema("Name2", "Surname2", "Thirdname2", "Активен", 2),
				new StudentSchema("Name3", "Surname3", "Thirdname3", "Активен", 3),
				new StudentSchema("Name4", "Surname4", "Thirdname4", "Активен", 4),
				new StudentSchema("Name5", "Surname5", "Thirdname5", "Активен", 5),
				new StudentSchema("Name6", "Surname6", "Thirdname6", "Активен", 6),
				new StudentSchema("Name7", "Surname7", "Thirdname7", "Активен", 7),
				new StudentSchema("Name8", "Surname8", "Thirdname8", "Активен", 8),
				new StudentSchema("Name9", "Surname9", "Thirdname9", "Активен", 9),
				new StudentSchema("Name10", "Surname10", "Thirdname10", "Активен", 10),
				new StudentSchema("Name11", "Surname11", "Thirdname11", "Активен", 11),
				new StudentSchema("Name12", "Surname12", "Thirdname12", "Активен", 12),
				new StudentSchema("Name13", "Surname13", "Thirdname13", "Активен", 13),
				new StudentSchema("Name14", "Surname14", "Thirdname14", "Активен", 14),
			];
			foreach (var student in students)
			{
				new StudentCreateTest(student, group).DoOperation().Wait();
				Console.WriteLine("Student created");
			}
			new StudentByPageTest(1, 14, group).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}

		public void TestStudentPaginationWithOtherGroup()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string groupNameOther = "Group 2";
			StudentsGroupSchema groupOther = new StudentsGroupSchema(groupNameOther);
			new StudentGroupCreateTest(groupOther).DoOperation().Wait();
			StudentSchema[] students =
			[
				new StudentSchema("Name1", "Surname1", "Thirdname1", "Активен", 1),
				new StudentSchema("Name2", "Surname2", "Thirdname2", "Активен", 2),
				new StudentSchema("Name3", "Surname3", "Thirdname3", "Активен", 3),
				new StudentSchema("Name4", "Surname4", "Thirdname4", "Активен", 4),
				new StudentSchema("Name5", "Surname5", "Thirdname5", "Активен", 5),
				new StudentSchema("Name6", "Surname6", "Thirdname6", "Активен", 6),
				new StudentSchema("Name7", "Surname7", "Thirdname7", "Активен", 7),
				new StudentSchema("Name8", "Surname8", "Thirdname8", "Активен", 8),
				new StudentSchema("Name9", "Surname9", "Thirdname9", "Активен", 9),
				new StudentSchema("Name10", "Surname10", "Thirdname10", "Активен", 10),
				new StudentSchema("Name11", "Surname11", "Thirdname11", "Активен", 11),
				new StudentSchema("Name12", "Surname12", "Thirdname12", "Активен", 12),
				new StudentSchema("Name13", "Surname13", "Thirdname13", "Активен", 13),
				new StudentSchema("Name14", "Surname14", "Thirdname14", "Активен", 14),
			];
			foreach (var student in students)
			{
				new StudentCreateTest(student, group).DoOperation().Wait();
				Console.WriteLine("Student created");
			}
			new StudentByPageTest(1, 14, group).DoOperation().Wait();
			new StudentByPageTest(1, 14, groupOther).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
			new StudentGroupDeleteTest(groupOther).DoOperation().Wait();
		}

		public void TestStudentPaginationWithPageAndFilter()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string groupNameOther = "Group 2";
			StudentsGroupSchema groupOther = new StudentsGroupSchema(groupNameOther);
			new StudentGroupCreateTest(groupOther).DoOperation().Wait();
			StudentSchema[] students =
			[
				new StudentSchema("Иван", "Иванов", "Иванович", "Активен", 1),
				new StudentSchema("Мария", "Петрова", "Сергеевна", "Активен", 2),
				new StudentSchema("Алексей", "Сидоров", "Павлович", "Активен", 3),
				new StudentSchema("Анна", "Смирнова", "Викторовна", "Активен", 4),
				new StudentSchema("Дмитрий", "Кузнецов", "Андреевич", "Активен", 5),
				new StudentSchema("Екатерина", "Попова", "Алексеевна", "Активен", 6),
				new StudentSchema("Сергей", "Васильев", "Владимирович", "Активен", 7),
				new StudentSchema("Ольга", "Фёдоровна", "Николаевна", "Активен", 8),
				new StudentSchema("Максим", "Михайлов", "Вячеславович", "Активен", 9),
				new StudentSchema("Татьяна", "Николаева", "Геннадьевна", "Активен", 10),
				new StudentSchema("Кирилл", "Романов", "Олегович", "Активен", 11),
				new StudentSchema("Дарья", "Лебедева", "Евгеньевна", "Активен", 12),
				new StudentSchema("Виктор", "Орлов", "Петрович", "Активен", 13),
				new StudentSchema("Игорь", "Королев", "Владиславович", "Активен", 14),
			];
			foreach (var student in students)
			{
				new StudentCreateTest(student, group).DoOperation().Wait();
				Console.WriteLine("Student created");
			}
			StudentSchema filterStudent = new StudentSchema("А", "", "", "", 0);
			new StudentByFilterTest(1, 14, filterStudent, group).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
			new StudentGroupDeleteTest(groupOther).DoOperation().Wait();
		}
	}

	public class FailureScenarios
	{
		public static FailureScenarios CreateFaliureScenarios() => new FailureScenarios();

		public void TryCreateDublicate()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string firstStudentName = "Name1";
			string firstStudentSurname = "Surname1";
			string firstStudentThirdname = "Thirdname1";
			string firstStudentState = "Активен";
			ulong firstStudentrecordBook = 123;
			StudentSchema firstStudent = new StudentSchema
			(
				firstStudentName,
				firstStudentSurname,
				firstStudentThirdname,
				firstStudentState,
				firstStudentrecordBook
			);
			new StudentCreateTest(firstStudent, group).DoOperation().Wait();
			string secondStudentName = "Name1";
			string secondStudentSurname = "Surname1";
			string secondStudentThirdname = "Thirdname1";
			string secondStudentState = "Активен";
			ulong secondStudentrecordBook = 123;
			StudentSchema secondStudent = new StudentSchema
			(
				secondStudentName,
				secondStudentSurname,
				secondStudentThirdname,
				secondStudentState,
				secondStudentrecordBook
			);
			new StudentCreateTest(secondStudent, group).DoOperation().Wait();
			new StudentDeleteTest(firstStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void TryCreateWithSameRecordBook()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string firstStudentName = "Name1";
			string firstStudentSurname = "Surname1";
			string firstStudentThirdname = "Thirdname1";
			string firstStudentState = "Активен";
			ulong firstStudentrecordBook = 123;
			StudentSchema firstStudent = new StudentSchema
			(
				firstStudentName,
				firstStudentSurname,
				firstStudentThirdname,
				firstStudentState,
				firstStudentrecordBook
			);
			new StudentCreateTest(firstStudent, group).DoOperation().Wait();
			string secondStudentName = "Name2";
			string secondStudentSurname = "Surname2";
			string secondStudentThirdname = "Thirdname2";
			string secondStudentState = "Активен";
			ulong secondStudentrecordBook = 123;
			StudentSchema secondStudent = new StudentSchema
			(
				secondStudentName,
				secondStudentSurname,
				secondStudentThirdname,
				secondStudentState,
				secondStudentrecordBook
			);
			new StudentCreateTest(secondStudent, group).DoOperation().Wait();
			new StudentDeleteTest(firstStudent).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
		public void TryCreateWithInvalidState()
		{
			string groupName = "Group 1";
			StudentsGroupSchema group = new StudentsGroupSchema(groupName);
			new StudentGroupCreateTest(group).DoOperation().Wait();
			string studentName = "Name1";
			string studentSurname = "Surname1";
			string studentThirdname = "Thirdname1";
			string studentState = "Строка";
			ulong recordBook = 123;
			StudentSchema student = new StudentSchema(studentName, studentSurname, studentThirdname, studentState, recordBook);
			new StudentCreateTest(student, group).DoOperation().Wait();
			new StudentDeleteTest(student).DoOperation().Wait();
			new StudentGroupDeleteTest(group).DoOperation().Wait();
		}
	}
}
