using Domain.Primitives;

namespace Domain.Entities;

public sealed class Webinar : Entity
{
    public Webinar()
    {
    }

    public Webinar(Guid id, string name, DateTime scheduleOn) : base(id)
    {
        Name = name;
        ScheduleOn = scheduleOn;
    }

    public string? Name { get; set; }
    public DateTime ScheduleOn { get; set; }
}
