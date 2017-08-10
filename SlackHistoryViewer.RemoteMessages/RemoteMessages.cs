using Newtonsoft.Json;
using RemoteMessages;
using SlackHistoryViewer.Database;
using SlackHistoryViewer.Slack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SlackHistoryViewer.RemoteMessages
{
    internal class RemoteMessages
    {

        public async Task Run()
        {
            await addUsers();
            await addChannels();
            await addMessages();
        }

        private async Task addUsers()
        {
            var usersContent = await Requests.RequestUsersList();
            var usersData = JsonConvert.DeserializeObject<RootMembers>(usersContent);

            Database.InsertUsersWithoutDuplicates(usersData.Members);
        }

        private async Task addChannels()
        {
            var channelsContent = await Requests.RequestChannelsList();
            var channelsData = JsonConvert.DeserializeObject<RootChannels>(channelsContent);

            Database.InsertChannelsWithoutDuplicates(channelsData.Channels);

        }

        private async Task addMessages()
        {
            var channelsContent = await Requests.RequestChannelsList();
            var channelsData = JsonConvert.DeserializeObject<RootChannels>(channelsContent);

            foreach (Channel channel in channelsData.Channels)
            {
                var messagesContent = await Requests.RequestChannelsHistory(channel.Id);
                var messagesData = JsonConvert.DeserializeObject<RootMessages>(messagesContent);
                Database.InsertMessagesWithoutDuplicates(messagesData.Messages, channel.Id);
            }

        }

    }
}
