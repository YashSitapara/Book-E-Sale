using BookStore.models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        RoleRepository _repository = new RoleRepository();
        [HttpGet("getRole/list")]
        //[ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetRoles(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var roles = _repository.GetRoles(pageIndex, pageSize, keyword);
                if (roles == null)
                    return BadRequest("Please Provide Correct Information");

                ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
                {
                    records = roles.records.Select(c => new RoleModel(c)),
                    totalRecords = roles.totalRecords
                };
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("getRole/{id}")]
        //[ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetRole(int id)
        {
            try
            {
                var role = _repository.GetRole(id);
                if (role == null)
                    return NotFound("Role Not Found");

                return Ok(new RoleModel(role));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("addRole")]
        //[ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddRole(RoleModel model)
        {
            try
            {
                if (model != null)
                {
                    Role role= new Role()
                    {
                        Name = model.Name,
                    };
                    var response = _repository.AddRole(role);
                    return Ok(new RoleModel(response));
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }


        [HttpPut("updateRole")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateRole(RoleModel model)
        {
            try
            {
                if (model != null)
                {
                    Role role= new Role()
                    {
                        Id = model.Id,
                        Name = model.Name,
                    };
                    var isUpdated = _repository.UpdateRole(role);
                    if (isUpdated)
                        return Ok("Role Detail Updated Successfully");

                    return NotFound("Role Not Found");
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                var isDeleted = _repository.DeleteRole(id);
                if (isDeleted)
                    return Ok("Role Deleted Successfully");

                return NotFound("Role Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
