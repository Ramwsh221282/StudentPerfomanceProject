using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentSessionDTO
{
    public Guid Id { get; init; }
    public int Number { get; init; }
    public string StartDate { get; init; }
    public string EndDate { get; init; }
    public string State { get; init; }
    public List<AssignmentWeekDTO> Weeks { get; init; }
    public List<AssignmentSessionCoursePerfomanceDTO> CoursePerfomances { get; init; } = [];
    public List<AssignmentSessionDirectionTypePerfomanceDTO> DirectionTypePerfomances { get; init; } =
        [];
    public List<AssignmentSessionDirectionCodePerfomanceDTO> DirectionCodePerfomances { get; init; } =
        [];
    public List<AssignmentSessionDepartmentPerfomanceDTO> DepartmentPerfomances { get; init; } = [];
    public AssignmentSessionUniversityPerfomanceDTO UniversityPerfomance { get; init; }

    public AssignmentSessionDTO(AssignmentSession session)
    {
        Id = session.Id;
        Number = session.EntityNumber;
        StartDate = session.SessionStartDate.ToString("yyyy-MM-dd");
        EndDate = session.SessionCloseDate.ToString("yyyy-MM-dd");
        State = session.State.State == true ? "Открыта" : "Закрыта";
        Weeks = session.Weeks.Select(s => new AssignmentWeekDTO(s)).ToList();

        Task<List<AssignmentSessionDirectionCodePerfomanceDTO>> directionCodeCalculation =
            new DirectionCodePerfomanceService(Weeks).Calculate();

        Task<List<AssignmentSessionDepartmentPerfomanceDTO>> departmentPerfomanceCalculation =
            new DepartmentPerfomanceService(Weeks).CalculateForDepartments();

        Task<AssignmentSessionUniversityPerfomanceDTO> universityPerfomanceCalculation =
            new UniversityPerfomanceService(Weeks).CalculateForUniversity();

        CoursePerfomances = new CoursePerfomanceService(Weeks).CoursePerfomances;
        DirectionTypePerfomances = new DirectionTypePerfomanceService(
            CoursePerfomances
        ).DirectionTypePerfomances;
        DepartmentPerfomances = departmentPerfomanceCalculation.Result;
        UniversityPerfomance = universityPerfomanceCalculation.Result;
        DirectionCodePerfomances = directionCodeCalculation.Result;
    }
}

public abstract class AssignmentContextPerfomanceDTO
{
    public double Average { get; set; }

    public double Perfomance { get; set; }
}

public class AssignmentSessionCoursePerfomanceDTO : AssignmentContextPerfomanceDTO
{
    public int Course { get; set; }

    public string DirectionType { get; set; } = string.Empty;
}

public class AssignmentSessionDirectionTypePerfomanceDTO : AssignmentContextPerfomanceDTO
{
    public string DirectionType { get; set; } = string.Empty;
}

public class AssignmentSessionDirectionCodePerfomanceDTO : AssignmentContextPerfomanceDTO
{
    public string DirectionCode { get; set; } = string.Empty;
}

public class AssignmentSessionDepartmentPerfomanceDTO : AssignmentContextPerfomanceDTO
{
    public string DepartmentName { get; set; } = string.Empty;
}

public class AssignmentSessionUniversityPerfomanceDTO : AssignmentContextPerfomanceDTO { }
