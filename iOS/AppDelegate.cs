using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace PruSign.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{

		public NSUrlSessionUploadTask uploadTask;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}

		public override void DidEnterBackground(UIApplication app)
		{
			UploadData("https://reqres.in/api/users");
		}


		public void UploadData(string url)
		{
			NSUrlSession session = null;

			NSUrlSessionConfiguration configuration = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransfer.BackgroundSession");
			session = NSUrlSession.FromConfiguration(configuration, (NSUrlSessionDelegate)new MySessionDelegate(), new NSOperationQueue());

			// URL
			NSMutableUrlRequest request = new NSMutableUrlRequest(new NSUrl(url));
			// METHOD
			request.HttpMethod = "POST";
			// HEADERS
			request.HttpMethod = "POST";
			var keys = new object[] { "Key1", "Key2" };
			var objects = new object[] { "Value1", "Value2" };
			var dictionnary = NSDictionary.FromObjectsAndKeys(objects, keys);
			request.Headers = dictionnary;

			NSObject postObj = FromObject("yourfieldname=yourpassedvalue");
			NSString postString = (NSString)postObj;
			NSData postData = NSData.FromString(postString);

			request.Body = postData;

			uploadTask = session.CreateUploadTask(request);
			uploadTask.Resume();

			}


	}


}
