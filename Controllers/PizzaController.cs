using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DebugDemo.Models;
using DebugDemo.Services;
using Microsoft.AspNetCore.Authorization;

namespace DebugDemo.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController()
        {
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();
        // GET by Id action

        // http://localhost:5000/api/pizza/1
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            // accessing request headers
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

           // Request.Headers.Remove("Authorization");


            //print id
            System.Console.WriteLine(id);
            var pizza = PizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            //PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }


        // example query
        //api/pizza/byName?firstName=ahnaf&lastName=ali
        [HttpGet("byName")]
        public string Welcome(string firstName, string lastName)
        {
            System.Console.WriteLine(firstName);
            System.Console.WriteLine(lastName);

            return $"welcome to my world";
        }
    }
}