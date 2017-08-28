using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SlackHistoryViewer.Configuration;

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
