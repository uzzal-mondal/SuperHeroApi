using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController :  Controller
    {
        private readonly DataContext _dataContext;

        public CarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<CarModel>>> Get()
        {
            var carList = _dataContext.CarModels.ToList();
            return Ok(carList);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarModel>> Get(int id)
        {
           var carList =  _dataContext.CarModels.Where(carId=>carId.Id == id).FirstOrDefault();
            if (carList == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(carList);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<CarModel>>> Post(CarModel model)
        {
            var carList = _dataContext.CarModels.Add(model);
            _dataContext.SaveChanges();
            return Ok(carList);
        }
      

        [HttpPut]
        public async Task<ActionResult<List<CarModel>>> Put(CarModel carRequest)
        {
            var carList = _dataContext.CarModels.ToList();
            var carUp =  carList.Find(carId => carId == carRequest);
            if (carUp != null)
            {
                carUp.Id = carRequest.Id;
                carUp.CarName = carRequest.CarName;
                carUp.CountryName = carRequest.CountryName;
                carUp.CarPrice = carRequest.CarPrice;
                return Ok(carList + " update successfully.");
            }
            else
            {
                return BadRequest("No data found.");
            }

            return Ok(carList);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           var carList = _dataContext.CarModels.ToList();
           var carDel =  _dataContext.CarModels.Where(carId=> carId.Id == id).FirstOrDefault();
            if (carDel != null)
            {
                carList.Remove(carDel);
                _dataContext.SaveChanges();
            }
            else
            {
                return BadRequest("Data is not found.");
            }
            return Ok(carList);

        }
       
    }
}
