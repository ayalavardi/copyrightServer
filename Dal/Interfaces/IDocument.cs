using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Models;

namespace Dal.Dalimplamantation
{
    public interface IDocument {

          Task<string> UplodFileAsync(Stream fileStream, string fileName, string mimeType, string folderId = null);
          Task<List<Google.Apis.Drive.v3.Data.File>> GetFilesInFolderAsync(string folderId);



    }
}
