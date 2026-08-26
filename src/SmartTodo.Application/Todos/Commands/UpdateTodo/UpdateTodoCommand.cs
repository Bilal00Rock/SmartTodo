using ErrorOr;
using SmartTodo.Domain.Todos;
using MediatR;
namespace SmartTodo.Application.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(Guid Id, string Title, TodoType TodoType, bool IsCompleted) : IRequest<ErrorOr<Todo>>;