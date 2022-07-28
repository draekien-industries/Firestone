namespace Firestone.Api.Controllers;

using Application.Common.Contracts;
using Application.FireProgressionTableEntry.Commands;
using Microsoft.AspNetCore.Mvc;
using Waystone.Common.Api.Controllers;

public class TableEntriesController : WaystoneApiController
{
    [HttpPost]
    public async Task<IActionResult> AddInitialInvestmentAsync(
        [FromBody] AddInitialInvestmentCommand command,
        CancellationToken cancellationToken)
    {
        FireProgressionTableEntryDto response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}
