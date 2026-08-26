using ErrorOr;
using SmartTodo.Domain.Todos;
using MediatR;

namespace SmartTodo.Application.Todos.Queries.GetTodo;

public record GetTodoQuery(Guid TodoId) : IRequest<ErrorOr<Todo>>;