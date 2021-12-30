using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Models.SPAREPART
{
    [Table("EWIP_UNITS")]
    public class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
    }
}
