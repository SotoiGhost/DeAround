using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using DeAround.ViewModels;

namespace DeAround.Views {
	public partial class RequestBluetoothPermissionPage : PopupPage {
		BluetoothViewModel? bluetoothViewModel;

		public RequestBluetoothPermissionPage ()
		{
			InitializeComponent ();
			bluetoothViewModel = (BluetoothViewModel) BindingContext;
		}

		void OnClose (object sender, EventArgs e)
		{
			ClosePopup ();
		}

		void Request_Clicked (System.Object sender, System.EventArgs e)
		{
			bluetoothViewModel?.RequestBluetoothPermissionCommand.Execute (null);
			ClosePopup ();
		}

		void Cancel_Clicked (System.Object sender, System.EventArgs e)
		{
			ClosePopup ();
		}

		void ClosePopup ()
		{
			bluetoothViewModel?.Dispose ();
			bluetoothViewModel = null;
			PopupNavigation.Instance.PopAsync ();
		}
	}
}
