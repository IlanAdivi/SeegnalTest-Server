using SeegnalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeegnalTest.Interfaces
{
    public interface IIngredientService
    {
        public Task<List<Ingredient>> GetAllIngredients(string reaction);
        public Task<IEnumerable<MainIngredient>> GetMainIngredients(string reaction);
    }
}
