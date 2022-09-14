using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

using PizzaRatingApp.Configurations;
using PizzaRatingApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaRatingApp.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private static readonly string DatabaseId = "storage";

        private readonly CosmosClient _client;


        public PizzaRepository(IOptions<CosmosSettings> settings)
        {
            _client = new CosmosClient(settings.Value.ConnectionString);
        }

        public async Task<List<Pizza>> GetPizzas()
        {
            Container c = await GetContainer("pizzas");

            QueryDefinition query = new QueryDefinition("SELECT * FROM pizzas");
            List<Pizza> pizzas = new();

            using (FeedIterator<Pizza> resultSetIterator = c.GetItemQueryIterator<Pizza>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<Pizza> response = await resultSetIterator.ReadNextAsync();
                    pizzas.AddRange(response);
                    if (response.Diagnostics != null)
                    {
                        Console.WriteLine($"\nQueryWithSqlParameters Diagnostics: {response.Diagnostics.ToString()}");
                    }
                }
            }

            return pizzas;
        }

        private async Task<Container> GetContainer(string collectionName)
        {
            Database database = await _client.CreateDatabaseIfNotExistsAsync(DatabaseId);

            ContainerProperties containerProperties = new ContainerProperties(id: collectionName, partitionKeyPath: "/id");

            return await database.CreateContainerIfNotExistsAsync(
                containerProperties: containerProperties,
                throughput: 400);
        }
    }
}
