﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SlackHistoryViewer.Configuration;

namespace SlackHistoryViewer.RestoreFromLocal
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ./RestoreFromLocal.exe directory_path");

                return;
            }

            var directoryPath = args[0];

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Error: The directory path does not exist");

                return;
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            configuration.Bind(AppSettings.Instance);

            new RestoreFromLocal().Run(directoryPath);
        }
    }
}