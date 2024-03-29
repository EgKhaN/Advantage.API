using Advantage.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;

        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/server
        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy(s=>s.ID).ToList(); 
            return Ok(response);
        }

        // GET api/server/5
        [HttpGet("{id}", Name ="GetServer")]
        public Server Get(int id)
        {
            return _ctx.Servers.Find(id);
        }

        // POST api/server
        [HttpPost]
        public IActionResult Post([FromBody] Server server)
        {
            if (server == null)
            {
                return BadRequest();
            }

            _ctx.Servers.Add(server);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetServer", new { id = server.ID }, server);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {

            var server = _ctx.Servers.FirstOrDefault(s => s.ID == id);

            if (server == null)
            {
                return NotFound();
            }

            // move update handling to a service, perhaps
            if(msg.Payload == "activate")
            {
                server.IsOnline = true;
                _ctx.SaveChanges();
            }

            if(msg.Payload == "deactivate")
            {
                server.IsOnline = false;
                _ctx.SaveChanges();
            }

            return new NoContentResult();
        }

        // DELETE api/server/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var server = _ctx.Servers.FirstOrDefault(t => t.ID == id);
            if (server == null)
            {
                return NotFound();
            }

            _ctx.Servers.Remove(server);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}