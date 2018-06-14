using System;

namespace IODeviceEmulator.api.DataObjects
{    
    
    public class DigitalInputViewDTO
    {
        public int pin { get; set; }
        public string state { get; set; }
    }
    public class DigitalInputUpdateDTO
    {
        public bool toggle { get; set; }
        public string state{get;set;}
    }

        public class DigitalOutputUpdateDTO
    {
        public string state { get; set; }
        public bool toggle { get; set; }
    }

        public class DigitalOutputViewDTO
    {
        public int pin { get; set; }
        public string state { get; set; }
    }

}