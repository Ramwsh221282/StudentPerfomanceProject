using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

public interface IControlWeekGroupDocument
{
    public Task RegisterGroup(StudentGroup group);

    public Task UnregisterGroup(StudentGroup group);

    public Task<bool> ShouldGroupSemesterIncrease(StudentGroup group);
}
