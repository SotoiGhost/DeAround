using System;

using Xamarin.Forms;

using DeAround.Models;

namespace DeAround.Controls {
	public class SearchingDataTemplateSelector : DataTemplateSelector {
		public DataTemplate? DefaultTemplate { get; set; }
		public DataTemplate? OtherTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate (object item, BindableObject container)
		{
			var searching = (bool) item;
			return searching ? OtherTemplate! : DefaultTemplate!;
		}
	}
}
