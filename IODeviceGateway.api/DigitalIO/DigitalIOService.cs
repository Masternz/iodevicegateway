using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace IODeviceGateway.api.DigitalIO
{
    
    public interface IDigitalIOService
    {
        Task<DigitalInputViewDTO> GetInputAsynch(int pin);
        Task<List<DigitalInputViewDTO>> GetInputsAsynch();
    }

    public class DigitalIOService : IDigitalIOService
    {
        const int MAX_PIN = 7;
        const int MIN_PIN = 0;
        private readonly IConfiguration _configuration;
        private readonly HttpMessageInvoker _httpMessageInvoker;
        
        public DigitalIOService(IConfiguration configuration, HttpMessageInvoker messageInvoker)
        {
            _configuration = configuration;
            _httpMessageInvoker = messageInvoker;
        }
        
        public async Task<DigitalInputViewDTO> GetInputAsynch(int pin)
        {
            //http://192.168.1.130:8126/io/in/6

            var baseUrl = _configuration["ioDeviceBaseURI"];
            string internalResult = null;
            DigitalInputViewDTO result = null;
            PinInRange(pin);

            internalResult = await CallDevice(baseUrl, $"/io/in/{pin.ToString()}");

            var jsonResult = JsonConvert.DeserializeObject<DigitalInputViewEntity>(internalResult);

            result = MapInternalToExternal(jsonResult);

            return result;
        }

        private static void PinInRange(int pin)
        {
            if ((pin > MAX_PIN) && (pin <= MIN_PIN))
                throw new HttpRequestException($"PIN must be in the range {MIN_PIN} to {MAX_PIN}");
        }

        private async Task<string> CallDevice(string baseUrl, string parameterString)
        {
           // string stringResult = null;

            using(var httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Method = new HttpMethod(HttpMethod.Get.Method);
                httpRequestMessage.RequestUri = new Uri(baseUrl + parameterString);

                using (var httpResponseMessage = await _httpMessageInvoker.SendAsync(httpRequestMessage, default(CancellationToken)))
                {
                    httpResponseMessage.EnsureSuccessStatusCode();

                    var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                    return responseString;
                }

            }

        }

        private DigitalInputViewDTO MapInternalToExternal(DigitalInputViewEntity internalResult)
        {
            return new DigitalInputViewDTO()
            {
                pin = internalResult.Pin,
                state = internalResult.State == "ON" ? true : false
            };
        }

        public async Task<List<DigitalInputViewDTO>> GetInputsAsynch()
        {
            var baseUrl = _configuration["ioDeviceBaseURI"];
            List<DigitalInputViewEntity> internalResult = new List<DigitalInputViewEntity>();
            List<DigitalInputViewDTO> result = new List<DigitalInputViewDTO>();

            string jsonResult = await CallDevice(baseUrl, $"/io/in/");

            internalResult = JsonConvert.DeserializeObject<List<DigitalInputViewEntity>>(jsonResult);

            foreach(DigitalInputViewEntity entity in internalResult)
            {
                result.Add(MapInternalToExternal(entity));
            }

            return result;    
            
        }

    }
}
