using PizzaRatingApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaRatingApp.Repositories
{
    public interface IPizzaRepository
    {
        public Task<List<Pizza>> GetPizzas();
    }
}
