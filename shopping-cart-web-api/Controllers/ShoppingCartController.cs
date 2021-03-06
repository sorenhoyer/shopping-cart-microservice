﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public static Dictionary<string, List<Product>> inventory = new Dictionary<string, List<Product>> {
            {
                "1", new List<Product> {
                    new Product("Product 1", 100),
                    new Product("Product 2", 200)
                }
            }
        };

        EventStore eventStore = new EventStore();

        // GET api/values
        [HttpGet("{userId}")]
        public ActionResult<List<Product>> Get(string userId)
        {
            return inventory[userId];
        }

        // POST api/values
        [HttpPost("{userId}/items")]
        public void Post(string userId, [FromBody] Product product)
        {
            inventory[userId].Add(product);
            Console.WriteLine("Product with id: " + product.Id + " and price: " + product.Price + " saved");
            eventStore.Add(new Event("AddProduct" + product.Id));
            Ok(product);
        }

        // DELETE api/values/5
        [HttpDelete("{userId}/items/{id}")]
        public void Delete(string userId, string id)
        {
            for (int i = 0; i < inventory[userId].Count; i++)
            {
                var product = inventory[userId][i];
                if (product.Id.ToString() == id)
                {
                    inventory[userId].RemoveAt(i);
                    Console.WriteLine("Removed product with id: " + product.Id);
                    eventStore.Add(new Event("DeleteProduct" + product.Id));
                    Ok(product);
                }
            }
            NotFound();
        }

        [HttpGet("events")]
        public ActionResult<List<string>> GetEvents(long from, long to)
        {
            Response.Headers.Add("cache-control", "private, max-age:3600");
            return Ok(eventStore.getEvents(from, to));
        }
    }
}
