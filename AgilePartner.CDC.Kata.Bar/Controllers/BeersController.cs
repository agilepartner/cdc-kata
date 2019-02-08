using AgilePartner.CDC.Kata.Bar;
using AgilePartner.CDC.Kata.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

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
        public void GiveMeABeer(GiveMeABeer giveMeABeer)
        {
            barService.GiveBeer(giveMeABeer);

            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            HttpContext.Response.ContentType = "application/json; charset=utf-8";
        }
    }
}