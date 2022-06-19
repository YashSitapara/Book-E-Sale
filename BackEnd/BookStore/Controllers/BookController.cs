using BookStore.models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        BookRepository _repository = new BookRepository();
        [HttpGet("getBook/list")]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetBooks(int pageIndex = 1,int pageSize = 10,string? keyword = "")
        {
            try
            {
                var books = _repository.GetBooks(pageIndex, pageSize, keyword);
                if (books == null)
                    return BadRequest("Please Provide Correct Information");
                
                ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
                {
                    records = books.records.Select(c => new BookModel(c)),
                    totalRecords = books.totalRecords
                };
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("getBook/{id}")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _repository.GetBook(id);
                if (book == null)
                    return NotFound("Book Not Found");

                return Ok(new BookModel(book));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("addBook")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            try
            {
                if(model != null)
                {
                    Book book = new Book()
                    {
                        Name = model.name,
                        Price = model.price,
                        Description = model.description,
                        Base64image = model.base64image,
                        Categoryid = model.categoryId,
                        Publisherid = model.publisherId,
                        Quantity = model.quantity,
                    };
                    var response = _repository.AddBook(book);
                    return Ok(new BookModel(response));
                }
                return BadRequest("Please Provide Correct Information");
            }catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }


        [HttpPut("updateBook")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            try
            {
                if(model != null)
                {
                    Book book = new Book()
                    {
                        Id = model.id,
                        Name = model.name,
                        Price = model.price,
                        Description = model.description,
                        Base64image = model.base64image,
                        Categoryid = model.categoryId,
                        Publisherid = model.publisherId,
                        Quantity = model.quantity,

                    };
                    var isUpdated = _repository.UpdateBook(book);
                    if (isUpdated)
                        return Ok("Book Detail Updated Successfully");

                    return NotFound("Book Not Found");
                }
                return BadRequest("Please Provide Correct Information");
            }catch(Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var isDeleted = _repository.DeleteBook(id);
                if (isDeleted)
                    return Ok("Book Deleted Successfully");

                return NotFound("Book Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
