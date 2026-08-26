using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SmartTodo.Domain.Todos;

namespace SmartTodo.Application.Todos.Queries.ListTodos
{  
  public record ListTodosQuery(Guid Id) : IRequest<ErrorOr<List<Todo>>>;
}