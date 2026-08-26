using ErrorOr;
using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Todos;
using MediatR;

namespace SmartTodo.Application.Todos.Queries.GetTodo;

public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, ErrorOr<Todo>>
{
    private readonly ITodosRepository _todosRepository;

    public GetTodoQueryHandler(ITodosRepository todosRepository)
    {
        _todosRepository = todosRepository;
    }

    public async Task<ErrorOr<Todo>> Handle(GetTodoQuery query, CancellationToken cancellationToken)
    {
        var todo = await _todosRepository.GetByIdAsync(query.TodoId);

        return todo is null
            ? Error.NotFound(description: "Todo not found")
            : todo;
    }
}
