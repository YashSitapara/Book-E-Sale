using BookStore.models.Models;
using BookStore.Repositories;
using BookStore.models.ViewModels;
using BookStore.models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using bookstore;

namespace BookStore.Controllers {

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DemoAES obj = new DemoAES();
        UserRepository _repository = new UserRepository();
        [HttpGet("getUser/list")]
        [ProducesResponseType(typeof(ListResponse<UserModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetUsers(int pageIndex = 1,int pageSize = 10,string? keyword = "")
        {
            try
            {
                var users = _repository.GetUsers(pageIndex, pageSize, keyword);
                if(users == null)
                    return BadRequest("Please Provide Correct Information");
                ListResponse<UserModel> listResponse = new ListResponse<UserModel>()
                {
                    records = users.records.Select(c => new UserModel(c)),
                    totalRecords = users.totalRecords,
                };
                return Ok(listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("getUser/{id}")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _repository.GetUser(id);
                
                if (user == null)
                    return NotFound("User Not Found");

                return Ok(new UserModel(user));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("updateUser")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateUser(UserModel model)
        {
            try
            {
                if(model != null)
                {
                    User user = new User()
                    {
                        Id = model.id,
                        Firstname = model.firstName,
                        Lastname = model.lastName,
                        Email = model.email,
                        Password = obj.ComputeMD5Hash(model.password),
                        Roleid = model.roleId,
                    };
                    var Updated = _repository.UpdateUser(user);
                    if (Updated!= null)
                        return Ok(new UserModel(Updated));

                    return NotFound("User Not Found");

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
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var isDeleted = _repository.DeleteUser(id);
                if (isDeleted)
                    return Ok("User Deleted Successfully");

                return NotFound("User Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
