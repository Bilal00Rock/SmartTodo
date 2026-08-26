using System.IO;
using ErrorOr;

namespace SmartTodo.Domain.Admins;

public static class AdminErrors
{

    public static readonly Error TodoWithIdNotAssigned = Error.Validation(
        code: "Admin.NotTodoWithId",
        description: "Todo with this ID is not assigned to this admin."
    );

    
}