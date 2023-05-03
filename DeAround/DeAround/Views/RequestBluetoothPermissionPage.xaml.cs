using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace DeAround.Views {
	public partial class RequestBluetoothPermissionPage : PopupPage {
		public RequestBluetoothPermissionPage ()
		{
			InitializeComponent ();
		}

		void OnClose (object sender, EventArgs e)
		{
			PopupNavigation.Instance.PopAsync ();
		}
	}
}
