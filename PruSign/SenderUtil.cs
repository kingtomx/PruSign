using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace PruSign
{
	public static class SenderUtil
	{


		public static void SendSign(String name, String customerId, String documentId, String appName, String datetime)
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var directoryname = System.IO.Path.Combine(documents, "temporalSignatures");

			byte[] signatureFile = System.IO.File.ReadAllBytes(System.IO.Path.Combine(directoryname, "signature.png"));

			byte[] nameBytes = GetBytes(name);
			byte[] customerIdBytes = GetBytes(customerId);
			byte[] documentIdBytes = GetBytes(documentId);
			byte[] appNameBytes = GetBytes(appName);
			byte[] datetimeBytes = GetBytes(datetime);

			byte[] rv = new byte[signatureFile.Length + nameBytes.Length + customerIdBytes.Length + documentIdBytes.Length + appNameBytes.Length + datetimeBytes.Length];
			System.Buffer.BlockCopy(signatureFile, 0, rv, 0, signatureFile.Length);
			System.Buffer.BlockCopy(nameBytes, 0, rv, signatureFile.Length, nameBytes.Length);
			System.Buffer.BlockCopy(customerIdBytes, 0, rv, signatureFile.Length + nameBytes.Length, customerIdBytes.Length);
			System.Buffer.BlockCopy(documentIdBytes, 0, rv, signatureFile.Length + nameBytes.Length + customerIdBytes.Length, documentIdBytes.Length);
			System.Buffer.BlockCopy(appNameBytes, 0, rv, signatureFile.Length + nameBytes.Length + customerIdBytes.Length + documentIdBytes.Length, appNameBytes.Length);
			System.Buffer.BlockCopy(datetimeBytes, 0, rv, signatureFile.Length + nameBytes.Length + customerIdBytes.Length + documentIdBytes.Length + appNameBytes.Length, datetimeBytes.Length);

			String hash = SHA512StringHash(rv);

			var outboxFolder = System.IO.Path.Combine(documents, "outbox");
			System.IO.Directory.CreateDirectory(outboxFolder);

			Signature sign = new Signature
			{
				customerName = name,
				customerId = customerId,
				documentId = documentId,
				applicationId = appName,
				datetime = datetime,
				image = signatureFile,
				hash = hash
			};
			var json = JsonConvert.SerializeObject(sign);
			var filename = System.IO.Path.Combine(outboxFolder, System.DateTime.Now.Ticks+".json");
			using (var streamWriter = new System.IO.StreamWriter(filename))
			{
				streamWriter.Write(json);
				streamWriter.Close();
			}



		}



		private static string SHA512StringHash(byte[] input)
		{
			SHA512 shaM = new SHA512Managed();
			// Convert the input string to a byte array and compute the hash.
			byte[] data = shaM.ComputeHash(input);
			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			// Return the hexadecimal string.
			return sBuilder.ToString();
		}


		private static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}



	}
}
