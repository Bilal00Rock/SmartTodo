using SmartTodo.Domain.Admins;

namespace SmartTodo.Application.Common.Interfaces;

public interface IAdminsRepository
{
    Task AddAdminAsync(Admin admin);
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
    Task DeleteAsync(Guid id); 
    // Task RemoveTodo(Guid Id);
}