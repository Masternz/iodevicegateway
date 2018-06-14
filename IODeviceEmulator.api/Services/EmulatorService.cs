using System;
using IODeviceEmulator.api.DataObjects;
using System.Collections.Generic;

namespace IODeviceEmulator.api.Services
{
    public interface IEmulatorService
    {
        List<DigitalInputViewDTO> GetAllInputs();
        List<DigitalOutputViewDTO> GetAllOutputs();
        DigitalInputViewDTO GetInput(int pin);
        DigitalOutputViewDTO GetOutput(int pin);
        DigitalInputViewDTO SetInput(int pin, DigitalInputUpdateDTO input);
        DigitalOutputViewDTO SetOutput(int pin, DigitalOutputUpdateDTO output);
        List<DigitalInputViewDTO> ResetInputs();
        List<DigitalOutputViewDTO> ResetOutputs();
    }


    public class EmulatorService : IEmulatorService
    {
        private List<DigitalInputViewDTO> inputs = new List<DigitalInputViewDTO>();
        private List<DigitalOutputViewDTO> outputs = new List<DigitalOutputViewDTO>(); 
        const int MAX_PIN = 7;
        const int MIN_PIN = 0;

        public EmulatorService()
        {
            ResetInputs();
            ResetOutputs();
        }

        public List<DigitalInputViewDTO> GetAllInputs()
        {
            return inputs;
        }

        public List<DigitalOutputViewDTO> GetAllOutputs()
        {
            return outputs;
        }

        public DigitalInputViewDTO GetInput(int pin)
        {
            // handle out of bounds error
            CheckOutOfBounds(pin);
            return inputs[pin];
        }

        private void CheckOutOfBounds(int pin)
        {
            if(!((pin >= MIN_PIN) && (pin <= MAX_PIN)))
                throw new HttpException(400, "Out xxx of bounds PIN must be between {MIN_PIN} and {MAX_PIN}");

        }

        public DigitalOutputViewDTO GetOutput(int pin)
        {
            CheckOutOfBounds(pin);
            return outputs[pin];
        }

        public List<DigitalInputViewDTO> ResetInputs()
        {
            inputs.Clear();
            for(int pin = MIN_PIN; pin <= MAX_PIN; pin++ )
            {
                inputs.Add(new DigitalInputViewDTO(){pin = pin, state = "OFF"});
            }
            return inputs;
        }

        public List<DigitalOutputViewDTO> ResetOutputs()
        {
            outputs.Clear();
            for(int pin = MIN_PIN; pin <= MAX_PIN; pin++ )
            {
                outputs.Add(new DigitalOutputViewDTO(){pin = pin, state = "OFF"});
            }
            return outputs;
        }

        public DigitalInputViewDTO SetInput(int pin, DigitalInputUpdateDTO input)
        {
            CheckOutOfBounds(pin);
            if (!(input.toggle == true))
                inputs[pin] = MapInputUpdateToView(pin, input);
            return inputs[pin];
        }

        private DigitalInputViewDTO MapInputUpdateToView(int pin, DigitalInputUpdateDTO input)
        {
            return new DigitalInputViewDTO(){pin = pin, state = input.state};
        }

        public DigitalOutputViewDTO SetOutput(int pin, DigitalOutputUpdateDTO output)
        {
            CheckOutOfBounds(pin);
            if (!(output.toggle == true))
                outputs[pin] = MapOutputUpdateToView(pin, output);
            return outputs[pin];
        }

        private DigitalOutputViewDTO MapOutputUpdateToView(int pin, DigitalOutputUpdateDTO output)
        {
            return new DigitalOutputViewDTO(){pin = pin, state = output.state};
        }
    }
}
