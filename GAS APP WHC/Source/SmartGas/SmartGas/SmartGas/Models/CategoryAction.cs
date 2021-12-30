using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace SmartGas.Models
{
    public class CategoryAction : ObservableObject
    {
        public string CategoryName { get; set; }

        public string Glyph { get; set; }

        /// <summary>
        /// Gets or sets the property that has been displays the background gradient start.
        /// </summary>
        public string BackgroundGradientStart { get; set; }

        /// <summary>
        /// Gets or sets the property that has been displays the background gradient end.
        /// </summary>
        public string BackgroundGradientEnd { get; set; }

    }
}
