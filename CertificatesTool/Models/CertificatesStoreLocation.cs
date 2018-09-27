using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificatesTool.Models
{
    internal class CertificatesStoreLocation
    {
        #region Fields

        private readonly System.Security.Cryptography.X509Certificates.StoreName _store;
        private readonly IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> _certificates;
        private readonly string _storeName;

        #endregion

        #region Constructor

        public CertificatesStoreLocation(
            System.Security.Cryptography.X509Certificates.StoreName store
            , IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> certificates)
        {
            this._store = store;
            this._certificates = certificates;
            this._storeName=Enum.GetName(typeof(System.Security.Cryptography.X509Certificates.StoreName), store);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Хранилище
        /// </summary>
        public System.Security.Cryptography.X509Certificates.StoreName Store { get => _store; }

        /// <summary>
        /// Имя хранилища
        /// </summary>
        public string StoreName { get => _storeName; }


        /// <summary>
        /// Сертификаты
        /// </summary>
        public IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> Certificates { get => _certificates; }

        #endregion
    }
}
