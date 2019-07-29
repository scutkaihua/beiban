using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace LD.forms
{
    class TabControlExt:TabControl
{

        private int iconWidth = 16;
        private int iconHeight = 16;
        private Image icon = null;

        public TabControlExt()
            : base()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;

           //// icon = LD.Properties.Resources.close;
           // iconWidth = icon.Width;
           // iconHeight = icon.Height;

            this.ItemSize = new System.Drawing.Size(20, 30);
            this.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = GetTabRect(e.Index);

            g.FillRectangle(Brushes.WhiteSmoke, r);
            string title = this.TabPages[e.Index].Text;
            
            g.DrawString(title, this.Font, new SolidBrush(Color.Black), new PointF(r.X + 2, r.Y + ItemSize.Height/2-4));

            r.Offset(r.Width - iconWidth - 3, 2);
           // g.DrawImage(icon, new Point(r.X, r.Y +8 ));




        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            Rectangle r = GetTabRect(this.SelectedIndex);
            r.Offset(r.Width - iconWidth - 3, 2);
            r.Width = iconWidth;
            r.Height = iconHeight;
            if (r.Contains(p)) this.TabPages.RemoveAt(this.SelectedIndex);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TabControlExt
            // 
            //this.Font = new System.Drawing.Font("幼圆", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ResumeLayout(false);

        }


    }
}
