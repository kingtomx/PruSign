using System;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace PruSign
{
	public class SenderUtil
	{
		public SenderUtil()
		{
		}


		public static void SendSign(Image img)
		{
			Device.OnPlatform(
				iOS: () =>
					{
						SendIOS(img);
					},
				Android: () =>
					{ 
						//SendAndroid(img); 
					}
			);

		}


		private static void SendIOS(Image img)
		{
			// hacer esto static
			PruSign.iOS.ImageTools.send(img.Source);
		}



	}
}
