using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificatesTool.Extensions
{
    internal static class X509CertificateExtensions
    {
        public static bool Contains(this X509Certificate2 certificate, string value)
        {
            var str = value.ToLower();
            if (certificate.SerialNumber.ToLower().Contains(str))
                return true;
            else if (certificate.Thumbprint.ToLower().Contains(str))
                return true;
            else if (certificate.GetNameInfo(X509NameType.SimpleName, true).ToLower().Contains(str))
                return true;

            return false;
        }
    }
}
