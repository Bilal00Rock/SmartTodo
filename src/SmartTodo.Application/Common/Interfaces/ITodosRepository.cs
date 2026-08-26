using SmartTodo.Domain.Todos;

namespace SmartTodo.Application.Common.Interfaces;

public interface ITodosRepository
{
    Task AddTodoAsync(Todo todo);
    Task<Todo?> GetByIdAsync(Guid id);
    Task<List<Todo>> ListByAdminIdAsync(Guid id);
    Task UpdateAsync(Todo todo);   
    Task DeleteAsync(Todo todo); 
}