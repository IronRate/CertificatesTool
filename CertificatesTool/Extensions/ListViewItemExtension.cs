using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertificatesTool.Extensions
{
    internal static class ListViewItemExtension
    {
        public static void Focus(this ListViewItem item) {
            item.Selected = true;
            item.Focused = true;
        }
    }
}
