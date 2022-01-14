using Microsoft.AspNetCore.Mvc;
using System.Diagnostics; 

namespace SimpleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomMultiplicationsController : ControllerBase
{

    private static Random random = new Random();
    private static int MILLION_NUMBERS_TO_GENERATE = 100;
    private static int TO_GENERATE;

    private readonly ILogger<RandomMultiplicationsController> _logger;

    public RandomMultiplicationsController(ILogger<RandomMultiplicationsController> logger)
    {
        _logger = logger;

        string? toGenerate = Environment.GetEnvironmentVariable("MILLION_NUMBERS_TO_GENERATE");
        if (toGenerate != null) {
            try {
                MILLION_NUMBERS_TO_GENERATE = int.Parse(toGenerate);
                _logger.LogWarning("Running " + MILLION_NUMBERS_TO_GENERATE + " millions random multiplications.");
            } catch (Exception e) {
                _logger.LogWarning("Could not parse value of MILLION_NUMBERS_TO_GENERATE as string; using default value " + MILLION_NUMBERS_TO_GENERATE + "(" + e.Message + ")");
            }
        } else {
            _logger.LogWarning("No value of MILLION_NUMBERS_TO_GENERATE given; using default value " + MILLION_NUMBERS_TO_GENERATE + ".");
        }
        TO_GENERATE = 1024 * 1024 * MILLION_NUMBERS_TO_GENERATE;
    }

    [HttpGet(Name = "RandomMultiplication")]
    public ContainerObject Get()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 0 ; i < TO_GENERATE ; i++) {
            double product = random.Next() * random.Next();
        }
        stopWatch.Stop();

        return new ContainerObject(){
            NumbersGenerated = TO_GENERATE,
            TimeUsed = stopWatch.Elapsed.TotalMilliseconds
        };

    }
}
