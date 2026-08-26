using SmartTodo.Domain.Todos;
using Throw;
using ErrorOr;
using SmartTodo.Domain.Common.Interfaces;
using SmartTodo.Domain.Admins.Events;


namespace SmartTodo.Domain.Admins;

public class Admin : Entity
{
    public Guid UserId { get; }
    public  List<Guid> TodoIds  { get; private set; } = [];

    public Admin(
        Guid userId,
        Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
    }

    private Admin() { }

    public ErrorOr<Success> AddTodo(Todo todo)
    {
        if (todo is null)
        {
            throw new ArgumentNullException(nameof(todo));
            // Throw();
        }
        
        if (TodoIds.Contains(todo.Id))
        {
            return Error.Conflict(description: "Todo already assigned to this admin"); //1rst way or error handling
        }
        
        TodoIds.Add(todo.Id);
        return Result.Success;
    }
    public ErrorOr<Success> RemoveTodo(Guid todoId)
    {
        if (!TodoIds.Contains(todoId))
        {
            return AdminErrors.TodoWithIdNotAssigned; //2nd way of error handling 
        }
        
        TodoIds.Remove(todoId);
        //call domain event
        _domainEvents.Add(new TodoDeletedEvent(todoId));
        return Result.Success;
    }

    public bool HasTodo(Guid todoId)
    {
        return TodoIds.Contains(todoId);
    }

    public int GetTodoCount()
    {
        return TodoIds.Count;
    }

    public void ClearAllTodos()
    {
        TodoIds.Clear();
    }

}