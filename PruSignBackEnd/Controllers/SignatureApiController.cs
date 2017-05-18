using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace PruSignBackEnd
{
	public class SignatureApiController : ApiController
	{


		public string get(int id)
		{
			return "value";
		}

		public HttpResponseMessage Post(PruSignBackEnd.PruSign.Object.Signature signature)
		{

			try
			{

				string appId = signature.applicationId;

				return new HttpResponseMessage(HttpStatusCode.OK);

			}
			catch (Exception ex)
			{
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}


		}



	}
}
