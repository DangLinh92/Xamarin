using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SmartGasAPI.Models
{
    public class InventoryGasModel
    {
        public string GAS_ID { get; set; }

        public string DATE { get; set; }

        public double QUANTITY { get; set; }

        public string DEPARTMENT { get; set; }

        public int MONTH { get; set; }

        public int YEAR { get; set; }
    }
}
