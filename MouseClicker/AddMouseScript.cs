using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseClicker
{
    public partial class AddMouseScript : Form
    {
        /// <summary>
        /// 用于指示是否已经激活鼠标捕获器
        /// </summary>
        bool isMouseTrapEnabled = false;

        /// <summary>
        /// 用于指示是否已经激活键盘捕获器
        /// </summary>
        bool isKeyTrapEnabled = false;

        public AddMouseScript()
        {
            InitializeComponent();
        }

        private void AddMouseScript_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isKeyTrapEnabled == false)
            {
                if (isMouseTrapEnabled == false)
                {
                    isMouseTrapEnabled = true;
                    button2.BackColor = Color.Red;
                    button2.Text = "关闭捕获器";
                    //开始捕获HOOK事件，下同
                }
                else
                {
                    isMouseTrapEnabled = false;
                    button2.BackColor = Color.Green;
                    button2.Text = "激活捕获器";
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isMouseTrapEnabled == false)
            {
                if (isKeyTrapEnabled == false)
                {
                    isKeyTrapEnabled = true;
                    button5.BackColor = Color.Red;
                    button2.Text = "关闭捕获器";
                }
                else
                {
                    isKeyTrapEnabled = false;
                    button2.BackColor = Color.Green;
                    button2.Text = "激活捕获器";
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown3.Enabled = true;
            }
            else
            {
                numericUpDown3.Value = 1;
                numericUpDown3.Enabled = false;
            }
        }
    }
}
