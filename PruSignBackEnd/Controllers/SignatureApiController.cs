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

                var httpContext = (System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"];
                var foo = httpContext.Request.Form["jsonRequest"];



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
