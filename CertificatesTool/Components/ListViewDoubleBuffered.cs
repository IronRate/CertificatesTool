using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificatesTool.Components
{
    internal class ListViewDoubleBuffered:ListView
    {
        public ListViewDoubleBuffered():base()
        {
            this.DoubleBuffered = true;
            this.FullRowSelect = true;
            this.GridLines = true;
        }

        protected override bool DoubleBuffered { get => base.DoubleBuffered; set => base.DoubleBuffered = value; }
    }
}
