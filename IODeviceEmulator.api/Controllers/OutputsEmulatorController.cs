using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IODeviceEmulator.api.Services;
using IODeviceEmulator.api.DataObjects;


namespace IODeviceEmulator.api.Controllers
{
    [Route("io/[controller]")]
    public class OutController : Controller
    {
        private readonly IEmulatorService _service;

        public OutController(IEmulatorService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return _service
            return Ok(_service.GetAllOutputs());
        }

        [HttpDelete]
        public async Task<IActionResult> SetAllOutputsToZero()
        {
            //return _service
            return Ok(_service.ResetOutputs());
        }


        // GET api/values/5
        [HttpGet("{pin}")]
        public async Task<IActionResult> Get(int pin)
        {
            try
            {
                return Ok(_service.GetOutput(pin));
            }
            catch(HttpException exc)
            {
                // Should log something here
                if(exc.Code==400)
                    return NotFound(exc.Message);
                else
                    return StatusCode(500);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        // An Action that sets all the Outputs to false.
        


        // PUT api/values/5
        [HttpPut("{pin}")]
        public async Task<IActionResult> Put(int pin, [FromBody]DigitalOutputUpdateDTO update)
        {
            try
            {
                return Ok(_service.SetOutput(pin, update));
            }
            catch(HttpException exc)
            {
                if(exc.Code==400)
                    return NotFound(exc.Message);
                else
                    return StatusCode(500);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
