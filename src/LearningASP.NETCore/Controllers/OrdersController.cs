using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;


namespace LearningASP.NETCore.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        [HttpGet]
        [Route("info")]
        public async Task<ActionResult> Info()
        {
            return await Task.Run(() =>
            {
                return Json(new { name = "irving", age = 25 });
            }).ContinueWith(t => t.Result);
        }

        [HttpGet]
        [Route("user")]
        public ActionResult user()
        {
            return Json(new { name = "irving", age = 25 });
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
