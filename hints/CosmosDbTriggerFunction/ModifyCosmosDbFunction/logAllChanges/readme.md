
```cs
if (input != null && input.Count > 0)
{
    log.LogInformation("Documents modified " + input.Count);
    foreach (var item in input)
    {
        log.LogInformation("Document Id " + item.Id);
    }
}
```
