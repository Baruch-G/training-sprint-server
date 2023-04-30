using TrainingServer.Models;
using TrainingServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace TrainingServer.Controllers;

[ApiController]
[Route("squadrons")]
public class SquadronController : ControllerBase
{
    private readonly SquadronService _squadronService;

    public SquadronController(SquadronService squadronService)
    {
        _squadronService = squadronService;
    }

    [HttpGet]
    public async Task<List<Squadron>> Get() => await _squadronService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Squadron>> Get(string id)
    {
        var squadron = await _squadronService.GetAsync(id);
        return squadron == null ? NotFound() : squadron;
    }

    [HttpPost]
    public async Task<ActionResult<Squadron>> Post([FromBody] Squadron squadron)
    {
        await _squadronService.CreateAsync(squadron);
        return CreatedAtAction(nameof(Get), new { id = squadron.Id }, squadron);
    }

    [HttpPost("many")]
    public async Task<IActionResult> Post(List<Squadron> squadrons)
    {
        foreach (var squadron in squadrons)
        {
            await _squadronService.CreateAsync(squadron);
        }

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var squadron = await _squadronService.GetAsync(id);

        if (squadron is null)
        {
            return NotFound();
        }

        await _squadronService.RemoveAsync(id);

        return NoContent();
    }
}
