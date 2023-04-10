using TrainingServer.Models;
using TrainingServer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TrainingServer.Controllers;

[ApiController]
[Route("training")]
public class TrainingController : ControllerBase
{
    private readonly TrainingService _trainingService;

    public TrainingController(TrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpGet]
    public async Task<List<Training>> Get() => await _trainingService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Training>> Get(string id)
    {
        var training = await _trainingService.GetAsync(id);

        if (training is null)
        {
            return NotFound();
        }

        return training;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Training training)
    {
        await _trainingService.CreateAsync(training);

        return CreatedAtAction(nameof(Get), new { id = training.Id }, training);
    }

    [HttpPost("many")]
    public async Task<IActionResult> Post(List<Training> training)
    {
        foreach (var trn in training)
        {
            await _trainingService.CreateAsync(trn);
        }

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var training = await _trainingService.GetAsync(id);

        if (training is null)
        {
            return NotFound();
        }

        await _trainingService.RemoveAsync(id);

        return NoContent();
    }
}
