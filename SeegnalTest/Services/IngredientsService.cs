using Newtonsoft.Json;
using SeegnalTest.Interfaces;
using SeegnalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SeegnalTest.Services
{
    public class IngredientService : IIngredientService
    {
        public async Task<List<Ingredient>> GetAllIngredients(string reaction)
        {
            if (reaction != "death" &&
                reaction != "headache" &&
                reaction != "nausea" &&
                reaction != "vomiting")
            {
                throw new ArgumentException("Invalid Request");
            }

            var URL = "https://api.fda.gov/drug/event.json?search=patient.reaction.reactionmeddrapt.exact:%22" +
                reaction + "%22&count=patient.drug.medicinalproduct.exact";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetStringAsync(URL);
            var results = JsonConvert.DeserializeObject<Results>(response);
            return results.Ingredients;
        }

        public async Task<IEnumerable<MainIngredient>> GetMainIngredients(string reaction)
        {
            List<Ingredient> ingredientsFromAPI = await GetAllIngredients(reaction);
             IEnumerable<Ingredient> ingredients = ingredientsFromAPI.OrderByDescending(i => i.Count).Take(10);
            List<MainIngredient> mainIngredients = new List<MainIngredient>();
            int sumOfMainIngrdients = calculateSumOfMainIngrdients(ingredients);
            foreach (var ingredient in ingredients)
            {
                if(ingredient != null)
                {
                    MainIngredient mainIngredient = new MainIngredient();
                    mainIngredient.Name = ingredient.Term;
                    mainIngredient.Count = ingredient.Count;
                    mainIngredient.Percentage = (ingredient.Count * 100) / sumOfMainIngrdients;
                    mainIngredients.Add(mainIngredient);
                }
            }
            return mainIngredients;
        }

        private int calculateSumOfMainIngrdients(IEnumerable<Ingredient> ingredients)
        {
            int sumOfMainIngrdients = 0;
            foreach (var ingredient in ingredients)
            {
                sumOfMainIngrdients += ingredient.Count;
            }
            return sumOfMainIngrdients;
        }
    }
}
