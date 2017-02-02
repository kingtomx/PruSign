using System;

using Xamarin.Forms;

namespace PruSign.iOS
{
	public class DrawView : ContentPage
	{
		public DrawView()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

