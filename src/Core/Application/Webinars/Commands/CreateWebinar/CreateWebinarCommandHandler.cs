using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Webinars.Commands.CreateWebinar;

public sealed record CreateWebinarCommandHandler : ICommandHandler<CreateWebinarCommand, Guid>
{
    private readonly IWebinarRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWebinarCommandHandler(IWebinarRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateWebinarCommand request, CancellationToken cancellationToken)
    {
        var webinar = new Webinar(Guid.NewGuid(), request.Nane, request.ScheduleOn);

        _repository.Insert(webinar);
        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return webinar.Id;
    }
}
