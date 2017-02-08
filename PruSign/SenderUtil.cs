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
			PruSign.iOS.ImageTools tool = new iOS.ImageTools();
			tool.send(img.Source);
		}



	}
}
