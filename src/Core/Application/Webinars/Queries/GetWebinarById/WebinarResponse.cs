namespace Application.Webinars.Queries.GetWebinarById;

public sealed record WebinarResponse(Guid Id, string Name, DateTime ScheduleOn);
