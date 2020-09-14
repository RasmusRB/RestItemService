using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestItemService.Controllers
{
    [Route("api/localItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static List<Item> _items = new List<Item>()
        {
            new Item(1, "Bread", "low", 1.0),
            new Item(2, "Fruit", "Fresh", 10.0),
            new Item(3, "Meat", "dead", 5.0),
            new Item(4, "Soda", "Poor", 6.5)
        };

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _items;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public IActionResult Get(int id)
        {
            if (_items.Exists(i => i.Id == id))
            {
                return Ok(_items.Find(i => i.Id == id));
            }

            return NotFound($"Item med id: {id} findes ikke.");
        }

        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {

            return ;
        }

        // GET api/<ItemsController>
        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(String substring)
        {
            List<Item> items;
            items = _items.FindAll(i => i.Name.Contains(substring));
            return items;
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            _items.Add(value);
        }

        // PUT api/<ItemsController>/5
        //[HttpPut]
        //[Route("{id}")]
        //public void Put(int id, [FromBody] Item value)
        //{
        //    Item item = Get(id);
        //    if (item != null)
        //    {
        //        item.Id = value.Id;
        //        item.Name = value.Name;
        //        item.Quality = value.Quality;
        //        item.Quantity = value.Quantity;
        //    }
        //}

        //// DELETE api/<ItemsController>/5
        //[HttpDelete]
        //[Route("{id}")]
        //public void Delete(int id)
        //{
        //    Item item = Get(id);
        //    _items.Remove(item);
        //}
    }
}
