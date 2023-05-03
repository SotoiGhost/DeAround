using System;

using Xamarin.Forms;

using DeAround.Models;

namespace DeAround.Controls {
	public class BluetoothPermissionStateDataTemplateSelector : DataTemplateSelector {
		public DataTemplate? DefaultTemplate { get; set; }
		public DataTemplate? OtherTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate (object item, BindableObject container)
		{
			var bluetoothPermissionStatus = (BluetoothPermissionStatus) item;
			return bluetoothPermissionStatus == BluetoothPermissionStatus.NotAllowed ? OtherTemplate! : DefaultTemplate!;
		}
	}
}
