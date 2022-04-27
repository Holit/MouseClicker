using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseClicker
{
    class Steps
    {

    }
    public class MouseStep
    {
        /// <summary>
        /// 用于指示二维点位置
        /// </summary>
        private struct COORD
        {
            int X;
            int Y;
            public COORD(int X, int Y) : this()
            {
                this.X = X;
                this.Y = Y;
            }
            public int Get_X()
            {
                return X;
            }
            public int Get_Y()
            {
                return Y;
            }
        }
        /// <summary>
        /// 鼠标坐标
        /// </summary>
        private COORD Pos;
        /// <summary>
        /// 鼠标原坐标
        /// </summary>
        private COORD Source_Pos;
        /// <summary>
        /// 用于指示击键的指令：
        /// -1：左键
        /// 0：中键（目前不支持）
        /// 1：右键
        /// </summary>

        private int CmdKey;
        /// <summary>
        /// 用于指示特殊击键指令
        /// 1：按下
        /// 2：抬起：
        /// 3：按下并抬起
        /// </summary>
        private int CmdKey_Extern;
        /// <summary>
        /// 初始化单一鼠标事件
        /// </summary>
        /// <param name="COORD_X">鼠标X坐标</param>
        /// <param name="COORD_Y">鼠标Y坐标</param>
        /// <param name="CmdKey">鼠标击键指令</param>
        /// <param name="CmdKey_Extern">附加指令</param>
        public MouseStep(int COORD_X,int COORD_Y,int CmdKey,int CmdKey_Extern)
        {
            Pos=new COORD(COORD_X,COORD_Y);
            this.CmdKey = CmdKey;
            this.CmdKey_Extern = CmdKey_Extern;
            Source_Pos = new COORD(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="isFlushSourcePos">指示是否在执行前刷新鼠标原来位置</param>
        public void Action(bool isFlushSourcePos)
        {
            if (isFlushSourcePos)
            {
                Source_Pos = new COORD(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            }
            WinApi.SetCursorPos(Pos.Get_X(), Pos.Get_Y());
            WinApi.BlockInput(true);
            switch (CmdKey)
            {
                case -1:
                    switch (CmdKey_Extern)
                    {
                        case 1:
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            break;
                        case 2:
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            break;
                        case 3:
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            break;
                        default:
                            WinApi.SetCursorPos(Source_Pos.Get_X(), Source_Pos.Get_Y());
                            WinApi.BlockInput(false);
                            throw new Exception("错误的鼠标指令");
                    }
                    break;
                case 0:
                    break;
                case 1:
                    switch (CmdKey_Extern)
                    {
                        case 1:
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                            break;                                                              
                        case 2:                                                                 
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            break;                                                              
                        case 3:                                                                 
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                            WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                            break;
                        default:
                            WinApi.SetCursorPos(Source_Pos.Get_X(), Source_Pos.Get_Y());
                            WinApi.BlockInput(false);
                            throw new Exception("错误的鼠标指令");
                    }
                    break;
                default:
                    WinApi.SetCursorPos(Source_Pos.Get_X(), Source_Pos.Get_Y());
                    WinApi.BlockInput(false);
                    throw new Exception("错误的鼠标指令");
            }
            WinApi.SetCursorPos(Source_Pos.Get_X(), Source_Pos.Get_Y());
            WinApi.BlockInput(false);
        }

    }
}
