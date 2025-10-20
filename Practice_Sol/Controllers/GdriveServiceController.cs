using EF_API_Pg.Model;
using Microsoft.AspNetCore.Mvc;
using Samplebacked_api.EFCore;
using Samplebacked_api.Model.GdriveService;

namespace Samplebacked_api.Controllers
{

    [ApiController]
    public class GdriveServiceController : Controller
    {
        private readonly GoogleDriveHelper _driveHelper;
        public GdriveServiceController(GoogleDriveHelper googleDrive)
        {
            _driveHelper = googleDrive;
        }


        [HttpPost]
        [Route("(api/[controller]/UploadToGDrive")]
        public IActionResult UploadToGDrive(IFormFile file, string folderId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Save file temporarily
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                file.CopyTo(stream);

            // Upload to Google Drive
            var driveHelper = new GoogleDriveHelper();
            var fileId = driveHelper.UploadFile(folderId, filePath);

            return Ok(new { message = "File uploaded successfully to Google Drive", fileId });
        }


        [HttpGet]
        [Route("api/[controller]/CreateFolder")]
        public IActionResult CreateFolder(string folderid, string Foldername)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                string data = _driveHelper.CreateFolder(folderid,Foldername);
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

    }
}
