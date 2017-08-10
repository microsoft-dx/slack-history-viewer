using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SlackHistoryViewer.Database;
using SlackHistoryViewer.Database.Models;
using SlackHistoryViewer.Configuration;
using SlackHistoryViewer.Slack.Models;
using SlackHistoryViewer.Helpers;

namespace SlackHistoryViewer.RestoreFromLocal
{
    class Database
    {

        private Database()
        {
        }

        public static void TruncateTables(params string[] tableNames)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var sql = string.Empty;

                foreach (var tableName in tableNames)
                {
                    sql = string.Format("DELETE FROM {0}", tableName);
                    dbContext.Database.ExecuteSqlCommand(sql);

                    sql = string.Format("DBCC CHECKIDENT ({0}, RESEED, 0)", tableName);
                    dbContext.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        public static void InsertUsers(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var data = GetData<Member>(path, AppSettings.Instance.UsersFile);

                dbContext.Users.AddRange(data.Select(u => new Users(u.Id, u.Name)));
                dbContext.Users.Add(new Users(AppSettings.Instance.SlackBotId, AppSettings.Instance.SlackBotName));
                dbContext.Users.Add(new Users(AppSettings.Instance.UnknownBotId, AppSettings.Instance.UnknownBotName));

                dbContext.SaveChanges();
            }
        }

        public static void InsertChannels(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var data = GetData<Channel>(path, AppSettings.Instance.ChannelsFile);

                dbContext.Channels.AddRange(data.Select(c => new Channels(c.Id, c.Name)));

                dbContext.SaveChanges();
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

        public static void InsertMessages(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                var files = Directory.GetFiles(@path, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    if (IsFileIgnored(file))
                    {
                        continue;
                    }

                    var words = file.Split('\\');
                    var channel = words[words.Length - 2];
                    var channelId = dbContext.Channels
                        .Where(c => c.Name == channel)
                        .Select(c => c.ChannelId)
                        .FirstOrDefault();

                    var text = File.ReadAllText(file);
                    var data = JsonConvert.DeserializeObject<List<Message>>(text);

                    foreach (var message in data)
                    {
                        if (IsAttachment(message))
                        {
                            continue;
                        }

                        AddMessage(dbContext, message, channelId);
                    }

                    dbContext.SaveChanges();
                }
            }
        }

        private static bool IsAttachment(Message message)
        {
            return string.IsNullOrEmpty(message.Text) && message.Attachments.Any();
        }

        private static bool IsFileIgnored(string file)
        {
            return file.EndsWith(AppSettings.Instance.UsersFile) ||
                file.EndsWith(AppSettings.Instance.ChannelsFile) ||
                file.EndsWith(AppSettings.Instance.IntegrationLogsFile);
        }

        private static List<T> GetData<T>(string path, string filename)
        {
            var file = string.Format("{0}\\{1}", path, filename);
            var text = File.ReadAllText(file);

            return JsonConvert.DeserializeObject<List<T>>(text);
        }

    }
}
