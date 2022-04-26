using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.model;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeControler : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeControler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            EmpDbHandler dBHandler = new EmpDbHandler(_configuration);
            ModelState.Clear();
            return Ok(dBHandler.GetEmployeeList());
        }



        [HttpPost]
        public async Task<ActionResult<List<Employee>>> Post(Employee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpDbHandler dBHandler = new EmpDbHandler(_configuration);
                    if (dBHandler.AddEmp(model))
                    {
                        ModelState.Clear();
                    }
                }
                return Ok("Emp Added successfully.");
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> Get(int id)
        {
            EmpDbHandler dbHandler = new EmpDbHandler(_configuration);

            if (dbHandler != null)
            {
                return Ok(dbHandler.GetEmployeeList().Find(carId => carId.Id == id));
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut]
        public async Task<ActionResult<List<Employee>>> Put(int id, Employee model)
        {

            EmpDbHandler dbHandler = new EmpDbHandler(_configuration);
            var updateData = dbHandler.GetEmployeeList().Find(cId => cId.Id == id);
            var upList = dbHandler.GetEmployeeList().Find(cId => cId.Id == model.Id);

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
                    return Ok(dbHandler.AddEmp(model));
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
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            EmpDbHandler dBHandler = new EmpDbHandler(_configuration);
            if (dBHandler.DeleteEmployee(id))
            {
                return Ok("Emp id is successfully deleted.");
            }
            else
            {
                return NotFound();
            }
        }




    }
}
