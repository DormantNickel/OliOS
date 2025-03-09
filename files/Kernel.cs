using Cosmos.Core.Memory;
using Cosmos.System.FileSystem;
using OliOS.Graphics;
using OliOS.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;

namespace OliOS
{
    public class Kernel : Sys.Kernel
    {
        public static string Version = "v1.2.0"; //OliOS version
        public static string Path = @"0:\";
        public static CosmosVFS fs; //File system
        public static bool RunGui;
        public static bool BootWithGui = true;
        int lastHeapCollect;

        protected override void BeforeRun()
        {
            Console.SetWindowSize(90, 30);
            Console.OutputEncoding = Cosmos.System.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            fs = new Cosmos.System.FileSystem.CosmosVFS();
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            if (BootWithGui == true)
            {
                WriteMessage.WriteOk("OliOS booted successfully.");
                WriteMessage.WriteInfo("Opening GUI...");
                Thread.Sleep(1500);
                Boot.onBoot();
            }
            else
            {
                WriteMessage.WriteOk("OliOS booted successfully.");
            }
            
        }

        protected override void Run()
        {
            if (!RunGui)
            {
                Console.Write(Path + ">");
                var command = Console.ReadLine();
                ConsoleCommands.RunCommand(command);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                GUI.Update();
            }
            if (lastHeapCollect >= 20)
            {
                Heap.Collect();
                lastHeapCollect = 0;
            }
            else
            {
                lastHeapCollect++;
            }
        }
    }
}
