using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace formdata.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {

        // POST /form
        // sends form data from client to server and save the file in disk
        [HttpPost]
        async public Task<ActionResult> PostFormData(IFormCollection form)
        {
            string email = form["email"];
            List<IFormFile> files = form.Files.ToList();
            // save files to local storage
            if (files[0].Length > 0)
            {
                // saving in relative directory of project
                string uploads = Path.Combine("", "uploads");

                string filePath = Path.Combine(uploads, files[0].FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await files[0].CopyToAsync(fileStream);
                }
            }

            return Ok(files);

        }

    }


}
