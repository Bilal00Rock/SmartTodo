using MediatR;
using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Admins.Events;

namespace SmartTodo.Application.Todos.Queries;

public class TodoDeletedEventHandler(ITodosRepository todosRepository, IUnitOfWork unitOfWork) : INotificationHandler<TodoDeletedEvent>
{
    private readonly ITodosRepository _todosRepository = todosRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork; 

    public async Task Handle(TodoDeletedEvent notification, CancellationToken cancellationToken)
    {
        var todo = await _todosRepository.GetByIdAsync(notification.todoId);

        if(todo is null){
            //resilient Error handling 
            throw new InvalidOperationException(); 
        }

        await _todosRepository.DeleteAsync(todo);
        await _unitOfWork.CommitChangesAsync();
    }
}
