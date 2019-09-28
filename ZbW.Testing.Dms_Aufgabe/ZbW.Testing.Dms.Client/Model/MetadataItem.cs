using System;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem
    {
        // TODO: Write your Metadata properties here
        private string _stichwoerter = String.Empty;

        public string Stichwoerter
        {
            get { return _stichwoerter; }
            set { _stichwoerter = value; }
        }
        public string Bezeichnung { get; set; }
        public string Benutzer { get; set; }
        public DateTime Erfassungsdatum { get; set; }
        public string FilePath { get; set; }
        public Boolean IsRemoveFileEnabled { get; set; }
        public string Type { get; set; }
        public DateTime ValutaDatum { get; set; }

        public MetadataItem () { }

        MetadataItem(
            string bezeichnung, 
            DateTime erfassungsdatum, 
            string filePath, 
            Boolean isRemoveFileEnabled,
            string type, 
            string stichwoerter, 
            DateTime valutaDatum, 
            string benutzer)
        {
            this.Bezeichnung = bezeichnung;
            this.Benutzer = benutzer;
            this.Erfassungsdatum = erfassungsdatum;
            this.FilePath = filePath;
            this.IsRemoveFileEnabled = isRemoveFileEnabled;
            this.Type = type;
            this.Stichwoerter = stichwoerter;
            this.ValutaDatum = valutaDatum;
        }
    }
}