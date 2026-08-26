using System;
namespace SmartTodo.Contracts.Todos;

public record TodoResponse(Guid Id, 
                            string Title,
                            TodoType TodoType,
                            bool IsCompleted,
                            DateTime? CompletedAt, 
                            DateTime CreatedAt );