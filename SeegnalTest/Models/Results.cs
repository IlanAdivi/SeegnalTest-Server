using Newtonsoft.Json;
using System.Collections.Generic;

namespace SeegnalTest.Models
{
    public class Results
    {
        [JsonProperty("results")]
        public List<Ingredient> Ingredients;
    }
}
