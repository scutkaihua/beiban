namespace LD.forms
{
    partial class PacketParse
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.解析 = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1140, 99);
            this.textBox1.TabIndex = 0;
            // 
            // 解析
            // 
            this.解析.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.解析.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.解析.Location = new System.Drawing.Point(0, 330);
            this.解析.Name = "解析";
            this.解析.Size = new System.Drawing.Size(1164, 40);
            this.解析.TabIndex = 1;
            this.解析.Text = "解析";
            this.解析.UseVisualStyleBackColor = true;
            this.解析.Click += new System.EventHandler(this.解析_Click);
            // 
            // result
            // 
            this.result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.result.Location = new System.Drawing.Point(12, 137);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(1140, 187);
            this.result.TabIndex = 2;
            this.result.Text = "";
            // 
            // PacketParse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1164, 370);
            this.Controls.Add(this.result);
            this.Controls.Add(this.解析);
            this.Controls.Add(this.textBox1);
            this.MinimizeBox = false;
            this.Name = "PacketParse";
            this.Text = "数据包解析";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button 解析;
        private System.Windows.Forms.RichTextBox result;
    }
}