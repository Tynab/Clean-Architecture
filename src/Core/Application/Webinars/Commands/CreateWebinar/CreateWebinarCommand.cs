using Application.Abstractions.Messaging;

namespace Application.Webinars.Commands.CreateWebinar;

public sealed record CreateWebinarCommand(string Nane, DateTime ScheduleOn) : ICommand<Guid>;
