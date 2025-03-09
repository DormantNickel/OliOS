using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OliOS.System
{
    public static class ConsoleCommands
    {
        public static void RunCommand(string command)
        {
            string[] words = command.Split(' ');
            if (words.Length > 0)
            {
                if (words[0] == "info") //pokazuje informacje o systemie
                {
                    WriteMessage.WriteLogo();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(WriteMessage.CenterText("OliOS"));
                    Console.Write(WriteMessage.CenterText(Kernel.Version));
                    Console.Write(WriteMessage.CenterText("By RubiconTT"));
                }

                else if (words[0] == "shutdown") //zamyka system
                {
                    WriteMessage.WriteWarning("OliOS will shutdown in 3 seconds.");
                    Thread.Sleep(3000);
                    WriteMessage.WriteInfo("Shutting down...");
                    Thread.Sleep(1000);
                    Cosmos.System.Power.Shutdown();
                }

                else if (words[0] == "reboot") //restartuje system
                {
                    WriteMessage.WriteWarning("OliOS will reboot in 3 seconds.");
                    Thread.Sleep(3000);
                    WriteMessage.WriteInfo("Rebooting...");
                    Thread.Sleep(1000);
                    Cosmos.System.Power.Reboot();
                }

                else if (words[0] == "help") //pokazuje liste komend
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("info - shows information about the system.");
                    Console.WriteLine("shutdown - shuts down the system.");
                    Console.WriteLine("reboot - reboots the system.");
                    Console.WriteLine("help - shows all commands list.");
                    Console.WriteLine("format = formates the virtual drive.");
                    Console.WriteLine("space - shows left space on virtual drive.");
                    Console.WriteLine("dir - shows all the virtual drives files.");
                    Console.WriteLine("echo - creates a file.");
                    Console.WriteLine("cat - shows the inside of files.");
                    Console.WriteLine("delete - deletes files or directories.");
                    Console.WriteLine("mkdir - creates directories.");
                    Console.WriteLine("cd - changes the path.");
                }

                else if (words[0] == "format") //formatuje virtualny dysk
                {
                    if (Kernel.fs.Disks[0].Partitions.Count > 0)
                    {
                        Kernel.fs.Disks[0].DeletePartition(0);
                    }
                    Kernel.fs.Disks[0].Clear();
                    Kernel.fs.Disks[0].CreatePartition((int)Kernel.fs.Disks[0].Size / (1024 * 1024));
                    Kernel.fs.Disks[0].FormatPartition(0, "FAT32", true);
                    WriteMessage.WriteOk("Success!");
                    WriteMessage.WriteWarning("OliOS will reboot in 3 seconds.");
                    Thread.Sleep(3000);
                    WriteMessage.WriteInfo("Rebooting...");
                    Thread.Sleep(1000);
                    Cosmos.System.Power.Reboot();
                }

                else if (words[0] == "space") //pokazuje miejsce na dysku
                {
                    long free = Kernel.fs.GetAvailableFreeSpace(Kernel.Path);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Free Space " + free / (1024 * 1024) + "MB");
                }

                else if (words[0] == "dir") //wypisuje pliki
                {
                    var Directories = Directory.GetDirectories(Kernel.Path);
                    var Files = Directory.GetFiles(Kernel.Path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Directories (" + Directories.Length + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < Directories.Length; i++)
                    {
                        Console.WriteLine(Directories[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Files (" + Files.Length + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < Files.Length; i++)
                    {
                        Console.WriteLine(Files[i]);
                    }
                }

                else if (words[0] == "echo") //zapisuje plik
                {
                    if (words.Length > 1)
                    {
                        string wholeString = "";
                        for (int i = 1; i < words.Length; i++)
                        {
                            wholeString += words[i] + " ";
                        }
                        int pathIndex = wholeString.LastIndexOf('>');
                        string text = wholeString.Substring(0, pathIndex);
                        string path = wholeString.Substring(pathIndex + 1);
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        var file_stream = File.Create(path);
                        file_stream.Close();
                        File.WriteAllText(path, text);
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");
                }

                else if (words[0] == "cat") //pokazuje zawartosc pliku
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (File.Exists(path))
                        {
                            string text = File.ReadAllText(path);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(text);
                        }
                        else
                            WriteMessage.WriteError("File " + path + " not found!");
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");
                }
                else if (words[0] == "delete") //usuwa plik
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            WriteMessage.WriteOk("Deleted " + path + ".");
                        }
                        else
                            WriteMessage.WriteError("File " + path + " not found!");
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");
                }
                else if (words[0] == "mkdir") //tworzy folder
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        Directory.CreateDirectory(path);
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");
                }
                else if (words[0] == "cd") //zmienia scieke
                {
                    if (words.Length > 1)
                    {
                        if (words[1] == "..")
                        {
                            if (Kernel.Path != @"0:\")
                            {
                                string tempPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
                                Kernel.Path = tempPath.Substring(0, tempPath.LastIndexOf(@"\") + 1);
                                return;
                            }
                            else
                                return;
                        }
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path + @"\";
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (!path.EndsWith(@"\"))
                            path += @"\";
                        if (Directory.Exists(path))
                            Kernel.Path = path;
                        else
                        { 
                            WriteMessage.WriteError("Directory " + path + " not found!");
                        }
                    }
                    else
                    {
                        Kernel.Path = @"0:\";
                    }
                }
                    

                else if (words[0] == "bootgui") //otwiera gui
                {
                    Boot.onBoot();
                }


            }
            else
            {
                WriteMessage.WriteError("This command doesn't exist.");
            }
        }
    }
}
