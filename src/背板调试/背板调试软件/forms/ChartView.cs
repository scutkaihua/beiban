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

        public ChartView(ChannelValues v)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            chartvalues = v;

            Timer t = new Timer();
            t.Interval = 1000;
            t.Tick += T_Tick;
            t.Start();

            MyChart.Series = new SeriesCollection
            {

                new GLineSeries
                {
                    Values = null,
                    Fill = Brushes.Transparent,
                    StrokeThickness = 2,
                    PointGeometry = null //use a null geometry when you have many series
                },
            };

            MyChart.DisableAnimations = true;
            MyChart.DataTooltip = null;
            MyChart.AnimationsSpeed = TimeSpan.FromMilliseconds(150);
            MyChart.Zoom = ZoomingOptions.None;

            MyChart.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.Peru,
                Title = "Peru Axis",
                Position = AxisPosition.RightTop,
            });


            scale_option.SelectedIndex = 0;
            scale_option.SelectedIndexChanged += Scale_option_SelectedIndexChanged;

            select.onConfigChanaged += new ChartDataSelect.OnConfigChanaged(OnConfigChanaged);

            select.LoadItems(chartvalues.ChannelValueNames());
        }



        
        private void T_Tick(object sender, EventArgs e)
        {
            //OnConfigChanaged(savechs);
        }

        /// <summary>
        /// 刷新曲线配置
        /// </summary>
        /// <param name="chs"></param>
        void RefreshConfig(List<ChannelValueSelectItems> chs)
        {
            if (chs == null || chs.Count == 0) return;
            MyChart.Series = new SeriesCollection();

            foreach (ChannelValueSelectItems i in chs)
            {
                if (i.names.Count == 0) continue;
                foreach(ChannelValueSelectItem ii in i.names)
                {
                    MyChart.Series.Add(new GLineSeries
                    {
                        Values = chartvalues.ChannelValue(i.channel,ii.name),
                        Fill = Brushes.Transparent,
                        StrokeThickness = 2,
                        PointGeometry = null //use a null geometry when you have many series
                    
                    });
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
    }
}
