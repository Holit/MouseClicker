using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
class WinApi
{

    /// <summary>
    /// 矩形类型
    /// </summary>
    public struct RECT
    {
        public int Left;                             //最左坐标
        public int Top;                             //最上坐标
        public int Right;                           //最右坐标
        public int Bottom;                        //最下坐标
    }

    /// <summary>
    /// 触发鼠标事件
    /// </summary>
    /// <param name="dwFlags">标志位集，指定点击按钮和鼠标动作的多种情况。此参数里的各位可以是下列值的任何合理组合：

    /// MOUSEEVENTF_ABSOLUTE：表明参数dX，dy含有规范化的绝对坐标。如果不设置此位，参数含有相对数据：相对于上次位置的改动位置。此标志可被设置，也可不设置，不管鼠标的类型或与系统相连的类似于鼠标的设备的类型如何。要得到关于相对鼠标动作的信息，参见下面备注部分。
    /// MOUSEEVENTF_MOVE：表明发生移动。
    /// MOUSEEVENTF_LEFTDOWN：表明接按下鼠标左键。
    /// MOUSEEVENTF_LEFTUP：表明松开鼠标左键。
    /// MOUSEEVENTF_RIGHTDOWN：表明按下鼠标右键。
    /// MOUSEEVENTF_RIGHTUP：表明松开鼠标右键。
    /// MOUSEEVENTF_MIDDLEDOWN：表明按下鼠标中键。
    /// MOUSEEVENTF_MIDDLEUP：表明松开鼠标中键。
    /// MOUSEEVENTF_WHEEL：在Windows NT中如果鼠标有一个轮，表明鼠标轮被移动。移动的数量由dwData给出。</param>
    /// <param name="dx">指定鼠标沿x轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。给出的绝对数据作为鼠标的实际X坐标；给出的相对数据作为移动的mickeys数。一个mickey表示鼠标移动的数量，表明鼠标已经移动。</param>
    /// <param name="dy">指定鼠标沿y轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。给出的绝对数据作为鼠标的实际y坐标，给出的相对数据作为移动的mickeys数。</param>
    /// <param name="dwData">如果dwFlags为MOUSEEVENTF_WHEEL，则dwData指定鼠标轮移动的数量。正值表明鼠标轮向前转动，即远离用户的方向；负值表明鼠标轮向后转动，即朝向用户。一个轮击定义为WHEEL_DELTA，即120。如果dwFlagsS不是MOUSEEVENTF_WHEEL，则dWData应为零。</param>
    /// <param name="dwExtraInfo">指定与鼠标事件相关的附加32位值。应用程序调用函数GetMessageExtraInfo来获得此附加信息。</param>
    /// <returns>无。</returns>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public const int MOUSEEVENTF_MOVE = 0x0001;//移动鼠标
    public const int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标左键按下 
    public const int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起 
    public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;//模拟鼠标右键按下 
    public const int MOUSEEVENTF_RIGHTUP = 0x0010;//模拟鼠标右键抬起 
    public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;//模拟鼠标中键按下
    public const int MOUSEEVENTF_MIDDLEUP = 0x0040;//模拟鼠标中键抬起 
    public const int MOUSEEVENTF_ABSOLUTE = 0x8000;//标示是否采用绝对坐标 
    public const int MOUSEEVENTF_WHEEL = 0x0800;//模拟鼠标滚轮滚动操作，必须配合dwData参数

    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr GetForegroundWindow();

    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
    public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);


    /// <summary>
    /// 查找窗口
    /// </summary>
    /// <param name="lpClassName"></param>
    /// <param name="lpWindowName"></param>
    /// <returns></returns>
    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    /// <summary>
    /// 获取指定窗口的设备场景
    /// </summary>
    /// <param name="hwnd">将获取其设备场景的窗口的句柄。若为0，则要获取整个屏幕的DC</param>
    /// <returns>指定窗口的设备场景句柄，出错则为0</returns>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hwnd);

    /// <summary>
    /// 释放由调用GetDC函数获取的指定设备场景
    /// </summary>
    /// <param name="hwnd">要释放的设备场景相关的窗口句柄</param>
    /// <param name="hdc">要释放的设备场景句柄</param>
    /// <returns>执行成功为1，否则为0</returns>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

    /// <summary>
    /// 在指定的设备场景中取得一个像素的RGB值
    /// </summary>
    /// <param name="hdc">一个设备场景的句柄</param>
    /// <param name="nXPos">逻辑坐标中要检查的横坐标</param>
    /// <param name="nYPos">逻辑坐标中要检查的纵坐标</param>
    /// <returns>指定点的颜色</returns>
    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
    /// <summary>
    /// 设定鼠标位置
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns></returns>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);

    /// <summary>
    /// 锁定鼠标
    /// </summary>
    /// <param name="Block">true时锁定鼠标，按下ctrl+alt+delete解锁</param>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern void BlockInput(bool Block);

    /// <summary>
    /// 获取屏幕指定像素颜色RGB
    /// </summary>
    /// <param name="x">绝对坐标X</param>
    /// <param name="y">绝对坐标Y</param>
    /// <returns>颜色</returns>
    public static Color GetColor(int x, int y)
    {
        IntPtr hdc = GetDC(IntPtr.Zero); uint pixel = GetPixel(hdc, x, y);
        ReleaseDC(IntPtr.Zero, hdc);
        Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
        return color;
    }
}