using PizzaRatingApp.Models;
using PizzaRatingApp.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaRatingApp.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository _repository;

        public RatingsService(IRatingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddRating(string connectionString, Rating rating)
        {
            return await _repository.AddRating(connectionString, rating);
        }

        public async Task<List<Rating>> GetRatings(string connectionString, int? min)
        {
            return await _repository.GetRatings(connectionString, min);
        }
    }
}
