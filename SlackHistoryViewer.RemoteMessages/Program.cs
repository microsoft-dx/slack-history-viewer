using System;
using System.Threading.Tasks;
using SlackHistoryViewer.Database;
using Newtonsoft.Json;
using SlackHistoryViewer.Slack.Models;
using RemoteMessages;
using SlackHistoryViewer.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SlackHistoryViewer.RemoteMessages
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            configuration.Bind(AppSettings.Instance);

            Task.Run(() => new RemoteMessages().Run())
                .Wait();

        }
    }
}
