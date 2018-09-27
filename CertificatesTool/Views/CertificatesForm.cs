using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            this.localMachineNode=this.rootNode.Nodes.Add("LocalMachine");
        }

        private void refresh()
        {

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

        private void listViewRefresh(IEnumerable<System.Security.Cryptography.X509Certificates.X509Certificate2> certificates)
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            if (certificates != null)
            {
                foreach (var cert in certificates)
                {
                    listView1.Items.Add(cert.SerialNumber);
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
            }
            else
                this.listViewRefresh(null);
        }
    }
}
