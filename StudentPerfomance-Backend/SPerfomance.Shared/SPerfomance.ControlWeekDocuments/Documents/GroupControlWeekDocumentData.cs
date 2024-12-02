using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.ControlWeekDocuments.Documents;

public class GroupControlWeekDocumentData
{
    public string GroupName { get; set; } = string.Empty;

    public GroupControlWeekDocumentData() { }

    public GroupControlWeekDocumentData(StudentGroup group)
    {
        GroupName = group.Name.Name;
    }
}
