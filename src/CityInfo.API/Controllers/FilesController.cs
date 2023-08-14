using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
   // [Authorize]
    public class FilesController : ControllerBase
    {

        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        private readonly IConfiguration configuration;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider,
            IConfiguration configuration)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
            this.configuration = configuration;
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var filePath = "test.txt";

            if(!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            else
            {

                if(!_fileExtensionContentTypeProvider.TryGetContentType(
                    filePath, out var contentType
                    ))
                {
                    contentType = "application/octet-stream";
                }


                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, contentType, Path.GetFileName(filePath));

            }

        }

        /// <summary>
        /// Return mail to address from app settings
        /// </summary>
        /// <returns></returns>
        [HttpGet("/mailtoaddress")]
        public ActionResult<string> GetMailserver()
        {
            return configuration["mailSettings:mailToAddress"]!;
        }
    }
}
