using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace l1
{
    class Folder
    {
        static public void PrintingDrive()
        {
            try
            {
                Console.Clear();
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    Console.WriteLine($"Disc name:{drive.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public string OpenDisk()
        {
            Console.WriteLine(@"Enter disc name. For example D:\");
            string disk = Console.ReadLine();
            string proverka = @":\";
            if (disk.Contains(proverka) == false)
            {
                Menu.InputError();
                OpenDisk();
            }
            Open(disk);
            return disk;
        }

        static public void Open(string path)
        {
            try
            {
                Console.WriteLine("\nSubdirectories:");
                string[] directories = Directory.GetDirectories(path);
                foreach (string directori in directories)
                {
                    Console.WriteLine(directori);
                }
                Console.WriteLine();
                Console.WriteLine("Files:");
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void Create(string path)
        {
            try
            {
                Console.WriteLine("Enter the name of folder:");
                string subpath = Console.ReadLine();
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                dirInfo.CreateSubdirectory(subpath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void Info(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                Console.WriteLine($"\nName : {dir.Name}");
                Console.WriteLine($"Full name: {dir.FullName}");
                Console.WriteLine($"Creation time: {dir.CreationTime}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void Delete(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    dir.Delete(true);
                    Console.WriteLine("Folder deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static public void Rename(string path, string newpath)
        {
            try
            {
                DirectoryInfo folder = new DirectoryInfo(path);
                if (folder.Exists)
                {
                    folder.MoveTo(newpath);
                    Console.WriteLine("Folder renamed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}