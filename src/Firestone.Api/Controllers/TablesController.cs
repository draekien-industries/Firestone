namespace Firestone.Api.Controllers;

using Application.Common.Contracts;
using Application.FireProgressionTable.Commands;
using Application.FireProgressionTable.Contracts;
using Application.FireProgressionTable.Queries;
using Microsoft.AspNetCore.Mvc;
using Waystone.Common.Api.Controllers;

public class TablesController : WaystoneApiController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetTableQuery request = new() { Id = id };
        FireProgressionTableDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> InitializeAsync(
        [FromBody] InitializeTableCommand request,
        CancellationToken cancellationToken)
    {
        FireProgressionTableDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPatch("{id:guid}/assetholders")]
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

    [HttpPatch("{id:guid}/entries")]
    public async Task<IActionResult> PopulateTableAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        PopulateTableCommand request = new() { TableId = id };

        FireProgressionTableDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
