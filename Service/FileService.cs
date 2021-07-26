using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace cloud_file_handling.Service
{
    public class EncryptionService
    {
        private const string secretKey = "[ENTER YOUR SECRET KEY HERE]";

        public KeyValuePair<string, string> GenerateSignature()
        {
            TimeSpan expiryTime = TimeSpan.FromMinutes(30);
            var expire = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + expiryTime.TotalSeconds).ToString(CultureInfo.InvariantCulture);
            var signature = StringToMD5(secretKey + expire);

            return new KeyValuePair<string, string>(expire, signature);

        }


        private string StringToMD5(string s)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                var hashBytes = md5.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        private string HexStringFromBytes(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        /* https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
        https://uploadcare.com/api-refs/rest-api/v0.5.0/#section/Authentication/Uploadcare.Simple */
        public async Task DeleteFile(string uuid)
        {
            // uploadcare public key for the project. Can be found under the API KEYS tab
            const string publicKey = "fe2e396bcb4a55d002bc";

            Console.WriteLine("Deleting file: " + uuid);
            string URL = $"https://api.uploadcare.com/files/{uuid}/storage/";
            HttpClient client = new HttpClient();
            // the following headers are required for the uploadcare API
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.uploadcare-v0.5+json");
            client.DefaultRequestHeaders.Add("Authentication", $"Uploadcare.Simple {publicKey}:{secretKey}");

            // DELETE request
            var response = client.DeleteAsync(URL);
            var msg = await response;
            Console.WriteLine(msg);

        }


    }
}