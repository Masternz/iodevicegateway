using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace IODeviceGateway.api.DigitalIO
{

public interface IDigitalOutputService
{
    Task<DigitalOutputViewDTO> UpdateOutputAsync(int pin, DigitalOutputUpdateDTO outputPin);
    Task<DigitalOutputViewDTO> GetOutputAsync(int pin);

    Task<List<DigitalOutputViewDTO>> GetOutputsAsync();
}


    class DigitalOutputService : IDigitalOutputService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpMessageInvoker _httpMessageInvoker;
        private const int MAX_PIN = 7;
        private const int MIN_PIN = 0;

        public DigitalOutputService(IConfiguration configuration, HttpMessageInvoker httpMessageInvoker)
        {
            _configuration = configuration;
            _httpMessageInvoker = httpMessageInvoker;
        }
        
        public Task<DigitalOutputViewDTO> GetOutputAsync(int pin)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DigitalOutputViewDTO>> GetOutputsAsync()
        {
            var baseUrl = _configuration["ioDeviceBaseURI"];
            string internalResult = null;
            List<DigitalOutputViewDTO> result = new List<DigitalOutputViewDTO>();

            internalResult = await CallDevice(baseUrl, $"/io/out");

            var formatedResult = JsonConvert.DeserializeObject<List<DigitalOutputViewEntity>>(internalResult);

            foreach(DigitalOutputViewEntity entity in formatedResult)
            {
                result.Add(MapInternalToExternal(entity));
            }
            return result;
        }

        public async Task<DigitalOutputViewDTO> UpdateOutputAsync(int pin, DigitalOutputUpdateDTO outputPin)
        {
            var baseUrl = _configuration["ioDeviceBaseURI"];
            DigitalOutputViewDTO result = null;

            string internalResult = null;

            // the body for the put call

           var messageBody = MapPUTDTOtoEntity(outputPin);

            internalResult = await CallDevicePut(baseUrl, $"/io/out/{pin.ToString()}", messageBody);

            var formatedResult = JsonConvert.DeserializeObject<DigitalOutputViewEntity>(internalResult);

            result = FormatOutputViewEntityToDTOEntity(formatedResult);
            
            return result;

        }

        private DigitalOutputUpdateEntity MapPUTDTOtoEntity(DigitalOutputUpdateDTO outputPin)
        {
            return new DigitalOutputUpdateEntity()
            {
                state = outputPin.state == true ? "ON" : "OFF",
                toggle = outputPin.toggle
            };
        }

        private DigitalOutputViewDTO FormatOutputViewEntityToDTOEntity(DigitalOutputViewEntity formatedResult)
        {
            return new DigitalOutputViewDTO()
            {
                pin = formatedResult.pin,
                state = formatedResult.state == "ON" ? true : false,
            };
        }

        private async Task<string> CallDevicePut(string baseUrl, string parameterString, DigitalOutputUpdateEntity entity)
        {
            
            using (var httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Method = new HttpMethod(HttpMethod.Put.Method);
                httpRequestMessage.RequestUri = new Uri(baseUrl + parameterString);
                var jsonMessage = JsonConvert.SerializeObject(entity);

                httpRequestMessage.Content = new StringContent(jsonMessage);

                using (var httpResponseMessage = await _httpMessageInvoker.SendAsync(httpRequestMessage, default(CancellationToken)))
                {
                    httpResponseMessage.EnsureSuccessStatusCode();

                    var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                    return responseString;
                }


            }

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
        private DigitalOutputViewDTO MapInternalToExternal(DigitalOutputViewEntity entity)
        {
            return new DigitalOutputViewDTO()
            {
                pin = entity.pin,
                state = entity.state.ToUpper() == "ON" ? true : false
            };
        }

        private static void PinInRange(int pin)
        {
            if ((pin > MAX_PIN) && (pin <= MIN_PIN))
                throw new HttpRequestException($"PIN must be in the range {MIN_PIN} to {MAX_PIN}");
        }

    }

}