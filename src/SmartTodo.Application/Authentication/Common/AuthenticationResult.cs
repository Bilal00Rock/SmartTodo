using SmartTodo.Domain.Users;

namespace SmartTodo.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);