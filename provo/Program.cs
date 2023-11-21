using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Provodnik
{
    internal class ArrowMenu
    {

        private static int minarrow;
        private static int maxarrow;
        public ArrowMenu(int min, int max)
        {
            minarrow = min;
            maxarrow = max;
        }
        public int Arrow()
        {
            int position = 2;
            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0, position);
                Console.WriteLine("->");

                key = Console.ReadKey();

                Console.SetCursorPosition(0, position);
                Console.WriteLine("  ");

                if (key.Key == ConsoleKey.UpArrow && position != minarrow)
                {
                    position--;
                }
                else if (key.Key == ConsoleKey.DownArrow && position != maxarrow)
                {
                    position++;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    position = -1;
                    break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return position;


        }



    }
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Выберите диск");
                Console.WriteLine("---------------");
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    Console.WriteLine("  " + drive.Name + "\t" + drive.TotalSize / 1073741824 + " GB(объем)");
                }
                ArrowMenu menu = new ArrowMenu(2, 3);
                int pos = menu.Arrow();
                string[] l = new string[1] { " " };
                string[] paths = l;
                string[] files = l;
                if (pos == 2)
                {
                    ShowFolder("C:\\", paths, files);


                }
                else if (pos == 3)
                {
                    ShowFolder("D:\\", paths, files);

                }
                else if (pos == -1)
                {
                    break;
                }
            }
        }

        static void ShowFolder(string s, string[] paths, string[] files)
        {

            while (true)
            {

                Console.Clear();


                if (Directory.Exists(s))
                {
                    paths = Directory.GetDirectories(s);
                    files = Directory.GetFiles(s);
                }
                string[] pf = paths.Concat(files).ToArray();

                foreach (string path in pf)
                {
                    DateTime pop = Directory.GetCreationTime(path);
                    Console.WriteLine("  " + path + "\t\tДата и время создания:\t" + pop);

                }


                ArrowMenu a = new ArrowMenu(0, pf.Length - 1);
                int position = a.Arrow();
                if (position == -1)
                {
                    return;
                }

                if (File.Exists(pf[position]))
                {
                    Process.Start(new ProcessStartInfo { FileName = pf[position], UseShellExecute = true });
                }
                ShowFolder(pf[position], paths, files);
            }
        }



    }
}
