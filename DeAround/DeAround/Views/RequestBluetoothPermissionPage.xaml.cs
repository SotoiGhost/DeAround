using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using DeAround.ViewModels;

namespace DeAround.Views {
	public partial class RequestBluetoothPermissionPage : PopupPage {
		public RequestBluetoothPermissionPage ()
		{
			InitializeComponent ();
		}

		void OnClose (object sender, EventArgs e)
		{
			ClosePopup ();
		}

		void Request_Clicked (System.Object sender, System.EventArgs e)
		{
			var bluetoothViewModel = (BluetoothViewModel) BindingContext;
			bluetoothViewModel.RequestBluetoothPermissionCommand.Execute (null);
			ClosePopup ();
		}

		void Cancel_Clicked (System.Object sender, System.EventArgs e)
		{
			ClosePopup ();
		}

		void ClosePopup ()
		{
			var bluetoothViewModel = (BluetoothViewModel) BindingContext;
			bluetoothViewModel.Dispose ();
			bluetoothViewModel = null;
			PopupNavigation.Instance.PopAsync ();
		}
	}
}
