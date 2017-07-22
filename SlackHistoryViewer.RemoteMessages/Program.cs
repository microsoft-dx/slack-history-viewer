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

        class RemoteMessages
        {

            public async Task Run()
            {
                using (var dbContext = new SlackHistoryViewerDbContext())
                {

                    //Adding users
                    var usersContent = await Requests.RequestUsersList();
                    var usersData = JsonConvert.DeserializeObject<RootMembers>(usersContent);
                    Helpers.Database.InsertUsersWithoutDuplicates(usersData.Members);

                    //Adding channels
                    var channelsContent = await Requests.RequestChannelsList();
                    var channelsData = JsonConvert.DeserializeObject<RootChannels>(channelsContent);
                    Helpers.Database.InsertChannelsWithoutDuplicates(channelsData.Channels);

                    //Adding messages
                    foreach (Channel channel in channelsData.Channels)
                    {
                        var messagesContent = await Requests.RequestChannelsHistory(channel.Id);
                        var messagesData = JsonConvert.DeserializeObject<RootMessages>(messagesContent);
                        Helpers.Database.InsertMessagesWithoutDuplicates(messagesData.Messages, channel.Id);
                    }
                }
            }
        }

    }
}
