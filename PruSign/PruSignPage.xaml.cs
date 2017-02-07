using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using CoreGraphics;

namespace PruSign
{
	public partial class PruSignPage : ContentPage
	{

		private ImageWithTouch DrawingImage;


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
					   		Content = DocumentIdFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 2
					},
					{
						new ContentView {
					   		Content = ButtonsFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 3
					}
				}
			};
		}




		private Frame BuildDrawingFrame()
		{
			DrawingImage = new ImageWithTouch
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				CurrentLineColor = Color.Black,
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




		private void ClearDrawingFrame()
		{

		}


		private StackLayout DocumentIdFrame()
		{

			var stack = new StackLayout
			{
				Spacing = PalleteSpacing,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			Label documentLabel = new Label
			{
				Text = "Document Id: ",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			var documentId = new Entry
			{
				Placeholder = "",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				PlaceholderColor = Color.Gray
			};


			stack.Children.Add(documentLabel);
			stack.Children.Add(documentId);
			return stack;
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
				FontSize = 9,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				LineBreakMode = LineBreakMode.NoWrap
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

			button2.Clicked += (sender, e) => {
				ClearDrawingFrame();
			};

			Picker application = new Picker
			{
				Title = "Application",
				VerticalOptions = LayoutOptions.Fill
			};

			application.Items.Add("eApplication");
			application.Items.Add("WSM");

			stack.Children.Add(agreeText);
			stack.Children.Add(agree);
			stack.Children.Add(button1);
			stack.Children.Add(application);
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
