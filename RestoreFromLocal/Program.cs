using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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
            }
            else
            {
                directoryPath = args[0];
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("Error: The directory path does not exist");
                    return;
                }
            }

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            configuration.Bind(ConfigurationSettings.Instance);

            Database.TruncateTable("Messages");
            Database.TruncateTable("Channels");
            Database.TruncateTable("Users");
            Database.InsertUsers(directoryPath);
            Database.InsertChannels(directoryPath);
            Database.InsertMessages(directoryPath);
        }
    }
}