using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseClicker
{

    public partial class Form1 : Form
    {
        
        KeyboardHook keyboardHook;
        /// <summary>
        /// 确定鼠标的操作步骤
        /// </summary>
        MouseStep[] MouseSteps;
        /// <summary>
        /// 确定鼠标操作顺序，用于显示，不用于定位
        /// </summary>
        int StepIndex = 1;
        MouseHook mouseHook;
        /// <summary>
        /// 用于传递全局消息：哪个键被按下
        /// </summary>
        int CmdKeyPressed = 1;
        /// <summary>
        /// 用于指示第几条消息
        /// </summary>
        int InfoIndex = 1;

        private void AddListView(string key,string description)
        {
            ListViewItem item = new ListViewItem();     
            item.Text = key;                            
            item.SubItems.Add(description);             
            listView1.Items.Add(item);                  
        }
        /// <summary>
        /// 添加消息到滚动文本框中
        /// </summary>
        /// <param name="info">欲添加的消息</param>
        private void AddInfo(string info)
        {
            textBox1.Text=textBox1.Text + "\r\n["+ DateTime.Now.ToLongTimeString().ToString()+"]#" + InfoIndex+" " + info;
            InfoIndex++;
            textBox1.SelectionStart = textBox1.Text.Length; //Set the current caret position at the end
            textBox1.ScrollToCaret(); //Now scroll it automatically
        }
        private void hook_KeyPressEvent(object sender, KeyPressEventArgs e)
        {

        }
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            CmdKeyPressed = e.KeyValue;
            //判断按下的键
            //Alt + A 左键
            if (e.KeyValue == (int)Keys.A && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                AddInfo("按下左键");
                WinApi.BlockInput(true);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                WinApi.BlockInput(false);
            }
            //Alt + S 右键
            if (e.KeyValue == (int)Keys.S && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                AddInfo("按下右键");
                WinApi.BlockInput(true);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                WinApi.BlockInput(false);
            }
        }
        //private void hook_Mouse_Event(object sender, MouseEventArgs e) 
        //{
        //    if (CmdKeyPressed == (int)Keys.NumPad1 && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
        //    {
        //        /// <summary>
        //        /// 用于指示击键的指令：
        //        /// -1：左键
        //        /// 0：中键（目前不支持）
        //        /// 1：右键
        //        /// </summary>
        //        int CmdKey = 0;
        //        if (e.Button == MouseButtons.Left)
        //        {
        //            CmdKey = -1;
        //        }
        //        else if(e.Button == MouseButtons.Right)
        //        {
        //            CmdKey = 1;
        //        }
        //        else if(e.Button == MouseButtons.Middle)
        //        {
        //            CmdKey = 0;
        //        }
        //        else if(e.Button == MouseButtons.None)
        //        {
        //            //do nothing
        //        }
        //        else
        //        {
        //            throw new Exception("错误的鼠标击键参数");
        //        }
        //
        //        /// <summary>
        //        /// 用于指示特殊击键指令
        //        /// 1：按下
        //        /// 2：抬起：
        //        /// 3：按下并抬起
        //        int Externs = 0;
        //
        //        if (e.Clicks == 1)
        //        {
        //
        //        }
        //        MouseSteps[0] = new MouseStep(e.X, e.Y, CmdKey, e.Clicks);
        //    }
        //}

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddInfo("加载窗体");
            keyboardHook = new KeyboardHook();
            //mouseHook = new MouseHook();
            //mouseHook.OnMouseActivity += new System.Windows.Forms.MouseEventHandler(hook_Mouse_Event);
            keyboardHook.KeyDownEvent += new System.Windows.Forms.KeyEventHandler(hook_KeyDown);
            keyboardHook.KeyPressEvent += hook_KeyPressEvent;
            try
            {
                keyboardHook.Start();//安装键盘钩子
                //mouseHook.Start();选择时间安装鼠标钩子
                AddInfo("加载钩子已成功");
            }
            catch (Exception)
            {
                AddInfo("加载钩子失败");
                throw;
            }
            AddListView("Alt+A", "模拟一次 左 键按下并抬起");
            AddListView("Alt+S", "模拟一次 右 键按下并抬起");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardHook.Stop();//结束键盘钩子
            //mouseHook.Stop();//结束鼠标钩子
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jerry Kits 组件\n由Jerry个人开发\n联系:gao1021229073@163.com");
        }
    }
}
