﻿using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace PruSign
{
	public partial class PruSignPage : ContentPage
	{

		private const int PalleteSpacing = 3;
		private ImageWithTouch DrawingImage;
		private Frame palleteFrame;

		private Entry nameEntry = null;
		private Entry idEntry = null;
		private Entry documentId = null;
		private Picker application = null;
		private Entry datetimeEntry = null;


		public PruSignPage()
		{
			InitializeComponent();
			try
			{
				Content = BuildGrid();
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 10);
			}
			catch (Exception ex) {
				DisplayAlert("Error", ex.Message, "Close"); 
			}


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
					{
						new ContentView {
					   		Content = datetimeFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
						}, 0, 1
					},
				  	{
						new ContentView {
					   		Content = BuildDrawingFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 2
					},
					{
						new ContentView {
					   		Content = ClientDataFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 3
					},
					{
						new ContentView {
					   		Content = DocumentIdFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 4
					},
					{
					new ContentView {
					   		Content = ButtonsFrame(),
					   		Padding = new Thickness(10, 0, 0, 0),
					   		HorizontalOptions = LayoutOptions.FillAndExpand,
					   		VerticalOptions = LayoutOptions.FillAndExpand
				  		}, 0, 5
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

			 palleteFrame = new Frame
			{
				BackgroundColor = Color.White,
				Padding = 5,
				HasShadow = false,
				OutlineColor = Color.Black,
				Content = DrawingImage
			};

			return palleteFrame;
		}


		private StackLayout ClientDataFrame()
		{

			var stack = new StackLayout
			{
				Spacing = PalleteSpacing,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			this.nameEntry = new Entry
			{
				Placeholder = "Customer name",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				PlaceholderColor = Color.Gray
			};

			idEntry = new Entry
			{
				Placeholder = "Customer Id",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				PlaceholderColor = Color.Gray
			};


			stack.Children.Add(nameEntry);
			stack.Children.Add(idEntry);
			return stack;
		}

		private StackLayout datetimeFrame()
		{

			var stack = new StackLayout
			{
				Spacing = PalleteSpacing,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			datetimeEntry = new Entry
			{
				IsEnabled = false,
				Text = System.DateTime.Now.ToString(),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				PlaceholderColor = Color.Gray
			};

			stack.Children.Add(datetimeEntry);
			return stack;
		}

		private StackLayout DocumentIdFrame()
		{

			var stack = new StackLayout
			{
				Spacing = PalleteSpacing,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			documentId = new Entry
			{
				Placeholder = "Document Id",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				PlaceholderColor = Color.Gray
			};

			stack.Children.Add(documentId);
			return stack;
		}

		private StackLayout ButtonsFrame()
		{

			var stack = new StackLayout
			{
				Spacing = PalleteSpacing,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			Button button1 = new Button
			{
				Text = " Agree & Send ",
				TextColor = Color.White,
				BackgroundColor = Color.Green,
				Font = Font.Default,
				BorderColor = Color.Gray

			};
			button1.Clicked += (sender, e) =>
			{
				if (nameEntry.Text == null)
				{
					DisplayAlert("Error", "Name cannot be empty", "Ok");
				}
				else if (idEntry.Text == null)
				{
					DisplayAlert("Error", "Customer Id cannot be empty", "Ok");
				}
				else if (documentId.Text == null)
				{
					DisplayAlert("Error", "Document Id cannot be empty", "Ok");
				}
				else if (application.SelectedIndex == -1)
				{
					DisplayAlert("Error", "Select an Application to send the signature", "Ok");
				}
				else {
					SenderUtil.SendSign(nameEntry.Text, idEntry.Text, documentId.Text, application.Items[application.SelectedIndex], datetimeEntry.Text);

				}
			};
			application = new Picker
			{
				Title = "Application",
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			application.Items.Add("eApplication");
			application.Items.Add("WSM");
			stack.Children.Add(button1);
			stack.Children.Add(application);
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
