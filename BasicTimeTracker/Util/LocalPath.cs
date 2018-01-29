using System;
using System.IO;
using System.Reflection;

namespace BasicTimeTracker.Util
{
    public class LocalPath
    {
        private static LocalPath _localPath;
        public string PathDirectory { get; private set; }

        private LocalPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var directoryName = Path.GetDirectoryName(codeBase);
            var uriString = directoryName;
            //if (uriString != null)
            //{
            //    var uri = new Uri(uriString);
            //    PathDirectory = uri.LocalPath;
            //}
            //else
            //{
            //    PathDirectory = string.Empty;
            //}
            PathDirectory = @"C:\BasicTimer";
        }

        public static LocalPath Instance()
        {
            if (_localPath == null)
            {
                _localPath = new LocalPath();
            }

            return _localPath;
        }
    }
}