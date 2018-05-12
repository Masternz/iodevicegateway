using System;

namespace IODeviceGateway.api.DigitalIO
{
    public class DigitalInputViewDTO
    {
        public int pin { get; set; }
        public bool state { get; set; }
    }
    public class DigitalInputUpdateDTO
    {
        public int pin { get; set; }
        public bool toggle { get; set; }
    }
}
