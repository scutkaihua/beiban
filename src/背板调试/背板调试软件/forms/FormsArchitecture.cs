using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace LD.forms
{

    /// <summary>
    /// 一个功能窗口,它是一个菜单的派生
    /// </summary>
    public class FormItem:ToolStripMenuItem
    {
        public MenuStrip fatherStrip;     //菜单
        public ToolStripMenuItem secondStrip; //一级菜单名
        public Control form;              //对应控件

        public bool floatOrNot;          //浮动窗口，不显示在tabControl

        public TabPage tabPage;           //非浮动窗口，显示对应的窗口

        public FormItem(MenuStrip m, ToolStripMenuItem s, Control self,String stripName,bool _floatOrNot)
        {
            InitializeComponent();    
            
            fatherStrip = m;
            secondStrip = s;
            form = self;
            this.Text = stripName;


            if (_floatOrNot == false)
            {
                this.tabPage = new TabPage();
                tabPage.Text = Text + "   ";
                tabPage.Size = form.Size;
                tabPage.Controls.Add(form);
                tabPage.Dock = DockStyle.Fill;
                tabPage.BorderStyle = BorderStyle.FixedSingle;
                form.Dock = DockStyle.Fill;
            }

            floatOrNot = _floatOrNot;
        }

        private void InitializeComponent()
        {
            // 
            // FormItem
            // 
            this.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
    }


    /// <summary>
    /// 功能窗口管理类
    /// </summary>
    class FormsArchitecture
    {

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 主菜单
        /// </summary>
        MenuStrip menuStrip = new System.Windows.Forms.MenuStrip();
        /// <summary>
        /// 功能窗口列表
        /// </summary>
        List<FormItem> formList = new List<FormItem>();


        //窗口显示
        TabControlExt tabControl = new TabControlExt();


        /// <summary>
        /// 查找菜单号
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="stripName"></param>
        /// <returns></returns>
        public ToolStripMenuItem ToolStripSearch(ToolStripItemCollection collection,  String stripName)
        {
            if (collection == null || stripName == null) return null;
            foreach (ToolStripItem i in collection)
            {
                if (i.Text == stripName) return (ToolStripMenuItem)i;
            }

            return null;
        }

        /// <summary>
        /// 查找窗口是否存在
        /// </summary>
        /// <param name="fatherStripName"></param>
        /// <param name="stripName"></param>
        /// <returns></returns>
        FormItem SearchForm(String fatherStripName,String stripName)
        {
            foreach (FormItem item in formList)
            {
                if (item.secondStrip.Text == fatherStripName && item.Text== stripName) return item;
            }
            return null;
        }

        /// <summary>
        /// 添加功能窗口
        /// </summary>
        /// <param name="fatherStripName">父菜单名</param>
        /// <param name="subject"> 功能窗口</param>
        /// <param name="stripName">菜单名称</param>
        /// <returns>true  or false</returns>
        public bool AddForm(Form subject,String fatherStripName, String stripName,bool floatOrNot)
        {
            //查找对应的菜单
            FormItem item = SearchForm(fatherStripName, stripName);

            if (item == null)
            {

                ///主菜单不存在
                ToolStripMenuItem f = ToolStripSearch(menuStrip.Items,fatherStripName);
                if(f==null){
                    f = new ToolStripMenuItem(fatherStripName);
                    menuStrip.Items.Add(f);
                }

                //添加子菜单
                if(floatOrNot == false)
                    subject.TopLevel = false;
                //名称
                subject.Text = stripName;

                item = new FormItem(menuStrip, f,subject,stripName,floatOrNot);
                f.DropDownItems.Add(item);

                item.Click += item_Click;
                
                //添加对列表
                formList.Add(item);

                return true;
            }

            return true;
            
        }

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, EventArgs e)
        {
            FormItem item  = (FormItem)sender;
            if (item.floatOrNot == true)
            {
                item.form.Show();
            }
            else
            {
                if (this.tabControl.Contains(item.form) == false)
                {
                    this.tabControl.Controls.Add(item.tabPage);
                }
                this.tabControl.SelectedTab = item.tabPage;
                item.form.Show();
            }
        }




#region 构造
        public  FormsArchitecture (Form parent)
        {

            parent.SuspendLayout();

            parent.Controls.Add(this.menuStrip);
            parent.MainMenuStrip = this.menuStrip;
            this.tabControl.Location = new Point(menuStrip.Location.X , menuStrip.Location.Y + menuStrip.Height);
            this.tabControl.Anchor = AnchorStyles.Top|AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.tabControl.Height = parent.ClientSize.Height - menuStrip.Size.Height;
            this.tabControl.Width = parent.ClientSize.Width ;

            parent.Controls.Add(this.tabControl);

            parent.ResumeLayout();
        }
#endregion


    }
}
