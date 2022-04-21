using System.IO;
using System;

namespace LargeContentGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var contentFiles = "contentFiles";
            if (!Directory.Exists(contentFiles))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(contentFiles);
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    FileStream fs = new FileStream($@".\{contentFiles}\{i}.txt", FileMode.CreateNew);

            //    fs.Seek(99L * 1024 * 1024, SeekOrigin.Begin);
            //    fs.WriteByte(0);
            //    fs.Close();
            //}

            const int sizeLimitInBytes = 70 * 1024 * 1024;
            long writtenSize = 0;
            for (int i = 0; i < 5; i++)
            {
                writtenSize = 0;
                using (var stream = new FileStream($"data{i}.csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                {
                    // if the file is newly created then write the csv column names in the first line
                    if (stream.Position == 0)
                    {
                        writer.WriteLine("Name, Age, Job, Address");
                    }

                    while (true)
                    {
                        if (writtenSize > sizeLimitInBytes)
                        {
                            break;
                        }
                        writer.WriteLine($"{KeyGenerator.GetUniqueKey(10)},{KeyGenerator.GetUniqueKey(10)},{KeyGenerator.GetUniqueKey(10)},{KeyGenerator.GetUniqueKey(10)},{KeyGenerator.GetUniqueKey(10)}"); // write data seperated by commas
                        writtenSize += 54;
                    }
                }
            }
        }
    }

    public class KeyGenerator
    {
        static Random random = new Random();

        internal const string chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static string GetUniqueKey(int size)
        {
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[size];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}
