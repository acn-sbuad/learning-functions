using PizzaRatingApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaRatingApp.Repositories
{
    public interface IRatingsRepository
    {

        public Task<List<Rating>> GetRatings(string connectionString, int? min);

        public Task<bool> AddRating(string connectionString, Rating rating);

        public Task<List<Rating>> GetRatingsForPizza(string connectionString, int pizzaId);
    }
}
