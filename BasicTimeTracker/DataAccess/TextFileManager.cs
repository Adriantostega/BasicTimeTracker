using System;
using System.IO;

namespace BasicTimeTracker.DataAccess
{
    internal class TextFileManager : IFileManager
    {
        public string ReadAllText(string fileNameWithPath)
        {
            return File.ReadAllText(fileNameWithPath);
        }

        public void CreateIfDoesNotExist(string fileNameWithPath)
        {
            if (!File.Exists(fileNameWithPath))
            {                
                File.WriteAllText(fileNameWithPath, string.Empty);
            }
        }

        public void AppendNewLine(string newLine, string fileNameWithPath)
        {
            string appendText = newLine + Environment.NewLine;
            File.AppendAllText(fileNameWithPath, appendText);
        }

        public void Delete(string fileNameWithPath)
        {
            File.Delete(fileNameWithPath);
        }

        public void Rename(string oldFileNAmeWithPath, string newfileNameWithPath)
        {
            File.Move(oldFileNAmeWithPath, newfileNameWithPath);
        }
    }
}