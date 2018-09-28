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
                    verifyCertificates();
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
                listView1.BeginUpdate();
                listView1.Items.Clear();
                List<ListViewItem> items = new List<ListViewItem>(_chain.ChainElements.Count);
                for (var i = 0; i <= this._chain.ChainElements.Count - 1; i++)
                {
                    var certificate = _chain.ChainElements[i].Certificate;
                    var item = new ListViewItem();
                    item.Text = certificate.GetNameInfo(X509NameType.SimpleName, true);
                    item.SubItems.Add(certificate.GetNameInfo(X509NameType.SimpleName, false));
                    item.SubItems.Add(certificate.NotBefore.ToShortDateString());
                    item.SubItems.Add(certificate.NotAfter.ToShortDateString());
                    items.Add(item);
                }
                listView1.Items.AddRange(items.ToArray());
                listView1.EndUpdate();
                //listView1.Items[0].Focus();

                this.fetchStatus(listView1.Items[0]);
            }
        }

        protected void verifyCertificates()
        {
            if (_chain == null)
                return;

            for (var i = 0; i <= this._chain.ChainElements.Count - 1; i++)
            {

                var certificate = _chain.ChainElements[i].Certificate;
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


                    listView1.BeginUpdate();
                    var item = listView1.Items[i];
                    if (!b)
                    {
                        item.BackColor = Color.FromArgb(255, 0, 0);
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = listView1.BackColor;
                        item.ForeColor = listView1.ForeColor;
                    }
                    listView1.EndUpdate();
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            this.disposeChain();
            base.OnClosed(e);
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fetchStatus(listView1.FocusedItem);
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
            var item = this.listView1.FocusedItem;
            if (item != null)
            {
                var certificate = this._chain.ChainElements[item.Index].Certificate;
                if (certificate != null)
                {
                    X509Certificate2UI.DisplayCertificate(certificate);
                }
            }
        }
    }
}
