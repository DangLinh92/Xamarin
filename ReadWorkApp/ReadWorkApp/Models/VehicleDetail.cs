using System;
using System.Collections.Generic;
using System.Text;

namespace ReadWorkApp.Models
{
    public class VehicleDetail
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string model { get; set; }
        public string engine { get; set; }
        public string color { get; set; }
        public string company { get; set; }
        public DateTime datePosted { get; set; }
        public string condition { get; set; }
        public bool isHotAndNew { get; set; }
        public bool isFeatured { get; set; }
        public string location { get; set; }
        public List<Image> images { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string imageUrl { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
        public string imageUrl { get; set; }
        public int vehicleId { get; set; }
        public object mageArray { get; set; }
    }
}
