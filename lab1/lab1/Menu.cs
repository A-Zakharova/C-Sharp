using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace l1
{
    class Menu
    {
        static public void MainMenu()
        {
            Console.WriteLine("Press Enter to start");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            while (true)
            {

                Console.Clear();
                Folder.PrintingDrive();
                MenuWithOpenDisk(Folder.OpenDisk());
            }

        }

        static void MenuWithOpenDisk(string path)
        {
            bool isNotBack = true;
            while (isNotBack == true)
            {
                Console.WriteLine("\n\nWhat's next?");
                Console.WriteLine("0-Return");
                Console.WriteLine("1-Open");
                Console.WriteLine("2-Create");
                Console.WriteLine("3-Delete");
                Console.WriteLine("4-Rename");
                Console.WriteLine("5-Get Info");
                Console.WriteLine("6-Write to file");
                Console.WriteLine("7-Zip");
                Console.WriteLine("8-Unzip");
                int choise;
                while (!int.TryParse(Console.ReadLine(), out choise))
                {
                    Console.WriteLine("Input Error! Please enter number");
                }
                switch (choise)
                {
                    case 0: isNotBack = false; break;
                    case 1: path = Open(path); break;
                    case 2: Create(path); break;
                    case 3: Delete(path); break;
                    case 4: Rename(path); break;
                    case 5: Info(path); break;
                    case 6: MyFile.Writer(path); break;
                    case 7: Archivate(path); break;
                    case 8: Decompress(path); break;
                    default: break;
                }
            }
        }
        static public string Open(string path)
        {
            string name = Input(path);
            FileInfo file = new FileInfo(name);
            if (file.Exists == true)
            {
                MyFile.OpenAndRead(name);
                return path;
            }
            if (Directory.Exists(name))
            {
                Folder.Open(name);
                return name;
            }
            else
            {
                Console.WriteLine("No such name");
                Console.WriteLine("Press Enter to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                return path;
            }
        }
        static public void Create(string path)
        {
            Console.WriteLine("What you want to create?");
            Console.WriteLine("1-folder");
            Console.WriteLine("2-file");
            int choise;
            while (!int.TryParse(Console.ReadLine(), out choise))
            {
                Console.WriteLine("Input Error! Please enter number");
            }
            switch (choise)
            {
                case 1: Folder.Create(path); break;
                case 2: MyFile.Create(path); break;
                default: break;
            }
            Folder.Open(path);
        }
        static public void Delete(string path)
        {
            string name = Input(path);
            FileInfo file = new FileInfo(name);
            if (file.Exists == true)
            {
                MyFile.Delete(name);

            }
            if (Directory.Exists(name))
            {
                Folder.Delete(name);
            }
            Folder.Open(path);
        }
        static private string Input(string path)
        {
            Console.WriteLine("\nEnter the name:");
            string name = Console.ReadLine();
            name = Path.Combine(path, name);
            return name;
        }
        static public void InputError()
        {
            Console.WriteLine("\nPossible mistakes:");
            Console.WriteLine(@"-Missing :\");
            Console.WriteLine("-Wrong inpur language");
            Console.WriteLine("-Wrong name\n");
        }
        static public void Info(string path)
        {
            string name = Input(path);
            FileInfo file = new FileInfo(name);
            if (file.Exists == true)
            {
                MyFile.Info(name);

            }
            if (Directory.Exists(name))
            {
                Folder.Info(name);
            }
            Folder.Open(path);
        }

        static public void Rename(string path)
        {
            string name1 = Input(path);

            Console.WriteLine("\nEnter a new name:");
            string name2 = Console.ReadLine();
            name2 = Path.Combine(path, name2);
            FileInfo file = new FileInfo(name1);
            if (file.Exists == true)
            {
                MyFile.Rename(name1, name2);

            }
            if (Directory.Exists(name1))
            {
                Folder.Rename(name1, name2);
            }
            Folder.Open(path);
        }

        static public void Archivate(string path)
        {
            Console.WriteLine("\nEnter the name:");
            string nameFile = Console.ReadLine();
            string fileWithout = Cut(nameFile);
            nameFile = Path.Combine(path, nameFile);
            string newPathCompression = Path.Combine(path, fileWithout + ".gz");
            Console.WriteLine(newPathCompression);

            using (FileStream source = new FileStream(nameFile, FileMode.OpenOrCreate))
            {
                using (FileStream target = File.Create(newPathCompression))
                {
                    using (GZipStream compression = new GZipStream(target, CompressionMode.Compress))
                    {
                        source.CopyTo(compression);
                        Console.WriteLine("Compression was successful\nInital size-{0}\nFinal size-{1}", source.Length.ToString(), target.Length.ToString());
                    }

                }
            }
            Folder.Open(path);
        }

        static public void Decompress(string path)
        {
            Console.WriteLine("\nEnter the name:");
            string nameFile = Console.ReadLine();
            string fileWithout = Cut(nameFile);

            Console.WriteLine("\nEnter extension, for example .txt:");
            string ras = Console.ReadLine();
            fileWithout += "_new" + ras;

            string compress = Path.Combine(path, nameFile);
            string decompress = Path.Combine(path, fileWithout);
            using (FileStream sourceStream = new FileStream(compress, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(decompress))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
            Folder.Open(path);
        }

        static private string Cut(string nameFile)
        {
            string newName = "";
            int flag = 0;
            for (int i = 0; i < nameFile.Length; i++)
            {
                if (nameFile[i] != '.' && flag == 0)
                    newName += nameFile[i];
                else if (nameFile[i] == '.' || flag == 1)
                {
                    flag = 1;
                    break;
                }
            }
            return newName;
        }
    }
}