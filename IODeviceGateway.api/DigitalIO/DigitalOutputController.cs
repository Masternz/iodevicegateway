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
    public class DigitalOutputController : Controller
    {
        private readonly IDigitalOutputService _service;
        public DigitalOutputController(IDigitalOutputService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                System.Diagnostics.Trace.WriteLine("Outputs::Get");
                return Ok(await _service.GetOutputsAsync());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{pin}")]
        public async Task<IActionResult> Get(int pin)
        {
            try{
                return Ok(await _service.GetOutputAsync(pin));
            }
            catch(Exception) // could also be a 404 error.
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DigitalOutputUpdateDTO content)
        {
            
            // DigitalOutputUpdateDTO content = new DigitalOutputUpdateDTO(){pin=1,state=true};
            try{
                return Ok(await _service.UpdateOutputAsync(id, content));
            }
            catch
            {
                return StatusCode(500);
            }
        }
        
    }
}