using SmartTodo.Domain.Users;

namespace SmartTodo.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}