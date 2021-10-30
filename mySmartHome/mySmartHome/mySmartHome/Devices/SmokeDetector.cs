using System;
using System.Collections.Generic;
using System.Text;

namespace mySmartHome.Devices
{
    public class SmokeDetector: SmartDevice
    {
        public enum Status
        {
            Normal,
            Smoke,
            Fire
        }

        public Status DetectorStatus
        {
            get=>detectorStatus;
            set
            {
                ChangePropertyValue(ref detectorStatus, value);
                TimeStamp = DateTime.Now;
            }
        }

        private Status detectorStatus = Status.Normal;
    }
}
