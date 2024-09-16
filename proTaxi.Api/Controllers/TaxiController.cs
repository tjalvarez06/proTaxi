using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proTaxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        public TaxiController() 
        {
        
        }
        // GET: api/<TaxiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaxiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaxiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TaxiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaxiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
