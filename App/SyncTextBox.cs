using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using Nik.UserControls;

namespace _2ndbrainalpha
{
    public class SyncTextBox : RichTextBox
    {
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20A;
        public const int SB_VERT = 0x1;
        public const int SB_HORZ = 0x0;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int EM_SCROLL = 0x00b5;

        const int SB_LINEDOWN = 1;
        const int SB_LINEUP = 0;

        public SyncTextBox()
        {
            this.Multiline = true;
            this.ScrollBars = RichTextBoxScrollBars.Vertical;
        }
        
        Control _buddy;
        public Control Buddy { 
            get 
            {
                return _buddy;
            } 
            set 
            {
                _buddy = value;
            }
        }

        public string Id { 
            get 
            {
                return IsBuddy ? "[buddy]" : "";
            }
        }

        public bool IsBuddy 
        {
            get {
                return Buddy == null;
            }
        }

        private static bool scrolling;   // In case buddy tries to scroll us
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // Trap WM_VSCROLL message and pass to buddy
            if ((m.Msg == EM_SCROLL || m.Msg == SB_VERT || m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP || m.Msg == WM_VSCROLL))
            {
                Debug.WriteLine($"{Id}WndProc::{Win32Msgs.GetMsgName(m.Msg)}");
                if (!scrolling && Buddy != null && Buddy.IsHandleCreated)
                {
                    scrolling = true;
                    SendMessage(Buddy.Handle, m.Msg, m.WParam, m.LParam);
                    scrolling = false;
                }
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Debug.WriteLine($"OnMouseWheel {Id}");

            if (!IsBuddy)
            {
                if (e.Delta > 0)
                {
                    SendMessage(this.Handle, EM_SCROLL, new IntPtr(SB_LINEUP), new IntPtr(1));
                }
                else
                {
                    SendMessage(this.Handle, EM_SCROLL, new IntPtr(SB_LINEDOWN), new IntPtr(1));
                }

            }
        }
    }
}