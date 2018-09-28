namespace CertificatesTool.Views
{
    partial class ChainViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewDoubleBuffered1 = new CertificatesTool.Components.ListViewDoubleBuffered();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.certificatesListView1 = new CertificatesTool.Components.CertificatesListView();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 212);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(800, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Issuer";
            this.columnHeader1.Width = 318;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Owner";
            this.columnHeader2.Width = 276;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Date from";
            this.columnHeader3.Width = 109;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date to";
            this.columnHeader4.Width = 161;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            this.columnHeader5.Width = 764;
            // 
            // listViewDoubleBuffered1
            // 
            this.listViewDoubleBuffered1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.listViewDoubleBuffered1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDoubleBuffered1.FullRowSelect = true;
            this.listViewDoubleBuffered1.GridLines = true;
            this.listViewDoubleBuffered1.Location = new System.Drawing.Point(0, 215);
            this.listViewDoubleBuffered1.Name = "listViewDoubleBuffered1";
            this.listViewDoubleBuffered1.Size = new System.Drawing.Size(800, 105);
            this.listViewDoubleBuffered1.TabIndex = 2;
            this.listViewDoubleBuffered1.UseCompatibleStateImageBehavior = false;
            this.listViewDoubleBuffered1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Status";
            this.columnHeader6.Width = 769;
            // 
            // certificatesListView1
            // 
            this.certificatesListView1.Certificates = null;
            this.certificatesListView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.certificatesListView1.FullRowSelect = true;
            this.certificatesListView1.GridLines = true;
            this.certificatesListView1.Location = new System.Drawing.Point(0, 0);
            this.certificatesListView1.Name = "certificatesListView1";
            this.certificatesListView1.Size = new System.Drawing.Size(800, 212);
            this.certificatesListView1.TabIndex = 3;
            this.certificatesListView1.UseCompatibleStateImageBehavior = false;
            this.certificatesListView1.View = System.Windows.Forms.View.Details;
            this.certificatesListView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.certificatesListView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // ChainViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 320);
            this.Controls.Add(this.listViewDoubleBuffered1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.certificatesListView1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ChainViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Certificate chain";
            this.Load += new System.EventHandler(this.ChainViewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private Components.ListViewDoubleBuffered listViewDoubleBuffered1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private Components.CertificatesListView certificatesListView1;
    }
}