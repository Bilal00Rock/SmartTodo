using ErrorOr;
using MediatR;

namespace SmartTodo.Application.Profiles.Commands.CreateAdminProfile;

public record CreateAdminProfileCommand(Guid UserId)
    : IRequest<ErrorOr<Guid>>;
