using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using RawRabbit.Configuration.Request;
using RawRabbit.vNext.Disposable;
using TestWebApi.Context;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly TestWebApiDbContext _context;
        private readonly IBusClient _client;

        public ValuesController(TestWebApiDbContext context, IBusClient client)
        {
            _context = context;
            _client = client;
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

        [Route("send")]
        [HttpGet]
        public async Task<string> TestEvent()
        {
            Action<IRequestConfigurationBuilder> requestBuilder = (ctx) => ctx.WithExchange(xfg => xfg.WithName("testwebapi")); //.WithRoutingKey("mymessage"));
            var response = await _client.RequestAsync<BasicMessage, BasicResponse>(new BasicMessage {Prop = "FROM FIRST" }, configuration: requestBuilder);
            return response.Result;
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
