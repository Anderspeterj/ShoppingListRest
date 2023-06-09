using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingLib;
using ShoppingListRest.Repositories;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingListRest.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    //URI: api/shoppingitems
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private ShoppingItemsRepository _repository;

        public ShoppingItemsController(ShoppingItemsRepository repository)
        {
            _repository = repository;
        }

        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<ShoppingItem>> Get([FromHeader] int? price,
    [FromQuery] string? namefilter,
    [FromQuery] int? quantity)
        {
            if (price.HasValue || !string.IsNullOrEmpty(namefilter))
            {
                var filteredItems = _repository.GetFiltered(price, namefilter);
                if (filteredItems.Count == 0)
                {
                    return NoContent();
                }
                return Ok(filteredItems);
            }
            else
            {
                var allItems = _repository.GetAll();
                if (allItems.Count == 0)
                {
                    return NoContent();
                }
                return Ok(allItems);
            }                  
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        public ActionResult<ShoppingItem> Post([FromBody] ShoppingItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            return _repository.Add(item);

        }

        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public ActionResult<ShoppingItem> Delete(int id)
        {
            
            // Check if the product exists before attempting to delete
            if (_repository.GetAll().Find(item => item.Id == id) == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return Ok();
           
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("totalprice")]
        public ActionResult<double> TotalPrice()
        {
            if (_repository.GetAll().Count == 0)
            {
                return NoContent();
            }
            return Ok(_repository.TotalPrice());

        }
     
    }
}
