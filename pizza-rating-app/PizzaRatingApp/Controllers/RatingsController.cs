using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PizzaRatingApp.Models;
using PizzaRatingApp.Services;

namespace PizzaRatingApp.Controllers
{
    [ApiController]
    [Route("ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService _ratingsService;
        private readonly ILogger<RatingsController> _logger;

        public RatingsController(ILogger<RatingsController> logger, IRatingsService ratingsService)
        {
            _logger = logger;
            _ratingsService = ratingsService;
        }

        [HttpGet]
        public async Task<List<Rating>> Get([FromQuery]string connectionString, [FromQuery] int? min)
        {
            return await _ratingsService.GetRatings(connectionString, min);
        }

        [HttpPost]
        public async Task<bool> Add([FromQuery] string connectionString, [FromBody] Rating rating)
        {
            return await _ratingsService.AddRating(connectionString, rating);

        }
    }
}
