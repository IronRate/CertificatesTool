using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificatesTool.Models
{
    public class CertificateInfoModel
    {
        public string IssuerName { get; set; }

        public string OwnerName { get; set; }

        public string SerialNumber { get; set; }

        public string NotBefore { get; set; }

        public string NotAfter { get; set; }

        public string KeyAlgorithmName { get; set; }

        public string ContainerName { get; set; }
    }
}
