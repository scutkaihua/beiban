using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using LD;
using LD.forms;


namespace LD
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 关于
        /// </summary>
        About about = new About();

        //////////////////////////////////////////////////////////////////////////
        //串口
        SerialPortSetting serialPortSetting = new SerialPortSetting();
        //设置
        //DeviceSetting deviceSetting = new DeviceSetting();

        //usb


        //////////////////////////////////////////////////////////////////////////
        //读写
        Function function;

        //发行用户卡
        //UserCardIssuerForm userCardIssuer;

        //充值
       // CardRecharge cardRecharge;


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //数据据包显示
        PacketView packetView = new PacketView();


        //窗口管理
        FormsArchitecture formsArchitecture;



        public Form1()
        {
            InitializeComponent();



            formsArchitecture = new FormsArchitecture(this);

            //userCardIssuer = new UserCardIssuerForm(deviceSetting.Cos());
            //cardRecharge = new CardRecharge(deviceSetting.Cos());

            //////////////////////////////////////////////////////////////////////////
            //设置
           // formsArchitecture.AddForm(deviceSetting,"设置","读卡器设置",false);
            formsArchitecture.AddForm(serialPortSetting, "设置", "通信串口设置", false);


            //////////////////////////////////////////////////////////////////////////
            ///功能
            function = new Function(serialPortSetting);
            formsArchitecture.AddForm(function, "功能选择", "测试1", true);
            function.pv = packetView;

            /////////////////////////////////////////////////////////////////////////
            ///显示
            formsArchitecture.AddForm(packetView, "显示", "串口数据",true);
            

            //////////////////////////////////////////////////////////////////////////
            ///卡操作
           // formsArchitecture.AddForm(cardRecharge,"卡操作","充值",false);
           // formsArchitecture.AddForm(userCardIssuer,"卡操作","发行用户卡",false);


            //////////////////////////////////////////////////////////////////////////
            ///帮助关于
            formsArchitecture.AddForm(about, "关于", "关于",true);

        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            System.Environment.Exit(0);
        }


    }
}
