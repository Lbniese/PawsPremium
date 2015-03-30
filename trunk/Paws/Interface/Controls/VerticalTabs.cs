/* CREDIT: hackersrage */
/* SOURCE: https://www.thebuddyforum.com/honorbuddy-forum/community-developer-forum/201626-4k-resolution-skinned-form-plugin-sample.html */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Paws.Interface
{

    public class VerticalTabs : TabControl
    {
        // [DllImportAttribute("uxtheme.dll")]
        // private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

        private System.Drawing.Color _TabFontColor = System.Drawing.Color.Black;
        private System.Drawing.Color _TabBackgroundColor = System.Drawing.Color.White;
        private System.Drawing.Color _TabSelectedFontColor = System.Drawing.Color.Black;
        private System.Drawing.Color _TabSelectedBackgroundColor = System.Drawing.Color.White;
        private System.Drawing.Font _TabFont = new Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Pixel);

        private System.Drawing.Color mBackColor = System.Drawing.Color.Transparent;

        public Color myBackColor
        {
            get { return mBackColor; }
            set { mBackColor = value; this.Invalidate(); }
        }
        public System.Drawing.Color TabFontColor { get { return _TabFontColor; } set { _TabFontColor = value; this.Refresh(); } }

        public System.Drawing.Color TabBackgroundColor { get { return _TabBackgroundColor; } set { _TabBackgroundColor = value; this.Refresh(); } }

        public System.Drawing.Color TabSelectedFontColor { get { return _TabSelectedFontColor; } set { _TabSelectedFontColor = value; this.Refresh(); } }

        public System.Drawing.Color TabSelectedBackgroundColor { get { return _TabSelectedBackgroundColor; } set { _TabSelectedBackgroundColor = value; this.Refresh(); } }

        public System.Drawing.Font TabFont { get { return _TabFont; } set { _TabFont = value; this.Refresh(); } }

        public StringAlignment TabTextHAlign { get; set; }

        public StringAlignment TabTextVAlign { get; set; }

        public VerticalTabs()
        {
            //            DrawItem += VerticalTabs_DrawItem;
            this.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ItemSize = new System.Drawing.Size(21, 200);
            this.Multiline = true;
            this.Padding = new System.Drawing.Point(0, 6);
            this.Size = new System.Drawing.Size(465, 296);
            this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        }


        private const int TCM_ADJUSTRECT = 0x1328;


        protected override void WndProc(ref Message m)
        {
            //Hide the tab headers at run-time
            if (m.Msg == TCM_ADJUSTRECT)
            {

                RECT rect = (RECT)(m.GetLParam(typeof(RECT)));
                rect.Left = this.Left - this.Margin.Left;
                rect.Right = this.Right + this.Margin.Right;

                rect.Top = this.Top - this.Margin.Top;
                rect.Bottom = this.Bottom + this.Margin.Bottom;
                Marshal.StructureToPtr(rect, m.LParam, true);
                //m.Result = (IntPtr)1;
                //return;
            }
            //else
            // call the base class implementation
            base.WndProc(ref m);
        }


        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        //  protected override void OnHandleCreated(EventArgs e) {
        //      SetWindowTheme(this.Handle, "", "");
        //      base.OnHandleCreated(e);
        //  }



        //            private void VerticalTabs_DrawItem( object sender, DrawItemEventArgs e ) {
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _TextBrush = default(Brush);
            Brush _BackgroundBrush = default(Brush);

            // Get the item from the collection. 
            TabPage _TabPage = this.TabPages[e.Index];

            // Get the real bounds for the tab rectangle. 
            Rectangle _TabBounds = this.GetTabRect(e.Index);

            if ((e.State == DrawItemState.Selected))
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _TextBrush = new SolidBrush(TabSelectedFontColor);
                _BackgroundBrush = new SolidBrush(TabSelectedBackgroundColor);
                g.FillRectangle(_BackgroundBrush, e.Bounds);
            }
            else
            {

                _TextBrush = new System.Drawing.SolidBrush(TabFontColor);
                _BackgroundBrush = new SolidBrush(TabBackgroundColor);
                g.FillRectangle(_BackgroundBrush, e.Bounds);

                //                    e.Graphics.Clear( System.Drawing.SystemColors.ControlDarkDark );
                //                    e.Graphics.Clear( System.Drawing.SystemColors.Control );
                // e.DrawBackground();
            }

            // Use our own font. 
            //Font _TabFont=new Font( "Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel );

            // Draw string. Center the text. 
            StringFormat _StringFlags = new StringFormat();
            _StringFlags.Alignment = TabTextHAlign;
            _StringFlags.LineAlignment = TabTextVAlign;
            g.DrawString(" " + _TabPage.Text, TabFont, _TextBrush, _TabBounds, new StringFormat(_StringFlags));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // VerticalTabs
            // 
            this.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.ItemSize = new System.Drawing.Size(30, 200);
            this.Multiline = true;
            this.Padding = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(465, 296);
            this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ResumeLayout(false);

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

            Rectangle TabControlArea = this.ClientRectangle;
            Rectangle TabArea = this.DisplayRectangle;

            //----------------------------
            // fill client area
            Brush br = new SolidBrush(mBackColor); //(SystemColors.Control); UPDATED
            g.FillRectangle(br, TabControlArea);
            br.Dispose();
            //----------------------------


        }

    }
}
