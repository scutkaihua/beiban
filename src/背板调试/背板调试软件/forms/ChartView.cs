using System;
using System.ComponentModel;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Geared;
using Geared.Wpf.Scrollable;
using System.Windows.Media;
using System.Collections.Generic;
using LD.lib;

//Install-Package LiveCharts.WinForms
//Install-Package LiveCharts.Wpf


namespace LD.forms
{
    public partial class ChartView : Form//UserControl
    {
        List<ChannelValueSelectItems> savechs;
        ChannelValues chartvalues;
        ChartDataSelect select = new ChartDataSelect();
        ScrollableViewModel scrollable = new ScrollableViewModel();
        AxesCollection axes = new AxesCollection();
        List<Axis> listAxis = new List<Axis>();
        Axis currentAxis = null;
        public ChartView(ChannelValues v)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            chartvalues = v;

            MyChart.DisableAnimations = true;
            MyChart.DataTooltip = null;
            MyChart.AnimationsSpeed = TimeSpan.FromMilliseconds(150);
            MyChart.Zoom = ZoomingOptions.None;
            MyChart.AxisY = axes;
            MyChart.LegendLocation = LegendLocation.Right;
            MyChart.DataHover += MyChart_DataHover; //鼠标经过显示数值

            scale_option.SelectedIndex = 0;
            scale_option.SelectedIndexChanged += Scale_option_SelectedIndexChanged;
            select.onConfigChanaged += new ChartDataSelect.OnConfigChanaged(OnConfigChanaged);
            select.Text = "监控数据项";
            select.LoadItems(chartvalues.ChannelValueNames());

            MIN.TextChanged += MIN_TextChanged;
            MAX.TextChanged += MAX_TextChanged;
            CB.SelectedIndexChanged += CB_SelectedIndexChanged;
        }


        #region 修改坐标轴最大最小值

        private void SetMin(float m)
        {
            if (currentAxis != null) currentAxis.MinValue = m;
            foreach(Axis a in listAxis)
            {
                if (a.Title == currentAxis.Title) { a.MinValue = m;break; }
            }
        }

        private void SetMax(float m)
        {
            if (currentAxis != null) currentAxis.MaxValue = m;
            foreach (Axis a in listAxis)
            {
                if (a.Title == currentAxis.Title) { a.MaxValue = m; break; }
            }
        }

        /// <summary>
        /// 选择坐标轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Axis a in axes) 
            { 
                if (a.Title == CB.Text) 
                { 
                    currentAxis = a;
                    MIN.Text = a.MinValue.ToString("f2");
                    MAX.Text = a.MaxValue.ToString("f2");
                    break; 
                } 
            }
        }

        private void MAX_TextChanged(object sender, EventArgs e)
        {
            try { float v = float.Parse(MAX.Text);SetMax(v); }catch(Exception ee) { }
        }

        private void MIN_TextChanged(object sender, EventArgs e)
        {
            try { float v = float.Parse(MIN.Text); SetMin(v); } catch (Exception ee) { }
        }

        #endregion


        private void MyChart_DataHover(object sender, ChartPoint chartPoint)
        {
            Tips.Text = string.Format("{0} :({1},{2})",
                chartPoint.SeriesView.Title,
                chartPoint.Key, chartPoint.SeriesView.ActualValues[chartPoint.Key].ToString());
        }


        /// <summary>
        /// 坐标轴索引
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetAxes(string name)
        {
            KeyInfo ki = chartvalues.GetKeyInfo(name);
            Axis r = null;
            foreach (Axis a in axes) { if (a.Title == ki.axie) { return axes.IndexOf(a); } }
            foreach ( Axis a in listAxis){if(a.Title == ki.axie){r = a;break;}}

            //没有，添加坐标轴
            Axis ax = new Axis
            {
                Foreground = ki.brush,
                Title = ki.axie,
                Position = ki.position,
                MinValue = (r==null)?ki.min:r.MinValue,
                MaxValue = (r==null)?ki.max:r.MaxValue,
            };
            if (r == null) listAxis.Add(ax);
            if (!CB.Items.Contains(ax.Title)) CB.Items.Add(ax.Title);
            axes.Add(ax);
            return axes.IndexOf(ax);
        }

        /// <summary>
        /// 刷新曲线配置
        /// </summary>
        /// <param name="chs"></param>
        void RefreshConfig(List<ChannelValueSelectItems> chs)
        {
            if (chs == null) return;
            MyChart.Series = new SeriesCollection();
            axes.Clear();currentAxis = null;CB.Items.Clear();CB.Text = null;MIN.Text = MAX.Text = null;
            foreach (ChannelValueSelectItems i in chs)
            {
                if (i.names.Count == 0) continue;
                foreach(ChannelValueSelectItem ii in i.names)
                {
                    int a = GetAxes(ii.name);
                    MyChart.Series.Add(new GLineSeries
                    {
                        Values = chartvalues.ChannelValue(i.channel, ii.name),
                        Fill = Brushes.Transparent,
                        StrokeThickness = 2,
                        ScalesYAt = a,
                        Title = (axes[a].Title=="布尔")?(string.Format("{0}-{1}-{2}",(i.channel),ii.name,axes[a].Title)):(string.Format("{0}-{1}", (i.channel), ii.name)),
                    });
                }
            }
       
        }

        
        /// <summary>
        /// 配置改变时，刷新
        /// </summary>
        /// <param name="chs"></param>
        void OnConfigChanaged(List<ChannelValueSelectItems> chs)
        {
            if(savechs != chs)
                savechs = chs;
            
            if (this.InvokeRequired)
            {
                ChartDataSelect.OnConfigChanaged handler = new ChartDataSelect.OnConfigChanaged(OnConfigChanaged);
                this.BeginInvoke(handler,new object[] { chs});
            }
            else
            {
                RefreshConfig(chs);
            }
        }

        /// <summary>
        /// 放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scale_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (scale_option.SelectedIndex)
            {
                case 0:MyChart.Zoom = ZoomingOptions.None;break;
                case 1:MyChart.Zoom = ZoomingOptions.X;break;
                case 2:MyChart.Zoom = ZoomingOptions.Y;break;
                case 3:MyChart.Zoom = ZoomingOptions.Xy;break;
                default: break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (select.Visible)
                select.Hide();
            else
              select.Show();
        }

        private void 清除历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show("确定删除历史数据吗??","确定删除?", MessageBoxButtons.YesNo);
            if(result== DialogResult.Yes)
            {
                chartvalues.Clear();
            } 
        }
    }
}
