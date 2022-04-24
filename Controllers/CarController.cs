using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {

        private readonly IConfiguration _configuration;

        public CarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<ActionResult<List<CarModel>>> Get()
        {
            CarDBHandler dBHandler = new CarDBHandler(_configuration);
            ModelState.Clear();
            return Ok(dBHandler.GetCarList());
        }

        [HttpPost]
        public async Task<ActionResult<List<CarModel>>> Post(CarModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CarDBHandler dBHandler = new CarDBHandler(_configuration);
                    if (dBHandler.AddCar(model))
                    {
                        ModelState.Clear();
                    }
                }
                return Ok("Car Added successfully.");
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<CarModel>>> Get(int id)
        {
            CarDBHandler dbHandler = new CarDBHandler(_configuration);

            if (dbHandler != null)
            {
                return Ok(dbHandler.GetCarList().Find(carId => carId.Id == id));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<ActionResult<List<CarModel>>> Put(int id, CarModel model)
        {
          
            CarDBHandler dbHandler = new CarDBHandler(_configuration);
            var updateData = dbHandler.GetCarList().Find(cId => cId.Id == id);
            var upList = dbHandler.GetCarList().Find(cId => cId.Id == model.Id);

            if (upList != null)
            {
                bool b = dbHandler.UpdateDetails(model);

                if (b == true)
                {
                    return Ok(updateData + " updated successfully.");
                }
                else
                {
                    return NotFound("Not found..");
                }
            }
            else
            {
                if (updateData != null)
                {
                    return Ok(dbHandler.AddCar(model));
                    return Ok(updateData);
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(upList);
        }


        [HttpDelete]
        public async Task<ActionResult<List<CarModel>>> Delete(int id)
        {
            CarDBHandler dBHandler = new CarDBHandler(_configuration);
            if(dBHandler.DeleteStudent(id))
            {
                return Ok("Car id is successfully deleted.");
            } 
            else
            {
                return NotFound();
            }
        }
    }
}
