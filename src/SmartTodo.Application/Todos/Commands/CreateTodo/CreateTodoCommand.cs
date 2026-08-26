using ErrorOr;
using SmartTodo.Domain.Todos;
using MediatR;
namespace SmartTodo.Application.Todos.Commands.CreateTodo;

public record CreateTodoCommand(
    string Title,
    TodoType TodoType,
    Guid AdminId) : IRequest<ErrorOr<Todo>>;