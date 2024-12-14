using System.Text.Json.Serialization;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed record ControlWeekReportDTO
{
    public int RowNumber { get; init; }
    public Guid Id { get; init; }
    public DateTime CreationDate { get; init; }
    public DateTime CompletionDate { get; init; }
    public GroupStatisticsReportDTO[] GroupParts { get; init; }
    public CourseStatisticsReportDTO[] CourseParts { get; init; }
    public DirectionTypeReportDTO[] DirectionTypeReport { get; init; }
    public DirectionCodeReportDTO[] DirectionCodeReport { get; init; }
    public double Average { get; init; }
    public double Perfomance { get; init; }

    [JsonInclude]
    public string Season { get; init; }

    [JsonInclude]
    public int Number { get; init; }

    public ControlWeekReportDTO(ControlWeekReportEntity report)
    {
        RowNumber = report.RowNumber;
        Id = report.Id;
        CreationDate = report.CreationDate;
        CompletionDate = report.CompletionDate;
        Season = report.ControlWeekSeason;
        Number = report.ControlWeekNumber;
        var groupTask = Task.Run(() => InitializeGroupReportArray(report.GroupParts));
        var courseTask = Task.Run(() => InitializeCourseReportArray(report.CourseParts));
        var directionTypeTask = Task.Run(
            () => InitializeDirectionTypeReportArray(report.DirectionTypeReport)
        );
        var directionCodeTask = Task.Run(
            () => InitializeDirectionCodeReportArray(report.DirectionCodeReport)
        );
        GroupParts = groupTask.Result;
        CourseParts = courseTask.Result;
        DirectionTypeReport = directionTypeTask.Result;
        DirectionCodeReport = directionCodeTask.Result;
        Average = DirectionCodeReport.Select(d => d.Average).Average();
        Perfomance = DirectionCodeReport.Select(d => d.Perfomance).Average();
    }

    private GroupStatisticsReportDTO[] InitializeGroupReportArray(
        List<GroupStatisticsReportEntity> groups
    )
    {
        int count = groups.Count;
        if (count == 0)
            return Array.Empty<GroupStatisticsReportDTO>();
        GroupStatisticsReportDTO[] report = new GroupStatisticsReportDTO[count];
        int left = 0;
        int right = count - 1;
        while (left <= right)
        {
            report[left] = new GroupStatisticsReportDTO(groups[left]);
            left++;
            if (left <= right)
            {
                report[right] = new GroupStatisticsReportDTO(groups[right]);
                right--;
            }
        }
        return report;
    }

    private CourseStatisticsReportDTO[] InitializeCourseReportArray(
        List<CourseStatisticsReportEntity> courses
    )
    {
        int count = courses.Count;
        if (count == 0)
            return Array.Empty<CourseStatisticsReportDTO>();
        CourseStatisticsReportDTO[] report = new CourseStatisticsReportDTO[count];
        int left = 0;
        int right = count - 1;

        while (left <= right)
        {
            report[left] = new CourseStatisticsReportDTO(courses[left]);
            left++;
            if (left <= right)
            {
                report[right] = new CourseStatisticsReportDTO(courses[right]);
                right--;
            }
        }
        return report;
    }

    private DirectionTypeReportDTO[] InitializeDirectionTypeReportArray(
        List<DirectionTypeStatisticsReportEntity> types
    )
    {
        int count = types.Count;
        if (count == 0)
            return Array.Empty<DirectionTypeReportDTO>();
        DirectionTypeReportDTO[] report = new DirectionTypeReportDTO[count];
        int left = 0;
        int right = count - 1;

        while (left <= right)
        {
            report[left] = new DirectionTypeReportDTO(types[left]);
            left++;
            if (left <= right)
            {
                report[right] = new DirectionTypeReportDTO(types[right]);
                right--;
            }
        }
        return report;
    }

    private DirectionCodeReportDTO[] InitializeDirectionCodeReportArray(
        List<DirectionCodeStatisticsReportEntity> codes
    )
    {
        int count = codes.Count;
        if (count == 0)
            return Array.Empty<DirectionCodeReportDTO>();
        DirectionCodeReportDTO[] report = new DirectionCodeReportDTO[count];
        int left = 0;
        int right = count - 1;

        while (left <= right)
        {
            report[left] = new DirectionCodeReportDTO(codes[left]);
            left++;
            if (left <= right)
            {
                report[right] = new DirectionCodeReportDTO(codes[right]);
                right--;
            }
        }
        return report;
    }

    public static async Task<ControlWeekReportDTO[]> InitializeArrayAsync(
        IEnumerable<ControlWeekReportEntity> reports
    )
    {
        var controlWeekReportEntities = reports as ControlWeekReportEntity[] ?? reports.ToArray();
        int count = controlWeekReportEntities.Length;
        if (count == 0)
            return Array.Empty<ControlWeekReportDTO>();

        Task<ControlWeekReportDTO>[] tasks = new Task<ControlWeekReportDTO>[count];
        int indexer = 0;
        foreach (var report in controlWeekReportEntities)
        {
            tasks[indexer] = Task.Run(() => new ControlWeekReportDTO(report));
        }

        await Task.WhenAll(tasks);
        ControlWeekReportDTO[] result = new ControlWeekReportDTO[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = tasks[i].Result;
        }

        return result;
    }
}
