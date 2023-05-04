using System;
using AndroidX.Activity;

namespace DeAround.Droid.Callbacks {
	public class BackPressedCallback : OnBackPressedCallback {
		public BackPressedCallback (bool enabled) : base (enabled)
		{
		}

		public override void HandleOnBackPressed ()
		{
			Rg.Plugins.Popup.Popup.SendBackPressed ();
		}
	}
}
