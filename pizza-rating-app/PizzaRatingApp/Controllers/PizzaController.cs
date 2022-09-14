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
    [Route("pizza")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger, IPizzaService pizzaService)
        {
            _logger = logger;
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<List<Pizza>> Get([FromQuery] string connectionString)
        {
            List<Pizza> result = await _pizzaService.GetPizzas(connectionString);
            return result;
        }
    }
}
