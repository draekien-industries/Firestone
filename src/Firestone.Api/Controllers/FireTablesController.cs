namespace Firestone.Api.Controllers;

using Application.FireTable.Commands;
using Application.FireTable.Contracts;
using Application.FireTable.Queries;
using Waystone.Common.Application.Contracts.Pagination;

/// <summary>
/// Endpoints for interacting with FIRE tables.
/// </summary>
public class FireTablesController : WaystoneApiController
{
    /// <summary>
    /// List a page of FIRE tables
    /// </summary>
    /// <param name="request">The <see cref="ListTablesQuery" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="PaginatedResponse{T}" /> of <see cref="FireTableSummaryDto" /></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<FireTableSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAsync([FromQuery] ListTablesQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<FireTableSummaryDto> response = await Mediator.Send(request, cancellationToken);

        response.Links = CreatePaginationLinks(nameof(ListAsync), request, response);

        return Ok(response);
    }

    /// <summary>
    /// Get a FIRE table by ID
    /// </summary>
    /// <param name="id">The ID of the <see cref="FireTableDto" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="FireTableDto" /></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FireTableDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetTableQuery request = new() { Id = id };
        FireTableDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Add a new FIRE table
    /// </summary>
    /// <param name="request">The <see cref="AddFireTableCommand" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="FireTableDto" /></returns>
    [HttpPost]
    [ProducesResponseType(typeof(FireTableDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync(
        [FromBody] AddFireTableCommand request,
        CancellationToken cancellationToken)
    {
        FireTableDto response = await Mediator.Send(request, cancellationToken);

        return CreatedAtAction("Get", new { id = response.Id }, response);
    }
}
