namespace Firestone.Api.Controllers;

using Application.Assets.Commands;
using Application.Assets.Contracts;
using Application.Assets.Queries;

/// <summary>
/// Endpoints for interacting with the Assets resource.
/// </summary>
public class AssetsController : WaystoneApiController
{
    /// <summary>
    /// Get an assets entry by its ID
    /// </summary>
    /// <param name="id">The ID of the assets entry</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="AssetsDto" /></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AssetsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetAssetsQuery request = new() { Id = id };
        AssetsDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Update an assets entry with new values
    /// </summary>
    /// <param name="id">The ID of the assets entry</param>
    /// <param name="data">The data for updating the assets entry</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The updated <see cref="AssetsDto" /></returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(AssetsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateAssetsDto data,
        CancellationToken cancellationToken)
    {
        UpdateAssetsCommand request = new() { Id = id, Data = data };
        AssetsDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
