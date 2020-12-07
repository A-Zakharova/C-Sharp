using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace l1
{
    class MyFile
    {
        static public void OpenAndRead(string path)
        {
            using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Text from file: \n{textFromFile}");
            }
        }
        static public void Create(string path)
        {
            try
            {
                Console.WriteLine("Enter the name of file");
                string fileName = Console.ReadLine();
                fileName = Path.Combine(path, fileName);
                FileInfo file = new FileInfo(fileName);
                file.Create();
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
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    //Если создать файл, а потом удалить, то исключение
                    file.Delete();
                    Console.WriteLine("File deleted");
                }
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
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    Console.WriteLine($"\nName:{file.Name}");
                    Console.WriteLine($"Creation date:{file.CreationTime}");
                    Console.WriteLine($"Size:{file.Length}");
                    Console.WriteLine($"Expansion:{file.Extension}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static public void Writer(string path)
        {
            try
            {
                Console.WriteLine("\nEnte file name for recording:");
                string name = Console.ReadLine();
                name = Path.Combine(path, name);
                Console.WriteLine("\nEnter text:");
                string text = Console.ReadLine();

                //Create only writer
                using (FileStream fstream = new FileStream(name, FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(text);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Text written to file");
                }
                Folder.Open(path);
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
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.MoveTo(newpath);
                    Console.WriteLine("File renamed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}