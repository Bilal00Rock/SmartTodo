using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;

namespace SmartTodo.Application.Todos.Commands.DeleteTodo
{
    public record DeleteTodoCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;
}