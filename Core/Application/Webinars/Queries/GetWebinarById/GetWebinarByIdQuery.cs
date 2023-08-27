using Application.Abstractions.Messaging;

namespace Application.Webinars.Queries.GetWebinarById;

public sealed record GetWebinarByIdQuery(Guid WebinarId) : IQuery<WebinarResponse>;
