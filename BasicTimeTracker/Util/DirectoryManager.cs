using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimeTracker.Util
{
    public class DirectoryManager : IDirectoryManager
    {
        public void CreateIfDoesNotExist(string pathDirectory)
        {            
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }                        
        }

        public string[] GetAllFilesName(string pathDirectory)
        {
            return Directory.GetFiles(pathDirectory);
        }
    }
}
