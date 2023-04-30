using TrainingServer.Models;
using TrainingServer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TrainingServer.Dtos;

namespace TrainingServer.Controllers;

[ApiController]
[Route("training")]
public class TrainingController : ControllerBase
{
    private readonly TrainingService _trainingService;
    private readonly AreaService _areaService;
    private readonly SquadronService _squdronService;

    public TrainingController(
        TrainingService trainingService,
        AreaService areaService,
        SquadronService squadronService
    )
    {
        _trainingService = trainingService;
        _areaService = areaService;
        _squdronService = squadronService;
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
    public async Task<ActionResult<TrainingDto>> Post([FromBody] CreateTrainingDto training)
    {
        if (training.Force != "air" && training.Force != "land")
        {
            return BadRequest(String.Format("force '{0}' is not valid", training.Force));
        }

        var area = await _areaService.GetAsync(training.AreaId);
        var squadron = training.SquadronId == null ? null : await _squdronService.GetAsync(training.SquadronId);
        if (area == null)
            return BadRequest("area id not found!");

        var item = new Training
        {
            Force = training.Force,
            CreationTime = DateTime.UtcNow,
            StartDate = training.StartDate,
            EndDate = training.EndDate,
            LastUpdateTime = DateTime.UtcNow,
            Area = area,
            System = squadron
        };

        await _trainingService.CreateAsync(item);

        return CreatedAtAction(nameof(Get), new { id = item.Id }, training);
    }

    [HttpPost("many")]
    public async Task<IActionResult> Post([FromBody] List<CreateTrainingDto> training)
    {
        foreach (var trn in training)
        {
            if (trn.Force != "air" && trn.Force != "land")
            {
                return BadRequest(String.Format("force '{0}' is not valid", trn.Force));
            }

            var area = await _areaService.GetAsync(trn.AreaId);
            var squadron = await _squdronService.GetAsync(trn.SquadronId);
            if (area == null || squadron == null)
                return BadRequest("area id not found!");

            var item = new Training
            {
                Force = trn.Force,
                CreationTime = DateTime.UtcNow,
                StartDate = trn.StartDate,
                EndDate = trn.EndDate,
                LastUpdateTime = DateTime.UtcNow,
                Area = area,
                System = squadron
            };

            await _trainingService.CreateAsync(item);
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
