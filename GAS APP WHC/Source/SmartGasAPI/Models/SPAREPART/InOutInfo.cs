using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Models.SPAREPART
{
    [Table("GOODS_RECEIPT_ISSUE_TYPE_DATA_TABLE_PUT_MOBILE")]
    public class InOutInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string RECEIPT_ISSUE_CODE { get; set; }
        public string SPARE_PART_CODE { get; set; }
        public string NAME { get; set; }
        public float QUANTITY { get; set; }
        public string UNIT { get; set; }
        public float PRICE_VN { get; set; }
        public float PRICE_US { get; set; }
        public float AMOUNT_VN { get; set; }
        public float AMOUNT_US { get; set; }
        public string CAUSE { get; set; }
        public string NOTE { get; set; }
        public string STOCK_CODE { get; set; }
        public string DEPT_CODE { get; set; }
        public string INT_OUT { get; set; }
        public string CREATE_DATE { get; set; }
        public string USER_CREATE { get; set; }
        public string USER_SYS { get; set; }
        public string ORDER_CODE { get; set; }
        public string STATUS { get; set; }
        public string TYPE_IN_OUT_CODE { get; set; }
        public string RETURN_TIME { get; set; }
        public string LOCATION { get; set; }
        public float QUANTITY_NG { get; set; }
        public string EXPRIRED_DATE { get; set; }
        public string IS_INTEGRATED { get; set; }
    }
}
