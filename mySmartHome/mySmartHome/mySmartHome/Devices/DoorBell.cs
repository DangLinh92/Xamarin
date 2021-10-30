using System;
using System.Collections.Generic;
using System.Text;

namespace mySmartHome.Devices
{
    public class DoorBell : SmartDevice
    {
        public enum Status
        {
            Normal,
            Ringing
        }

        private Status doorBellStatus;
        public Status DoorBellStatus
        {
            get=> doorBellStatus;
            set
            {
                ChangePropertyValue(ref doorBellStatus, value);
                TimeStamp = DateTime.Now;
            }
        }

        public DoorBell()
        {
            IconUrl = "door.png";
        }
    }
}
