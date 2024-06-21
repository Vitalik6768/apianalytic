using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/analytic")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly GoogleAnalyticsService _googleAnalyticsService;

    public AnalyticsController(GoogleAnalyticsService googleAnalyticsService)
    {
        _googleAnalyticsService = googleAnalyticsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _googleAnalyticsService.GetTotalUsersLast30DaysAsync();
        return Ok(data);
    }
}