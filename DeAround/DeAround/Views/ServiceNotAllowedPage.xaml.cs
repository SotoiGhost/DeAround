using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Rg.Plugins.Popup.Pages;
using DeAround.ViewModels;
using Rg.Plugins.Popup.Services;

namespace DeAround.Views {
	public partial class ServiceNotAllowedPage : PopupPage {
		BluetoothViewModel? bluetoothViewModel;

		public ServiceNotAllowedPage ()
		{
			InitializeComponent ();
			bluetoothViewModel = (BluetoothViewModel) BindingContext;
		}

		void OnClose (object sender, EventArgs e)
		{
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

		void OpenSettings_Clicked (System.Object sender, System.EventArgs e)
		{
			bluetoothViewModel?.OpenSettingsCommand.Execute (null);
			ClosePopup ();
		}
	}
}

