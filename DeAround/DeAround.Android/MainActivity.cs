﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.Activity;

using DeAround.Droid.Callbacks;
using DeAround.Droid.Singletons;
using DeAround.Services;

namespace DeAround.Droid {
	[Activity (Label = "DeAround", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Rg.Plugins.Popup.Popup.Init (this);

			Xamarin.Essentials.Platform.Init (this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init (this, savedInstanceState);
			global::Xamarin.Forms.DependencyService.RegisterSingleton<IBluetoothService> (BluetoothServiceSingleton.SharedInstance);

			LoadApplication (new App ());

			OnBackPressedDispatcher.AddCallback (new BackPressedCallback (true));
		}

		public override void OnRequestPermissionsResult (int requestCode, string [] permissions, [GeneratedEnum] Android.Content.PM.Permission [] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult (requestCode, permissions, grantResults);

			if (requestCode == DeAround.Droid.Services.BluetoothService.RequestCode)
				BluetoothServiceSingleton.SharedInstance.NotifyUpdatedState ();

			base.OnRequestPermissionsResult (requestCode, permissions, grantResults);
		}
	}
}
