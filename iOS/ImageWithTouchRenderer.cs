using System;

using Xamarin.Forms;

namespace PruSign.iOS
{
	public class ImageWithTouchRenderer : ContentPage
	{
		public ImageWithTouchRenderer()
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

