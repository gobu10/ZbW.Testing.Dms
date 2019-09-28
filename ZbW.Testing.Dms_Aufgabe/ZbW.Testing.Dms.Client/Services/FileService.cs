using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    public class FileService
    {
        internal FileTestable fileTestable { private get; set; }
        public FileTestable FileTestable { get; set; }

        public void CreateValutaFolderIfNotExists(string path)
        {
            Directory.CreateDirectory(path);
        }

        public FileService()
        {
            fileTestable=new FileTestable();
        }

        public void RemoveDocumentOnSource(string path)
        {
            File.Delete(path);
        }

        public void CopyDocumentToTarge(String sourcePath, String targetPath)
        {
            try
            {
                fileTestable.Copy(sourcePath, targetPath, true);
            }
            catch (Exception e)
            {
                throw new CouldNotCopyFileException("File kann nicht koppiert werden", e);
            }
        }

        public String GetNewFileName(String typeName, String fileName, Guid guid)
        {
            var fileExtension = this.GetFileExtension(fileName);
            return $"{guid}_{typeName}.{fileExtension}";
        }

        public String GetFileExtension(String fileName)
        {
            var splittedByPoint = fileName.Split('.');
            return splittedByPoint[splittedByPoint.Length - 1];
        }
    }
}
