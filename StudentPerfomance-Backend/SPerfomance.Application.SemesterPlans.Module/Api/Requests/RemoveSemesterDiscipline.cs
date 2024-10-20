using System.Text.Json.Serialization;

using SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;
using SPerfomance.Application.Shared.Module.DTOs.Semesters;

namespace SPerfomance.Application.SemesterPlans.Module.Api.Requests;

public record RemoveSemesterDiscipline(SemesterDTO Semester, SemesterPlanDTO SemesterPlan, string Token);
