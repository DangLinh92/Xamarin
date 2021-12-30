using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Models.MRO
{
    [Table("GOODS_RECEIPT_ISSUE_TYPE_DATA_TABLE_PUT_MOBILE")]
    public class InOutInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string CODE { get; set; }
        public float LOT_QUANTITY { get; set; }
        public float QUANTITY_PER_STOCK { get; set; }
        public string MAKE_DATE { get; set; }
        public string EXP_DATE { get; set; }
        public string TRAN_USER { get; set; }
        public string LOT_NO { get; set; }
        public string NGUOI_THAO_TAC { get; set; }
        public float QUANTITY { get; set; }
        public string DEPARTMENT { get; set; }
        public string IN_OUT { get; set; }
    }
}
