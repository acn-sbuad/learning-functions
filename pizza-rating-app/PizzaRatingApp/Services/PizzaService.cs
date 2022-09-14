using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PizzaRatingApp.Models;
using PizzaRatingApp.Repositories;

namespace PizzaRatingApp.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        private readonly IRatingsRepository _ratingsRepository;

        public PizzaService(IPizzaRepository pizzaRepository, IRatingsRepository ratingsRepository)
        {
            _pizzaRepository = pizzaRepository;
            _ratingsRepository = ratingsRepository;
        }

        public async Task<List<Pizza>> GetPizzas(string connectionString)
        {
            List<Pizza> pizzas = await _pizzaRepository.GetPizzas();


            foreach (Pizza pizza in pizzas)
            {
                List<Rating> ratings = await _ratingsRepository.GetRatingsForPizza(connectionString, pizza.Id);
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
