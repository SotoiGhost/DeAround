using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Rg.Plugins.Popup.Pages;
using DeAround.ViewModels;
using Rg.Plugins.Popup.Services;

namespace DeAround.Views {
	public partial class ServiceNotAllowedPage : PopupPage {
		public ServiceNotAllowedPage ()
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

