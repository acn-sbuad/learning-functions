using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Memory;
using PizzaRatingApp.Models;

namespace PizzaRatingApp.Repositories;

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

        var c = await GetContainer(connectionString);
        var res = await c.CreateItemAsync(rating);
        return res.StatusCode == HttpStatusCode.Created;
    }

    public async Task<List<Rating>> GetRatings(string connectionString, int? min = null)
    {
        var c = await GetContainer(connectionString);

        var queryString = min == null
            ? "SELECT * FROM ratings"
            : $"SELECT * FROM ratings r WHERE r.created >= '{DateTime.UtcNow.AddMinutes(-(double)min).ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")}'";

        List<Rating> ratings = new();

        using (var resultSetIterator = c.GetItemQueryIterator<Rating>(new QueryDefinition(queryString)))
        {
            while (resultSetIterator.HasMoreResults)
            {
                var response = await resultSetIterator.ReadNextAsync();
                ratings.AddRange(response);
                if (response.Diagnostics != null)
                    Console.WriteLine($"\nQueryWithSqlParameters Diagnostics: {response.Diagnostics}");
            }
        }

        return ratings;
    }

    public async Task<List<Rating>> GetRatingsForPizza(string connectionString, int pizzaId)
    {
        var c = await GetContainer(connectionString);

        var query = new QueryDefinition($"SELECT * FROM ratings r WHERE r.pizzaId = {pizzaId}");

        List<Rating> ratings = new();

        using (var resultSetIterator = c.GetItemQueryIterator<Rating>(query))
        {
            while (resultSetIterator.HasMoreResults)
            {
                var response = await resultSetIterator.ReadNextAsync();
                ratings.AddRange(response);
            }
        }

        return ratings;
    }

    private async Task<Container> GetContainer(string connectionString)
    {
        var container = await _memoryCache.GetOrCreateAsync(connectionString, async cacheEntry =>
        {
            var _client = new CosmosClient(connectionString,
                new CosmosClientOptions
                {
                    SerializerOptions = new CosmosSerializationOptions
                        { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase }
                });

            Database database = await _client.CreateDatabaseIfNotExistsAsync(DatabaseId);

            await database.CreateContainerIfNotExistsAsync(new ContainerProperties("leases", "/id"));

            var containerProperties = new ContainerProperties("ratings", "/pizzaId");

            return await database.CreateContainerIfNotExistsAsync(
                containerProperties);
        });

        return container;
    }
}