namespace SmartTodo.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}