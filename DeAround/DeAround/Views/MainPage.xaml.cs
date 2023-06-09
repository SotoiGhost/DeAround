﻿using System;
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

			if (bluetoothViewModel.IsFirstTime) {
				var page = new RequestBluetoothPermissionPage ();
				await PopupNavigation.Instance.PushAsync (page);
			} else {
				if (bluetoothViewModel.IsSearching) {
					bluetoothViewModel.StopSearchingCommand.Execute (null);
					btnSearch.Text = "Search";
				} else {
					bluetoothViewModel.StartSearchingCommand.Execute (null);
					btnSearch.Text = "Stop";
				}
			}
		}
	}
}

