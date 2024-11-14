using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Statistics.DataAccess.EntityModels;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReport
{
    public record Request(TokenContract Token, Guid Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApi}/report", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        ControlWeekRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        ControlWeekReportEntity? report = await repository.GetById(request.Id);
        return report == null ? Results.Ok() : Results.Ok(new ReportDTO(report, report.GroupParts));
    }

    public record ReportDTO
    {
        public DateTime CreationDate { get; init; }
        public DateTime CompletionDate { get; init; }
        public List<GroupReportDTO> GroupParts { get; init; }
        public CourseReportDTO CourseReport { get; init; }
        public DirectionTypeReportDTO DirectionTypeReport { get; init; }
        public DirectionCodeReportDTO DirectionCodeReport { get; init; }
        public DepartmentStatisticsReportDTO DepartmentReport { get; init; }

        public string Average { get; init; }
        public string Perfomance { get; init; }

        public ReportDTO(
            ControlWeekReportEntity entity,
            List<GroupStatisticsReportEntity> groupReports
        )
        {
            CreationDate = entity.CreationDate;
            CompletionDate = entity.CompletionDate;
            GroupParts = groupReports.Select(r => new GroupReportDTO(r)).ToList();
            CourseReport = new CourseReportDTO(entity.CourseReport);
            DirectionTypeReport = new DirectionTypeReportDTO(entity.DirectionTypeReport);
            DirectionCodeReport = new DirectionCodeReportDTO(entity.DirectionCodeReport);
            DepartmentReport = new DepartmentStatisticsReportDTO(entity.DepartmentReport);
        }
    }

    public record DepartmentStatisticsReportDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public List<DepartmentStatisticsReportPartEntityDTO> Parts { get; init; }

        public DepartmentStatisticsReportDTO(DepartmentStatisticsReportEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            Parts = entity
                .Parts.Select(r => new DepartmentStatisticsReportPartEntityDTO(r))
                .ToList();
        }
    }

    public record DepartmentStatisticsReportPartEntityDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public string DepartmentName { get; init; }
        public List<TeacherStatisticsReportPartEntityDTO> Parts { get; init; }

        public DepartmentStatisticsReportPartEntityDTO(DepartmentStatisticsReportPartEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            DepartmentName = entity.DepartmentName;
            Parts = entity.Parts.Select(r => new TeacherStatisticsReportPartEntityDTO(r)).ToList();
        }
    }

    public record TeacherStatisticsReportPartEntityDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public string TeacherName { get; init; }
        public string TeacherSurname { get; init; }
        public string? TeacherPatronymic { get; init; }

        public TeacherStatisticsReportPartEntityDTO(TeacherStatisticsReportPartEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            TeacherName = entity.TeacherName;
            TeacherSurname = entity.TeacherSurname;
            TeacherPatronymic = entity.TeacherPatronymic;
        }
    }

    public record DirectionCodeReportDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public List<DirectionCodeReportPartDTO> Parts { get; init; }

        public DirectionCodeReportDTO(DirectionCodeReportEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            Parts = entity.Parts.Select(r => new DirectionCodeReportPartDTO(r)).ToList();
        }
    }

    public record DirectionCodeReportPartDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public string Code { get; init; }

        public DirectionCodeReportPartDTO(DirectionCodeReportPartEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            Code = entity.Code;
        }
    }

    public record DirectionTypeReportDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public List<DirectionTypeReportPartDTO> Parts { get; init; }

        public DirectionTypeReportDTO(DirectionTypeReportEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            Parts = entity.Parts.Select(r => new DirectionTypeReportPartDTO(r)).ToList();
        }
    }

    public record DirectionTypeReportPartDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public string DirectionType { get; init; }

        public DirectionTypeReportPartDTO(DirectionTypeReportEntityPart entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            DirectionType = entity.DirectionType;
        }
    }

    public record CourseReportDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public List<CourseReportPartDTO> Parts { get; init; }

        public CourseReportDTO(CourseReportEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            Parts = entity.Parts.Select(r => new CourseReportPartDTO(r)).ToList();
        }
    }

    public record CourseReportPartDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public byte Course { get; init; }

        public CourseReportPartDTO(CourseReportEntityPart entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            Course = entity.Course;
        }
    }

    public record GroupReportDTO
    {
        public string Average { get; init; }
        public string Perfomance { get; init; }
        public string GroupName { get; init; }
        public byte AtSemester { get; init; }
        public string DirectionCode { get; init; }
        public string DirectionType { get; init; }
        public List<StudentReportDTO> Parts { get; init; }

        public GroupReportDTO(GroupStatisticsReportEntity entity)
        {
            Average = entity.Average;
            Perfomance = entity.Perfomance;
            GroupName = entity.GroupName;
            AtSemester = entity.AtSemester;
            DirectionCode = entity.DirectionCode;
            DirectionType = entity.DirectionType;
            Parts = entity.Parts.Select(r => new StudentReportDTO(r)).ToList();
        }
    }

    public record StudentReportDTO
    {
        public string StudentName { get; init; }
        public string StudentSurname { get; init; }
        public string? StudentPatronymic { get; init; }
        public ulong StudentRecordBook { get; init; }
        public string Perfomance { get; init; }
        public string Average { get; init; }
        public List<StudentDisciplineReportDTO> Disciplines { get; init; }

        public StudentReportDTO(StudentStatisticsPartEntity entity)
        {
            StudentName = entity.StudentName;
            StudentSurname = entity.StudentSurname;
            StudentPatronymic = entity.StudentPatronymic;
            StudentRecordBook = entity.StudentRecordBook;
            Perfomance = entity.Perfomance;
            Average = entity.Average;
            Disciplines = entity.Parts.Select(r => new StudentDisciplineReportDTO(r)).ToList();
        }
    }

    public record StudentDisciplineReportDTO
    {
        public string DisciplineName { get; init; }

        public StudentDisciplineReportDTO(StudentStatisticsOnDisciplinePartEntity entity)
        {
            DisciplineName = entity.DisciplineName;
        }
    }
}
