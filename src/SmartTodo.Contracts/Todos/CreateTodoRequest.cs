namespace SmartTodo.Contracts.Todos;

public record CreateTodoRequest(
    string Title,
    string TodoType,
    Guid AdminId);