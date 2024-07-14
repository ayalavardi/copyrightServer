//using Dal.Dalimplamantation;
//using Dal.Models;
//using Google.Apis.Drive.v3;
//using Google.Apis.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dal.DalService
//{
//    public class DocumentService : IDocument
//    {
//        private CopyRightContext db;
//        private readonly DriveService _driveService;

//        public DocumentService(CopyRightContext db)
//        {
//            this.db = db;
//            _driveService = new DriveService(new BaseClientService.Initializer()
//            {
//                HttpClientInitializer = credential,
//                ApplicationName = "copyRightDrive",
//            });
//        }

//        public async Task<Document> CreateAsync(Document item)
//        {
//            try
//            {
//                db.Documents.Add(item);
//                await db.SaveChangesAsync();
//                await db.SaveChangesAsync();
//                return item;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }


//        public async Task<bool> DeleteAsync(int item)

//        {
//            try
//            {
//                Document l =await db.Documents.FirstOrDefaultAsync(c => c.DocumentId == item);
//                if (l == null)
//                    throw new Exception("lead does not exist in DB");
//                db.Documents.Remove(l);
//                db.SaveChanges();
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public async Task<List<Document>> ReadAsync(Predicate<Document> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<List<Document>> ReadAllAsync() => db.Documents.ToList();

//        public async Task<bool> UpdateAsync(Document item)
//        {
//            try
//            {
//                var existingLead = db.Documents.FirstOrDefault(x => x.DocumentId == item.DocumentId);

//                if (existingLead == null)
//                    throw new Exception("Lead does not exist in DB");

//                //existingLead.FirstName = item.FirstName;
//                //existingLead.LastName = item.LastName;
//                //existingLead.BusinessName = item.FirstName;
//                //existingLead.LastContactedDate = item.LastContactedDate;
//                //existingLead.Phone = item.Phone;
//                //existingLead.Email = item.Email;
//                //existingLead.Source = item.Source;
//                //existingLead.Notes = item.Notes;

//                await db.SaveChangesAsync();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                return false;
//            }
//        }

//        public async Task<Document> GetByIdAsync(int id)
//        {
//            try
//            {
//                Document l = await db.Documents.FirstOrDefaultAsync(x => x.DocumentId == id);
//                if (l == null)
//                    throw new Exception("user does not exist in DB");
//                else
//                    return l;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                return null;
//            }
//        }
//    }
//    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string mimeType, string folderId = null)
//    {
//        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
//        {
//            Name = fileName,
//            Parents = new List<string> { "1nz8OzOX1W3mF4dF6CchJQP80SaR9JjXI" }
//        };

//        var request = _driveService.Files.Create(fileMetadata, fileStream, mimeType);
//        request.Fields = "id, name, parents";

//        var file = await request.UploadAsync();

//        if (file.Status != Google.Apis.Upload.UploadStatus.Completed)
//        {
//            throw new Exception($"Upload failed: {file.Exception?.Message}");
//        }

//        var uploadedFile = request.ResponseBody;
//        Console.WriteLine($"Uploaded file: {uploadedFile.Name} with ID: {uploadedFile.Id} in folder: {uploadedFile.Parents}");

//        // Construct the file URL
//        string fileUrl = $"https://drive.google.com/file/d/{uploadedFile.Id}/view";

//        return fileUrl;
//    }

//}
