using SmartGas.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGas.Models
{
    public class OUT_PUT_INFO
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string spare_part_code { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
        public string unit { get; set; }
        public string in_out { get; set; } = Constants.OUT;
        public string location { get; set; }
        public string create_date { get; set; }
        public string exprired_date { get; set; }
        public string user_create { get; set; }
        public string department { get; set; }
        public string lot_no { get; set; }
        public string tran_user { get; set; }
        public string nguoi_thao_tac { get; set; }
        public string isDelete { get; set; }
        public string qrCode { get; set; }
    }
}
