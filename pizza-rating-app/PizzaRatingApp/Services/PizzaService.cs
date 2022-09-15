using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using PizzaRatingApp.Models;
using PizzaRatingApp.Repositories;

namespace PizzaRatingApp.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IRatingsRepository _ratingsRepository;

        private readonly string _path;

        public PizzaService(IRatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
            _path =
                Path
                    .Combine(Path
                        .GetDirectoryName(Assembly
                            .GetExecutingAssembly()
                            .Location),
                    @"pizzas.json");
        }

        public async Task<List<Pizza>> GetPizzas(string connectionString)
        {
            List<Pizza> pizzas =
                System
                    .Text
                    .Json
                    .JsonSerializer
                    .Deserialize<List<Pizza>>(File.ReadAllText(_path),
                    new System.Text.Json.JsonSerializerOptions {
                        PropertyNamingPolicy =
                            System.Text.Json.JsonNamingPolicy.CamelCase
                    });

            if (string.IsNullOrEmpty(connectionString))
            {
                return pizzas;
            }

            foreach (Pizza pizza in pizzas)
            {
                List<Rating> ratings =
                    await _ratingsRepository
                        .GetRatingsForPizza(connectionString, pizza.Id);
                int[] scores = new int[6];
                pizza.RatingSummary = new Dictionary<int, int>();
                foreach (Rating r in ratings)
                {
                    scores[r.Score] += 1;
                }

                for (int i = 0; i < 6; i++)
                {
                    pizza.RatingSummary.Add(i, scores[i]);
                }
            }

            return pizzas;
        }
    }
}
