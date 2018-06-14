using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace IODeviceGateway.api.DigitalIO
{
    
    [Route("api/[controller]")]
    public class DigitalInputController : Controller
    {
        
        private readonly IDigitalIOService _service;
        public DigitalInputController(IDigitalIOService service)
        {
            _service = service;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                return Ok(await _service.GetInputsAsynch());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/values/5
        [HttpGet("{pin}")]
        public async Task<IActionResult> Get(int pin)
        {
            try
            {
                return Ok(await _service.GetInputAsynch(pin));
            }
            catch(HttpRequestException httpRequestException)
            {
                System.Diagnostics.Trace.WriteLine($"HResult {httpRequestException.HResult.ToString()}");
                return BadRequest(httpRequestException.Message);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }

}
