using ErrorOr;

using MediatR;
using SmartTodo.Application.Authentication.Common;

namespace SmartTodo.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;