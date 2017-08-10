using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SlackHistoryViewer.Database;
using SlackHistoryViewer.Database.Models;
using SlackHistoryViewer.Configuration;
using SlackHistoryViewer.Slack.Models;
using SlackHistoryViewer.Helpers;
using System;

namespace SlackHistoryViewer.RemoteMessages
{
    public class Database
    {

        private Database()
        {
        }

        public static void InsertUsersWithoutDuplicates(IEnumerable<Member> users)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var newUsers = users
                    .Where(u => !CheckDuplicateUser(u.Id))
                    .Select(u1 => new Users(u1.Id, u1.Name));

                dbContext.Users.AddRange(newUsers);
                dbContext.SaveChanges();
            }
        }

        public static void InsertChannelsWithoutDuplicates(IEnumerable<Channel> channels)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var newChannels = channels
                    .Where(c => !CheckDuplicateUser(c.Id))
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
                    var hash = Helpers.MD5Hasher.GetMd5Hash(key);

                    if (!Database.CheckDuplicateMessage(hash))
                    {
                        Database.AddMessage(dbContext, message, channelId);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        public static bool CheckDuplicateUser(string id)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var result = dbContext.Users
                    .Where(u => u.UserId == id)
                    .FirstOrDefault();

                return result != null;
            }
        }

        public static bool CheckDuplicateChannel(string id)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var result = dbContext.Channels
                    .Where(c => c.ChannelId == id)
                    .FirstOrDefault();

                return result != null;
            }

        }

        public static bool CheckDuplicateMessage(string hash)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var result = dbContext.Messages
                    .Where(m => m.MessageId == hash)
                    .FirstOrDefault();

                return result != null;
            }
        }

        public static void AddMessage(SlackHistoryViewerDbContext dbContext, Message message, string channelId)
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
