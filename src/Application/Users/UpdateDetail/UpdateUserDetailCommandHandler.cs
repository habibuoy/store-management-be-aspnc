using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Common;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Users.UpdateDetail;

internal sealed class UpdateUserDetailCommandHandler(IApplicationDbContext dbContext,
    ILogger<UpdateUserDetailCommandHandler> logger) 
        : ICommandHandler<UpdateUserDetailCommand, UpdateUserDetailResponse>
{
    public async Task<Result<UpdateUserDetailResponse>> HandleAsync(UpdateUserDetailCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                return UserErrors.NotFound(command.UserId);
            }

            user.UpdateDetail(command.FirstName, command.LastName);

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateUserDetailResponse(user.Name.First, user.Name.Last);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "DB error has occurred while updating user '{command.UserId}' 's detail to DB",
                command.UserId);
            return ApplicationErrors.DBOperationError(nameof(UpdateUserDetailCommandHandler),
                $"DB error has occurred while updating user '{command.UserId}' 's detail to DB");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "");
            return ApplicationErrors.OperationCancelledError(nameof(UpdateUserDetailCommandHandler), ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error has occurred while updating user '{command.UserId}' 's detail to DB",
                command.UserId);
            return ApplicationErrors.UnexpectedError(nameof(UpdateUserDetailCommandHandler),
                $"Unexpected error has occurred while updating user '{command.UserId}' 's detail to DB");
        }
    }
}