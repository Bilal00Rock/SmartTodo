using System.Net;
using System.Net.Cache;
using ErrorOr;
using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Todos;
using MediatR;
namespace SmartTodo.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, ErrorOr<Todo>>
{
    private readonly ITodosRepository _todosRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoCommandHandler(ITodosRepository todosRepository, IUnitOfWork unitOfWork)
    {
        _todosRepository = todosRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Todo>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _todosRepository.GetByIdAsync(request.Id);

        if (todo is null)
        {
            return Error.NotFound(description: "Todo not found");
        }
        
        var updateResult= todo.UpdateTodo(request.Title, request.TodoType, request.IsCompleted);
        
        if(updateResult.IsError){
            return updateResult.Errors;
        }

        await _todosRepository.UpdateAsync(todo);
        await _unitOfWork.CommitChangesAsync();

        return todo;
    }
}
