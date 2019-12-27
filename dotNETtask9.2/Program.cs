using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNETtask9._2
{
    class Program
    {
        static string folderToWatch = "";
        static string logFile = "";
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Для использования программы необходимо ввести в качестве " +
                    "аргументов командной строки путь к папке и имя файла для логирования!");
                Console.ReadLine();
                return;
            }
            folderToWatch = args[0];
            logFile = args[1];
            using (StreamWriter sw = File.CreateText(logFile))
            {
                string s1 = "Date,             Time:             Name:               Change Type:";
                string s2 = "————————————————————————————————————————————————————————————————————";
                sw.WriteLine(s1);
                sw.WriteLine(s2);
                Console.WriteLine(s1);
                Console.WriteLine(s2);
            }
            
            FileSystemWatcher watcher = new FileSystemWatcher();
            try
            {
                watcher.Path = folderToWatch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName |
                                   NotifyFilters.LastWrite | NotifyFilters.LastAccess;

            watcher.Filter = "*.cs";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;

            
            Console.ReadLine();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string s =
                $"{DateTime.Now.ToShortDateString()}\t{DateTime.Now.ToShortTimeString()}\t{e.Name}\t\t{e.ChangeType}";
            Console.WriteLine(s);
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(s);
            }
        }
    }
}
