using System.Net.Cache;
using Microsoft.AspNetCore.Mvc;
using SmartTodo.Contracts.Todos;
using SmartTodo.Application.Todos.Commands.CreateTodo;
using SmartTodo.Application.Todos.Commands.UpdateTodo;
using SmartTodo.Application.Todos.Commands.DeleteTodo;
using SmartTodo.Application.Todos.Queries.GetTodo;
using SmartTodo.Application.Todos.Queries.ListTodos;
using DomainTodoType = SmartTodo.Domain.Todos.TodoType;
using MediatR;

namespace SmartTodo.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ApiController
{
    private readonly ISender _mediator;

    public TodoController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public  async Task<IActionResult>  CreateTodo(CreateTodoRequest request)
    {
        if (!DomainTodoType.TryFromName(
            request.TodoType,
            ignoreCase: true,
            out var todoType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"Invalid Todo type. Valid values: {string.Join(", ", Enum.GetNames<TodoType>())}");
        }

        var command = new CreateTodoCommand(
            request.Title,
            todoType,
            request.AdminId);

        var createTodoResult = await _mediator.Send(command);

        return createTodoResult.Match(
            todo => CreatedAtAction(
                nameof(GetTodo),
                new { todoId = todo.Id },
                new TodoResponse(
                    todo.Id,
                    todo.Title,
                    ToDto(todo.TodoType),
                    todo.IsCompleted, 
                    todo.CompletedAt, 
                    todo.CreatedAt)),
            Problem);
    }

    [HttpGet("{todoId:guid}")]
    public async Task<IActionResult> GetTodo(Guid todoId)
    {
        var query = new GetTodoQuery(todoId);

        var getTodosResult = await _mediator.Send(query);

        return getTodosResult.Match(
            todo => Ok(new TodoResponse(
                                todo.Id,
                                todo.Title,
                                ToDto(todo.TodoType),
                                todo.IsCompleted, 
                                todo.CompletedAt, 
                                todo.CreatedAt)),
                        Problem);
    }
    [HttpGet("GetListByAdminId")]
    public async Task<IActionResult> ListTodosByAdminId([FromQuery] Guid adminId)
    {
        var query = new ListTodosQuery(adminId);

        var getTodosResult = await _mediator.Send(query);

        return getTodosResult.Match(
            todos => Ok(todos.ConvertAll(todo=> new TodoResponse(
                                todo.Id,
                                todo.Title,
                                ToDto(todo.TodoType),
                                todo.IsCompleted, 
                                todo.CompletedAt, 
                                todo.CreatedAt))),
                        Problem);
    }
    [HttpPut("Update/{todoId:guid}")]
    public async Task<IActionResult> UpdateTodo(Guid todoId, [FromBody] UpdateTodoRequest request)
    {
        if (!DomainTodoType.TryFromName(
            request.TodoType,
            ignoreCase: true,
            out var todoType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"Invalid Todo type. {string.Join(", ", Enum.GetNames<TodoType>())}");
        }
        var command = new UpdateTodoCommand(
            Id: todoId,
            Title: request.Title,
            TodoType: todoType,
            IsCompleted: request.IsCompleted
        );

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            todo => Ok(new TodoResponse(
                                todo.Id,
                                todo.Title,
                                ToDto(todo.TodoType),
                                todo.IsCompleted,
                                todo.CompletedAt,
                                todo.CreatedAt)),
            Problem);
    }
    [HttpDelete("Delete/{todoId:guid}")]
    public async Task<IActionResult> DeleteTodo(Guid todoId)
    {
        var command = new DeleteTodoCommand(todoId);

        var deleteTodoResult = await _mediator.Send(command);

        return deleteTodoResult.Match(
            _ => NoContent(),
            Problem);
    }

    private static TodoType ToDto(DomainTodoType todoType)
    {
        return todoType.Name switch
        {
            nameof(DomainTodoType.Urgent) => TodoType.Urgent,
            nameof(DomainTodoType.Work) => TodoType.Work,
            nameof(DomainTodoType.Education) => TodoType.Education,
            nameof(DomainTodoType.Personal) => TodoType.Personal,
            nameof(DomainTodoType.Shopping) => TodoType.Shopping,
            nameof(DomainTodoType.Health) => TodoType.Health,
            _ => throw new InvalidOperationException(),
        };
    }
}
