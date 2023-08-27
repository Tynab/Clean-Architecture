using Application.Webinars.Commands.CreateWebinar;
using Application.Webinars.Queries.GetWebinarById;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Presentation.Controllers;

/// <summary>
/// Represents the webinars controller.
/// </summary>
public sealed class WebinarsController : ApiController
{
    /// <summary>
    /// Gets the webinar with the specified identifier, if it exists.
    /// </summary>
    /// <param name="webinarId">The webinar identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The webinar with the specified identifier, if it exists.</returns>
    [HttpGet("{webinarId:guid}")]
    [ProducesResponseType(typeof(WebinarResponse), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async ValueTask<IActionResult> GetWebinar(Guid webinarId, CancellationToken cancellationToken)
    {
        var query = new GetWebinarByIdQuery(webinarId);
        var webinar = await Sender!.Send(query, cancellationToken);

        return Ok(webinar);
    }

    /// <summary>
    /// Creates a new webinar based on the specified request.
    /// </summary>
    /// <param name="request">The create webinar request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created webinar.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IActionResult> CreateWebinar([FromBody] CreateWebinarRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateWebinarCommand>();
        var webinarId = await Sender!.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetWebinar), new
        {
            webinarId
        }, webinarId);
    }
}
