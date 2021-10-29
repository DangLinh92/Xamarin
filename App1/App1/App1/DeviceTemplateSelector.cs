using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1
{
    public class DeviceTemplateSelector : DataTemplateSelector
    {
        DataTemplate doorBellTemplate;
        DataTemplate smokeTemplate;
        DataTemplate thermostatTemplate;

        public DeviceTemplateSelector()
        {
            doorBellTemplate = new DataTemplate(typeof(TextCell));
            smokeTemplate = new DataTemplate(typeof(ContactViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //if (item is DoorBell)
            //    return doorBellTemplate;

            //if (item is SmokeDetector)
            //    return smokeTemplate;

            //if (item is Thermostat)
            //    return thermostatTemplate;

            throw new Exception("Could not find the device type");
        }


    }
}
