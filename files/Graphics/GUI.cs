using OliOS.Apps;
using OliOS.System;
using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using Cosmos.Core.IOGroup;

namespace OliOS.Graphics
{
    public static class GUI
    {
        public static int screenSizeX = 1920, screenSizeY = 1080;
        public static SVGAIICanvas mainCanvas;
        public static Bitmap Wallpaper, Cursor;
        public static Colors colors = new Colors();
        public static bool Clicked;
        public static Process currentProcess;
        public static int MX, MY;
        static int oldX, oldY;
        public static PCScreenFont FontDefault = PCScreenFont.Default;
        public static void Update()
        {
            MX = (int)MouseManager.X;
            MY = (int)MouseManager.Y;
            mainCanvas.DrawImage(Wallpaper, 0, 0);
            Move();
            ProcessManager.Update();
            mainCanvas.DrawImageAlpha(Cursor, (int)MouseManager.X, (int)MouseManager.Y);
            if(MouseManager.MouseState == MouseState.Left)
            {
                Clicked = true;
            }
            else if(MouseManager.MouseState == MouseState.None && Clicked)
            {
                Clicked = false;
                currentProcess = null;
            }
            mainCanvas.Display();
        }
        public static void Move()
        {
            if (currentProcess != null)
            {
                currentProcess.WindowData.WinPos.X = (int)MouseManager.X - oldX;
                currentProcess.WindowData.WinPos.Y = (int)MouseManager.Y - oldY;
            }
            if (MouseManager.MouseState == MouseState.Left && !Clicked)
            {
                foreach (var proc in ProcessManager.ProcessList)
                {
                    if (!proc.WindowData.MoveAble)
                    {
                        continue;
                    }
                    if (MX > proc.WindowData.WinPos.X && MX < proc.WindowData.WinPos.X + proc.WindowData.WinPos.Width)
                    {
                        if(MY > proc.WindowData.WinPos.Y && MY < proc.WindowData.WinPos.Y + Window.TopSize)
                        {
                            currentProcess = proc;
                            oldX = MX - proc.WindowData.WinPos.X;
                            oldY = MY - proc.WindowData.WinPos.Y;
                        }
                    }
                }
            }
        }
        public static void StartGUI()
        {
            mainCanvas = new SVGAIICanvas(new Mode((uint)screenSizeX, (uint)screenSizeY, ColorDepth.ColorDepth32));
            MouseManager.ScreenWidth = (uint)screenSizeX;
            MouseManager.ScreenHeight = (uint)screenSizeY;
            MouseManager.X = (uint)screenSizeX / 2;
            MouseManager.Y = (uint)screenSizeY / 2;
            ProcessManager.Start(new MessageBox {WindowData = new WindowData {WinPos = new Rectangle(100,100,350,200)}, Name = "Okienko Testowe.exe"});
        }
    }
}
