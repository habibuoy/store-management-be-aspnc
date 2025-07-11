using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Common;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Users.Delete;

internal sealed class DeleteUserCommandHandler(IApplicationDbContext dbContext,
    ILogger<DeleteUserCommandHandler> logger)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                return UserErrors.NotFound(command.UserId);
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Succeed();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "DB error has occurred while deleting user '{command.UserId}' 's detail from DB",
                command.UserId);
            return ApplicationErrors.DBOperationError(nameof(DeleteUserCommandHandler),
                $"DB error has occurred while deleting user '{command.UserId}' 's detail from DB");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "");
            return ApplicationErrors.OperationCancelledError(nameof(DeleteUserCommandHandler), ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error has occurred while deleting user '{command.UserId}' 's detail from DB",
                command.UserId);
            return ApplicationErrors.UnexpectedError(nameof(DeleteUserCommandHandler),
                $"Unexpected error has occurred while deleting user '{command.UserId}' 's detail from DB");
        }
    }
}
