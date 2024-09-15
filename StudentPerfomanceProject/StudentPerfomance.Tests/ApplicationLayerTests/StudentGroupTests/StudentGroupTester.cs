using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupTester
{
	private StudentsGroupSchema? _creationSchema;
	private StudentsGroupSchema? _deletionSchema;
	private StudentsGroupSchema? _updateOldSchema;
	private StudentsGroupSchema? _updateNewSchema;
	private (int, int) _paginationTuple;
	private StudentsGroupSchema? _getByNameSchema;
	private (StudentsGroupSchema, int, int) _filterSchema;
	private StudentsGroupSchema? _filterByStartsWithNameSchema;

	public StudentGroupTester BuildCreationSchema(string name)
	{
		_creationSchema = new StudentsGroupSchema(name);
		return this;
	}

	public StudentGroupTester BuildDeletionSchema(string name)
	{
		_deletionSchema = new StudentsGroupSchema(name);
		return this;
	}

	public StudentGroupTester BuildUpdateOldSchema(string name)
	{
		_updateOldSchema = new StudentsGroupSchema(name);
		return this;
	}

	public StudentGroupTester BuildUpdateNewSchema(string name)
	{
		_updateNewSchema = new StudentsGroupSchema(name);
		return this;
	}

	public StudentGroupTester BuildPaginationTuple(int page, int pageSize)
	{
		_paginationTuple = (page, pageSize);
		return this;
	}

	public StudentGroupTester BuildGetByNameSchema(string name)
	{
		_getByNameSchema = new StudentsGroupSchema(name);
		return this;
	}
	public StudentGroupTester BuildFilterSchema(string name, int page, int pageSize)
	{
		_filterSchema = (new StudentsGroupSchema(name), page, pageSize);
		return this;
	}
	public StudentGroupTester BuildStartsWithNameSchema(string name)
	{
		_filterByStartsWithNameSchema = new StudentsGroupSchema(name);
		return this;
	}

	public void BuildAndProcessCreationTest()
	{
		if (_creationSchema == null)
		{
			Console.WriteLine("Creation schema was not build");
			return;
		}
		new StudentGroupCreateTest(_creationSchema)
		.DoOperation().Wait();
	}

	public void BuildAndProcessDeletionTest()
	{
		if (_deletionSchema == null)
		{
			Console.WriteLine("Deletion schema was not build");
			return;
		}
		new StudentGroupDeleteTest(_deletionSchema)
		.DoOperation().Wait();
	}

	public void BuildAndProcessNameChangeTest()
	{
		if (_updateOldSchema == null || _updateNewSchema == null)
		{
			Console.WriteLine("Old or New schemas were not build");
			return;
		}
		var result = new StudentGroupChangeDataTest(_updateOldSchema, _updateNewSchema)
		.DoOperation().Result;
		if (!result.IsFailed && result.Result != null)
			Console.WriteLine($"Changed group name: {result.Result.Name.Name}");
	}

	public void BuildAndProcessPaginationTest()
	{
		if (_paginationTuple.Item1 == default || _paginationTuple.Item2 == default)
		{
			Console.WriteLine("Page or page size were not set");
			return;
		}
		StudentGroupPaginationTest paginationTest = new StudentGroupPaginationTest(_paginationTuple.Item1, _paginationTuple.Item2);
		OperationResult<IReadOnlyCollection<StudentGroup>> result = paginationTest.DoOperation().Result;
		if (!result.IsFailed && result.Result != null)
		{
			Console.WriteLine($"Groups count: {result.Result.Count}");
			foreach (var group in result.Result)
			{
				Console.WriteLine(group.Name.Name);
			}
			foreach (var group in result.Result)
			{
				BuildDeletionSchema(group.Name.Name);
				BuildAndProcessDeletionTest();
			}
		}
	}

	public void BuildAndProcessGetByNameTest()
	{
		if (_getByNameSchema == null)
		{
			Console.WriteLine("Get by name schema was not built");
			return;
		}
		StudentGroupByNameTest test = new StudentGroupByNameTest(_getByNameSchema);
		var result = test.DoOperation().Result;
		Console.WriteLine($"Get by name entry param: {_getByNameSchema.Name}");
		if (result.Result != null)
			Console.WriteLine($"Search result: {result.Result.Name.Name}");
		else
			Console.WriteLine($"Nothing found with entry param: {_getByNameSchema.Name}");
	}

	public void BuildAndProcessFilterTest() =>
		new StudentGroupByFilterTest(_filterSchema.Item1, _filterSchema.Item2, _filterSchema.Item3).DoOperation().Wait();
	public void BuildAndProcessStartsWithNameFilterTest()
	{
		if (_filterByStartsWithNameSchema == null)
		{
			Console.WriteLine("Filter by starts with name was not built");
			return;
		}
		new StudentGroupByNameParamTest(_filterByStartsWithNameSchema).DoOperation().Wait();
	}
}
