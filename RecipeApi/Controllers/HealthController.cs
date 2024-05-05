using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace RecipeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController(HealthCheckService healthCheckService) : ControllerBase
{
    /// <summary>
    /// Provides an indication about the health of the API
    /// </summary>
    /// <response code="200">API is healthy</response>
    /// <response code="503">API is unhealthy or in degraded state</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.ServiceUnavailable)]
    public async Task<IActionResult> GetHealth()
    {
        var report = await healthCheckService.CheckHealthAsync(_ => false);

        return report.Status == HealthStatus.Healthy ? Ok("Healthy") : StatusCode((int)HttpStatusCode.ServiceUnavailable, "Unhealthy");
    }

    /// <summary>
    /// Provides an indication about the health of the database
    /// </summary>
    /// <response code="200">API is healthy</response>
    /// <response code="503">API is unhealthy or in degraded state</response>
    [HttpGet("database")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.ServiceUnavailable)]
    public async Task<IActionResult> GetDatabaseHealth()
    {
        var report = await healthCheckService.CheckHealthAsync(healthCheck => healthCheck.Name == "Database");

        return report.Status == HealthStatus.Healthy ? Ok("Healthy") : StatusCode((int)HttpStatusCode.ServiceUnavailable, "Unhealthy");
    }
}
