using LiteDB.Async;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.ControlWeekDocuments.Documents;

public class GroupControlWeekDocument : IControlWeekGroupDocument
{
    private const string Connection = @"Filename=group_control_week_document.db; Connection=shared";
    private const string Collection = "group_control_week";

    public async Task RegisterGroup(StudentGroup group)
    {
        using var db = new LiteDatabaseAsync(Connection);
        var collection = db.GetCollection<GroupControlWeekDocumentData>(Collection);

        var documents = await collection.FindAsync(d => d.GroupName == group.Name.Name);
        if (documents.Count() == 2)
            return;

        var document = new GroupControlWeekDocumentData(group);
        await collection.InsertAsync(document);
    }

    public async Task UnregisterGroup(StudentGroup group)
    {
        using var db = new LiteDatabaseAsync(Connection);
        var collection = db.GetCollection<GroupControlWeekDocumentData>(Collection);
        await collection.DeleteManyAsync(d => d.GroupName == group.Name.Name);
    }

    public async Task<bool> ShouldGroupSemesterIncrease(StudentGroup group)
    {
        using var db = new LiteDatabaseAsync(Connection);
        var collection = db.GetCollection<GroupControlWeekDocumentData>(Collection);
        var documents = await collection.FindAsync(d => d.GroupName == group.Name.Name);
        return documents.Count() == 2;
    }
}
