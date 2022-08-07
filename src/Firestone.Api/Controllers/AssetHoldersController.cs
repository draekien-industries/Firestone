namespace Firestone.Api.Controllers;

using Application.AssetHolder.Commands;
using Application.AssetHolder.Contracts;
using Application.AssetHolder.Queries;

/// <summary>
/// Endpoints for interacting with Asset Holders.
/// </summary>
public class AssetHoldersController : WaystoneApiController
{
    /// <summary>
    /// Get an asset holder by id.
    /// </summary>
    /// <param name="id">The ID of the asset holder.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="AssetHolderDto" /></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AssetHolderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetAssetHolderQuery request = new() { Id = id };
        AssetHolderDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Add an asset holder
    /// </summary>
    /// <param name="request">The <see cref="AddAssetHolderCommand" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="AssetHolderDto" /></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AssetHolderDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync(
        [FromBody] AddAssetHolderCommand request,
        CancellationToken cancellationToken)
    {
        AssetHolderDto response = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction("Get", new { id = response.Id }, response);
    }
}
