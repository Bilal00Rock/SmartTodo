using SmartTodo.Domain.Common.Interfaces;

namespace SmartTodo.Domain.Admins.Events;

public record TodoDeletedEvent(Guid todoId): IDomainEvent;