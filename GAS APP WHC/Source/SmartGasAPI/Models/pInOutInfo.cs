using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Models
{
    public class pInOutInfo
    {
        //SPAREPART 
        public string receipt_issue_code { get; set; }
        public string spare_part_code { get; set; }
        public string name { get; set; }
        public float quantity { get; set; }
        public string unit { get; set; }
        public string stock_code { get; set; }
        public string create_date { get; set; }
        public string user_create { get; set; }
        public string user_sys { get; set; }
        public string order_code { get; set; }
        public string status { get; set; }
        public string type_in_out_code { get; set; }
        public string location { get; set; }
        public string exprired_date { get; set; }
        public string is_integrated { get; set; }

        //MRO
        public string code { get; set; }
        public string lot_quantity { get; set; }
        public float quantity_per_stock { get; set; }
        public string make_date { get; set; }
        public string exp_date { get; set; }
        public string tran_user { get; set; }
        public string lot_no { get; set; }
        public string nguoi_thao_tac { get; set; }
        public string department { get; set; }
        public string in_out { get; set; }
    }
}
