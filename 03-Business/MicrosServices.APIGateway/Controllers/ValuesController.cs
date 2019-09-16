using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nacos;

namespace MicrosServices.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private readonly INacosConfigClient _configClient;
        public ValuesController(INacosConfigClient configClient)
        {
            _configClient = configClient;
        }
        // GET api/config?key=demo1
        [HttpGet("")]
        public async Task<string> Get([FromQuery]string key)
        {
            var res = await _configClient.GetConfigAsync(new GetConfigRequest
            {
                DataId = key,
                Group = "DEFAULT_GROUP",
                //Tenant = "tenant"
            });
            return string.IsNullOrWhiteSpace(res) ? "Not Found" : res;
        }
    }
}
