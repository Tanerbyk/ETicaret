using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application
{
    public enum FileKeys
    {
        Products,
        Sliders
    }
    public class FilePathOptions
    {
        public const string ConfigurationPath = "FilePath";
        public string RootPath { get; set; }
        public List<FilePath> FilePaths { get; set; }

        public string ResponsePath { get; set; }

        public FilePath GetByKey(FileKeys key)
        {
           return FilePaths.FirstOrDefault(x => x.Key.ToLower() == key.ToString().ToLower());
        }

    }
    public class FilePath
    {
        public string Key { get; set; }
        public string ResponsePath { get; set; }
        public string FileUrl(string fileName)
        {
            return $"{this.ResponsePath.TrimEnd('/')}/{fileName}";
        }
    }
}

