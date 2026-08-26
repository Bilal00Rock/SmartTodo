using ErrorOr;

namespace SmartTodo.Domain.Todos;

public static class TodoErrors
{
    public static readonly Error CannotUpdateCompletedTodo = Error.Validation(
        code: "Todo.CannotUpdateCompletedTodo",
        description: "Cannot update a completed todo. Mark it as incomplete first if you need to modify it.");
    
    // You could also add:
    public static readonly Error InvalidTitle = Error.Validation(
        code: "Todo.InvalidTitle",
        description: "Todo title cannot be empty or exceed maximum length.");
    
    public static readonly Error InvalidTodoType = Error.Validation(
        code: "Todo.InvalidTodoType",
        description: "The specified todo type is invalid.");
}