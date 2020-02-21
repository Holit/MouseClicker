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
        private int Save_Mouse_X;
        private int Save_Mouse_Y;
        private int Backup_Mouse_X;
        private int Backup_Mouse_Y;
        private void AddListView(string key,string description)
        {
            ListViewItem item = new ListViewItem();      //先实例化ListViewItem这个类
            item.Text = key;                             //添加第1列内容,注意是"Text"
            item.SubItems.Add(description);                      //添加第2列内容
            listView1.Items.Add(item);                   //集体添加进去
        }
        private void AddInfo(string info)
        {
            textBox1.Text=textBox1.Text + "\r\n["+ DateTime.Now.ToLongTimeString().ToString()+"] " + info; 
            textBox1.SelectionStart = textBox1.Text.Length; //Set the current caret position at the end
            textBox1.ScrollToCaret(); //Now scroll it automatically
        }
        private void hook_KeyPressEvent(object sender, KeyPressEventArgs e)
        {

        }
        private void hook_KeyDown(object sender, KeyEventArgs e)
        {

            //判断按下的键(Alt + A)启动鼠标连点器
            if (e.KeyValue == (int)Keys.A && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                WinApi.BlockInput(true);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
              WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                WinApi.BlockInput(false);
            }

            if (e.KeyValue == (int)Keys.S && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                WinApi.BlockInput(true);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                WinApi.BlockInput(false);
            }

            if (e.KeyValue == (int)Keys.F1 && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                Save_Mouse_X = System.Windows.Forms.Cursor.Position.X;
                Save_Mouse_Y = System.Windows.Forms.Cursor.Position.Y;
                AddInfo("已记录鼠标位置 (" + Save_Mouse_X + "," + Save_Mouse_Y + ")");
            }
            if (e.KeyValue == (int)Keys.F2 && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                if (Save_Mouse_X == 0 || Save_Mouse_Y == 0)
                {
                    MessageBox.Show("无可用鼠标位置，按下Alt+F1记录");
                    return;
                }
                Backup_Mouse_X = System.Windows.Forms.Cursor.Position.X;
                Backup_Mouse_Y = System.Windows.Forms.Cursor.Position.Y;
                WinApi.BlockInput(true);
                WinApi.SetCursorPos(Save_Mouse_X, Save_Mouse_Y);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                WinApi.SetCursorPos(Backup_Mouse_X, Backup_Mouse_Y);
                WinApi.BlockInput(false);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyDownEvent += new System.Windows.Forms.KeyEventHandler(hook_KeyDown);
            keyboardHook.KeyPressEvent += hook_KeyPressEvent;
            try
            {
                keyboardHook.Start();//安装键盘钩子
            }
            catch (Exception)
            {
                throw;
            }
            AddListView("Alt+R", "模拟一次 左 键按下并抬起");
            AddListView("Alt+S", "模拟一次 右 键按下并抬起");
            AddListView("Alt+F1", "记录当前鼠标位置并设置点击快捷方式");
            AddListView("Alt+F2", "按下快捷方式按键");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardHook.Stop();//结束键盘钩子
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
