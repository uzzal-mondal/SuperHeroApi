using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> _listOfHero = new List<SuperHero>
        {
            new SuperHero
            {
                    Id = 1,
                    MovieNme = "Spider Man",
                    FirstName = "Peter",
                    LastName ="Marker",
                    Place ="New Work City"

            },

            new SuperHero
            {
                    Id = 2,
                    MovieNme = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Long Island"

            }

        };




        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(_listOfHero);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var heroId = _listOfHero.Find(h => h.Id == id);
            if (heroId == null)
            {
                return BadRequest("Data not found");
            }

            return Ok(heroId);
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _listOfHero.Add(hero);
            return Ok(_listOfHero);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero reuest)
        {
            var heroUp = _listOfHero.Find(h => h.Id == reuest.Id);
            if (heroUp == null)
            {
                return BadRequest("Update not successfully.");
            }
            else
            {
                heroUp.MovieNme = reuest.MovieNme;
                heroUp.FirstName = reuest.FirstName;
                heroUp.LastName = reuest.LastName;
                heroUp.Place = reuest.Place;
                return Ok("Update is successfully");
            }


            return Ok(_listOfHero);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero(int id)
        {
            var heroDel = _listOfHero.Find(h => h.Id == id);
            if (heroDel == null)
            {
                return BadRequest("Hero data not found.");
            }
            _listOfHero.Remove(heroDel);
            return Ok(heroDel);
        }
    }
}
