using TrainingServer.Models;
using TrainingServer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TrainingServer.Controllers;

[ApiController]
[Route("areas")]
public class AreaController : ControllerBase
{
    private readonly AreaService _areaService;

    public AreaController(AreaService areaService)
    {
        _areaService = areaService;
    }

    [HttpGet]
    public async Task<List<Area>> Get() => await _areaService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Area>> Get(string id)
    {
        var area = await _areaService.GetAsync(id);

        if (area is null)
        {
            return NotFound();
        }

        return area;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Area area)
    {
        await _areaService.CreateAsync(area);

        return CreatedAtAction(nameof(Get), new { id = area.Id }, area);
    }

    [HttpPost("many")]
    public async Task<IActionResult> Post(List<Area> areas)
    {
        foreach (var area in areas)
        {
            await _areaService.CreateAsync(area);
        }

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var area = await _areaService.GetAsync(id);

        if (area is null)
        {
            return NotFound();
        }

        await _areaService.RemoveAsync(id);

        return NoContent();
    }
}
