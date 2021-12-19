using ReadWorkApp.Models;
using ReadWorkApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ReadWorkApp.ViewModel
{
    public class ExploreViewModel : ObservableObject
    {
        public ObservableRangeCollection<HotAndNewAd> HotVehicleCollection { get; set; }
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }

        public IAsyncCommand SearchCmd { get; }

        public ExploreViewModel()
        {
            SearchCmd = CommandFactory.Create(SearchData);
            HotVehicleCollection = new ObservableRangeCollection<HotAndNewAd>();
            GetHotAndNewVehicles();
        }

        private async Task SearchData()
        {
           var result = await ApiService.SearchVehicles(searchText);
        }

        public async void GetHotAndNewVehicles()
        {
           var vehicle = await ApiService.HotAndNewAds();
            HotVehicleCollection.AddRange(vehicle);
        }
    }
}
