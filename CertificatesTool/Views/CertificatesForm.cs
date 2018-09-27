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
    public partial class CertificatesForm : Form
    {

        private Repositories.CertificatesRepository certificatesRepository = new Repositories.CertificatesRepository();
        TreeNode rootNode;
        TreeNode localMachineNode;
        TreeNode currentUserNode;

        public CertificatesForm()
        {
            InitializeComponent();
        }

        private void CertificatesForm_Load(object sender, EventArgs e)
        {
            this.initTree();
            this.refresh();
        }

        private void initTree()
        {
            this.rootNode = this.treeView1.Nodes.Add("Certificates");
            this.currentUserNode = this.rootNode.Nodes.Add("CurrentUser");
            this.localMachineNode = this.rootNode.Nodes.Add("LocalMachine");
        }

        private void refresh()
        {
            currentUserNode.Nodes.Clear();
            localMachineNode.Nodes.Clear();
            refresh(System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser, currentUserNode);
            refresh(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, localMachineNode);
        }

        private void refresh(System.Security.Cryptography.X509Certificates.StoreLocation storeLocation, TreeNode parentNode)
        {
            var certificateNodes = certificatesRepository.Get(storeLocation);
            foreach (var cert in certificateNodes)
            {
                var node = parentNode.Nodes.Add(cert.StoreName);
                node.Tag = cert.Certificates;
            }
        }

        private void validateCertificates(ListView.ListViewItemCollection items)
        {

            foreach (ListViewItem item in items)
            {
                var certificate = item.Tag as System.Security.Cryptography.X509Certificates.X509Certificate2;
                if (certificate != null)
                {
                    var b = Task.Run(() =>
                      {
                          bool valid = false;
                          try
                          {
                              valid = certificate.Verify();
                          }
                          catch (System.Security.Cryptography.CryptographicException ex)
                          {
                              item.ToolTipText = ex.Message;
                          }
                          return valid;
                      }).Result;


                    listView1.BeginUpdate();
                    if (b)
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

        private void listViewRefresh(IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> certificates)
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            if (certificates != null)
            {
                foreach (var cert in certificates)
                {
                    var item = listView1.Items.Add(cert.Issuer);
                    item.SubItems.Add(cert.SerialNumber);
                    item.SubItems.Add(cert.NotBefore.ToShortDateString());
                    item.SubItems.Add(cert.NotAfter.ToShortDateString());
                    item.Tag = cert;

                }
            }
            listView1.EndUpdate();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var certificates = e.Node.Tag as IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2>;
                this.listViewRefresh(certificates);
                this.validateCertificates(listView1.Items);

            }
            else
                this.listViewRefresh(null);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var item = this.listView1.FocusedItem;
            if (item != null && item.Tag != null)
            {
                var certificate = item.Tag as System.Security.Cryptography.X509Certificates.X509Certificate2;
                if (certificate != null)
                {
                    X509Certificate2UI.DisplayCertificate(certificate);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.refresh();
        }


    }
}

