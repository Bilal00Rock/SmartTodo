using System.Text.Json.Serialization;

namespace SmartTodo.Contracts.Todos;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TodoType
{
    Urgent,
    Work,
    Personal,
    Shopping,
    Health,
    Education

}