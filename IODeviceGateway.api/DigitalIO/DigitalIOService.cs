using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace IODeviceGateway.api.DigitalIO
{
    
    public interface IDigitalIOService
    {
        Task<DigitalInputViewDTO> GetInputAsynch(int pin);
        List<DigitalInputViewDTO> GetInputs();
        bool SetInput(DigitalInputUpdateDTO input);
    }

    public class DigitalIOService : IDigitalIOService
    {
        IConfiguration _configuration;
        
        public DigitalIOService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<DigitalInputViewDTO> GetInputAsynch(int pin)
        {
            //http://192.168.1.130:8126/io/in/6
            
            var baseUrl = _configuration["ioDeviceBaseURI"];
            DigitalInputViewEntity internalResult = null;
            DigitalInputViewDTO result = null;

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = await client.GetAsync($"/io/in/{pin.ToString()}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    internalResult = JsonConvert.DeserializeObject<DigitalInputViewEntity>(stringResult);
                    
                }
                catch (Exception exc)
                {
                    System.Diagnostics.Trace.WriteLine(exc.Message);
                    throw exc;
                }
            }

            result = MapInternalToExternal(internalResult);

            return result;
        }

        private DigitalInputViewDTO MapInternalToExternal(DigitalInputViewEntity internalResult)
        {
            return new DigitalInputViewDTO()
            {
                pin = internalResult.Pin,
                state = internalResult.State == "ON" ? true : false
            };
        }

        public List<DigitalInputViewDTO> GetInputs()
        {
            throw new NotImplementedException();
        }

        public bool SetInput(DigitalInputUpdateDTO input)
        {
            throw new NotImplementedException();
        }
    }
}
