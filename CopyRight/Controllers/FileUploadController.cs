using Bl.Blservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly GoogleDriveService _googleDriveService;

        public FileUploadController(GoogleDriveService googleDriveService)
        {
            _googleDriveService = googleDriveService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = file.OpenReadStream())
            {
                var fileId = await _googleDriveService.UploadFileAsync(stream, file.FileName, file.ContentType);
                var fileUrl = $"https://drive.google.com/file/d/{fileId}/view";

                return Ok(new { FileId = fileId, FileUrl = fileUrl });
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetFiles()
        {
            string folderId = "1nz8OzOX1W3mF4dF6CchJQP80SaR9JjXI";
            var files = await _googleDriveService.GetFilesInFolderAsync(folderId);

            if (files == null || files.Count == 0)
            {
                return NotFound("No files found in the folder.");
            }

            return Ok(files.Select(file => new {
                file.Id,
                file.Name,
                file.MimeType,
                file.ThumbnailLink,
                file.WebViewLink
            }));
        }

    }
}
