using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificatesTool.Repositories
{
    internal class CertificatesRepository
    {
        public IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> Get(
            System.Security.Cryptography.X509Certificates.StoreName storeName
            , System.Security.Cryptography.X509Certificates.StoreLocation storeLocation)
        {

            List<System.Security.Cryptography.X509Certificates.X509Certificate2> certificates = null;

            System.Security.Cryptography.X509Certificates.X509Store x509Store = new System.Security.Cryptography.X509Certificates.X509Store(storeName, storeLocation);
            x509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadOnly);

            certificates = new List<System.Security.Cryptography.X509Certificates.X509Certificate2>(x509Store.Certificates.Count);
            for (int i = 0; i < x509Store.Certificates.Count - 1; i++)
            {
                certificates.Add(x509Store.Certificates[i]);
            }
            x509Store.Close();
            return certificates;
        }

        public IEnumerable<Models.CertificatesStoreLocation> Get(
                   System.Security.Cryptography.X509Certificates.StoreLocation storeLocation)
        {
            List<Models.CertificatesStoreLocation> groupingCertificates = null;

            var values = Enum.GetValues(typeof(System.Security.Cryptography.X509Certificates.StoreName));
            groupingCertificates = new List<Models.CertificatesStoreLocation>(values.Length);

            foreach (System.Security.Cryptography.X509Certificates.StoreName storeName in values)
            {
                groupingCertificates.Add(new Models.CertificatesStoreLocation(storeName, this.Get(storeName, storeLocation)));
            }

            return groupingCertificates;
        }

    }
}
