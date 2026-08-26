using System.Runtime.CompilerServices;
using System;
using ErrorOr;
using Throw;
using SmartTodo.Domain.Common.Interfaces;

namespace SmartTodo.Domain.Todos;

public class Todo : Entity
{
    public string Title { get; private set; }
    public TodoType TodoType { get; private set; }
    public Guid AdminId { get; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public Todo(
        TodoType todoType,
        string title,
        Guid adminId,
        Guid? id = null,
        DateTime? createdAt = null,  
        bool isCompleted = false ) 
            : base(id ?? Guid.NewGuid() )
    {
        TodoType = todoType;
        Title = title;
        AdminId = adminId;
        CreatedAt = createdAt ?? DateTime.UtcNow; 
        IsCompleted = isCompleted;
        
        if (isCompleted)
        {
            CompletedAt = DateTime.UtcNow;
        }
    }

    private Todo()
    {
        Title = string.Empty;
        TodoType = null!; 
    }
    
    public ErrorOr<Success> UpdateTodo(string title, TodoType todoType, bool isCompleted)
    {
        Title = title;
        TodoType = todoType;
        
        if (isCompleted && !IsCompleted)
        {
            MarkAsCompleted();
        }
        else if (!isCompleted && IsCompleted)
        {
            MarkAsIncomplete();
        }
        
        return Result.Success;
    }
    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
    
    public void MarkAsIncomplete()
    {
        IsCompleted = false;
        CompletedAt = null;
    }
}