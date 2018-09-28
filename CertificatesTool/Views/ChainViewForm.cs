using CertificatesTool.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificatesTool.Views
{
    public partial class ChainViewForm : Form
    {
        private X509Certificate2 _certificate;
        private X509Chain _chain = null;


        #region Properties

        public X509Certificate2 Certificate
        {
            get => _certificate;
            set { _certificate = value; this.initCertificate(); }
        }

        #endregion
        #region Constructor

        public ChainViewForm()
        {
            InitializeComponent();
        }

        #endregion

        private void ChainViewForm_Load(object sender, EventArgs e)
        {

        }


        private void initCertificate()
        {
            if (this._certificate != null)
            {
                this.disposeChain();
                _chain = new X509Chain();
                try
                {
                    _chain.Build(this.Certificate);
                    Console.WriteLine();

                }
                catch (Exception)
                {
                    this.disposeChain();
                }
                finally
                {
                    refreshListView();
                }
            }
        }

        private void disposeChain()
        {
            if (_chain != null)
            {
                _chain.Dispose();
            }
            _chain = null;
        }

        private void refreshListView()
        {
            if (this._chain != null)
            {

                List<X509Certificate2> certificates = new List<X509Certificate2>(this._chain.ChainElements.Count);
                for (var i = 0; i <= this._chain.ChainElements.Count - 1; i++)
                {
                    var certificate = _chain.ChainElements[i].Certificate;
                    certificates.Add(certificate);
                }
                this.certificatesListView1.Certificates = certificates;

                this.fetchStatus(this.certificatesListView1.Items[0]);
            }
        }



        protected override void OnClosed(EventArgs e)
        {
            this.disposeChain();
            base.OnClosed(e);
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fetchStatus(this.certificatesListView1.FocusedItem);
        }

        private void fetchStatus(ListViewItem item)
        {
            if (_chain != null)
            {
                listViewDoubleBuffered1.BeginUpdate();
                listViewDoubleBuffered1.Items.Clear();
                var i = item?.Index;
                if (i != null)
                {
                    foreach (var status in _chain.ChainElements[i.Value].ChainElementStatus)
                    {
                        listViewDoubleBuffered1.Items.Add(status.StatusInformation);
                    }
                }
                listViewDoubleBuffered1.EndUpdate();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

            var certificate = this.certificatesListView1.FocusedCertificate;
            if (certificate != null)
            {
                certificate.Show();
            }

        }
    }
}
