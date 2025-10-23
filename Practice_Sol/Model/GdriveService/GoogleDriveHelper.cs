using EF_API_Pg.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Samplebacked_api.Model.GdriveService
{
    public class GoogleDriveHelper
    {

        private readonly DriveService _service;

        public GoogleDriveHelper()
        {
            string[] scopes = { DriveService.Scope.Drive };
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Patient File Uploader",
            });
        }

        // Upload file to an existing folder
        public string UploadFile(string folderId, string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string> { folderId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = _service.Files.Create(fileMetadata, stream, GetMimeType(filePath));
                request.Fields = "id";
                request.Upload();
            }

            return request.ResponseBody.Id;
        }

        private string GetMimeType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            return ext switch
            {
                ".pdf" => "application/pdf",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".txt" => "text/plain",
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }



        public ApiResponse CreateFolder(string folderid,string folderName)
        {
          //var driveHelper = new GoogleDriveHelper();

            //string folderName = "PortfolioUploads";

            // Check if folder exists
            ApiResponse response = new ApiResponse();
            response.Code = "Sucess";
            var listRequest = _service.Files.List();
            listRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
            var files = listRequest.Execute().Files;

            
            if (files != null && files.Count > 0)
            {
                response.ResponseData = files[0].Id;
                response.Message = "Folder already exists: {folderName} (ID: {files[0]})";
            }
            else
            {
                // Create folder
                var folderMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder"
                };
                var request = _service.Files.Create(folderMetadata);
                request.Fields = "id";
                var folder = request.Execute();
               response.ResponseData=  folder.Id;
                response.Message = "Folder already exists: {folderName} (ID: {files[0]}";
            }
            return response;
        }

        public async Task<IList<Google.Apis.Drive.v3.Data.File>> GetAllFilesAsync()
        {
            var request = _service.Files.List();
            request.Fields = "files(id, name, mimeType, modifiedTime, size)";
            var result = await request.ExecuteAsync();
            return result.Files;
        }

    }
}