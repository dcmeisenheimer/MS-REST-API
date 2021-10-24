using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ContonsoPizza.Models;
using ContonsoPizza.Services;
using System.Linq;

namespace ContonsoPizza.Controllers
{
    [ApiController]
    //Route defines a mapping to the controller token
    [Route("[controller]")]
    public class PizzaController : ControllerBase //Inherit from base controller
    {
        public PizzaController()
        {

        }

        //GET - To retrieve data from webservice
        [HttpGet]
        //Action to get all pizzas from the list when called
        public ActionResult<List<Pizza>> GetAll() => 
            PizzaService.GetAll();

        //When Get a pizza by ID you only return one pizza
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if(pizza == null)
                return NotFound();

            return pizza;
        }
        
        //Post - creates a new item of data on the web service        
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            //Code will save pizza and return a result

            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id}, pizza);

        }

        //PUT- used to update/modify an item of data on the web service    
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            //This code will update the pizza and return a result
            
            if (id != pizza.Id)
                return BadRequest();
            
            var existingPizza = PizzaService.Get(id);
            if(existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);

            return NoContent();
        }

        //DELETE - used to delete an Item of data on the web service
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //This code will delete the pizza and return a result
            var pizza = PizzaService.Get(id);

            if(pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }
    }
}
