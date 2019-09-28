using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Prism.Mvvm;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public class DocumentService: BindableBase
    {
        private static String FILE_TYPE_NAME = "Content";
        private static String METASATA_TYPE_NAME = "Metadata";

        private String _targePath;

        private List<MetadataItem> _metadataItems;

        private FileService _fileService;
        private XmlService _xmlService;

        public DocumentService()
        {
            this._targePath = ConfigurationManager.AppSettings["RepositoryDir"];
            this._fileService = new FileService();
            this._xmlService = new XmlService();
        }

        public List<MetadataItem> MetadataItems
        {
            get { return _metadataItems; }
            set { SetProperty(ref _metadataItems, value); }
        }

        internal void AddDocumentToDms(MetadataItem metadataItem)
        {
            var targetPath = this._targePath + "/" + metadataItem.ValutaDatum.Year;
            var guid = Guid.NewGuid();
            var newFileName = _fileService.GetNewFileName(FILE_TYPE_NAME, metadataItem.FilePath, guid);
            var sourcePath = metadataItem.FilePath;
            metadataItem.FilePath = targetPath + "/" + newFileName; ;
            _fileService.CreateValutaFolderIfNotExists(targetPath);

            this.HandelDocument(metadataItem, sourcePath, guid);
            this.HandelMetadata(metadataItem, targetPath, guid);
        }

        public void OpenFile(MetadataItem metadataItem)
        {
            Process.Start(metadataItem.FilePath);
        }

        private void HandelDocument(MetadataItem metadataItem, String sourcePath, Guid guid)
        {
            var targetPath = metadataItem.FilePath;
            _fileService.CopyDocumentToTarge(sourcePath,targetPath);

            if (metadataItem.IsRemoveFileEnabled)
            {
                _fileService.RemoveDocumentOnSource((metadataItem.FilePath));
            }
        }
        private void HandelMetadata (MetadataItem metadataItem, String targetPath, Guid guid)
        {
            var newFileName = _fileService.GetNewFileName(METASATA_TYPE_NAME, ".xml", guid);
            var newFilePath = targetPath + "/" + newFileName;

            var serializeXml = this._xmlService.SeralizeMetadataItem (metadataItem);
            this._xmlService.SaveXml (serializeXml, newFilePath);
        }
        public List<MetadataItem> GetAllMetadataItems () {

            var folderPaths = this.GetAllFolderPaths (this._targePath);
            ArrayList xmlPathsFromAllFolders = new ArrayList ();
            ArrayList metadataItemList = new ArrayList();

            foreach (string folderPath in folderPaths)
            {
                var xmlPathsFromOneFolder = this.GetAllXmlPaths(folderPath);
                xmlPathsFromAllFolders.AddRange(xmlPathsFromOneFolder);
            }

            foreach (var xmlPath in xmlPathsFromAllFolders)
            {
                metadataItemList.Add(this._xmlService.DeseralizeMetadatItem((String) xmlPath));
            }

            this.MetadataItems = metadataItemList.Cast<MetadataItem>().ToList();
            return this.MetadataItems;
        }

        public List<MetadataItem> FilterMetadataItems(string type, string searchParam)
        {
            if (String.IsNullOrEmpty(searchParam) && String.IsNullOrEmpty(type))
            {
                return this.MetadataItems;
            }

            if (String.IsNullOrEmpty(searchParam))
            {
                searchParam = "";
            }

            var filteredItems = this.MetadataItems.Where(item =>
            {
                return (item.Bezeichnung.Contains(searchParam) ||
                        item.Stichwoerter.Contains(searchParam) ||
                        item.Erfassungsdatum.ToString().Contains(searchParam) ||
                        item.ValutaDatum.ToString().Contains(searchParam)) &&
                       (String.IsNullOrEmpty(type) || item.Type.Equals(type));
            }).ToList();
            return filteredItems;
        }

        private String[] GetAllFolderPaths(String targetPath) {
            return Directory.GetDirectories (targetPath);
        }

        private ArrayList GetAllXmlPaths (String folderPath) {
            ArrayList xmlPaths = new ArrayList ();
            foreach(string file in Directory.EnumerateFiles (folderPath, "*.xml")) {
                xmlPaths.Add (file);
            }

            return xmlPaths;
        }
    }
}
