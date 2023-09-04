using Domain.Entities;

namespace Domain.Abstractions;

public interface IWebinarRepository
{
    public void Insert(Webinar webinar);
}
