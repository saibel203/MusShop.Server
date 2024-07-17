using Microsoft.AspNetCore.Mvc;
using MusShop.Presentation.Controllers.Base;
using Serilog;

namespace MusShop.Presentation.Controllers;

public class TestController : BaseController
{
    private readonly ILogger _logger = Log.ForContext<TestController>(); 
    
    [HttpGet("Test")]
    public IActionResult Test()
    {
        _logger.Information("Hello world!");
        var x = 1;
        var y = 0;
        return Ok(x / y);
    }

    [HttpGet("Test2")]
    public IActionResult Test2()
    {
        return Ok("Hello world!");
    }
}