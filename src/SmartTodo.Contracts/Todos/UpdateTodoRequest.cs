namespace SmartTodo.Contracts.Todos;

public record UpdateTodoRequest(string Title,
    string TodoType,bool IsCompleted);
