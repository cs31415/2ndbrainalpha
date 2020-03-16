using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2ndbrainalpha
{
    public class SyncBuddyTextBox : RichTextBox
    {
        public void HandleMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            HandleMouseWheel(e);
        }
    }
}
