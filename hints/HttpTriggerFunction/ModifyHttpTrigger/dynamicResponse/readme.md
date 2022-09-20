```cs
[FunctionName("HttpTriggerFunction")]
public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
    ILogger log)
{
    string responseString = $"The current date and time is {System.DateTime.Now}";
    return new OkObjectResult(responseString);
}
```
