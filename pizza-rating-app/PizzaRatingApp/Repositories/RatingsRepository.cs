using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;



using PizzaRatingApp.Configurations;
using PizzaRatingApp.Models;

namespace PizzaRatingApp.Repositories
{
    public class RatingsRepository : IRatingsRepository
    {

        private static readonly string DatabaseId = "storage";

        private readonly CosmosClient _client;
        private readonly IMemoryCache _memoryCache;


        public RatingsRepository(IMemoryCache cache)
        {
            _memoryCache = cache;
        }

        public async Task<bool> AddRating(string connectionString, Rating rating)
        {
            rating.Id = Guid.NewGuid();
            rating.Created = DateTime.UtcNow;

            Container c = await GetContainer(connectionString);
            ItemResponse<Rating> res = await c.CreateItemAsync(rating);
            return res.StatusCode == HttpStatusCode.Created;
        }

        public async Task<List<Rating>> GetRatings(string connectionString, int? min = null)
        {
            Container c = await GetContainer(connectionString);

            string queryString = min == null ? "SELECT * FROM ratings" : $"SELECT * FROM ratings r WHERE r.created >= '{DateTime.UtcNow.AddMinutes(-(double)min).ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")}'";

            List<Rating> ratings = new();

            using (FeedIterator<Rating> resultSetIterator = c.GetItemQueryIterator<Rating>(new QueryDefinition(queryString)))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<Rating> response = await resultSetIterator.ReadNextAsync();
                    ratings.AddRange(response);
                    if (response.Diagnostics != null)
                    {
                        Console.WriteLine($"\nQueryWithSqlParameters Diagnostics: {response.Diagnostics.ToString()}");
                    }
                }
            }

            return ratings;
        }

        public async Task<List<Rating>> GetRatingsForPizza(string connectionString, int pizzaId)
        {
            Container c = await GetContainer(connectionString);

            QueryDefinition query = new QueryDefinition($"SELECT * FROM ratings r WHERE r.pizzaId = {pizzaId}");

            List<Rating> ratings = new();

            using (FeedIterator<Rating> resultSetIterator = c.GetItemQueryIterator<Rating>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<Rating> response = await resultSetIterator.ReadNextAsync();
                    ratings.AddRange(response);
                }
            }

            return ratings;
        }

        private async Task<Container> GetContainer(string connectionString)
        {        
           
            var client = await _memoryCache.GetOrCreateAsync(connectionString, async cacheEntry => 
                {
                    var _client = new CosmosClient(connectionString,
                new CosmosClientOptions { SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase } });
     
            Database database = await _client.CreateDatabaseIfNotExistsAsync(DatabaseId);

            // setting up leases collection for function
            ContainerProperties leasesProp = new ContainerProperties(id: $"leases", partitionKeyPath: "/id");
            await database.CreateContainerIfNotExistsAsync(
            containerProperties: leasesProp,
            throughput: 400);


            ContainerProperties containerProperties = new ContainerProperties(id: $"ratings", partitionKeyPath: "/pizzaId");

            return await database.CreateContainerIfNotExistsAsync(
                containerProperties: containerProperties,
                throughput: 400);
                });

            return client;
        }
    }
}
