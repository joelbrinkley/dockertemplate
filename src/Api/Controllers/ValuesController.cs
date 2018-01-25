using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging;
using Microsoft.AspNetCore.Mvc;
using Core.Logging;
using Core;

namespace Api.Controllers {
    [Route("api/[controller]")]
    public class ValuesController : Controller {
        private readonly NatsBus _bus;
        private readonly IValuesRepository _valuesRepository;
        private readonly ILog _log;

        public ValuesController(NatsBus bus, IValuesRepository valuesRepository, ILog log) {
            this._bus = bus;
            this._valuesRepository = valuesRepository;
            this._log = log;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get() {
            var values = await this._valuesRepository.GetAllValuesAsync();
            return values.Select(x=>x.Description);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) {

            _log.LogInfo($"Post: Value:{value}");

            var addValueCommand = new AddValueCommand() {
                Value = value
            };

            _bus.Publish(addValueCommand);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}