using System;
using System.IO;


namespace RestoreFromLocal
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ./program.exe directory_path");
                return;
            } else {
                directoryPath = args[0];
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("Error: The directory path does not exist");
                    return;
                }
            }
            Database.TruncateTable("Messages");
            Database.TruncateTable("Channels");
            Database.TruncateTable("Users");
            Database.InsertUsers(directoryPath);
            Database.InsertChannels(directoryPath);
            Database.InsertMessages(directoryPath);
        }
    }
}