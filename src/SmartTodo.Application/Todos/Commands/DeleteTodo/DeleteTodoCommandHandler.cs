
using ErrorOr;
using SmartTodo.Application.Common.Interfaces;
using MediatR;

namespace SmartTodo.Application.Todos.Commands.DeleteTodo
{
    public class DeleteTodoCommandHandler(
        IAdminsRepository adminsRepository,
        ITodosRepository todosRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteTodoCommand, ErrorOr<Deleted>>
    {
        private readonly IAdminsRepository _adminsRepository = adminsRepository;
        private readonly ITodosRepository _todosRepository = todosRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Deleted>> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _todosRepository.GetByIdAsync(command.Id);

            if (todo is null)
            {
                return Error.NotFound(description: "Todo not found");
            }

            var admin = await _adminsRepository.GetByIdAsync(todo.AdminId);

            if (admin is null)
            {
                return Error.Unexpected(description: "Admin not found");
            }

            admin.RemoveTodo(command.Id);
            //Domain Events Vs Orchestration 
            //also Transactional Consistency vs Eventual Consistency 
            //Transantional Consistency : low cognitive load, simplicity 
            //Eventual Consistency : High performance,  Flexible error handling, Scalability 
            //we want to change this appraoch to create domain event appraoch
            await _adminsRepository.UpdateAsync(admin);
            //await _todosRepository.DeleteAsync(todo);  //no need for this domain event will call this after user recieves response
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
    
}

