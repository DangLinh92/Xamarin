﻿using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CheckControl.Styles
{
    /// <summary>
    /// Class helps to reduce repetitive markup, and allows an apps appearance to be more easily changed.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Colors
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Styles" /> class.
        /// </summary>
        public Colors()
        {
            this.InitializeComponent();
        }
    }
}