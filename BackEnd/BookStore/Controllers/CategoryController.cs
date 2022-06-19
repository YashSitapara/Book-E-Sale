using BookStore.models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _repository = new CategoryRepository();
        [HttpGet("getCategory/list")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetCategories(int pageIndex = 1,int pageSize = 10,string? keyword = "")
        {
            try
            {
                var categories = _repository.GetCategories(pageIndex, pageSize, keyword);
                if (categories == null)
                    return BadRequest("Please Provide Correct Information");

                ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
                {
                    records = categories.records.Select(c => new CategoryModel(c)),
                    totalRecords = categories.totalRecords,
                };
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("getCategory/{id}")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetCategory(int id)
        {

            try
            {
                var category = _repository.GetCategory(id);
                if (category == null)
                    return NotFound("Category Not Found");

                return Ok(new CategoryModel(category));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("addCategory")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {

            try
            {
                if (model != null) { 
                    Category category = new Category()
                    {
                        Name = model.Name
                    };
                    var response = _repository.AddCategory(category);

                    return Ok(new CategoryModel(response));
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("updateCategory")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            try
            {
                if (model != null)
                {
                    Category category = new Category()
                    {
                        Id = model.Id,
                        Name = model.Name
                    };
                    var isUpdated = _repository.UpdateCategory(category);
                    if (isUpdated)
                        return Ok("Category Detail Updated Successfully");

                    return NotFound("Category Not Found");
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var isDeleted = _repository.DeleteCategory(id);
                if (isDeleted)
                    return Ok("Category Deleted Successfully");

                return NotFound("Category Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
