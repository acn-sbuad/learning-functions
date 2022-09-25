```cs
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LearningFunctions.RatingTrigger
{
    public static class RatingTrigger
    {
        [FunctionName("RatingTrigger")]
        public static void Run([CosmosDBTrigger(
            databaseName: "storage",
            collectionName: "ratings",
            ConnectionStringSetting = "learningfunctionsakmdb_DOCUMENTDB",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                foreach (var item in input)
                {
                    log.LogInformation("Document Id " + item.Id);
                    log.LogInformation("Document content: " + item.ToString());

                    var rating = JsonSerializer.Deserialize<Rating>(item.ToString());
                    if(rating.score == 4) {
                        log.LogInformation(rating.id + " GOT A PERFECT SCORE!!");
                    }
                }
            }
        }
    }


    public class Rating
    {
        public Guid id { get; set; }
        public int pizzaId { get; set; }
        public int score { get; set; }
        public DateTime created { get; set; }
    }
}
```
