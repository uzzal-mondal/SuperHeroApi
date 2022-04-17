using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
       

        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
      
         

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            var _listOfHero = _dataContext.SuperHeroes.ToList();
            return Ok(_listOfHero);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var heroId = _dataContext.SuperHeroes.Where(h => h.Id == id).FirstOrDefault();
            if (heroId == null)
            {
                return BadRequest("Data not found");
            }

            return Ok(heroId);
        }



        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            var _listOfHero = _dataContext.SuperHeroes.Add(hero);
            _dataContext.SaveChanges();
            return Ok(_listOfHero);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero reuest)
        {
            var _listOfHero = _dataContext.SuperHeroes.ToList();
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
            var _listOfHero = _dataContext.SuperHeroes.ToList();
            var heroDel = _dataContext.SuperHeroes.Where(h => h.Id == id).FirstOrDefault();
            if (heroDel == null)
            {
                return BadRequest("Hero data not found.");
            }
            else
            {
                _dataContext.Remove(heroDel);
                _dataContext.SaveChanges();
            }
           
            return Ok(heroDel);
        }
    }
}
