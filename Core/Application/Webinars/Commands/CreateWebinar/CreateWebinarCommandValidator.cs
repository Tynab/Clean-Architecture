using FluentValidation;

namespace Application.Webinars.Commands.CreateWebinar;

public sealed class CreateWebinarCommandValidator : AbstractValidator<CreateWebinarCommand>
{
    public CreateWebinarCommandValidator()
    {
        _ = RuleFor(x => x.Nane).NotEmpty();
        _ = RuleFor(x => x.ScheduleOn).NotEmpty();
    }
}
