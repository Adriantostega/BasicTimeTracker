using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace BasicTimeTracker.Util
{
    class RegistryManager
    {
        public string RegistryKeyName { get; private set; }

        public string ExecutablePath { get; private set; }

        public string ApplicationName { get; private set; }

        public RegistryManager(string applicationName, string executablePath, string registryKeyName)
        {
            ApplicationName = applicationName;
            ExecutablePath = executablePath;
            RegistryKeyName = registryKeyName;
        }

        //public bool ExistRegistryKey()
        //{
        //    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        //    //Option1
        //    var subKeyNames = registryKey.GetSubKeyNames();
        //    string keyName = subKeyNames.FirstOrDefault(sk => sk.Equals(applicationName));

        //    //Option2
        //    var openSubKey = registryKey.OpenSubKey(path);

        //    if (string.IsNullOrEmpty(keyName))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public void AddRegistryKey(string applicationName, string executablePath)
        //{
        //    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        //    registryKey.SetValue(applicationName, false);
        //}

        //public void DeleteRegistryKey(string applicationName, string )
        //{
        //    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        //    registryKey.DeleteValue(applicationName, false);

        //}
    }
}
