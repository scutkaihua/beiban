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
            this.tv_addr = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.values = new System.Windows.Forms.Label();
            this.state = new LD.forms.State();
            this.warn = new LD.forms.State();
            this.error = new LD.forms.State();
            this.SuspendLayout();
            // 
            // tv_addr
            // 
            this.tv_addr.AutoSize = true;
            this.tv_addr.Location = new System.Drawing.Point(3, 6);
            this.tv_addr.Name = "tv_addr";
            this.tv_addr.Size = new System.Drawing.Size(41, 12);
            this.tv_addr.TabIndex = 0;
            this.tv_addr.Text = "label1";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(64, 3);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(121, 21);
            this.id.TabIndex = 4;
            // 
            // values
            // 
            this.values.AutoSize = true;
            this.values.Location = new System.Drawing.Point(3, 36);
            this.values.Name = "values";
            this.values.Size = new System.Drawing.Size(41, 12);
            this.values.TabIndex = 5;
            this.values.Text = "label1";
            // 
            // state
            // 
            this.state.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.state.Location = new System.Drawing.Point(210, 3);
            this.state.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(485, 34);
            this.state.TabIndex = 6;
            // 
            // warn
            // 
            this.warn.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.warn.Location = new System.Drawing.Point(210, 34);
            this.warn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.warn.Name = "warn";
            this.warn.Size = new System.Drawing.Size(474, 34);
            this.warn.TabIndex = 7;
            // 
            // error
            // 
            this.error.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.error.Location = new System.Drawing.Point(210, 67);
            this.error.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(474, 34);
            this.error.TabIndex = 8;
            // 
            // channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.error);
            this.Controls.Add(this.warn);
            this.Controls.Add(this.state);
            this.Controls.Add(this.values);
            this.Controls.Add(this.id);
            this.Controls.Add(this.tv_addr);
            this.Name = "channel";
            this.Size = new System.Drawing.Size(681, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tv_addr;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label values;
        private State state;
        private State warn;
        private State error;
    }
}
