using OliOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliOS.System
{
    public static class CustomDrawing
    {
        public static void DrawFullRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
        {
            GUI.mainCanvas.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
            GUI.mainCanvas.DrawFilledRectangle(col, x, y + radius, radius, height - 2 * radius);
            GUI.mainCanvas.DrawFilledRectangle(col, x + width - radius, y + radius, radius, height - 2 * radius);
            GUI.mainCanvas.DrawFilledCircle(col, x + radius, y + radius, radius);
            GUI.mainCanvas.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
            GUI.mainCanvas.DrawFilledCircle(col, x + radius, y + height - radius - 1, radius);
            GUI.mainCanvas.DrawFilledCircle(col, x + width - radius - 1, y + height - radius - 1, radius);
        }
        public static void DrawTopRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
        {
            GUI.mainCanvas.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
            GUI.mainCanvas.DrawFilledRectangle(col, x, y + radius, width, height - radius);
            GUI.mainCanvas.DrawFilledCircle(col, x + radius, y + radius, radius);
            GUI.mainCanvas.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
        }
    }
}
