using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SlackHistoryViewer.Configuration;
using SlackHistoryViewer.Database;
using SlackHistoryViewer.Database.Models;
using SlackHistoryViewer.Helpers;
using SlackHistoryViewer.Slack.Models;

namespace SlackHistoryViewer.RemoteMessages
{
    internal class Database
    {
        private Database()
        {
        }

        public static void InsertUsersWithoutDuplicates(IEnumerable<Member> users)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var newUsers = users
                    .Where(u => !CheckDuplicateUser(dbContext, u.Id))
                    .Select(u => new Users(u.Id, u.Name));

                dbContext.Users.AddRange(newUsers);

                dbContext.SaveChanges();
            }
        }

        public static void InsertChannelsWithoutDuplicates(IEnumerable<Channel> channels)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var newChannels = channels
                    .Where(c => !CheckDuplicateUser(dbContext, c.Id))
                    .Select(c => new Channels(c.Id, c.Name));

                dbContext.Channels.AddRange(newChannels);

                dbContext.SaveChanges();
            }
        }

        public static void InsertMessagesWithoutDuplicates(IEnumerable<Message> messages, string channelId)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                foreach (Message message in messages)
                {
                    var key = message.User + message.TimeStamp;
                    var hash = MD5Hasher.GetMd5Hash(key);

                    if (!CheckDuplicateMessage(dbContext, hash))
                    {
                        AddMessage(dbContext, message, channelId);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private static bool CheckDuplicateUser(SlackHistoryViewerDbContext dbContext, string id)
        {
            var result = dbContext.Users
                .Where(u => u.UserId == id)
                .FirstOrDefault();

            return result != null;
        }

        private static bool CheckDuplicateChannel(SlackHistoryViewerDbContext dbContext, string id)
        {
            var result = dbContext.Channels
                .Where(c => c.ChannelId == id)
                .FirstOrDefault();

            return result != null;
        }

        private static bool CheckDuplicateMessage(SlackHistoryViewerDbContext dbContext, string hash)
        {
            var result = dbContext.Messages
                .Where(m => m.MessageId == hash)
                .FirstOrDefault();

            return result != null;
        }

        private static void AddMessage(SlackHistoryViewerDbContext dbContext, Message message, string channelId)
        {
            var newMessage = new Messages();
            newMessage.MessageId = MD5Hasher.GetMd5Hash(message.User + message.TimeStamp);

            var idUser = dbContext.Users
                .Where(u => u.UserId == message.User)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (idUser == 0)
            {
                var botId = dbContext.Users
                    .Where(u => u.UserId == AppSettings.Instance.UnknownBotId)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                idUser = botId;
            }

            newMessage.UserId = idUser;

            var idChannel = dbContext.Channels
                .Where(c => c.ChannelId == channelId)
                .Select(c => c.Id)
                .FirstOrDefault();

            newMessage.ChannelId = idChannel;
            newMessage.JsonData = JsonConvert.SerializeObject(message);

            dbContext.Messages.Add(newMessage);
        }
    }
}
