using System;
using System.Collections.Generic;
using System.Text;

namespace ReadWorkApp.Models
{
    public class UserImageModel
    {
        public string imageUrl { get; set; }

        public string FullImagePath => $"https://vihicleapp.azurewebsites.net/{imageUrl}";
    }
}
