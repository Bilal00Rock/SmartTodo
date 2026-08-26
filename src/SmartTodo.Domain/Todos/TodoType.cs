using Ardalis.SmartEnum;

namespace SmartTodo.Domain.Todos;

public class TodoType : SmartEnum<TodoType>
{
    public static readonly TodoType Urgent = new(nameof(Urgent), 0);
    public static readonly TodoType Work = new(nameof(Work), 1);
    public static readonly TodoType Personal = new(nameof(Personal), 2);
    public static readonly TodoType Shopping = new(nameof(Shopping), 3);
    public static readonly TodoType Health = new(nameof(Health), 4);
    public static readonly TodoType Education = new(nameof(Education), 5);

    public TodoType(string name, int value) : base(name, value)
    {
    }
}