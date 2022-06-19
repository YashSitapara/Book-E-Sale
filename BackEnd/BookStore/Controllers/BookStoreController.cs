using bookstore;
using BookStore.models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/public")]
    public class BookStoreController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        DemoAES obj = new DemoAES();

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                User user = new User()
                {
                    Email = model.Email,
                    Password = obj.ComputeMD5Hash(model.Password)
                };
                User response = _repository.Login(user);
                if (response == null)
                    return BadRequest("Please Provide Correct Information");
                
                return Ok(new UserModel(response));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);   
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                if (model != null)
                {
                    User user = new User()
                    {
                        Email = model.Email,
                        Password = obj.ComputeMD5Hash(model.Password),
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        Roleid = model.Roleid,
                    };
                    var response = _repository.Register(user);
                    return Ok(new UserModel(response));
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
