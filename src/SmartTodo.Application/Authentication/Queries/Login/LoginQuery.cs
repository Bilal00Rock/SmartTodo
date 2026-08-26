using ErrorOr;
using MediatR;
using SmartTodo.Application.Authentication.Common;

namespace SmartTodo.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;