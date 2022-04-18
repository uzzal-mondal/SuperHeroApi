using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.model;

namespace SuperHeroApi.Controllers
{
    public interface IBookController
    {
        Task<ActionResult> Delete(int id);
        Task<ActionResult<List<Book>>> Get();
        Task<ActionResult<List<Book>>> Get(int id);
        Task<ActionResult<List<Book>>> Post(Book book);
        Task<ActionResult<List<Book>>> Put(Book bRequest);
    }
}