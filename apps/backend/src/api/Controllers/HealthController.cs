﻿using Microsoft.AspNetCore.Mvc;

namespace Thread.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Healthy!");
    }
}
