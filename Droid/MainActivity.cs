using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;

namespace PruSign.Droid
{
	[Activity(Label = "PruSign.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}



		protected override void OnStop()
		{
			base.OnStop();

			ThreadPool.QueueUserWorkItem (o => sendRestSignature ());

		}

		private void sendRestSignature()
		{
			try
			{
				FileHelper fh = new FileHelper();
				SignatureDatabase db = new SignatureDatabase(fh.GetLocalFilePath("PruSign.db"));
				System.Threading.Tasks.Task<System.Collections.Generic.List<SignatureItem>> items = db.GetItemsNotDoneAsync();
				foreach (var item in items.Result)
				{
					try
					{
						//Post("https://48.177.151.81:29398/api/SignatureApi", item.SignatureObject);
						item.Sent = true;
						item.SentTimeStamp = System.DateTime.Now.Ticks;
						db.SaveItemAsync(item);
					} catch (Exception ex) {
						// POR AHORA HAGAMOS ESTO EN CASO DE ERROR
						item.Miscelanea = ex.Message;
						db.SaveItemAsync(item);
					}
				}
			} catch (Exception ex) {
				String error = ex.Message;
			}
		}



	}
}
