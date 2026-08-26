using System.Net;
using System.Net.Cache;
using ErrorOr;
using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Todos;
using MediatR;
namespace SmartTodo.Application.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, ErrorOr<Todo>>
{
    private readonly ITodosRepository _todosRepository;
    private readonly IAdminsRepository _adminsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoCommandHandler(ITodosRepository todosRepository, IUnitOfWork unitOfWork, IAdminsRepository adminsRepository)
    {
        _todosRepository = todosRepository;
        _unitOfWork = unitOfWork;
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Todo>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

        if (admin is null)
        {
            return Error.NotFound(description: "Admin not found");
        }

        var todo = new Todo(
            title: request.Title,
            todoType: request.TodoType,
            adminId: request.AdminId);

        

        admin.AddTodo(todo);
        //this is a Transaction Type of Commiting 
        //the user will wait online till we do all the work 
        //and only if all the work is done then it will COMMIT to DB else if theres a Error all changes will be Rollback
        await _todosRepository.AddTodoAsync(todo);
        await _adminsRepository.UpdateAsync(admin);
        await _unitOfWork.CommitChangesAsync();

        return todo;
    }
}
