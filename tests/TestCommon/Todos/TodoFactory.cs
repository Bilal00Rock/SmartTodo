using SmartTodo.Domain.Todos;
namespace TestCommon.Todos;

public static class TodoFactory
{
    public static Todo CreateTodo(
        TodoType? todoType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Todo(
            todoType: todoType ?? Constants.Todo.DefaultTodoType,
            adminId ?? Constants.Admin.Id,
            id ?? Constants.Todo.Id);
    }
}
