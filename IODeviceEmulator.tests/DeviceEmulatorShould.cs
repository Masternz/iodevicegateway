using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IODeviceEmulator.tests
{
    public class DeviceEmulatorShould
    {

        private static readonly HttpClient client = new HttpClient();
        private static string _baseURL = "http://localhost:8088";

        [Fact]
        public async void ReturnA400BadRequestWhenPinIsOutOfNormalRange()
        {
            // Set up client

            // Call service
            var str = await CallDevice(_baseURL,"");

            // Inspect results.

        }

        [Fact]
        public void ReturnTheStateOfThePINOnAGET()
        {
            // Set up Emulator so that the PIN is set to known value

            

            // Check the PIN value

        }




        private async Task<string> CallDevice(string baseUrl, string parameterString)
        {
           // string stringResult = null;

            var stringTask = await client.GetStringAsync($"{baseUrl}{parameterString}");

            return stringTask;

        }





    }
}
