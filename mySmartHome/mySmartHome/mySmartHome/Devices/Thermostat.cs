using System;
using System.Collections.Generic;
using System.Text;

namespace mySmartHome.Devices
{
    public class Thermostat : SmartDevice
    {
        public enum Status
        {
            Heating,
            Cooling,
            Normal,
        }

        public double CurrentTemperature
        {
            get => currentTemperature;
            set
            {
                ChangePropertyValue(ref currentTemperature, value);
                RaisePropertyChanged(nameof(ThermostatStatus));
                TimeStamp = DateTime.Now;
            }
        }
        double currentTemperature;

        public Status ThermostatStatus
        {
            get => GetStatus();
        }

        Status GetStatus()
        {
            if (TargetTemperature > CurrentTemperature)
                return Status.Heating;
            if (TargetTemperature < CurrentTemperature)
                return Status.Cooling;
            return Status.Normal;
        }

        public double TargetTemperature
        {
            get => targetTemperature;
            set
            {
                ChangePropertyValue(ref targetTemperature, value);
                RaisePropertyChanged(nameof(ThermostatStatus));
                TimeStamp = DateTime.Now;
            }
        }
        double targetTemperature;
    }
}
