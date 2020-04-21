using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace _2ndbrainalpha
{
    public class BufferedTreeView : TreeView
    {
        public BufferedTreeView() : base()
        {
        }

        public void Copy()  { SendMessage(GetEditControl(), 0x301, IntPtr.Zero, IntPtr.Zero); }

        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        private IntPtr GetEditControl() {
            // Use TVM_GETEDITCONTROL to get the handle of the edit box
            IntPtr hEdit = SendMessage(this.Handle, 0x1100 + 15, IntPtr.Zero, IntPtr.Zero);
            if (hEdit == IntPtr.Zero) throw new InvalidOperationException("Not currently editing a label");
            return hEdit;
        }

        // Pinvoke:
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        
    }
}
