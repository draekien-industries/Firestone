namespace Firestone.Api.Controllers;

using Application.LineItem.Commands;
using Application.LineItem.Contracts;
using Application.LineItem.Queries;

/// <summary>
/// Endpoints for interacting with FIRE Table Line Items.
/// </summary>
public class LineItemsController : WaystoneApiController
{
    /// <summary>
    /// Get a line item by ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="LineItemDto" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="LineItemDto" /></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LineItemDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetLineItemQuery request = new() { Id = id };
        LineItemDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Add a new line item to a FIRE table
    /// </summary>
    /// <param name="request">The <see cref="AddLineItemCommand" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="LineItemDto" /></returns>
    [HttpPost]
    [ProducesResponseType(typeof(LineItemDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync(
        [FromBody] AddLineItemCommand request,
        CancellationToken cancellationToken)
    {
        LineItemDto response = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction("Get", new { id = response.Id }, response);
    }
}
