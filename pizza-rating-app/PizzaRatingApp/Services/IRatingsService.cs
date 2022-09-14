using PizzaRatingApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaRatingApp.Services
{
    public interface IRatingsService
    {

        public Task<bool> AddRating(string connectionString, Rating rating);

        public Task<List<Rating>> GetRatings(string connectionString, int? min);
    }
}
