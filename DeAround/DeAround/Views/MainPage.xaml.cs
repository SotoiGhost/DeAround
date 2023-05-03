using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using DeAround.Models;
using DeAround.Services;
using DeAround.ViewModels;
using Rg.Plugins.Popup.Services;

namespace DeAround.Views {
	public partial class MainPage : ContentPage {
		public MainPage ()
		{
			InitializeComponent ();
		}

		async void btnSearch_Clicked (System.Object sender, System.EventArgs e)
		{
			var bluetoothViewModel = (BluetoothViewModel) BindingContext;

			//devicesCollection.EmptyView = bluetoothViewModel.BluetoothPermissionStatus.ToString ();

			//if (bluetoothViewModel.BluetoothPermissionStatus == BluetoothPermissionStatus.Allowed) {

			//} else {
			//	var page = new RequestBluetoothPermissionPage ();
			//	page.BindingContext = bluetoothViewModel;
			//	await PopupNavigation.Instance.PushAsync (page);
			//}
		}
	}
}

