using Microsoft.AspNetCore.Mvc;
using System.Diagnostics; 

namespace SimpleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{

    private readonly ILogger<LogController> _logger;

    public LogController(ILogger<LogController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Log")]
    public string Post(string theValue)
    {
        _logger.LogWarning("Logging to _logger: " + theValue);
        System.Console.WriteLine("Logging to System.Console: " + theValue);
        System.Console.Error.WriteLine("Logging to System.Error: " + theValue);

        return "Logged string'" + theValue + "'.";
    }
}
