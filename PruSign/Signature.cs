using System;
namespace PruSign
{
	public class Signature
	{
		public byte[] image;
		public PruSign.iOS.PointWhen[] points;
		public String datetime;
		public String customerName;
		public String customerId;
		public String documentId;
		public String applicationId;
		public String hash;

	}
}
