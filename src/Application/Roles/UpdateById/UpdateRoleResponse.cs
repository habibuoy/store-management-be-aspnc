namespace Application.Roles.UpdateById;

public sealed record UpdateRoleResponse(string Name,
    string NormalizedName, string? Description);