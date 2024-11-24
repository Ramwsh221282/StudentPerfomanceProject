using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Api.Features.Semesters.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.Application.Semesters.DTO;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Api.Features.SemesterPlans;

public static class GetSemesterPlansBySemester
{
    public record Request(
        GetEducationDirectionQuery Direction,
        EducationPlanContract Plan,
        SemesterContract Semester
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{SemesterPlanTags.Api}", Handler)
                .WithTags(SemesterPlanTags.Tag)
                .WithOpenApi()
                .WithName("GetSemesterPlansBySemester")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает дисциплины семестра")
                        .AppendLine("Результат ОК (200): Список дисциплин.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Семестр не найден.")
                        .ToString()
                );
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<IEnumerable<SemesterPlanDto>>
        >
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "directionName")] string directionName,
        [FromQuery(Name = "directionCode")] string directionCode,
        [FromQuery(Name = "directionType")] string directionType,
        [FromQuery(Name = "planYear")] int planYear,
        [FromQuery(Name = "semesterNumber")] int semesterNumber,
        IUsersRepository users,
        IQueryDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение дисциплин семестра");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var directionQuery = new GetEducationDirectionQuery(
            directionName,
            directionCode,
            directionType
        );
        var direction = await dispatcher.Dispatch<GetEducationDirectionQuery, EducationDirection>(
            directionQuery,
            ct
        );

        if (direction.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на получение дисциплин семестра отменён. Причина: {text}",
                jwtToken.UserId,
                direction.Error.Description
            );
            return TypedResults.NotFound(direction.Error.Description);
        }

        var planQuery = new GetEducationPlanQuery(direction.Value, planYear);
        var plan = await dispatcher.Dispatch<GetEducationPlanQuery, EducationPlan>(planQuery, ct);

        if (plan.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на получение дисциплин семестра отменён. Причина: {text}",
                jwtToken.UserId,
                plan.Error.Description
            );
            return TypedResults.NotFound(plan.Error.Description);
        }

        var semesterQuery = new GetSemesterQuery(plan.Value, (byte)semesterNumber);
        var semester = await dispatcher.Dispatch<GetSemesterQuery, Semester>(semesterQuery, ct);

        if (semester.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на получение дисциплин семестра отменён. Причина: {text}",
                jwtToken.UserId,
                semester.Error.Description
            );
            return TypedResults.NotFound(semester.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} получает дисциплины семестра {number} плана {planYear} направления подготовки {code} {name} {type}",
            jwtToken.UserId,
            semesterNumber,
            planYear,
            directionCode,
            directionName,
            directionType
        );
        return TypedResults.Ok(semester.Value.Disciplines.Select(d => d.MapFromDomain()));
    }
}
