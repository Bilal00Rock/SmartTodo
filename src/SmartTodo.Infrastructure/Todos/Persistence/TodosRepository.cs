using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Todos;
using SmartTodo.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SmartTodo.Infrastructure.Todos.Persistence;

public class TodosRepository : ITodosRepository
{
    private readonly SmartTodoDbContext _dbContext;

    public TodosRepository(SmartTodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTodoAsync(Todo Todo)
    {
        await _dbContext.Todos.AddAsync(Todo);
    }

    public async Task<Todo?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Todo>> ListByAdminIdAsync(Guid id)
    {
        return await _dbContext.Todos.Where(todo => todo.AdminId == id).ToListAsync();
    }

    public Task UpdateAsync(Todo todo)
    {
        _dbContext.Todos.Update(todo);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Todo todo)
    {
        if (todo is not null)
        {
            _dbContext.Todos.Remove(todo);
        }

        return Task.CompletedTask;
    }
}