using mySmartHome.Devices;
using mySmartHome.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mySmartHome
{
    public class DeviceTemplateSelector : DataTemplateSelector
    {
        DataTemplate doorBellTemplate;
        DataTemplate smokeTemplate;
        DataTemplate thermostatTemplate;

        public DeviceTemplateSelector()
        {
            doorBellTemplate = new DataTemplate(typeof(DoorBellViewCell));
            smokeTemplate = new DataTemplate(typeof(SmokeDetectorViewCell));
            thermostatTemplate = new DataTemplate(typeof(ThermostatViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is DoorBell)
                return doorBellTemplate;

            if (item is SmokeDetector)
                return smokeTemplate;

            if (item is Thermostat)
                return thermostatTemplate;

            throw new Exception($"Unknown device object: {item.GetType().Name}");
        }
    }
}
