using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly DataContext _dataContext;

        public CarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

      

         
       
    }
}
