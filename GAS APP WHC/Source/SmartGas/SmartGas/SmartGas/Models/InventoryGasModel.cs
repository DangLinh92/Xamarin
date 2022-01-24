using SmartGas.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace SmartGas.Models
{
    public class InventoryGasModel : BaseViewModel
    {

        #region Fields
        /// <summary>
        /// Gets or sets a value indicating image path.
        /// </summary>
        private string imagePath;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the article image path.
        /// </summary>
        [DataMember(Name = "itemImage")]
        public ImageSource ImagePath
        {
            get
            {
                if (GasId == "ARGON")
                {
                    return ImageSource.FromFile("Arg.jpg");
                }
                else
                if (GasId == "CF4")
                {
                    return ImageSource.FromFile("cf4.jpg");
                }
                else
                if (GasId == "CO2")
                {
                    return ImageSource.FromFile("CO2.jpg");
                }
                else
                if (GasId == "O2")
                {
                    return ImageSource.FromFile("O2.jpg");
                }
                return ImageSource.FromFile("gas.jpg");
            }
        }

        /// <summary>
        /// Gets or sets the article name.
        /// </summary>
        [DataMember(Name = "gas_id")]
        public string GasId { get; set; }

        /// <summary>
        /// Gets or sets the article publish date.
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get => DateTime.Now.ToString("Y"); }

        [DataMember(Name = "quantity")]
        public float Quantity { get; set; }

        [DataMember(Name = "department")]
        public string Department { get; set; }

        [DataMember(Name = "month")]
        public int Month { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }

        public string QuantityBindding
        {
            get => "Quantity :" + Quantity;
        }

        #endregion
    }
}
