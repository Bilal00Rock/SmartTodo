namespace SmartTodo.Contracts.Profiles;

public record ListProfilesResponse(Guid? AdminId, Guid? NormalUserId);