using SPerfomance.Application.Shared.Users.Module.DTOs;

namespace SPerfomance.Application.Shared.Users.Module.API.Requests;

public record UserRegistrationRequest(UserRegistrationDTO User, string? Token);