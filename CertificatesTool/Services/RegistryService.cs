using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificatesTool.Services
{
    public class RegistryService
    {
        public void Export(string exportPath, string registryPath)
        {
            string path = exportPath;// "\"" + exportPath + "\"";
            string key = registryPath;// "\"" + registryPath + "\"";
            Process proc = new Process();

            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;

                proc = Process.Start("regedit.exe", "/e " + path + " " + key);
                proc.WaitForExit();
            }
            catch (Exception)
            {
                proc.Dispose();
            }
        }
    }
}
