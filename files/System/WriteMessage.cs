using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliOS.System
{
    public static class WriteMessage
    {
        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERR] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
        }
        public static void WriteWarning(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARN] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(warning);
        }
        public static void WriteOk(string ok)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[OK] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ok);
        }
        public static void WriteInfo(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[INFO] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(info);
        }

        public static void WriteLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(CenterText(" ________  ___       ___  ________  ________      "));
            Console.Write(CenterText("|\\   __  \\|\\  \\     |\\  \\|\\   __  \\|\\   ____\\     "));
            Console.Write(CenterText("\\ \\  \\|\\  \\ \\  \\    \\ \\  \\ \\  \\|\\  \\ \\  \\___|_    "));
            Console.Write(CenterText(" \\ \\  \\\\\\  \\ \\  \\    \\ \\  \\ \\  \\\\\\  \\ \\_____  \\   "));
            Console.Write(CenterText("  \\ \\  \\\\\\  \\ \\  \\____\\ \\  \\ \\  \\\\\\  \\|____|\\  \\  "));
            Console.Write(CenterText("   \\ \\_______\\ \\_______\\ \\__\\ \\_______\\____\\_\\  \\ "));
            Console.Write(CenterText("    \\|_______|\\|_______|\\|__|\\|_______|\\_________\\"));
            Console.Write(CenterText("                                      \\|_________|"));
            Console.Write(CenterText("                                                  "));
        }

        public static string CenterText(string text)
        {
            int consoleWidth = 90;
            int padding = (consoleWidth - text.Length) / 2;
            string centeredText = text.PadLeft(padding + text.Length).PadRight(consoleWidth);
            return centeredText;
        }
    }
}
