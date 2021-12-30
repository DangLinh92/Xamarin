using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGas.Models
{
    public class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public int creation_Time { get; set; }
        public int expiration_Time { get; set; }
        public string user_Id { get; set; }
        public string user_Name { get; set; }
    }
}
