using CertificatesTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificatesTool.Services
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CRYPT_KEY_PROV_INFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwszContainerName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwszProvName;
        public uint dwProvType;
        public uint dwFlags;
        public uint cProvParam;
        public IntPtr rgProvParam;
        public uint dwKeySpec;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CRYPTOAPI_BLOB
    {
        public uint cbData;
        public IntPtr pbData;
    }





    public class CryptographyService
    {
        public CertificateInfoModel GetInfo(X509Certificate2 cert)
        {
            CertificateInfoModel certificateInfo = new CertificateInfoModel()
            {
                IssuerName = cert.GetNameInfo(X509NameType.SimpleName, true),
                OwnerName = cert.GetNameInfo(X509NameType.SimpleName, false),
                SerialNumber = cert.SerialNumber,
                NotBefore = cert.NotBefore.ToShortDateString(),
                NotAfter = cert.NotAfter.ToShortDateString()
            };

            certificateInfo.KeyAlgorithmName = cert.PublicKey.EncodedKeyValue.Oid.FriendlyName;
            certificateInfo.ContainerName = GetCertificateContextProperty(cert).pwszContainerName;

            return certificateInfo;
        }

        public static CRYPT_KEY_PROV_INFO GetCertificateContextProperty(X509Certificate2 certificate)
        {
            try
            {
                IntPtr certhandle = certificate.Handle;
                uint pcbData = 0;
                if (CertGetCertificateContextProperty(certhandle, 2, IntPtr.Zero, ref pcbData))
                {
                    IntPtr memoryChunk = Marshal.AllocHGlobal((int)pcbData);
                    try
                    {
                        if (CertGetCertificateContextProperty(certhandle, 2, memoryChunk,
                 ref pcbData))
                        {
                            CRYPT_KEY_PROV_INFO context =
                        (CRYPT_KEY_PROV_INFO)Marshal.PtrToStructure(memoryChunk,
                        typeof(CRYPT_KEY_PROV_INFO));
                            return context;
                        }

                    }
                    finally
                    {
                        Marshal.FreeHGlobal(memoryChunk);
                    }
                }
            }
            finally {

            }
            return new CRYPT_KEY_PROV_INFO();
        }


        [DllImport("crypt32.dll")]
        private static extern bool CertGetCertificateContextProperty(IntPtr pCertContext,
       uint dwPropId, IntPtr pvData, ref uint pcbData);
    }
}
