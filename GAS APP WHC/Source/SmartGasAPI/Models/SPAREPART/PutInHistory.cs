using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Models.SPAREPART
{
    public class PutInHistory
    {
        public string SPARE_PART_CODE { get; set; }
        public string NAME { get; set; }
        public double QUANTITY { get; set; }
        public string UNIT { get; set; }
        public string CREATE_DATE { get; set; }
    }
}
