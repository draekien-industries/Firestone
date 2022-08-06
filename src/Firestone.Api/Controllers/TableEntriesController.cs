namespace Firestone.Api.Controllers;

using Application.Common.Contracts;
using Application.FireProgressionTableEntry.Commands;
using Microsoft.AspNetCore.Mvc;
using Waystone.Common.Api.Controllers;

/// <summary>
/// Endpoints for interacting with entries inside a FIRE progression table.
/// </summary>
public class TableEntriesController : WaystoneApiController
{
    /// <summary>
    /// Creates a new entry in a FIRE progression table which represents the initial investment.
    /// </summary>
    /// <param name="command">The <see cref="AddInitialInvestmentCommand" /></param>
    /// <param name="cancellationToken">The <see cref="CancellationToken" /></param>
    /// <returns>The created <see cref="FireProgressionTableEntryDto" /></returns>
    [HttpPost]
    [ProducesResponseType(typeof(FireProgressionTableEntryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddInitialInvestmentAsync(
        [FromBody] AddInitialInvestmentCommand command,
        CancellationToken cancellationToken)
    {
        FireProgressionTableEntryDto response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}
