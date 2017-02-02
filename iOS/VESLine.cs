using System;

using Xamarin.Forms;

namespace PruSign.iOS
{
	public class VESLine : ContentPage
	{
		public VESLine()
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

