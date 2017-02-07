using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PruSign
{
	public partial class PruSignPage : ContentPage
	{

		private const int PalleteSpacing = 3;
		private ImageWithTouch DrawingImage;
		private Dictionary<string, Color> ColorPallete;

		public PruSignPage()
		{
			InitializeComponent();
			Content = BuildGrid();
			Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 10);


		}



		private Grid BuildGrid()
		{
			return new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition {
						Height = new GridLength(1, GridUnitType.Auto)
					},
					new RowDefinition {
						Height = new GridLength(1, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength(1, GridUnitType.Auto)
					}
				},
				ColumnDefinitions = {
					new ColumnDefinition {
						Width = new GridLength(1, GridUnitType.Star)
					}
				},
				Children = {
					{ 
						new Image {
							Aspect = Aspect.AspectFit,
							Source = ImageSource.FromFile("prudential.png"),
							HorizontalOptions = LayoutOptions.CenterAndExpand,
							VerticalOptions = LayoutOptions.FillAndExpand
						}, 0, 0
					},
					/* { BuildPalletFrame(), 0, 1 }, */
				  	{
						new ContentView {
					   		Content = BuildDrawingFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 1
					},
					{
						new ContentView {
					   		Content = ButtonsFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 2
					}
				}
			};
		}







		private static Color GetTextColor(Color backgroundColor)
		{
			var backgroundColorDelta = ((backgroundColor.R * 0.3) + (backgroundColor.G * 0.6) + (backgroundColor.B * 0.1));

			return (backgroundColorDelta > 0.4f) ? Color.Black : Color.White;
		}




		private Frame BuildDrawingFrame()
		{
			DrawingImage = new ImageWithTouch
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				CurrentLineColor = Color.Black
			};

			DrawingImage.SetBinding(ImageWithTouch.CurrentLineColorProperty, "CurrentLineColor");

			var palleteFrame = new Frame
			{
				BackgroundColor = Color.White,
				Padding = 5,
				HasShadow = false,
				OutlineColor = Color.Black,
				Content = DrawingImage
			};

			return palleteFrame;
		}


		private StackLayout ButtonsFrame()
		{

			var stack = new StackLayout
			{
				Spacing = PalleteSpacing,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			Label agreeText = new Label
			{
				Text = "Agree",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			Switch agree = new Switch 
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			Button button1 = new Button
			{
				Text = " Send ",
				TextColor = Color.White,
				BackgroundColor = Color.Green,
				Font = Font.Default,
				BorderColor = Color.Gray

			};

			Button button2 = new Button
			{
				Text = " Clear ",
				TextColor = Color.Black,
				BackgroundColor = Color.FromHex("aaaaaa"),
				Font = Font.Default,
				BorderColor = Color.Gray

			};

			stack.Children.Add(agreeText);
			stack.Children.Add(agree);
			stack.Children.Add(button1);
			stack.Children.Add(button2);

			return stack;
		}



		#region Event Handlers

		private void OnButtonClicked(object sender, EventArgs e)
		{
			var button = (Button)sender;

			DrawingImage.CurrentLineColor = button.BackgroundColor;
		}

		private void OnStackSizeChanged(object sender, EventArgs args)
		{
			var stackLayout = (StackLayout)sender;

			var width = stackLayout.Width;
			var height = stackLayout.Height;

			if (width <= 0 || height <= 0)
			{
				return;
			}

			var stackChildSize = height / stackLayout.Children.Count;
			var font = Font.BoldSystemFontOfSize(0.4 * stackChildSize);

			foreach (var button in stackLayout.Children.Cast<Button>())
			{
				button.Font = font;

				button.HeightRequest = stackChildSize - PalleteSpacing;
			}
		}

		#endregion




	}
}
