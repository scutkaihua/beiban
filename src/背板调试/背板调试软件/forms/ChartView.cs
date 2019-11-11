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

            scale_option.SelectedIndex = 0;
            scale_option.SelectedIndexChanged += Scale_option_SelectedIndexChanged;

            select.onConfigChanaged += new ChartDataSelect.OnConfigChanaged(OnConfigChanaged);

            select.LoadItems(chartvalues.ChannelValueNames());
        }

        private int GetAxes(string name)
        {
            KeyInfo ki = chartvalues.GetKeyInfo(name);

            foreach( Axis a in axes)
            {
                if(a.Title == ki.axie)
                {
                    return axes.IndexOf(a);
                }
            }
             
            //没有，添加坐标轴
            Axis ax = new Axis
            {
                Foreground = ki.brush,
                Title = ki.axie,
                Position = AxisPosition.LeftBottom,
                MinValue = ki.min,
                MaxValue = ki.max,

            };
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
            axes.Clear();
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
                        Title = string.Format("{0}-{1}",(i.channel),ii.name),
                    }); ;

                  
                }
            }
       
        }

        
        void OnConfigChanaged(List<ChannelValueSelectItems> chs)
        {
            if(savechs != chs)
                savechs = chs;
            
            if (this.InvokeRequired)
            {
                ChartDataSelect.OnConfigChanaged handler = new ChartDataSelect.OnConfigChanaged(OnConfigChanaged);
                this.Invoke(handler,new object[] { chs});
            }
            else
            {
                RefreshConfig(chs);
            }
        }

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
            select.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show("确定删除历史数据吗??","确定删除?", MessageBoxButtons.YesNo);
            if(result== DialogResult.Yes)
            {
                chartvalues.Clear();
            } 
        }
    }
}
