using AgilePartner.CDC.Kata.Bar;
using AgilePartner.CDC.Kata.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AgilePartner.CDC.Kata.Controllers
{
    [Route("api/bar/beers")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBarService barService;

        public BeersController(IBarService barService)
        {
            this.barService = barService;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult GiveMeABeer([FromBody] GiveMeABeer giveMeABeer)
        {
            barService.GiveBeer(giveMeABeer);
            return Created("", null);
        }
    }
}