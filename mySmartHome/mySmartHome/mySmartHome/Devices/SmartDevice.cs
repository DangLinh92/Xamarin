using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace mySmartHome.Devices
{
    public class SmartDevice: INotifyPropertyChanged
    {
        private string iConUrl;

        public string IconUrl
        {
            get { return iConUrl; }
            set => ChangePropertyValue(ref iConUrl, value);
        }

        private string name;

        public string Name
        {
            get { return name; }
            set => ChangePropertyValue(ref name, value);
        }

        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set => ChangePropertyValue(ref timeStamp, value);
        }

        public string Id { get; } = Guid.NewGuid().ToString();

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool ChangePropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
