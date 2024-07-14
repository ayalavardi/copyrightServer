using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Bl.Blservice { 
public class GoogleDriveService
{
    private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
    private readonly DriveService _driveService;

    public GoogleDriveService()
    {
        var credential = GoogleCredential.FromFile("Resources/credentials.json")
            .CreateScoped(Scopes);

        _driveService = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "copyRightDrive",
        });
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string mimeType, string folderId = null)
    {
        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = fileName,
            Parents = new List<string> { "1nz8OzOX1W3mF4dF6CchJQP80SaR9JjXI" }
        };

        var request = _driveService.Files.Create(fileMetadata, fileStream, mimeType);
        request.Fields = "id, name, parents";

        var file = await request.UploadAsync();

        if (file.Status != Google.Apis.Upload.UploadStatus.Completed)
        {
            throw new Exception($"Upload failed: {file.Exception?.Message}");
        }

        var uploadedFile = request.ResponseBody;
        Console.WriteLine($"Uploaded file: {uploadedFile.Name} with ID: {uploadedFile.Id} in folder: {uploadedFile.Parents}");

        return uploadedFile.Id;
    }


        public async Task<List<Google.Apis.Drive.v3.Data.File>> GetFilesInFolderAsync(string folderId)
        {
            var request = _driveService.Files.List();
            request.Q = $"'{folderId}' in parents";
            request.Fields = "files(id, name, mimeType, thumbnailLink, webViewLink)";

            var result = await request.ExecuteAsync();
            return result.Files.ToList();
        }

    }
}