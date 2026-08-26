using SmartTodo.Application.Common.Models;

namespace SmartTodo.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}