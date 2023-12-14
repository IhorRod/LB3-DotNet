using LB3.Models;
using LB3.Services;
using Microsoft.AspNetCore.Mvc;

namespace LB3.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WineController: ControllerBase
    {

        private readonly WineService WineService;

        public WineController(WineService wineService)
        {
            WineService = wineService;
        }

        [HttpGet("get")]
        public IActionResult Get(string name)
        {
            var wine = WineService.get(name);
            if (wine == null)
            {
                return NotFound();
            }
            return Ok(wine);
        }

        [HttpGet("predict")]
        public IActionResult Predict(string country, float price)
        {
            var wine = WineService.get(country, price);
            if (wine == null)
            {
                return NotFound();
            }
            return Ok(wine);
        }

        [HttpGet("countries")]
        public IActionResult Countries()
        {
            return Ok(WineService.countries());
        }

    }
}
