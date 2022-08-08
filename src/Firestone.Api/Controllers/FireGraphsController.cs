namespace Firestone.Api.Controllers;

using Application.FireGraph.Contracts;
using Application.FireGraph.Queries;

public class FireGraphsController : WaystoneApiController
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FireGraphDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetGraphQuery request = new() { Id = id };
        FireGraphDto response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
