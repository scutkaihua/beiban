using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LD.lib;

namespace LD.forms
{
    public partial class ChartDataSelect : Form
    {

        List<ChannelValueSelectItems> chs = new List<ChannelValueSelectItems>();


        public ChartDataSelect()
        {
            InitializeComponent();
            this.dgv.ColumnHeaderMouseDoubleClick += Dgv_ColumnHeaderMouseDoubleClick;
            this.dgv.RowHeaderMouseDoubleClick += Dgv_RowHeaderMouseDoubleClick;
            this.dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
            this.dgv.CellDoubleClick += Dgv_CellDoubleClick;

            //按列数据添加数据项
            for(int i = 1; i < dgv.Columns.Count; i++) {
                ChannelValueSelectItems items = new ChannelValueSelectItems();items.channel = i;chs.Add(items);
            }
        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex < 1) return;
            ColorDialog ColorForm = new ColorDialog();
            if (ColorForm.ShowDialog() == DialogResult.OK)
            {
                Color GetColor = ColorForm.Color;
                //GetColor就是用户选择的颜色，接下来就可以使用该颜色了
                ((DataGridView)sender).CurrentCell.Style.BackColor = GetColor;
            }
        }

        /// <summary>
        /// 编辑单元格时，使用datavaluechanged 有效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        /// <summary>
        /// 单元选择更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int ch = e.ColumnIndex;
            int r = e.RowIndex;

            DataGridViewCell c = dgv.Rows[r].Cells[ch];
            DataGridViewCell n = dgv.Rows[r].Cells[0];
            if (n.Value == null || n.Value==null) return;
            if ((bool)(c.Value) == true)
            {
                chs[ch - 1].Add(r,ch,n.Value.ToString(),c.Style.BackColor);
                onConfigChanaged?.Invoke(chs);
            }
            else
            {
                chs[ch - 1].Remove(n.Value.ToString());
                onConfigChanaged?.Invoke(chs);
            }
                
        }
        /// <summary>
        /// 行全选 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int r = e.RowIndex;
            int col = dgv.Columns.Count;
            
            bool s = true;
            dgv.EndEdit();
            for (int i = 1; i < col; i++)
            {
                DataGridViewCell c = dgv.Rows[r].Cells[i];
                if (c.Value == null) continue;
                bool v = (bool)c.Value;
                if (v == true) { s = false; break; }
            }
            for (int i = 1; i < col; i++)
            {
                DataGridViewCell c = dgv.Rows[r].Cells[i];
                c.Value = s;
            }
        }

        /// <summary>
        /// 列全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rows = dgv.Rows.Count;
            int col = e.ColumnIndex;
            bool s = true;
            dgv.EndEdit();
            for(int i = 0; i < rows; i++)
            {
                DataGridViewCell c = dgv.Rows[i].Cells[col];
                if (c.Value == null) continue;
                bool v = (bool)c.Value;
                if (v == true) { s = false; break; }
            }
            for (int i = 0; i < rows; i++)
            {
                DataGridViewCell c = dgv.Rows[i].Cells[col];
                c.Value = s;
            }
        }


        /// <summary>
        /// 加载行
        /// </summary>
        /// <param name="items"></param>
        public void LoadItems(string [] items)
        {
            this.dgv.Rows.Clear();
            dgv.CellValueChanged -= Dgv_CellValueChanged;
            //添加行
            foreach (string i  in items)
            {
                int index = dgv.Rows.Add(new DataGridViewRow());
                dgv.Rows[index].Cells[0].Value = i ;
            }

            //设置曲线颜色
            float red=255, green=0, blue=0;
            float step = 7.4F ;
            int state = 0;
            foreach(DataGridViewRow r in dgv.Rows)
            {
                foreach(DataGridViewCell c in r.Cells)
                {
                    if (c.ColumnIndex == 0) continue;
                    switch (state)
                    {
                        case 0: red   -= step;   green += step; if (red < 0  ) { red = 0;   green = 254; state++; } break;
                        case 1: green -= step;   blue  += step; if (green < 0) { green = 0; blue  = 254; state++; } break;
                        case 2: blue  -= step;   red   += step; if (blue < 0 ) { blue = 0;  red   = 254; state=0; } break;
                    }
                    red %= 255;green %= 255;blue %= 255;

                    //c.Style.BackColor = Color.FromArgb((int)red, (int)green, (int)blue);

                }
            }

            //cell change
            dgv.CellValueChanged += Dgv_CellValueChanged;
        }



        public List<ChannelValueSelectItems> SelectConfigGet()
        {
            return chs;
        }

        public delegate void OnConfigChanaged(List<ChannelValueSelectItems> c);
        public OnConfigChanaged onConfigChanaged;



        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            base.OnClosing(e);
        }

    }
}
