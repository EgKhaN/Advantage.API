using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext _ctx;

        public CustomerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/customer/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Customers.OrderBy(c => c.ID);
            var page = new PaginatedResponse<Customer>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public Customer Get(int id)
        {
            return _ctx.Customers.Find(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.ID }, customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.ID != id)
            {
                return BadRequest();
            }

            var updatedCustomer = _ctx.Customers.FirstOrDefault(c => c.ID == id);

            if (updatedCustomer == null)
            {
                return NotFound();
            }

            updatedCustomer.Email = customer.Email;
            updatedCustomer.Name = customer.Name;
            updatedCustomer.State = customer.State;

            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _ctx.Customers.FirstOrDefault(t => t.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}