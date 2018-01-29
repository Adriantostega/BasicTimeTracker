using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimeTracker.DataAccess
{
    public class FileWriter : IDataWriter
    {
        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        public bool CreateFileIfNotExist { get; private set; }

        public FileWriter(string fileName, bool createFileIfNotExist, string filePath = "")
        {
            FileName = fileName;
            FilePath = string.IsNullOrEmpty(filePath) ? GetCurrentPath() : filePath;
            CreateFileIfNotExist = createFileIfNotExist;

            if (createFileIfNotExist)
            {
                CreateFileIfDoesNotExist();
            }
        }

        private void CreateFileIfDoesNotExist()
        {
            var pathWithName = GetPathWithName();

            if (!File.Exists(pathWithName))
            {
                FileStream fileStream = new FileStream(pathWithName, FileMode.Create);
                fileStream.Close();
            }
        }

        private string GetCurrentPath()
        {
            return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
        }


        public string Read()
        {
            string valueFromFile;
            string pathWithName = GetPathWithName();
            using (var streamReader = new StreamReader(pathWithName))
            {
                valueFromFile = streamReader.ReadToEnd();
            }

            return valueFromFile;
        }

        public void Write(string value)
        {
            string pathWithName = GetPathWithName();
            using (var streamWriter = new StreamWriter(pathWithName))
            {
                streamWriter.Write(value);
            }
        }

        private string GetPathWithName()
        {
            return Path.Combine(FilePath, FileName);
        }
    }
}
