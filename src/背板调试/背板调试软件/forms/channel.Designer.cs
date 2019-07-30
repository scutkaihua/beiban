namespace LD.forms
{
    partial class channel
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.id = new System.Windows.Forms.TextBox();
            this.values = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.addr = new System.Windows.Forms.TextBox();
            this.time = new System.Windows.Forms.TextBox();
            this.error = new LD.forms.State();
            this.warn = new LD.forms.State();
            this.state = new LD.forms.State();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(55, 2);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(121, 21);
            this.id.TabIndex = 4;
            // 
            // values
            // 
            this.values.AutoSize = true;
            this.values.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.values.ForeColor = System.Drawing.Color.Coral;
            this.values.Location = new System.Drawing.Point(4, 34);
            this.values.Name = "values";
            this.values.Size = new System.Drawing.Size(45, 17);
            this.values.TabIndex = 5;
            this.values.Text = "label1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(647, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "租";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(702, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "还";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(647, 34);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "开仓";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(702, 34);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(46, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "运维";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // addr
            // 
            this.addr.Location = new System.Drawing.Point(5, 2);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(44, 21);
            this.addr.TabIndex = 13;
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(647, 67);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(101, 21);
            this.time.TabIndex = 14;
            this.time.Text = "0A";
            // 
            // error
            // 
            this.error.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.error.Location = new System.Drawing.Point(180, 67);
            this.error.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(462, 34);
            this.error.TabIndex = 8;
            // 
            // warn
            // 
            this.warn.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warn.Location = new System.Drawing.Point(180, 34);
            this.warn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.warn.Name = "warn";
            this.warn.Size = new System.Drawing.Size(462, 34);
            this.warn.TabIndex = 7;
            // 
            // state
            // 
            this.state.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.state.Location = new System.Drawing.Point(180, 3);
            this.state.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(462, 34);
            this.state.TabIndex = 6;
            // 
            // channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.time);
            this.Controls.Add(this.addr);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.error);
            this.Controls.Add(this.warn);
            this.Controls.Add(this.state);
            this.Controls.Add(this.values);
            this.Controls.Add(this.id);
            this.Name = "channel";
            this.Size = new System.Drawing.Size(751, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label values;
        private State state;
        private State warn;
        private State error;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox addr;
        private System.Windows.Forms.TextBox time;
    }
}
