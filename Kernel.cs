using Cosmos.System.FileSystem;
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
        public static string Version = "v1.0.2"; //OliOS version
        public static string Path = @"0:\";
        public static CosmosVFS fs; //File system

        protected override void BeforeRun()
        {
            Console.SetWindowSize(90, 30);
            Console.OutputEncoding = Cosmos.System.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            fs = new Cosmos.System.FileSystem.CosmosVFS();
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            WriteMessage.WriteOk("OliOS booted successfully.");
        }

        protected override void Run()
        {
            Console.Write(Path + ">");
            var command = Console.ReadLine();
            ConsoleCommands.RunCommand(command);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
