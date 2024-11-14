using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Api.Features.EducationPlans.Contracts;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class FilterEducationPlans
{
    public record Request(
        EducationDirectionContract Direction,
        EducationPlanContract Plan,
        PaginationContract Pagination,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationPlanTags.Api}/filter", Handler).WithTags(EducationPlanTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IEducationPlansRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        IReadOnlyCollection<EducationPlan> plans = await repository.GetPagedFiltered(
            request.Direction.Name,
            request.Direction.Code,
            request.Direction.Type,
            request.Plan.PlanYear,
            request.Pagination.Page,
            request.Pagination.PageSize
        );

        return Results.Ok(plans.Select(p => p.MapFromDomain()));
    }
}
