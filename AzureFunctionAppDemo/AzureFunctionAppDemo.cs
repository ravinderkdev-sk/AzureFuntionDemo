using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureFunctionAppDemo;

public class AzureFunctionAppDemo
{
    private readonly ILogger<AzureFunctionAppDemo> _logger;

    public AzureFunctionAppDemo(ILogger<AzureFunctionAppDemo> logger)
    {
        _logger = logger;
    }

    [Function("AzureFunctionAppDemo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string name = req.Query["name"]!;

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        dynamic data = JsonConvert.DeserializeObject<dynamic>(requestBody)!;

        name ??= data?.name!;

        string responseMessage = string.IsNullOrEmpty(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.";
        return new OkObjectResult(responseMessage);
    }
}