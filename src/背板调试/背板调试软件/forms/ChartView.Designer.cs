namespace LD.forms
{
    partial class ChartView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MyChart = new LiveCharts.WinForms.CartesianChart();
            this.scale_option = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Tips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MyChart
            // 
            this.MyChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MyChart.BackColor = System.Drawing.Color.White;
            this.MyChart.Location = new System.Drawing.Point(12, 42);
            this.MyChart.Name = "MyChart";
            this.MyChart.Size = new System.Drawing.Size(770, 587);
            this.MyChart.TabIndex = 0;
            this.MyChart.Text = "cartesianChart1";
            // 
            // scale_option
            // 
            this.scale_option.FormattingEnabled = true;
            this.scale_option.Items.AddRange(new object[] {
            "无缩放",
            "缩放 X",
            "缩放 Y",
            "缩放 XY"});
            this.scale_option.Location = new System.Drawing.Point(12, 12);
            this.scale_option.Name = "scale_option";
            this.scale_option.Size = new System.Drawing.Size(90, 20);
            this.scale_option.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "选择数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(691, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "清除历史数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Tips
            // 
            this.Tips.AutoSize = true;
            this.Tips.Location = new System.Drawing.Point(232, 15);
            this.Tips.Name = "Tips";
            this.Tips.Size = new System.Drawing.Size(29, 12);
            this.Tips.TabIndex = 3;
            this.Tips.Text = "tips";
            // 
            // ChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(794, 641);
            this.Controls.Add(this.Tips);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scale_option);
            this.Controls.Add(this.MyChart);
            this.Name = "ChartView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart MyChart;
        private System.Windows.Forms.ComboBox scale_option;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Tips;
    }
}
