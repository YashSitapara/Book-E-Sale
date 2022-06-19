using BookStore.models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        CartRepository _repository = new CartRepository();
        [HttpGet("getCart/list")]
        //[ProducesResponseType(typeof(List<CartModel>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetCartItems(int pageIndex=1,int pageSize=10,int userId=0)
        {
            try
            {
                var cart = _repository.GetCartItems(pageIndex,pageSize,userId);
                if(cart == null)
                    return BadRequest("Please Provide Correct Information");
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("getCart/{id}")]
        //[ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetCartItem(int id)
        {

            try
            {
                var cart = _repository.GetCartItem(id);
                if (cart == null)
                    return NotFound("Cart Not Found");

                return Ok(new CartModel(cart));
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("addCart")]
        //[ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCart(CartModel model)
        {

            try
            {
                if (model != null)
                {
                    Cart cart = new Cart()
                    {
                        Userid = model.Userid,
                        Bookid = model.Bookid,
                        Quantity = 1,
                    };
                    var response = _repository.AddCartItem(cart);

                    return Ok(new CartModel(response));
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("updateCart")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCart(CartModel model)
        {
            try
            {
                if (model != null)
                {
                    Cart cart= new Cart()
                    {
                        Id = model.Id,
                        Userid = model.Userid,
                        Bookid = model.Bookid,
                        Quantity = model.Quantity,
                    };
                    var isUpdated = _repository.UpdateCart(cart);
                    if (isUpdated)
                        return Ok("Cart Detail Updated Successfully");

                    return NotFound("Cart Not Found");
                }
                return BadRequest("Please Provide Correct Information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var isDeleted = _repository.DeleteCart(id);
                if (isDeleted)
                    return Ok("Cart Deleted Successfully");

                return NotFound("Cart Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

    }
}
