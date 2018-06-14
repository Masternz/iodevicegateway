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
    public class InController : Controller
    {
        private readonly IEmulatorService _service;

        public InController(IEmulatorService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return _service
            return Ok(_service.GetAllInputs());
        }

        [HttpDelete]
        public async Task<IActionResult> ResetInputsToZero()
        {
            //return _service
            return Ok(_service.ResetInputs());
        }


        // GET api/values/5
        [HttpGet("{pin}")]
        public async Task<IActionResult> Get(int pin)
        {
            try
            {
                return Ok(_service.GetInput(pin));
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

        // An action that sets all the Inputs back to false
        



        // PUT api/values/5
        [HttpPut("{pin}")]
        public async Task<IActionResult> Put(int pin, [FromBody]DigitalInputUpdateDTO update)
        {
            try
            {
                return Ok(_service.SetInput(pin, update));
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
