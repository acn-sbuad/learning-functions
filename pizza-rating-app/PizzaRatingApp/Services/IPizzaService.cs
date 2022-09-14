using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PizzaRatingApp.Models;

namespace PizzaRatingApp.Services
{
   public interface IPizzaService
    {
        public Task<List<Pizza>> GetPizzas(string connectionString);
    }
}
