using CarRentalSystemAD.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystemAD.Web.Controllers.Api;

[ApiController]
[Route("api/stats")]
public class StatsApiController : ControllerBase
{
    private readonly IStatsService statsService;

    public StatsApiController(IStatsService statsService)
    {
        this.statsService = statsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var stats = await statsService.GetStatsAsync();

        return Ok(stats);
    }
}