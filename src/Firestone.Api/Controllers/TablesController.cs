namespace Firestone.Api.Controllers;

using Application.Common.Contracts;
using Application.FireProgressionTable.Commands;
using Application.FireProgressionTable.Contracts;
using Application.FireProgressionTable.Queries;
using Microsoft.AspNetCore.Mvc;
using Waystone.Common.Api.Controllers;
using Waystone.Common.Application.Contracts.Pagination;

/// <summary>
/// Endpoints for interacting with the FIRE progression table.
/// </summary>
public class TablesController : WaystoneApiController
{
    /// <summary>
    /// Gets a paginated list of all FIRE progression tables.
    /// </summary>
    /// <param name="request">The <see cref="GetTablesQuery" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="PaginatedResponse{T}" /> of <see cref="FireProgressionTableSummaryDto" /></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<FireProgressionTableSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetTablesQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<FireProgressionTableSummaryDto> response = await Mediator.Send(request, cancellationToken);

        response.Links = CreatePaginationLinks(nameof(GetAsync), request, response);

        return Ok(response);
    }

    /// <summary>
    /// Gets a specific FIRE progression table by it's ID.
    /// </summary>
    /// <param name="id">The ID of the table.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="FireProgressionTableDto" /></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FireProgressionTableDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetTableByIdQuery request = new() { Id = id };
        FireProgressionTableDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Creates a new FIRE progression table.
    /// </summary>
    /// <param name="request">The <see cref="InitializeTableCommand" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="FireProgressionTableDto" /></returns>
    [HttpPost]
    [ProducesResponseType(typeof(FireProgressionTableDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> InitializeAsync(
        [FromBody] InitializeTableCommand request,
        CancellationToken cancellationToken)
    {
        FireProgressionTableDto response = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction("GetById", new { id = response.Id }, response);
    }

    /// <summary>
    /// Creates a new asset holder for a FIRE progression table.
    /// </summary>
    /// <param name="id">The ID of the table.</param>
    /// <param name="assetHolder">The asset holder to be created.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="AssetHolderDto" /></returns>
    [HttpPatch("{id:guid}/assetholders")]
    [ProducesResponseType(typeof(AssetHolderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAssetHolderAsync(
        [FromRoute] Guid id,
        [FromBody] NewAssetHolderDto assetHolder,
        CancellationToken cancellationToken)
    {
        AddAssetHolderCommand request = new()
        {
            TableId = id,
            NewAssetHolder = assetHolder,
        };

        AssetHolderDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Populates a FIRE progression table with projected data. Requires an initial investment to exist in the table.
    /// </summary>
    /// <param name="id">The ID of the table</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}/entries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PopulateTableAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        PopulateTableCommand request = new() { TableId = id };

        await Mediator.Send(request, cancellationToken);

        return Ok();
    }
}
