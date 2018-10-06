using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificatesTool.Components
{
    internal class CertificatesListView : ListViewDoubleBuffered
    {
        private IList<X509Certificate2> _certificates;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;

        public CertificatesListView() : base()
        {
            this.View = View.Details;

            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7
            });

            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Issuer";
            this.columnHeader1.Width = 168;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Serial";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Valid from";
            this.columnHeader3.Width = 108;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Valid to";
            this.columnHeader4.Width = 108;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Algorithm key";
            this.columnHeader6.Text = "Owner";
            this.columnHeader7.Text = "Container name";

            this.Resize += CertificatesListView_Resize;


        }

        private void CertificatesListView_Resize(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Список сертификатов
        /// </summary>
        public IList<X509Certificate2> Certificates
        {
            get => _certificates; set { _certificates = value; listViewRefresh(_certificates); this.validateCertificates(this.Items); OnResize(null); }
        }

        public X509Certificate2 FocusedCertificate
        {
            get
            {
                var i = this.FocusedItem?.Index;
                if (i != null)
                {
                    return this._certificates[i.Value];
                }
                return null;
            }
        }

        private void listViewRefresh(IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> certificates)
        {
            Services.CryptographyService cryptographyService = new Services.CryptographyService();
            this.BeginUpdate();
            this.Items.Clear();
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            if (certificates != null)
            {
                foreach (var cert in certificates)
                {
                    var info = cryptographyService.GetInfo(cert);
                    var item = this.Items.Add(info.IssuerName);
                    item.SubItems.Add(info.OwnerName);
                    item.SubItems.Add(info.SerialNumber);
                    item.SubItems.Add(info.NotBefore);
                    item.SubItems.Add(info.NotAfter);
                    item.SubItems.Add(info.KeyAlgorithmName);
                    item.SubItems.Add(info.ContainerName);


                }
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();
        }

        private void validateCertificates(ListView.ListViewItemCollection items)
        {
            //this.BeginUpdate();
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            foreach (ListViewItem item in items)
            {
                var certificate = _certificates[item.Index];
                if (certificate != null)
                {
                    var b = Task.Run(() =>
                    {
                        bool valid = false;
                        string error = null;
                        try
                        {
                            valid = certificate.Verify();
                        }
                        catch (System.Security.Cryptography.CryptographicException ex)
                        {

                            //item.ToolTipText = ex.Message;

                        }
                        return valid;
                    }).Result;


                    if (!b)
                    {
                        item.BackColor = Color.FromArgb(255, 0, 0);
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = this.BackColor;
                        item.ForeColor = this.ForeColor;
                    }

                }
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //this.EndUpdate();
        }
    }
}
