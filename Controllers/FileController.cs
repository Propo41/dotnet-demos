using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloud_file_handling.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/* 
With Secure Uploading you can control who and when
can upload files to one of your Uploadcare project.
You need to generate a token on your backend, 
and a trusted user should use this token to upload a new file.

The token is called signature. Technically, it's a string sent 
along with your upload request. It requires your Uploadcare project's 
Secret Key and should be created on your back end.

The signature is an HMAC/SHA256 with two parameters:
- secret_key — a generated key
- expire — an expiration time message (string)

The function GenerateSignature() inside the class FileService, generates the signature
This signature is sent to the front end. 

References: 
https://uploadcare.com/docs/security/secure-uploads/ 
https://uploadcare.com/api-refs/upload-api/#operation/fileUploadInfo 
https://github.com/okolobaxa/uploadcare-dotnet/blob/master/Uploadcare/Upload/SignedFileUploader.cs 
 */

namespace cloud_file_handling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        EncryptionService _encryptionService = new EncryptionService();

        [HttpGet("upload-url")]
        public ActionResult GetSignedUrl()
        {
            KeyValuePair<string, string> pair = _encryptionService.GenerateSignature();
            Console.WriteLine("Expire: " + pair.Key);
            Console.WriteLine("Signature: " + pair.Value);
            return new JsonResult(new { expire = pair.Key, signature = pair.Value });
        }

        [HttpGet("delete/{uuid}")]
        public ActionResult DeleteFile(string uuid)
        {
            _encryptionService.DeleteFile(uuid);
            return new JsonResult(new { success = true });
        }
    }


}
