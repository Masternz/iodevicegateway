using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IODeviceGateway.api.DigitalIO
{
    
    [Route("api/[controller]")]
    public class DigitalIOController : Controller
    {
        
        private readonly IDigitalIOService _service;
        public DigitalIOController(IDigitalIOService service)
        {
            _service = service;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{pin}")]
        public async Task<IActionResult> Get(int pin)
        {
            try
            {
                return Ok(await _service.GetInputAsynch(pin));
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{pin},{pulse}")]
        public void Put(int pin, bool pulse)
        {
        }

    }

}
