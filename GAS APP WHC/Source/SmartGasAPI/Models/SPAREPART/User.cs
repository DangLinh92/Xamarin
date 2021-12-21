using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Models.SPAREPART
{
    [Table("ESYSMSTUSR")]
    public class User
    {
        public string PLANT { get; set; }
        public string DEPARTMENT { get; set; }
        public string USER_ID { get; set; }
        public string PASSWORD { get; set; }
        public string USER_NAME { get; set; }
        public string PHONE_NUM { get; set; }
        public string EMAIL { get; set; }
        public string USEFLAG { get; set; }
        public string ADDRESS { get; set; }
        public string REMARKS { get; set; } // image Url
        public string CREATE_TIME { get; set; }
        public string CREATE_USER { get; set; }
        public string UPDATE_TIME { get; set; }
        public string UPDATE_USER { get; set; }
        public string MASTER_FLAG { get; set; }
    }
}
