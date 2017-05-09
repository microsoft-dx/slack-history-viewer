using RestoreFromLocal.Models;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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

            Data database = new Data();
            database.truncateAllTables();
            database.insertUsers(directoryPath);
            database.insertChannels(directoryPath);
            database.insertMessages(directoryPath);
        }
    }
}