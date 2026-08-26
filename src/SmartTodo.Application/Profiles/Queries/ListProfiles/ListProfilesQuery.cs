using ErrorOr;
using MediatR;

namespace SmartTodo.Application.Profiles.Queries.ListProfiles;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;