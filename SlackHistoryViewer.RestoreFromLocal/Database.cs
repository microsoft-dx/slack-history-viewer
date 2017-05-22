using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SlackHistoryViewer.Database;
using SlackHistoryViewer.Database.Models;
using SlackHistoryViewer.Helpers;
using SlackHistoryViewer.RestoreFromLocal.Configuration;
using SlackHistoryViewer.Slack.Models;

namespace SlackHistoryViewer.RestoreFromLocal
{
    internal class Database
    {
        private Database()
        {
        }

        public static void TruncateTable(string tableName)
        {
            using (var dbContext = new SlackHistoryViewerDbContext(AppSettings.Instance.ConnectionString))
            {
                var sql = string.Format("DELETE FROM {0}", tableName);
                dbContext.Database.ExecuteSqlCommand(sql);

                sql = string.Format("DBCC CHECKIDENT ({0}, RESEED, 0)", tableName);
                dbContext.Database.ExecuteSqlCommand(sql);
            }
        }

        public static void InsertUsers(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext(AppSettings.Instance.ConnectionString))
            {
                var data = GetData<Member>(path, AppSettings.Instance.UsersFile);

                dbContext.Users.AddRange(data.Select(u => new Users(u.Id, u.Name)));

                dbContext.SaveChanges();
            }
        }

        public static void InsertChannels(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext(AppSettings.Instance.ConnectionString))
            {
                var data = GetData<Channel>(path, AppSettings.Instance.ChannelsFile);

                dbContext.Channels.AddRange(data.Select(c => new Channels(c.Id, c.Name)));

                dbContext.SaveChanges();
            }
        }

        public static void AddMessage(Message message, string channelId)
        {
            using (var dbContext = new SlackHistoryViewerDbContext(AppSettings.Instance.ConnectionString))
            {
                var newMessage = new Messages();
                newMessage.MessageId = MD5Hasher.GetMd5Hash(message.User + message.TimeStamp);

                var idUser = dbContext.Users.Where(u => u.UserId == message.User)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                if (idUser == null)
                {
                    var botId = dbContext.Users.Where(u => u.UserId == AppSettings.Instance.SlackBotId)
                        .Select(u => u.Id)
                        .FirstOrDefault();

                    idUser = botId;
                }

                newMessage.UserId = idUser;

                var idChannel = dbContext.Channels.Where(c => c.ChannelId == channelId)
                    .Select(c => c.Id)
                    .FirstOrDefault();

                newMessage.ChannelId = idChannel;
                newMessage.JsonData = JsonConvert.SerializeObject(message);

                dbContext.Messages.Add(newMessage);

                dbContext.SaveChanges();
            }
        }

        public static void InsertMessages(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext(AppSettings.Instance.ConnectionString))
            {
                var files = Directory.GetFiles(@path, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    if (IsChannelsOrUsersFile(file))
                    {
                        continue;
                    }

                    var words = file.Split('\\');
                    var channel = words[words.Length - 2];
                    var channelId = dbContext.Channels.Where(c => c.Name == channel)
                        .Select(c => c.ChannelId)
                        .FirstOrDefault();

                    var text = File.ReadAllText(file);
                    var data = JsonConvert.DeserializeObject<List<Message>>(text);

                    foreach (Message message in data)
                    {
                        AddMessage(message, channelId);
                    }
                }
            }
        }

        private static bool IsChannelsOrUsersFile(string file)
        {
            return file.EndsWith(AppSettings.Instance.UsersFile) || file.EndsWith(AppSettings.Instance.ChannelsFile);
        }

        private static List<T> GetData<T>(string path, string filename)
        {
            var file = string.Format("{0}\\{1}", path, filename);
            var text = File.ReadAllText(file);

            return JsonConvert.DeserializeObject<List<T>>(text);
        }
    }
}
