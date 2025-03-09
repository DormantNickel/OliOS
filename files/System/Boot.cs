using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using OliOS.Graphics;

namespace OliOS.System
{
    public static class Boot
    {
        public static void onBoot()
        {
            Kernel.RunGui = true;
            GUI.Wallpaper = new Bitmap(Assets.Files.OliOSBackgroundRaw);
            GUI.Cursor = new Bitmap(Assets.Files.OliOSCursorRaw);
            GUI.StartGUI();
        }
    }
}

