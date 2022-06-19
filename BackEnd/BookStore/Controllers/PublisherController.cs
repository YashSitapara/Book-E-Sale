using BookStore.models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class publisherController : ControllerBase
    {
        publisherRepository _repository = new publisherRepository();
        [HttpGet("getPublisher/list")]
        [ProducesResponseType(typeof(ListResponse<publisherModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetPublishers(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var publishers = _repository.GetPublishers(pageIndex, pageSize, keyword);
                if (publishers == null)
                    return BadRequest("Please Provide Correct Information");
                ListResponse<publisherModel> listResponse = new ListResponse<publisherModel>()
                {
                    records = publishers.records.Select(c => new publisherModel(c)),
                    totalRecords = publishers.totalRecords,
                };
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("getPublisher/{id}")]
        [ProducesResponseType(typeof(publisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetPublisher(int id)
        {
            try
            {
                Publisher publisher = _repository.GetPublisher(id);
                if(publisher == null)
                    return NotFound("Publisher Not Found");
                return Ok(new publisherModel(publisher)); ;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("addPublisher")]
        [ProducesResponseType(typeof(publisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddPublisher(publisherModel model)
        {
            try
            {
                if (model != null)
                {
                    Publisher publisher = new Publisher()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Contact = model.Contact,
                    };
                    var response = _repository.AddPublisher(publisher);
                    return Ok(new publisherModel(response));
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("updatePublisher")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePublisher(publisherModel model)
        {
            try
            {
                if (model != null)
                {
                    Publisher publisher = new Publisher()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Address = model.Address,
                        Contact = model.Contact,
                    };
                    var isUpdated = _repository.UpdatePublisher(publisher);
                    if (isUpdated)
                        return Ok("Publisher Detail Updated Successfully");

                    return NotFound("Publisher Not Found");
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
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                var isDeleted = _repository.DeletePublisher(id);
                if (isDeleted)
                    return Ok("Publisher Deleted Successfully");

                return NotFound("Publisher Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
