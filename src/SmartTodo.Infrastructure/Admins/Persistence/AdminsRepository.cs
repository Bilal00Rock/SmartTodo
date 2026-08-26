using SmartTodo.Application.Common.Interfaces;
using SmartTodo.Domain.Admins;
using SmartTodo.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SmartTodo.Infrastructure.Admins.Persistence;

public class AdminsRepository : IAdminsRepository
{
    private readonly SmartTodoDbContext _dbContext;

    public AdminsRepository(SmartTodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAdminAsync(Admin admin)
    {
        await _dbContext.Admins.AddAsync(admin);
    }
    public Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);
    }

    public Task UpdateAsync(Admin admin)
    {
        _dbContext.Admins.Update(admin);

        return Task.CompletedTask;
    }
     public async Task DeleteAsync(Guid id)
    {
        var admin = await GetByIdAsync(id);
        if (admin is not null)
        {
            _dbContext.Admins.Remove(admin);
        }
    }
}