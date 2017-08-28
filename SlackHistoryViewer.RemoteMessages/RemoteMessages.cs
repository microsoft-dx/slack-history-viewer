using System.Threading.Tasks;
using Newtonsoft.Json;
using RemoteMessages;
using SlackHistoryViewer.Slack.Models;

namespace SlackHistoryViewer.RemoteMessages
{
    internal class RemoteMessages
    {
        public async Task Run()
        {
            await AddUsers();
            await AddChannels();
            await AddMessages();
        }

        private async Task AddUsers()
        {
            var usersContent = await Requests.RequestUsersList();
            var usersData = JsonConvert.DeserializeObject<RootMembers>(usersContent);

            Database.InsertUsersWithoutDuplicates(usersData.Members);
        }

        private async Task AddChannels()
        {
            var channelsContent = await Requests.RequestChannelsList();
            var channelsData = JsonConvert.DeserializeObject<RootChannels>(channelsContent);

            Database.InsertChannelsWithoutDuplicates(channelsData.Channels);
        }

        private async Task AddMessages()
        {
            var channelsContent = await Requests.RequestChannelsList();
            var channelsData = JsonConvert.DeserializeObject<RootChannels>(channelsContent);

            foreach (var channel in channelsData.Channels)
            {
                var messagesContent = await Requests.RequestChannelsHistory(channel.Id);
                var messagesData = JsonConvert.DeserializeObject<RootMessages>(messagesContent);

                Database.InsertMessagesWithoutDuplicates(messagesData.Messages, channel.Id);
            }
        }
    }
}
