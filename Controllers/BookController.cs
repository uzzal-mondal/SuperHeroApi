using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.model;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller, IBookController
    {
        private readonly DataContext _dataContext;
        public BookController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            var bookList = await _dataContext.Books.ToListAsync();
            return Ok(bookList);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<List<Book>>> Get(int id)
        {
            var bookList = _dataContext.Books.Where(i => i.Id == id).FirstOrDefault();
            if (bookList != null)
            {
                return Ok(bookList);
            }
            else
            {
                return BadRequest("No data found.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> Post(Book book)
        {
            var bookList = _dataContext.Books.Add(book);
            _dataContext.SaveChanges();
            return Ok(bookList);
        }


        [HttpPut]
        public async Task<ActionResult<List<Book>>> Put(Book bRequest)
        {
            var bookList = _dataContext.Books.ToList();
            var upList = bookList.Find(bId => bId.Id == bRequest.Id);
            if (upList != null)
            {
                upList.Id = bRequest.Id;
                upList.BookName = bRequest.BookName;
                upList.BookPrice = bRequest.BookPrice;
                upList.BookCountry = bRequest.BookCountry;
                return Ok(upList + " updated successfully");
            }
            else
            {
                return BadRequest("No data found.");
            }

            return Ok(bookList);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookList = _dataContext.Books.ToList();
            var bookDel = _dataContext.Books.Where(b => b.Id == id).FirstOrDefault();
            if (bookDel != null)
            {
                bookList.Remove(bookDel);
                _dataContext.SaveChanges();
            }
            else
            {
                return BadRequest("No data found.");
            }

            return Ok(bookList);
        }
    }
}
