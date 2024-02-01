using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorFileUpload.Shared;
using System.Net;


namespace BlazorFileUpload.Server.Controller
{
    //[Route("api/[controller]")]
    [Route("api/File")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<List<UploadResult>>> Uploadfile(List<IFormFile> files)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();
            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

                //trustedFileNameForFileStorage = Path.GetRandomFileName();//original on tutorial.
                trustedFileNameForFileStorage = file.FileName;//
                var path = Path.Combine(_env.ContentRootPath, "uploads", trustedFileNameForFileStorage);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                uploadResult.StoredFileName = trustedFileNameForDisplay;
                uploadResults.Add(uploadResult);
            }

            return Ok(uploadResults);
        }
    }
}
