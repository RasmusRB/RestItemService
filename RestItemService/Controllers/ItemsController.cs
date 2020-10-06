using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;
using RestItemService.Controllers.DBUtil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestItemService.Controllers
{
    [Route("api/localItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        ManageItems items = new ManageItems();

        private static List<Item> _items = new List<Item>()
        {
            new Item(1, "Bread", "low", 1.0),
            new Item(2, "Fruit", "Fresh", 10.0),
            new Item(3, "Meat", "dead", 5.0),
            new Item(4, "Soda", "Poor", 6.5)
        };

        /// <summary>
        /// Get's all items
        /// </summary>
        /// <returns></returns>
        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items.Get();
        }

        /// <summary>
        /// Get's item by ID + status codes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        public Item Get(int id)
        {
            return items.GetId(id);
        }

        /// <summary>
        /// Get filter item + status codes
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("Search")]
        //[ProducesResponseType(statusCode: 200)]
        //[ProducesResponseType(statusCode: 404)]
        //public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        //{
        //    List < Item > tmpList = null;
        //    if (filter.HighQuantity != null)
        //    {
        //        tmpList = _items.FindAll(f => f.Name.Contains(filter.HighQuantity));
        //    }
        //    else
        //    {
        //        tmpList = _items;
        //    }

        //    return tmpList;
        //}

        /// <summary>
        /// Get item through substring
        /// </summary>
        /// <param name="substring"></param>
        /// <returns></returns>
        // GET api/<ItemsController>
        //[HttpGet]
        //[Route("Name/{substring}")]
        //public IEnumerable<Item> GetFromSubstring(String substring)
        //{
        //    List<Item> items;
        //    items = _items.FindAll(i => i.Name.Contains(substring));
        //    return items;
        //}

        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="value"></param>
        // POST api/<ItemsController>
        [HttpPost]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        public void Post([FromBody] Item value)
        {
            items.Post(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            items.Put(id, value);
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            items.Delete(id);
        }
    }
}
