using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.PerfomanceContext.Contracts;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.MakeAssignment;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class MakeAssignmentByTeacher
{
    public record Request(TokenContract Token, TeacherAssignmentContract Assignment);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/make-assignment", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        IStudentAssignmentsRepository assignments
    )
    {
        Token token = request.Token;
        if (!await new UserVerificationService(users).IsVerified(token, UserRole.Teacher))
            return Results.BadRequest(UserTags.UnauthorizedError);

        MakeAssignmentCommand command = new MakeAssignmentCommand(
            request.Assignment.Id,
            request.Assignment.Mark
        );
        Result<StudentAssignment> assignment = await new MakeAssignmentCommandHandler(
            sessions,
            assignments
        ).Handle(command);
        return assignment.IsFailure
            ? Results.BadRequest(assignment.Error.Description)
            : Results.Ok(new StudentMarkAssignmentFromTeacherDTO(assignment.Value));
    }
}
