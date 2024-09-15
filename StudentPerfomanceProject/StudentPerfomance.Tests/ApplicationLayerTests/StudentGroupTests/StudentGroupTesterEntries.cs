namespace StudentPerfomance.Tests.ApplicationLayerTests.StudentGroupTests;

public sealed class StudentGroupTesterEntries
{
	public class SuccessScenarios
	{
		private StudentGroupTester tester = new StudentGroupTester();
		public static SuccessScenarios CreateSuccessScenarios() => new SuccessScenarios();
		public void Reset() => tester = new StudentGroupTester();
		public void InvokeCreationEntry()
		{
			string entryName = "E Created";
			tester.BuildCreationSchema(entryName);
			tester.BuildAndProcessCreationTest();
			tester.BuildDeletionSchema(entryName);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokeDeletionEntry()
		{
			string entryName = "E Deleted";
			tester.BuildCreationSchema(entryName);
			tester.BuildAndProcessCreationTest();
			tester.BuildDeletionSchema(entryName);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokeUpdateEntry()
		{
			string entryOld = "E Old";
			string entryNew = "E New";
			tester.BuildCreationSchema(entryOld);
			tester.BuildAndProcessCreationTest();
			tester.BuildUpdateOldSchema(entryOld);
			tester.BuildUpdateNewSchema(entryNew);
			tester.BuildAndProcessNameChangeTest();
			tester.BuildDeletionSchema(entryNew);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokeGetByNameEntry()
		{
			string groupName = "E By Name";
			tester.BuildCreationSchema(groupName);
			tester.BuildGetByNameSchema(groupName);
			tester.BuildAndProcessCreationTest();
			tester.BuildAndProcessGetByNameTest();
			tester.BuildDeletionSchema(groupName);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokePaginationlEntry()
		{
			string[] schemaNameParams = ["E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10", "E11", "E12", "E13", "E14"];
			for (int index = 0; index < schemaNameParams.Length; index++)
			{
				tester.BuildCreationSchema(schemaNameParams[index]);
				tester.BuildAndProcessCreationTest();
			}
			tester.BuildPaginationTuple(1, schemaNameParams.Length);
			tester.BuildAndProcessPaginationTest();
		}

		public void InvokeFilterEntry()
		{
			string[] schemaNameParams = ["Entry One", "Entry Two", "BEntry One", "BEntry Two"];
			for (int index = 0; index < schemaNameParams.Length; index++)
			{
				tester.BuildCreationSchema(schemaNameParams[index]);
				tester.BuildAndProcessCreationTest();
			}
			tester.BuildFilterSchema("BE", 1, 14);
			tester.BuildAndProcessFilterTest();
			for (int index = 0; index < schemaNameParams.Length; index++)
			{
				tester.BuildDeletionSchema(schemaNameParams[index]);
				tester.BuildAndProcessDeletionTest();
			}
		}

		public void InvokeFilterStartsWithNameEntry()
		{
			string[] schemaNameParams = ["Entry One", "Entry Two", "BEntry One", "BEntry Two"];
			for (int index = 0; index < schemaNameParams.Length; index++)
			{
				tester.BuildCreationSchema(schemaNameParams[index]);
				tester.BuildAndProcessCreationTest();
			}
			tester.BuildStartsWithNameSchema("En");
			tester.BuildAndProcessStartsWithNameFilterTest();
			for (int index = 0; index < schemaNameParams.Length; index++)
			{
				tester.BuildDeletionSchema(schemaNameParams[index]);
				tester.BuildAndProcessDeletionTest();
			}
		}
	}

	public class FailureScenarios
	{
		private StudentGroupTester tester = new StudentGroupTester();
		public void Reset() => tester = new StudentGroupTester();
		public static FailureScenarios CreateFailure() => new FailureScenarios();
		public void InvokeCreationEntry()
		{
			string entryName = "E Created";
			tester.BuildCreationSchema(entryName);
			tester.BuildAndProcessCreationTest();
			tester.BuildCreationSchema(entryName);
			tester.BuildAndProcessCreationTest();
			string emptyName = "";
			tester.BuildCreationSchema(emptyName);
			tester.BuildAndProcessCreationTest();
			string invalidName = "выфвыфывфывлыждфывжыдфлыфжвлыфвжыфдыфл";
			tester.BuildCreationSchema(invalidName);
			tester.BuildAndProcessCreationTest();
			tester.BuildDeletionSchema(entryName);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokeDeletionEntry()
		{
			string entryName = "E Deleted";
			tester.BuildCreationSchema(entryName);
			tester.BuildAndProcessCreationTest();
			string notExistedName = "Deleted E";
			tester.BuildDeletionSchema(notExistedName);
			tester.BuildAndProcessDeletionTest();
			string emptyName = "";
			tester.BuildDeletionSchema(emptyName);
			tester.BuildAndProcessDeletionTest();
			string invalidName = "выфвыывфвыффвыжлвыфофывфывовфдфыовдлфы";
			tester.BuildDeletionSchema(invalidName);
			tester.BuildAndProcessDeletionTest();
			tester.BuildDeletionSchema(entryName);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokeUpdateEntry()
		{
			string entryOld = "E Old";
			string entryNew = "E Old";
			tester.BuildCreationSchema(entryOld);
			tester.BuildAndProcessCreationTest();
			tester.BuildUpdateOldSchema(entryOld);
			tester.BuildUpdateNewSchema(entryNew);
			tester.BuildAndProcessNameChangeTest();
			tester.BuildDeletionSchema(entryNew);
			tester.BuildAndProcessDeletionTest();
			string entryOldNew = "E Old New";
			tester.BuildCreationSchema(entryOldNew);
			tester.BuildAndProcessCreationTest();
			string emptyNewName = "";
			tester.BuildUpdateOldSchema(entryOldNew);
			tester.BuildUpdateNewSchema(emptyNewName);
			tester.BuildAndProcessNameChangeTest();
			tester.BuildDeletionSchema(entryOldNew);
			tester.BuildAndProcessDeletionTest();
		}
		public void InvokeGetByNameEntry()
		{
			string groupName = "E By Name";
			tester.BuildCreationSchema(groupName);
			tester.BuildGetByNameSchema("E");
			tester.BuildAndProcessCreationTest();
			tester.BuildAndProcessGetByNameTest();
			tester.BuildDeletionSchema(groupName);
			tester.BuildAndProcessDeletionTest();
		}
	}
}
