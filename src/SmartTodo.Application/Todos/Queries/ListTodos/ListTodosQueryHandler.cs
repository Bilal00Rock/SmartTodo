using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Todos;

namespace SmartTodo.Application.Todos.Queries.ListTodos
{
    public class ListTodosQueryHandler: IRequestHandler<ListTodosQuery, ErrorOr<List<Todo>>>
    {
        private readonly ITodosRepository _todosRepository;
        public ListTodosQueryHandler(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<ErrorOr<List<Todo>>> Handle(ListTodosQuery query, CancellationToken cancellationToken)
        {
            var todos = await _todosRepository.ListByAdminIdAsync(query.Id);

            return todos is null
                ? Error.NotFound(description: "No Todos are listed.")
                : todos;
        }
    }
}