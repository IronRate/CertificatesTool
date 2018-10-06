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
    public partial class CertificatesForm : Form
    {

        private Repositories.CertificatesRepository certificatesRepository = new Repositories.CertificatesRepository();
        TreeNode rootNode;
        TreeNode localMachineNode;
        TreeNode currentUserNode;
        private string searchString;

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
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            this.rootNode = this.treeView1.Nodes.Add("Certificates");
            this.currentUserNode = this.rootNode.Nodes.Add($"CurrentUser ({userName})");
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

        //private void validateCertificates(ListView.ListViewItemCollection items)
        //{

        //    foreach (ListViewItem item in items)
        //    {
        //        var certificate = item.Tag as System.Security.Cryptography.X509Certificates.X509Certificate2;
        //        if (certificate != null)
        //        {
        //            var b = Task.Run(() =>
        //              {
        //                  bool valid = false;
        //                  string error = null;
        //                  try
        //                  {
        //                      valid = certificate.Verify();
        //                  }
        //                  catch (System.Security.Cryptography.CryptographicException ex)
        //                  {

        //                      //item.ToolTipText = ex.Message;

        //                  }
        //                  return valid;
        //              }).Result;


        //            listView1.BeginUpdate();
        //            if (!b)
        //            {
        //                item.BackColor = Color.FromArgb(255, 0, 0);
        //                item.ForeColor = Color.White;
        //            }
        //            else
        //            {
        //                item.BackColor = listView1.BackColor;
        //                item.ForeColor = listView1.ForeColor;
        //            }
        //            listView1.EndUpdate();
        //        }
        //    }

        //}

        private X509Certificate2 searchCertificates(TreeNode currentNode)
        {
            if (currentNode.Nodes != null && currentNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in currentNode.Nodes)
                {
                    var findedCertificate = searchCertificates(node);
                    if (findedCertificate != null)
                        return findedCertificate;
                }
            }
            else
            {
                var certificates = currentNode.Tag as IEnumerable<X509Certificate2>;
                if (certificates != null)
                {
                    foreach (var certificate in certificates)
                    {
                        if (certificate.Contains(this.searchString))
                        {
                            currentNode.Parent.Expand();
                            treeView1.SelectedNode = currentNode;
                            return certificate;
                        }
                    }
                }
            }
            return null;
        }

        private void listViewRefresh(IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> certificates)
        {
            certificatesListView1.Certificates = certificates?.ToList();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var certificates = e.Node.Tag as IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2>;
                this.listViewRefresh(certificates);

            }
            else
                this.listViewRefresh(null);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var certificate = this.certificatesListView1.FocusedCertificate;

            if (certificate != null)
            {
                certificate.Show();
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.searchString = toolStripTextBox1.Text;
            if (!string.IsNullOrWhiteSpace(this.searchString))
            {
                var findedCertificate = this.searchCertificates(this.rootNode);
                if (findedCertificate != null)
                {

                }
            }
        }



        private void chainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.certificatesListView1.FocusedCertificate!=null)
            {
                var chainViewForm = new ChainViewForm();
                chainViewForm.Certificate = this.certificatesListView1.FocusedCertificate;
                chainViewForm.ShowDialog(this);
                chainViewForm.Dispose();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.certificatesListView1.FocusedCertificate != null) {
                SaveFileDialog fileDialog = new SaveFileDialog();
                if (fileDialog.ShowDialog(this) == DialogResult.OK) {
                    var containerColumn = this.certificatesListView1.GetColumnFromFocuseRow(6);
                    var containerName = containerColumn.Text.Substring(containerColumn.Text.IndexOf(@"\")+1);
                    Services.RegistryService registryService = new Services.RegistryService();
                    registryService.Export(fileDialog.FileName, @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Crypto Pro\Settings\Keys"+containerName);
                }
            }
        }
    }
}

