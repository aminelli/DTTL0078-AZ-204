using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FuncAppAMinelli
{
    public class FnAMinelli
    {
        private readonly ILogger<FnAMinelli> _logger;

        public FnAMinelli(ILogger<FnAMinelli> logger)
        {
            _logger = logger;
        }

        [Function("FnAMinelli")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req
            )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");



            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
