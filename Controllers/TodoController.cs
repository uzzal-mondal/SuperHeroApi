using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.model;

namespace SuperHeroApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly DataContext _dataContext;

        public TodoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            var items = await _dataContext.TodoModels.ToListAsync();
            return Ok(items);
        }


        [HttpPost]
        public async Task<ActionResult> AddItems(TodoModel todoModel)
        {
            if (ModelState.IsValid)
            {
                await _dataContext.TodoModels.AddAsync(todoModel);
                await _dataContext.SaveChangesAsync();
                return CreatedAtAction("GetItem", new { todoModel.Id }, todoModel);
            }

            return new JsonResult("Something went wrong ") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetId(int id)
        {
            var todoId = await _dataContext.TodoModels.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (todoId != null)
            {
                return Ok(todoId);
            }
            else
            {
                return BadRequest(" id isn't found.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItems(int id, TodoModel todoModel)
        {
            if (id != todoModel.Id)
            {
                return NotFound();
            }

          var upItem = await _dataContext.TodoModels.FirstOrDefaultAsync(i => i.Id == todoModel.Id);
            
            if (upItem != null)
            {
                todoModel.Id = upItem.Id;
                todoModel.Title = upItem.Title;
                todoModel.Description = upItem.Description;
                await _dataContext.SaveChangesAsync();
                return Ok(todoModel);
                
            }
            else
            {
                return NotFound();
              
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> delId(int id)
        {
           var delItem =  await _dataContext.TodoModels.FirstOrDefaultAsync(i => i.Id == id);
            if (delItem == null)
            {
                return NotFound();
            }
            else
            {
                _dataContext.Remove(delItem);
                await _dataContext.SaveChangesAsync();
                return Ok(delItem);
            }
        }
            
    

    }
}
