using SmartGas.ApiService;
using SmartGas.DataService;
using SmartGas.Helpers;
using SmartGas.Models;
using SmartGas.Services;
using SmartGas.ViewModels;
using SmartGas.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "Montserrat-Bold")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
[assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
[assembly: ExportFont("UIFontGas.ttf", Alias = "FontIcons_GAS")]
namespace SmartGas
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTUwNTM1QDMxMzkyZTM0MmUzMEs3UXpMcHk3VzdqeEVCRENxZkszM3R5Tk11TlJ4THQrQXJGTmY0akR3dkU9");

            DependencyService.Register<NavigationService>();

            InitializeComponent();

            var navigationService = DependencyService.Get<INavigationService>();
            // Use the NavigateToAsync<ViewModelName> method
            // to display the corresponding view.
            // Code lines below show how to navigate to a specific page.
            // Comment out all but one of these lines
            // to open the corresponding page.
            //navigationService.NavigateToAsync<LoginViewModel>();
            navigationService.NavigateToAsync<LoginViewModel>();
            // MainPage = new LoginPage();

            //_ = new ConnectActivityCheck();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public class ConnectActivityCheck
    {
        public ConnectActivityCheck()
        {
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var current = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            if (current == NetworkAccess.Internet && profiles.Contains(ConnectionProfile.WiFi))
            {
                // Connection to internet is available
                SmartGasDatabase database = await SmartGasDatabase.Instance;
                List<OUT_PUT_INFO> lstInfo = await database.GetItemsNotDoneAsync();

                if (lstInfo != null && lstInfo.Count > 0)
                {
                    bool isResult = true;
                    foreach (OUT_PUT_INFO item in lstInfo)
                    {
                        PutInOutModel model = new PutInOutModel();
                        if (GetActiveByDepartment.Active())// SPARE PART CODE
                        {
                            model.spare_part_code = item.spare_part_code;
                            model.name = item.name;
                            model.quantity = float.Parse(item.quantity);
                            model.unit = item.unit;
                            model.in_out = item.in_out;
                            model.location = item.location;
                            model.create_date = item.create_date;
                            model.exprired_date = item.exprired_date;
                            model.user_create = item.user_create;
                            model.department = item.department;
                        }
                        else
                        {
                            model.spare_part_code = item.spare_part_code;
                            model.quantity = float.Parse(item.quantity);
                            model.in_out = item.in_out;
                            model.create_date = item.create_date;
                            model.lot_no = item.lot_no;
                            model.tran_user = item.tran_user;
                            model.nguoi_thao_tac = item.nguoi_thao_tac;
                            model.department = item.department;
                        }

                        if (await InOutStockService.InStock(model))
                        {
                            await database.DeleteItemAsync(item);
                        }
                        else
                        {
                            isResult = false;
                        }
                    }

                    if (isResult)
                    {
                        await Application.Current.MainPage.DisplayAlert("Xuất kho thành công", "Export success", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Xuất kho thất bại", "Export fault", "OK");
                    }
                }
            }
        }
    }
}
