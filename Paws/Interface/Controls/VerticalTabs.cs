/* CREDIT: hackersrage */
/* SOURCE: https://www.thebuddyforum.com/honorbuddy-forum/community-developer-forum/201626-4k-resolution-skinned-form-plugin-sample.html */

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Paws.Interface.Controls
{
    public class VerticalTabs : TabControl
    {
        private const int TcmAdjustrect = 0x1328;
        private Color _tabBackgroundColor = Color.White;
        private Font _tabFont = new Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Pixel);
        // [DllImportAttribute("uxtheme.dll")]
        // private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

        private Color _tabFontColor = Color.Black;
        private Color _tabSelectedBackgroundColor = Color.White;
        private Color _tabSelectedFontColor = Color.Black;

        private Color _mBackColor = Color.Transparent;

        public VerticalTabs()
        {
            //            DrawItem += VerticalTabs_DrawItem;
            Alignment = TabAlignment.Left;
            DrawMode = TabDrawMode.OwnerDrawFixed;
            ItemSize = new Size(21, 200);
            Multiline = true;
            Padding = new Point(0, 6);
            Size = new Size(465, 296);
            SizeMode = TabSizeMode.Fixed;
        }

        public Color MyBackColor
        {
            get { return _mBackColor; }
            set
            {
                _mBackColor = value;
                Invalidate();
            }
        }

        public Color TabFontColor
        {
            get { return _tabFontColor; }
            set
            {
                _tabFontColor = value;
                Refresh();
            }
        }

        public Color TabBackgroundColor
        {
            get { return _tabBackgroundColor; }
            set
            {
                _tabBackgroundColor = value;
                Refresh();
            }
        }

        public Color TabSelectedFontColor
        {
            get { return _tabSelectedFontColor; }
            set
            {
                _tabSelectedFontColor = value;
                Refresh();
            }
        }

        public Color TabSelectedBackgroundColor
        {
            get { return _tabSelectedBackgroundColor; }
            set
            {
                _tabSelectedBackgroundColor = value;
                Refresh();
            }
        }

        public Font TabFont
        {
            get { return _tabFont; }
            set
            {
                _tabFont = value;
                Refresh();
            }
        }

        public StringAlignment TabTextHAlign { get; set; }

        public StringAlignment TabTextVAlign { get; set; }


        protected override bool ShowFocusCues
        {
            get { return false; }
        }


        protected override void WndProc(ref Message m)
        {
            //Hide the tab headers at run-time
            if (m.Msg == TcmAdjustrect)
            {
                var rect = (Rect) m.GetLParam(typeof (Rect));
                rect.Left = Left - Margin.Left;
                rect.Right = Right + Margin.Right;

                rect.Top = Top - Margin.Top;
                rect.Bottom = Bottom + Margin.Bottom;
                Marshal.StructureToPtr(rect, m.LParam, true);
                //m.Result = (IntPtr)1;
                //return;
            }
            //else
            // call the base class implementation
            base.WndProc(ref m);
        }

        //  protected override void OnHandleCreated(EventArgs e) {
        //      SetWindowTheme(this.Handle, "", "");
        //      base.OnHandleCreated(e);
        //  }


        //            private void VerticalTabs_DrawItem( object sender, DrawItemEventArgs e ) {
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var textBrush = default(Brush);
            var backgroundBrush = default(Brush);

            // Get the item from the collection. 
            var tabPage = TabPages[e.Index];

            // Get the real bounds for the tab rectangle. 
            var tabBounds = GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                textBrush = new SolidBrush(TabSelectedFontColor);
                backgroundBrush = new SolidBrush(TabSelectedBackgroundColor);
                g.FillRectangle(backgroundBrush, e.Bounds);
            }
            else
            {
                textBrush = new SolidBrush(TabFontColor);
                backgroundBrush = new SolidBrush(TabBackgroundColor);
                g.FillRectangle(backgroundBrush, e.Bounds);

                //                    e.Graphics.Clear( System.Drawing.SystemColors.ControlDarkDark );
                //                    e.Graphics.Clear( System.Drawing.SystemColors.Control );
                // e.DrawBackground();
            }

            // Use our own font. 
            //Font _TabFont=new Font( "Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel );

            // Draw string. Center the text. 
            var stringFlags = new StringFormat();
            stringFlags.Alignment = TabTextHAlign;
            stringFlags.LineAlignment = TabTextVAlign;
            g.DrawString(" " + tabPage.Text, TabFont, textBrush, tabBounds, new StringFormat(stringFlags));
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // VerticalTabs
            // 
            Alignment = TabAlignment.Left;
            DrawMode = TabDrawMode.OwnerDrawFixed;
            ItemSize = new Size(30, 200);
            Multiline = true;
            Padding = new Point(0, 0);
            Size = new Size(465, 296);
            SizeMode = TabSizeMode.Fixed;
            ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawControl(e.Graphics);
        }

        internal void DrawControl(Graphics g)
        {
            if (!Visible)
                return;

            var tabControlArea = ClientRectangle;
            var tabArea = DisplayRectangle;

            //----------------------------
            // fill client area
            Brush br = new SolidBrush(_mBackColor); //(SystemColors.Control); UPDATED
            g.FillRectangle(br, tabControlArea);
            br.Dispose();
            //----------------------------
        }

        private struct Rect
        {
            public int Left, Top, Right, Bottom;
        }
    }
}