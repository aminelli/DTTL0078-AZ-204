using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace etlforma.apis.test;

public class HttpTonyTest
{
    private readonly ILogger<HttpTonyTest> _logger;

    public HttpTonyTest(ILogger<HttpTonyTest> logger)
    {
        _logger = logger;
    }

    [Function("HttpTonyTest")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}