using Microsoft.AspNetCore.Mvc;
using SeegnalTest.Interfaces;
using System;
using System.Threading.Tasks;

namespace SeegnalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        [Route("GetMainIngredients")]
        public async Task<IActionResult> GetMainIngredients([FromQuery] string reaction)
        {
            try
            {
                var mainIngredients = await _ingredientService.GetMainIngredients(reaction);
                return Ok(mainIngredients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
