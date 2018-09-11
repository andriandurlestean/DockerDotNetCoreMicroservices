using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Context;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly TestWebApiDbContext _context;

        public ValuesController(TestWebApiDbContext context)
        {
            _context = context;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<Catalog> Get()
        {
            var result = _context.Catalogs.ToList();

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var client = new HttpClient();
            var result = await client.GetAsync("http://proxy/second/api/values");
            return await result.Content.ReadAsStringAsync();
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
