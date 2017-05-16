using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace RestoreFromLocal
{
    class Database
    {
        private Database() {}

        public static void TruncateTable(string tableName)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                string sql = String.Format("DELETE FROM {0}", tableName);
                dbContext.Database.ExecuteSqlCommand(sql);
                sql = String.Format("DBCC CHECKIDENT ({0}, RESEED, 0)", tableName);
                dbContext.Database.ExecuteSqlCommand(sql);
            }
        }

        public static void InsertUsers(string path)
        {
            string usersFile = path + "\\users.json";
            string text = File.ReadAllText(usersFile);
            var data = JsonConvert.DeserializeObject<List<Member>>(text);
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                dbContext.Users.AddRange(data.Select(u => new Users(u.Id, u.Name)));
                dbContext.SaveChanges();
            }
        }

        public static void InsertChannels(string path)
        {
            string channelsFile = path + "\\channels.json";
            string text = File.ReadAllText(channelsFile);
            var data = JsonConvert.DeserializeObject<List<Channel>>(text);
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                dbContext.Channels.AddRange(data.Select(c => new Channels(c.Id, c.Name)));
                dbContext.SaveChanges();
            }
        }

        public static void AddMessage(Message message, string channelId)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                Messages newMessage = new Messages();
                string key = message.User + message.Ts;
                string hash = MD5Hasher.GetMd5Hash(key);
                newMessage.MessageId = hash;
                var idUser = dbContext.Users.Where(u => u.UserId == message.User).Select(
                    u => u.Id).FirstOrDefault();
                if (idUser == null)
                {
                    int botId = dbContext.Users.Where(u => u.UserId == "USLACKBOT").Select(
                        u => u.Id).FirstOrDefault();
                    idUser = botId;
                }
                newMessage.UserId = idUser;
                var idChannel = dbContext.Channels.Where(c => c.ChannelId == channelId).Select(
                    c => c.Id).FirstOrDefault();
                newMessage.ChannelId = idChannel;
                newMessage.JsonData = JsonConvert.SerializeObject(message);
                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
            }
        }

        public static void InsertMessages(string path)
        {
            using (var dbContext = new SlackHistoryViewerDbContext())
            {
                string[] files = Directory.GetFiles(@path, "*.*", SearchOption.AllDirectories);
                foreach (string f in files)
                {
                    if (f.EndsWith("users.json") || f.EndsWith("channels.json"))
                    {
                        continue;
                    }
                    string[] words = f.Split('\\');
                    string channel = words[words.Length - 2];
                    var channelId = dbContext.Channels.Where(c => c.Name == channel).Select(
                        c => c.ChannelId).FirstOrDefault();
                    string text = File.ReadAllText(f);
                    var data = JsonConvert.DeserializeObject<List<Message>>(text);
                    foreach (Message message in data)
                    {
                        AddMessage(message, channelId);
                    }
                }
            }
        }
    }
}
