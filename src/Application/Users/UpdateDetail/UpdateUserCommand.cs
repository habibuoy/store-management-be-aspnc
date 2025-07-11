using Application.Abstractions.Messaging;

namespace Application.Users.UpdateDetail;

public sealed record UpdateUserDetailCommand(Guid UserId, string FirstName, string? LastName)
    : ICommand<UpdateUserDetailResponse>;