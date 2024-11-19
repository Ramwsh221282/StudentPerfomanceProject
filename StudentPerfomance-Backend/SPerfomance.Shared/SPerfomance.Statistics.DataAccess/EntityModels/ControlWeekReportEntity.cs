using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class ControlWeekReportEntity
{
    public int RowNumber { get; set; }
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public List<GroupStatisticsReportEntity> GroupParts { get; set; } = [];
    public List<CourseStatisticsReportEntity> CourseParts { get; set; } = [];
    public List<DirectionTypeStatisticsReportEntity> DirectionTypeReport { get; set; } = [];
    public List<DirectionCodeStatisticsReportEntity> DirectionCodeReport { get; set; } = [];

    public void SetEntityNumber(int number) => RowNumber = number;

    public static ControlWeekReportEntity CreateReport(AssignmentSessionView view)
    {
        ControlWeekReportEntity entity = new ControlWeekReportEntity
        {
            Id = view.Id,
            RowNumber = view.Number,
            CreationDate = DateTime.Parse(view.StartDate),
            CompletionDate = DateTime.Parse(view.EndDate),
        };

        entity.GroupParts = GroupStatisticsReportEntity.CreateReport(entity, view.Weeks);
        entity.CourseParts = CourseStatisticsReportEntity.CreateReport(entity, entity.GroupParts);
        entity.DirectionTypeReport = DirectionTypeStatisticsReportEntity.CreateReport(
            entity,
            entity.CourseParts
        );

        entity.DirectionCodeReport = DirectionCodeStatisticsReportEntity.CreateReport(
            entity,
            entity.GroupParts
        );

        return entity;
    }
}
