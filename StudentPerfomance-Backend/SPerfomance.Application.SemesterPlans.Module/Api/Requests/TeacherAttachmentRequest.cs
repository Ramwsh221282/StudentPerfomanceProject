using SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;
using SPerfomance.Application.Shared.Module.DTOs.Teachers;

namespace SPerfomance.Application.SemesterPlans.Module.Api.Requests;

public record class TeacherAttachmentRequest(SemesterPlanDTO? Plan, TeacherDTO? Teacher);
